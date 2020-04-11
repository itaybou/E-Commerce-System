using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.TransactionManagement;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    class SystemManager
    {
        private static readonly Lazy<SystemManager> lazy = new Lazy<SystemManager>(() => new SystemManager());
        public static SystemManager Instance => lazy.Value;

        private UsersManagement _userManagement;
        private StoreManagement _storeManagement;
        private TransactionManager _transactionManager;
        private SearchAndFilter _searchAndFilter;

        public SearchAndFilter SearchAndFilterSystem { get => _searchAndFilter; }

        private SystemManager()
        {
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
            _transactionManager = new TransactionManager();
            _searchAndFilter = new SearchAndFilter(_storeManagement);
        }

        public void makePurchase(double totalPrice, Dictionary<Product, int> allProducts, IEnumerable<(Store, double)> storePayments,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            if (_transactionManager.paymentTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV)) // pay for entire cart
            {
                if (_transactionManager.supplyTransaction(allProducts, address))                                                        // supply all prodcuts by quantity
                {
                    foreach ((Store, double) storePayment in storePayments)                                                             // send payment to all stores bought from
                    {       
                        _transactionManager.sendPayment(storePayment.Item1, storePayment.Item2);
                    }
                }
                else _transactionManager.refundTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);   // if supply failed, refund user
            }
            _userManagement.logUserPurchase(totalPrice, allProducts, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        public List<Product> purchaseUserShoppingCart(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var shoppingCart = _userManagement.getActiveUserShoppingCart();                                                 // User shopping cart
            var storeShoppingCarts = shoppingCart.StoreCarts.Select(storeCart => (storeCart.store, storeCart.getTotalCartPrice(), storeCart.Products));    // Store => store cart
            var availableProducts = storeShoppingCarts.Where(storeCart => storeCart.Products.ToList().All(prodQuantity => prodQuantity.Key.Quantity >= prodQuantity.Value));    // Available products by quantity
            var unavailableProducts = storeShoppingCarts.Except(availableProducts);                                         // unavailable products by qunatity
            if (!unavailableProducts.Any())
            {
                var totalPrice = shoppingCart.getTotalACartPrice();                                                         // total user cart price
                var allProdcuts = shoppingCart.StoreCarts.Select(storeCart => storeCart.Products)                           // Get dictionary from Product => Quantity for each store cart
                 .SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
                var storePayments = availableProducts.Select(s => (s.store, s.Item2));
                makePurchase(totalPrice, allProdcuts, storePayments, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                return null;
            }
            else return unavailableProducts.Select(prod => prod.Products.Select(p => p.Key)).SelectMany(i => i).ToList();   // return all unavailable products from cart
        }

        public List<Product> purchaseProducts(List<Tuple<Store, (Product, int)>> products, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var availableProducts = products.Where(product => product.Item2.Item1.Quantity >= product.Item2.Item2);
            var unavailableProducts = products.Except(availableProducts);
            if (!unavailableProducts.Any())
            {
                var productQuantities = products.Select(tuple => tuple.Item2);
                var totalPrice = productQuantities.Aggregate(0.0, (total, current) => total += (current.Item1.CalculateDiscount() * current.Item2));
                var allProducts = productQuantities.ToDictionary(pair => pair.Item1, pair => pair.Item2);
                var storePayments = products.Select(p => (p.Item1, p.Item2.Item1.CalculateDiscount() * p.Item2.Item2));
                makePurchase(totalPrice, allProducts, storePayments, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                return null;
            }
            return unavailableProducts.Select(prod => prod.Item2.Item1).ToList();
        }

        public bool purchaseProduct(Tuple<Store, (Product, int)> product, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var available = product.Item2.Item1.Quantity >= product.Item2.Item2;
            if(available)
            {
                var totalPrice = product.Item2.Item1.CalculateDiscount() * product.Item2.Item2;
                var allProducts = new Dictionary<Product, int>()
                {
                    {product.Item2.Item1,  product.Item2.Item2}
                };
                var storePayments = new List<(Store, double)>()
                {
                    (product.Item1, totalPrice)
                };
                makePurchase(totalPrice, allProducts, storePayments, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                return true;
            }
            return false;
        }
    }
}
