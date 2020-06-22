using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Models;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystemUnitTests.DomainLayer.SystemManagement;

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
        private ICollection<(Store, double, IDictionary<Product, int>)> _storeProducts;
        private string _firstName = "fname", _lastName = "lname", _address = "address,city,country,zip", _creditCardNumber = "413-547";
        private int _id = 54362432, _CVV = 300;
        private DateTime _expirationCreditCard = DateTime.Now.AddDays(2.0);
        private Product product1;
        private Product product2;
        private Product product3;
        private Product product4;
        private Guid _userID;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            DataAccess.Instance.SetTestContext();
            TransactionManager.Instance.setTestExternalSystems(new ExternalSystemsStub());
        }

        [SetUp]
        public void setUp()
        {
            _systemManager = SystemManager.Instance;
            _userManagement = UsersManagement.Instance;
            _userManagement.register("user1", "pA55word", "user", "last", "mail@mail");
            _userManagement.login("user1", "pA55word");
            _store1 = new Store("owner1", "store1");
            _store2 = new Store("owner2", "store2");
            product1 = new Product(null, null, 20, 20, Guid.NewGuid());
            product2 = new Product(null, null, 20, 20, Guid.NewGuid());
            product3 = new Product(null, null, 20, 20, Guid.NewGuid());
            product4 = new Product(null, null, 20, 20, Guid.NewGuid());
            _userID = _userManagement.getUserByName("user1").Guid;
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
            _storeProducts = new List<(Store, double, IDictionary<Product, int>)>()
            {
                {(_store1, store1_products.ToList().Aggregate(0.0, (total, prod) => total += prod.Key.CalculateDiscount() * prod.Value), store1_products )},
                {(_store2, store2_products.ToList().Aggregate(0.0, (total, prod) => total += prod.Key.CalculateDiscount() * prod.Value), store2_products )},
            };
        }

        [TearDown]
        public void tearDown()
        {
            //StoreManagement.Instance.Stores.Clear();
        }

        [OneTimeTearDown]
        public void tearDownFixture()
        {
            TransactionManager.Instance.setRealExternalSystems();
            DataAccess.Instance.DropTestDatabase();
        }


        [Test()]
        public void makePurchaseSuccessTest()
        {
            var ans = _systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.True(ans.Result);
            Assert.AreEqual(product1.Quantity, 15); // First product quantity decreased (first store purchased from)
            Assert.AreEqual(product2.Quantity,10);
            Assert.AreEqual(product3.Quantity, 18);
            Assert.AreEqual(product4.Quantity, 0);
        }


        [Test()]
        public void makePurchaseCheckStorePurchaseHistoryUpdateAfterPurchaseTest()
        {
            var ans= _systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id,
                _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.True(_store1.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product1.Id)));
            Assert.True(_store1.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product2.Id)));
            Assert.True(_store2.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product3.Id)));
            Assert.True(_store2.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product4.Id)));
        }

        [Test()]
        public void makePurchaseCheckStorePurchaseHistoryCorrectAfterPurchaseTest()
        {
            var ans = _systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id,
                _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.False(_store2.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product1.Id)));
            Assert.False(_store2.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product2.Id)));
            Assert.False(_store1.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product3.Id)));
            Assert.False(_store1.PurchaseHistory.First().ProductsPurchased.ToList().Exists(p => p.Id.Equals(product4.Id)));
        }

        [Test()]
        public void purchaseUserShoppingCartTestSuccess()
        {
            StoreShoppingCart store1_cart = new StoreShoppingCart(_store1);
            store1_cart.AddToCart(product1, 18);
            store1_cart.AddToCart(product2, 15);
            StoreShoppingCart store2_cart = new StoreShoppingCart(_store2);
            store2_cart.AddToCart(product3, 12);
            store2_cart.AddToCart(product4, 9);

            UserShoppingCart userCart = _userManagement.getUserCart(_userManagement.getUserByGUID(_userID,true));
            userCart.StoreCarts.Add(store1_cart);
            userCart.StoreCarts.Add(store2_cart);

            Assert.IsNull(_systemManager.purchaseUserShoppingCart(_userID, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            Assert.AreEqual(product1.Quantity, 2); 
            Assert.AreEqual(product2.Quantity, 5);
            Assert.AreEqual(product3.Quantity, 8);
            Assert.AreEqual(product4.Quantity, 11);
        }


        [Test()]
        public void purchaseUserShoppingCartWithExceededProductQuantityTest()
        {
            StoreShoppingCart store1_cart = new StoreShoppingCart(_store1);
            store1_cart.AddToCart(product1, 18);
            store1_cart.AddToCart(product2, 15);
            StoreShoppingCart store2_cart = new StoreShoppingCart(_store2);
            store2_cart.AddToCart(product3, 25);
            store2_cart.AddToCart(product4, 9);

            UserShoppingCart userCart = _userManagement.getUserCart(_userManagement.getUserByGUID(_userID, true));
            userCart.StoreCarts.Add(store1_cart);
            userCart.StoreCarts.Add(store2_cart);

             var ans = _systemManager.purchaseUserShoppingCart(_userID, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            List<ProductModel> unavailablePRoducts = ans.Result;
            Assert.AreEqual(product3.Id, unavailablePRoducts.ElementAt(0).Id);
            Assert.AreEqual(product1.Quantity, 20);
            Assert.AreEqual(product2.Quantity, 20);
            Assert.AreEqual(product3.Quantity, 20);
            Assert.AreEqual(product4.Quantity, 20);
        }


    }
}