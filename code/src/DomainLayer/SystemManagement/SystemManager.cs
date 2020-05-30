using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private IDataAccess _data;

        public SearchAndFilter SearchAndFilterSystem { get => _searchAndFilter; }

        private SystemManager()
        {
            SystemLogger.initLogger();
            _data = DataAccess.Instance;
            _searchAndFilter = new SearchAndFilter();
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
            _transactionManager = TransactionManager.Instance;
        }

        public async Task<bool> makePurchase(Guid userID, double totalPrice, ICollection<(Store, double, IDictionary<Product, int>)> storeProducts, IDictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var purchased = false;
            if (await _transactionManager.paymentTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV)) // pay for entire cart
            {
                if (await _transactionManager.supplyTransaction(allProducts, address))  // supply all prodcuts by quantity
                {
                    var user = _userManagement.getUserByGUID(userID, false);
                    foreach (var (store, storePayment, storeBoughtProducts) in storeProducts)                                                             // send payment to all stores bought from
                    {
                        await _transactionManager.sendPayment(store, storePayment);
                        _storeManagement.logStorePurchase(store, user, storePayment, storeBoughtProducts);
                        _storeManagement.sendPurchaseNotification(store, user.Name); //send notification to all managers and owners of the store about the purchase
                    }
                    purchased = true;
                }
                else if (!(await _transactionManager.refundTransaction(totalPrice, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV)))   // if supply failed, refund user
                {
                    SystemLogger.LogError("Refund transaction failed to credit card: " + creditCardNumber);
                }
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
        public async Task<List<ProductModel>> purchaseUserShoppingCart(Guid userID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var user = _userManagement.getUserByGUID(userID, false);
            var shoppingCart = _userManagement.getUserCart(user);  // User shopping cart
            var storeProducts = shoppingCart.getProductsStoreAndTotalPrices(); // (Store, Store Price To Pay, {Product, Quantity})

            //check that the cart satisfy the stores purchase policy
            if (!isSatisfyPurchasePolicies(storeProducts, address))
            {
                return new List<ProductModel>();
            }

            IDictionary<Product, int> productsToPurchase = shoppingCart.getAllCartProductsAndQunatities();

            //reduce products quantities
            List<Product> unavailableProducts = decreaseQuntityOfAllProducts(storeProducts); // unavailable products by qunatity

            if (unavailableProducts.Any()) //there are unavailable products 
            {
                restoreProductsQuantities(unavailableProducts, storeProducts);
                return unavailableProducts.Select(p => ModelFactory.CreateProduct(p)).ToList();
            }

            var totalPrice = shoppingCart.getTotalACartPrice();  // total user cart price with discounts

            // make the payment and suplly transaction, in addiotion send purchase notification
            if (await makePurchase(userID, totalPrice, storeProducts, productsToPurchase, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address))
            {
                _userManagement.resetUserShoppingCart(userID);
                _userManagement.logUserPurchase(userID, totalPrice, productsToPurchase, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
                _data.Transactions.PurchaseTransaction(user, storeProducts);
                return null;
            }
            else // purchase failed
            {
                restoreProductsQuantities(unavailableProducts, storeProducts);
                return new List<ProductModel>(); 
            }
        }

        private void restoreProductsQuantities(List<Product> unavailableProducts, ICollection<(Store, double, IDictionary<Product, int>)> storeProducts)
        {
            List <Guid> unavailableProductsIDs = unavailableProducts.Select(prod => prod.Id).ToList();
            foreach ((Store store, double storePayment, IDictionary<Product, int> storeBoughtProducts) in storeProducts)                                                             // send payment to all stores bought from
            {
                foreach (var prod in storeBoughtProducts)
                {
                    if (!unavailableProductsIDs.Contains(prod.Key.Id))
                    {
                        prod.Key.increaseProductQuantity(prod.Value);
                    }
                }
            }
        }

        private List<Product> decreaseQuntityOfAllProducts(ICollection<(Store, double, IDictionary<Product, int>)> storeProducts)
        {
            List<Product> unavailableProducts = new List<Product>();
            foreach ((Store store, double storePayment, IDictionary<Product, int> storeBoughtProducts) in storeProducts)                                                             // send payment to all stores bought from
            {
                foreach(var prod in storeBoughtProducts)
                {
                    if(!prod.Key.decreaseProductQuantity(prod.Value))
                    {
                        unavailableProducts.Add(prod.Key);
                    }
                }
            }
            return unavailableProducts;
        }

        private bool isSatisfyPurchasePolicies(ICollection<(Store, double, IDictionary<Product, int>)> storeProducts, string address)
        {
            foreach(var s in storeProducts)
            {
                IDictionary<Guid, int> products = s.Item3.ToDictionary(pair => pair.Key.Id, pair => pair.Value); // product => quantity
                if(!s.Item1.canBuy(products, s.Item2, address))
                {
                    return false; //cant buy the products by the store policy
                }
            }
            return true;
        }

    }
}