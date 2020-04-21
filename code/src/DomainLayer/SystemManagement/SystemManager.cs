using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public class SystemManager
    {
        private object _transactionLock = new object();
        private static readonly Lazy<SystemManager> lazy = new Lazy<SystemManager>(() => new SystemManager());
        public static SystemManager Instance => lazy.Value;

        private UsersManagement _userManagement;
        private StoreManagement _storeManagement;
        private TransactionManager _transactionManager;
        private SearchAndFilter _searchAndFilter;

        public SearchAndFilter SearchAndFilterSystem { get => _searchAndFilter; }

        private SystemManager()
        {
            SystemLogger.initLogger();
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
            _transactionManager = TransactionManager.Instance;
            _searchAndFilter = new SearchAndFilter();
        }

        public bool makePurchase(double totalPrice, Dictionary<Product, int> allProducts, Dictionary<Store, Dictionary<Product, int>> storeProducts, List<(Store, double)> storePayments,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var purchased = false;
            lock (_transactionLock)
            {
                if (_transactionManager.paymentTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV)) // pay for entire cart
                {
                    if (_transactionManager.supplyTransaction(allProducts, address))                                                        // supply all prodcuts by quantity
                    {
                        foreach ((Store, double) storePayment in storePayments)                                                             // send payment to all stores bought from
                        {
                            storeProducts[storePayment.Item1].ToList().ForEach(prod =>
                            {
                                prod.Key.Quantity -= prod.Value;
                                if (prod.Key.Quantity.Equals(0))
                                    storePayment.Item1.Inventory.Products.ForEach(inv =>
                                    {
                                        if (inv.ProductList.Contains(prod.Key))
                                            inv.ProductList.Remove(prod.Key);
                                    });
                            });
                            _transactionManager.sendPayment(storePayment.Item1, storePayment.Item2);
                            var storeBoughtProducts = storeProducts[storePayment.Item1];
                            _storeManagement.logStorePurchase(storePayment.Item1, _userManagement.getLoggedInUser(), storePayment.Item2, storeBoughtProducts);
                        }
                        purchased = true;
                    }
                    else if (!_transactionManager.refundTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV))   // if supply failed, refund user
                    {
                        SystemLogger.LogError("Refund transaction failed to credit card: " + creditCardNumber);
                    }
                }
                if (purchased)
                    _userManagement.logUserPurchase(totalPrice, allProducts, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
            }
            return purchased;
        }

        /// <summary>
        /// Makes a purchase for the current logged user shopping cart
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="id"></param>
        /// <param name="creditCardNumber"></param>
        /// <param name="expirationCreditCard"></param>
        /// <param name="CVV"></param>
        /// <param name="address"></param>
        /// <returns>List of unavailable products if there are any, null if succeeded purchase and empty list if payment/supply was unseccesful</returns>
        public List<Product> purchaseUserShoppingCart(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var shoppingCart = _userManagement.getActiveUserShoppingCart();                                                 // User shopping cart
            var storeShoppingCarts = shoppingCart.StoreCarts.Select(storeCart => (storeCart.store, storeCart.getTotalCartPrice(), storeCart.Products)).ToList();    // Store => store cart
            var unavailableProducts = storeShoppingCarts.Select(storeCart => storeCart.Products.Select(p => (p.Key, p.Value))).SelectMany(p => p).Where(p => p.Key.Quantity < p.Value).ToList();                              // unavailable products by qunatity
            if (!unavailableProducts.Any())
            {
                var totalPrice = shoppingCart.getTotalACartPrice();                                                         // total user cart price
                var storeProdcuts = shoppingCart.StoreCarts.Select(storeCart => (storeCart.store, storeCart.Products)).ToDictionary(pair => pair.store, pair => pair.Products);
                var allProdcuts = shoppingCart.StoreCarts.Select(storeCart => storeCart.Products)                           // Get dictionary from Product => Quantity for each store cart
                 .SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                var storePayments = storeShoppingCarts.Select(s => (s.store, s.Item2));
                var purchased = makePurchase(totalPrice, allProdcuts, storeProdcuts, storePayments.ToList(), firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                if(purchased)
                    _userManagement.resetActiveUserShoppingCart();
                return purchased ? null : new List<Product>(); // if purchased return null else return empty lost indicating that payment process/supply was not successful
            }
            else return unavailableProducts.Select(p => p.Key).ToList();
        }

        public List<Product> purchaseProducts(List<Tuple<Store, (Product, int)>> products, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var availableProducts = products.Where(product => product.Item2.Item1.Quantity >= product.Item2.Item2).ToList();
            var unavailableProducts = products.Except(availableProducts).ToList();
            if (!unavailableProducts.Any())
            {
                var productQuantities = products.Select(tuple => tuple.Item2);
                var totalPrice = productQuantities.Aggregate(0.0, (total, current) => total += (current.Item1.CalculateDiscount() * current.Item2));
                var storeProducts = new Dictionary<Store, Dictionary<Product, int>>();
                products.ForEach(prod =>
                {
                    if (storeProducts.ContainsKey(prod.Item1))
                    {
                        storeProducts[prod.Item1].Add(prod.Item2.Item1, prod.Item2.Item2);
                    }
                    else storeProducts.Add(prod.Item1, new Dictionary<Product, int>() { { prod.Item2.Item1, prod.Item2.Item2 } });
                });
                var allProducts = productQuantities.ToDictionary(pair => pair.Item1, pair => pair.Item2);
                var storePayments = storeProducts.Select(p => (p.Key, p.Value.ToList().Aggregate(0.0, (total, curr) => total += curr.Key.CalculateDiscount() * curr.Value)));
                var purchased = makePurchase(totalPrice, allProducts, storeProducts, storePayments.ToList(), firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                if (purchased)
                {
                    var userCart = _userManagement.getActiveUserShoppingCart();
                    userCart.StoreCarts.ForEach(s => products.Where(pair => pair.Item1 == s.store)
                    .ToList().ForEach(p => s.ChangeProductQuantity(p.Item2.Item1, s.Products[p.Item2.Item1] - p.Item2.Item2)));
                    userCart.StoreCarts = userCart.StoreCarts.Where(s => s.Products.Any()).ToList();
                }
                return purchased? null : new List<Product>();
            }
            return unavailableProducts.Select(prod => prod.Item2.Item1).ToList();
        }

        public bool purchaseProduct(Tuple<Store, (Product, int)> product, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var available = product.Item2.Item1.Quantity >= product.Item2.Item2;
            if (available)
            {
                var totalPrice = product.Item2.Item1.CalculateDiscount() * product.Item2.Item2;
                var allProducts = new Dictionary<Product, int>()
                {
                    {product.Item2.Item1,  product.Item2.Item2}
                };
                var storeProducts = new Dictionary<Store, Dictionary<Product, int>>()
                {
                    {product.Item1,  allProducts}
                };
                var storePayments = new List<(Store, double)>()
                {
                    (product.Item1, totalPrice)
                };
                return makePurchase(totalPrice, allProducts, storeProducts, storePayments, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address); ;
            }
            return false;
        }
    }
}