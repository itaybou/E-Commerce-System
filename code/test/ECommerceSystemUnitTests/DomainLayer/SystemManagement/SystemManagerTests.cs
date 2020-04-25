using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.SystemManagement.Tests
{
    [TestFixture()]
    public class SystemManagerTests
    {
        private SystemManager _systemManager;
        private UsersManagement _userManagement;
        private StoreManagement _storeManagement;
        private Store _store1, _store2;
        private double _totalPrice;
        private Dictionary<Product, int> _allProducts;
        private Dictionary<Store, Dictionary<Product, int>> _storeProducts;
        private IEnumerable<(Store, double)> _storePayments;
        private string _firstName = "fname", _lastName = "lname", _address = "address 1", _creditCardNumber = "413-547";
        private int _id = 54362432, _CVV = 300;
        private DateTime _expirationCreditCard = DateTime.Now;
        private Guid guid1;
        private Guid guid2;
        private Guid guid3;
        private Guid guid4;


        [SetUp]
        public void setUp()
        {
            _systemManager = SystemManager.Instance;
            _userManagement = UsersManagement.Instance;
            _userManagement.register("user1", "pA55word", "user", "last", "mail@mail");
            _userManagement.login("user1", "pA55word");
            _store1 = new Store(null, null, "owner1", "store1");
            _store2 = new Store(null, null, "owner2", "store2");
            guid1 = Guid.NewGuid();
            guid2 = Guid.NewGuid();
            guid3 = Guid.NewGuid();
            guid4 = Guid.NewGuid();

            var product1 = new Product(null, null, 20, 25.0, guid1, "", "");
            var product2 = new Product(null, null, 10, 10.0, guid2, "", "");
            var product3 = new Product(null, null, 15, 12.5, guid3, "", "");
            var product4 = new Product(null, null, 25, 50.0, guid4, "", "");
            var store1_products = new Dictionary<Product, int>()
            {
                {product1, 5 },
                {product2, 10 }
            };
            var store2_products = new Dictionary<Product, int>()
            {
                {product3, 2 },
                {product4, 20 }
            };
            _totalPrice = (5 * 25.0) + (10 * 10.0) + (12.5 * 2) + (20 * 50.0);
            _allProducts = store1_products.ToList().Concat(store2_products.ToList()).ToDictionary(pair => pair.Key, pair => pair.Value);
            _storeProducts = new Dictionary<Store, Dictionary<Product, int>>()
            {
                {_store1, store1_products },
                {_store2, store2_products },
            };
            _storePayments = _storeProducts.ToList().Select
                (s => (s.Key, s.Value.ToList().Aggregate(0.0, (total, prod) => total += prod.Key.CalculateDiscount() * prod.Value)));
        }

        [Test()]
        public void makePurchaseTest()
        {
            _systemManager.makePurchase(_totalPrice, _allProducts, _storeProducts, _storePayments.ToList(), _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
            Assert.AreEqual(_storeProducts[_store1].ToList().First().Key.Quantity, 15); // First product quantity decreased (first store purchased from)
            Assert.AreEqual(_storeProducts[_store1].ToList().ElementAt(1).Key.Quantity, 0); // Second product quantity decreased (first store purchased from)
            Assert.AreEqual(_storeProducts[_store2].ToList().First().Key.Quantity, 13); // First product quantity decreased (second store purchased from)
            Assert.AreEqual(_storeProducts[_store2].ToList().ElementAt(1).Key.Quantity, 5); // Second product quantity decreased (second store purchased from)
            //Test for store 1 purchase log
            var store1_purchase = _store1.PurchaseHistory.First();
            Assert.True(store1_purchase.User == _userManagement.getLoggedInUser());
            Assert.AreEqual(_storeProducts[_store1].ToList().Aggregate(0.0, (total, prod) => total += prod.Key.CalculateDiscount() * prod.Value), store1_purchase.TotalPrice);
            foreach (var prodPurchase in store1_purchase.ProductsPurchased)  // Verify purchase log contains the correct products for store 1
            {
                Assert.True(_storeProducts[_store1].ToList().Exists(p => p.Key.Id.Equals(prodPurchase.Id)));
                Assert.False(_storeProducts[_store2].ToList().Exists(p => p.Key.Id.Equals(prodPurchase.Id)));
                Assert.True(_storeProducts[_store1].ToList().All(p => p.Key != prodPurchase));  // New product is stored in purchase history (indeifferent to changes)
            }
            //Test for store 2 purchase log
            var store2_purchase = _store2.PurchaseHistory.First();
            Assert.True(store2_purchase.User == _userManagement.getLoggedInUser());
            Assert.AreEqual(_storeProducts[_store2].ToList().Aggregate(0.0, (total, prod) => total += prod.Key.CalculateDiscount() * prod.Value), store2_purchase.TotalPrice);
            foreach (var prodPurchase in store2_purchase.ProductsPurchased)  // Verify purchase log contains the correct products for store 2
            {
                Assert.True(_storeProducts[_store2].ToList().Exists(p => p.Key.Id.Equals(prodPurchase.Id)));
                Assert.False(_storeProducts[_store1].ToList().Exists(p => p.Key.Id.Equals(prodPurchase.Id)));
                Assert.True(_storeProducts[_store2].ToList().All(p => p.Key != prodPurchase));  // New product is stored in purchase history (indeifferent to changes)
            }
            //Test for user purchase log
            var logged_purchase_history = ((Subscribed)(_userManagement.getLoggedInUser()._state)).PurchaseHistory.First();
            Assert.True(logged_purchase_history.ProductsPurchased.Exists(p => p.Id.Equals(guid1)));
            Assert.True(logged_purchase_history.ProductsPurchased.Exists(p => p.Id.Equals(guid2)));
            Assert.True(logged_purchase_history.ProductsPurchased.Exists(p => p.Id.Equals(guid3)));
            Assert.True(logged_purchase_history.ProductsPurchased.Exists(p => p.Id.Equals(guid4)));
            Assert.AreEqual(logged_purchase_history.ProductsPurchased.Count, 4);
            Assert.AreEqual(logged_purchase_history.TotalPrice, _totalPrice);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.FirstName, _firstName);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.LastName, _lastName);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.Id, _id);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.ExpirationCreditCard, _expirationCreditCard);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.Address, _address);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.CreditCardNumber, _creditCardNumber);
            Assert.AreEqual(logged_purchase_history.PaymentShippingMethod.CVV, _CVV);
        }

        [Test()]
        public void purchaseUserShoppingCartTest()
        {
            var store1_cart = new StoreShoppingCart(_store1);
            var store2_cart = new StoreShoppingCart(_store2);
            _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store1_cart);
            _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store2_cart);
            store1_cart.AddToCart(_storeProducts[_store1].Keys.First(), 26);
            Assert.IsNotEmpty(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            Assert.True(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address).First().Id.Equals(guid1));
            store1_cart.ChangeProductQuantity(_storeProducts[_store1].Keys.First(), 20);
            Assert.IsNull(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            store1_cart = new StoreShoppingCart(_store1);
            store2_cart = new StoreShoppingCart(_store2);
            _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store1_cart);
            _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store2_cart);
            store1_cart.AddToCart(_storeProducts[_store1].Keys.ElementAt(1), 26);
            store2_cart.AddToCart(_storeProducts[_store2].Keys.First(), 9);
            Assert.AreEqual(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address).Count, 1);
            _userManagement.getLoggedInUser()._cart = new UserShoppingCart();
            _userManagement.Users[_userManagement.getLoggedInUser()] = _userManagement.getLoggedInUser()._cart;
            store1_cart = new StoreShoppingCart(_store1);
            store2_cart = new StoreShoppingCart(_store2);
            _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store1_cart);
            _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store2_cart);
            store1_cart.AddToCart(_storeProducts[_store2].Keys.ElementAt(1), 20);
            store1_cart.AddToCart(_storeProducts[_store2].Keys.First(), 6);
            Assert.IsNull(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        }

        [Test()]
        public void purchaseProductsTest()
        {
            var product1 = new Product(null, null, 20, 25.0, Guid.NewGuid(), "", "");
            var product2 = new Product(null, null, 10, 10.0, Guid.NewGuid(), "", "");
            var product3 = new Product(null, null, 15, 12.5, Guid.NewGuid(), "", "");
            var product4 = new Product(null, null, 25, 50.0, Guid.NewGuid(), "", "");
            var productsToPurchase = new List<Tuple<Store, (Product, int)>>()
            {
                {Tuple.Create(_store1, (product1, 15) )},
                {Tuple.Create(_store1, (product2, 20) )},
            };
            Assert.IsNotEmpty(_systemManager.purchaseProducts(productsToPurchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            productsToPurchase = new List<Tuple<Store, (Product, int)>>()
            {
                {Tuple.Create(_store1, (product1, 15) )},
                {Tuple.Create(_store1, (product2, 10) )},
            };
            Assert.IsNull(_systemManager.purchaseProducts(productsToPurchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            productsToPurchase = new List<Tuple<Store, (Product, int)>>()
            {
                {Tuple.Create(_store2, (product3, 10) )},
                {Tuple.Create(_store1, (product1, 5) )},
            };
            Assert.IsNull(_systemManager.purchaseProducts(productsToPurchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        }

        [Test()]
        public void purchaseProductTest()
        {
            var product1 = new Product(null, null, 20, 25.0, Guid.NewGuid(), "", "");

            var purchase = Tuple.Create(_store1, (product1, 25));
            Assert.False(_systemManager.purchaseProduct(purchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            purchase = Tuple.Create(_store1, (product1, 20));
            Assert.True(_systemManager.purchaseProduct(purchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        }
    }
}