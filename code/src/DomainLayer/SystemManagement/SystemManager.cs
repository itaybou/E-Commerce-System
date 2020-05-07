using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public class SystemManager
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
            SystemLogger.initLogger();
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
            _transactionManager = TransactionManager.Instance;
            _searchAndFilter = new SearchAndFilter();
        }

        public bool makePurchase(Guid userID, double totalPrice, ICollection<(Store, double, IDictionary<Product, int>)> storeProducts, IDictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var purchased = false;
            if (_transactionManager.paymentTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV)) // pay for entire cart
            {
                if (_transactionManager.supplyTransaction(allProducts, address))                                                        // supply all prodcuts by quantity
                {
                    foreach (var (store, storePayment, storeBoughtProducts) in storeProducts)                                                             // send payment to all stores bought from
                    {
                        storeBoughtProducts.ToList().ForEach(prod => store.reduceProductQuantity(prod.Key, prod.Value));
                        _transactionManager.sendPayment(store, storePayment);
                        _storeManagement.logStorePurchase(store, _userManagement.getUserByGUID(userID), storePayment, storeBoughtProducts);
                    }
                    purchased = true;
                }
                else if (!_transactionManager.refundTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV))   // if supply failed, refund user
                {
                    SystemLogger.LogError("Refund transaction failed to credit card: " + creditCardNumber);
                }
            }
            if (purchased)
                _userManagement.logUserPurchase(userID, totalPrice, allProducts, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
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
        public  List<ProductModel> purchaseUserShoppingCart(Guid userID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var purchased = false;
            var shoppingCart = _userManagement.getUserCart(_userManagement.getUserByGUID(userID));                                                                                        // User shopping cart
            var storeProducts = shoppingCart.getProductsStoreAndTotalPrices(); // (Store, Store Price To Pay, {Product, Quantity})
            var productsToPurchase = shoppingCart.getAllCartProductsAndQunatities(); // Product ==> Cart Quantity
            var unavailableProducts = new List<Product>();
            WaitCallback pFunc = delegate   // Lock purchased products untill purchase is completed
            {
                unavailableProducts = productsToPurchase.Where(p => p.Key.Quantity < p.Value).Select(k => k.Key).ToList();                              // unavailable products by qunatity
                if (!unavailableProducts.Any())
                {
                    var totalPrice = shoppingCart.getTotalACartPrice();                                                         // total user cart price
                    purchased = makePurchase(userID, totalPrice, storeProducts, productsToPurchase, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                    if (purchased)
                        _userManagement.resetUserShoppingCart(userID);
                }
            };
            ProductPurchaseLock(productsToPurchase.Keys.OrderBy(p => p.Id).ToList(), pFunc);    // sort products to lock in the same order
            return unavailableProducts.Any() ? unavailableProducts.Select(p => ModelFactory.CreateProduct(p)).ToList()  // If one or more of the products were unavailable return them
                : purchased ? new List<ProductModel>() : null; // if purchased return null else return empty lost indicating that payment process/supply was not successful
        }

        private async void ProductPurchaseLock(ICollection<Product> locks, WaitCallback pFunc, int index = 0)
        {
            if (index < locks.Count())
            {
                lock (locks.ElementAt(index))
                {
                    ProductPurchaseLock(locks, pFunc, index + 1);
                }
            }
            else
            {
                 ThreadPool.QueueUserWorkItem(pFunc);
            }
        }

        //public bool purchaseProduct(Tuple<string, (Guid, int)> product, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        //{
        //    var store = _storeManagement.getStoreByName(product.Item1);
        //    var available = product.Item2.Item1.Quantity >= product.Item2.Item2;
        //    if (available)
        //    {
        //        var totalPrice = product.Item2.Item1.CalculateDiscount() * product.Item2.Item2;
        //        var allProducts = new Dictionary<Product, int>()
        //        {
        //            {product.Item2.Item1,  product.Item2.Item2}
        //        };
        //        var storeProducts = new Dictionary<Store, Dictionary<Product, int>>()
        //        {
        //            {product.Item1,  allProducts}
        //        };
        //        var storePayments = new List<(Store, double)>()
        //        {
        //            (product.Item1, totalPrice)
        //        };
        //        return makePurchase(totalPrice, allProducts, storeProducts, storePayments, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address); ;
        //    }
        //    return false;
        //}

        //public List<Product> purchaseProducts(List<Tuple<Store, (Product, int)>> products, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        //{
        //    var availableProducts = products.Where(product => product.Item2.Item1.Quantity >= product.Item2.Item2).ToList();
        //    var unavailableProducts = products.Except(availableProducts).ToList();
        //    if (!unavailableProducts.Any())
        //    {
        //        var productQuantities = products.Select(tuple => tuple.Item2);
        //        var totalPrice = productQuantities.Aggregate(0.0, (total, current) => total += (current.Item1.CalculateDiscount() * current.Item2));
        //        var storeProducts = new Dictionary<Store, Dictionary<Product, int>>();
        //        products.ForEach(prod =>
        //        {
        //            if (storeProducts.ContainsKey(prod.Item1))
        //            {
        //                storeProducts[prod.Item1].Add(prod.Item2.Item1, prod.Item2.Item2);
        //            }
        //            else storeProducts.Add(prod.Item1, new Dictionary<Product, int>() { { prod.Item2.Item1, prod.Item2.Item2 } });
        //        });
        //        var allProducts = productQuantities.ToDictionary(pair => pair.Item1, pair => pair.Item2);
        //        var storePayments = storeProducts.Select(p => (p.Key, p.Value.ToList().Aggregate(0.0, (total, curr) => total += curr.Key.CalculateDiscount() * curr.Value)));
        //        var purchased = makePurchase(totalPrice, allProducts, storeProducts, storePayments.ToList(), firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        //        if (purchased)
        //        {
        //            var userCart = _userManagement.getActiveUserShoppingCart();
        //            userCart.StoreCarts.ForEach(s => products.Where(pair => pair.Item1 == s.store)
        //            .ToList().ForEach(p => s.ChangeProductQuantity(p.Item2.Item1, s.Products[p.Item2.Item1] - p.Item2.Item2)));
        //            userCart.StoreCarts = userCart.StoreCarts.Where(s => s.Products.Any()).ToList();
        //        }
        //        return purchased? null : new List<Product>();
        //    }
        //    return unavailableProducts.Select(prod => prod.Item2.Item1).ToList();
        //}
    }
}