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
        private ICollection<(Store, double, IDictionary<Product, int>)> _storeProducts;
        private string _firstName = "fname", _lastName = "lname", _address = "address 1", _creditCardNumber = "413-547";
        private int _id = 54362432, _CVV = 300;
        private DateTime _expirationCreditCard = DateTime.Now.AddDays(2.0);
        private Product product1;
        private Product product2;
        private Product product3;
        private Product product4;
        private Guid _userID;


        [SetUp]
        public void setUp()
        {
            _systemManager = SystemManager.Instance;
            _userManagement = UsersManagement.Instance;
            _userManagement.register("user1", "pA55word", "user", "last", "mail@mail");
            _userManagement.login("user1", "pA55word");
            _store1 = new Store( "owner1", "store1");
            _store2 = new Store( "owner2", "store2");
            product1 = new Product(null, null,   20, 20, Guid.NewGuid());
            product2 = new Product(null, null,  20, 20, Guid.NewGuid());
            product3 = new Product(null, null,  20, 20, Guid.NewGuid());
            product4 = new Product(null, null,  20, 20, Guid.NewGuid());
            _userID= _userManagement.getUserByName("user1").Guid;
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
            StoreManagement.Instance.Stores.Clear();
        }

        [Test()]
        public void makePurchaseSuccessTest()
        {
            Assert.True(_systemManager.makePurchase(_userID,   _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
            Assert.AreEqual(product1.Quantity, 15); // First product quantity decreased (first store purchased from)
            Assert.AreEqual(product2.Quantity,10);
            Assert.AreEqual(product3.Quantity, 18);
            Assert.AreEqual(product4.Quantity, 0);
        }

        [Test()]
        public void makePurchaseFaildBadCreditCardDetailsTest()
        {
            Assert.False(_systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV /10, _address));
            Assert.False(_systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id, _creditCardNumber, DateTime.Now.AddDays(-1.0), _CVV, _address));
        }

        [Test()]
        public void makePurchaseCheckStorePurchaseHistoryUpdateAfterPurchaseTest()
        {
            _systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id,
                _creditCardNumber, _expirationCreditCard, _CVV, _address);
            Assert.True(_store1.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product1.Id)));
            Assert.True(_store1.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product2.Id)));
            Assert.True(_store2.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product3.Id)));
            Assert.True(_store2.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product4.Id)));
        }


        [Test()]
        public void makePurchaseCheckStorePurchaseHistoryCorrectAfterPurchaseTest()
        {
            _systemManager.makePurchase(_userID, _totalPrice, _storeProducts, _allProducts, _firstName, _lastName, _id,
                _creditCardNumber, _expirationCreditCard, _CVV, _address);
            Assert.False(_store2.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product1.Id)));
            Assert.False(_store2.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product2.Id)));
            Assert.False(_store1.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product3.Id)));
            Assert.False(_store1.PurchaseHistory.First().ProductsPurchased.Exists(p => p.Id.Equals(product4.Id)));
        }


        //[Test()]
        //public void purchaseUserShoppingCartTest()
        //{
        //    var store1_cart = new StoreShoppingCart(_store1);
        //    var store2_cart = new StoreShoppingCart(_store2);
        //    _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store1_cart);
        //    _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store2_cart);
        //    store1_cart.AddToCart(product1, 26);
        //    Assert.IsNotEmpty(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        //    Assert.True(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address).First().Id.Equals(guid1));
        //    store1_cart.ChangeProductQuantity(_storeProducts[_store1].Keys.First(), 20);
        //    Assert.IsNull(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        //    store1_cart = new StoreShoppingCart(_store1);
        //    store2_cart = new StoreShoppingCart(_store2);
        //    _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store1_cart);
        //    _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store2_cart);
        //    store1_cart.AddToCart(_storeProducts[_store1].Keys.ElementAt(1), 26);
        //    store2_cart.AddToCart(_storeProducts[_store2].Keys.First(), 9);
        //    Assert.AreEqual(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address).Count, 1);
        //    _userManagement.getLoggedInUser()._cart = new UserShoppingCart();
        //    _userManagement.Users[_userManagement.getLoggedInUser()] = _userManagement.getLoggedInUser()._cart;
        //    store1_cart = new StoreShoppingCart(_store1);
        //    store2_cart = new StoreShoppingCart(_store2);
        //    _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store1_cart);
        //    _userManagement.getUserCart(_userManagement.getLoggedInUser()).StoreCarts.Add(store2_cart);
        //    store1_cart.AddToCart(_storeProducts[_store2].Keys.ElementAt(1), 20);
        //    store1_cart.AddToCart(_storeProducts[_store2].Keys.First(), 6);
        //    Assert.IsNull(_systemManager.purchaseUserShoppingCart(_firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        //}


        //[Test()]
        //public void purchaseProductTest()
        //{
        //    var product1 = new Product(null, null, new VisibleDiscount(20.0f, new DiscountPolicy()), new ImmediatePurchase(), 20, 20, Guid.NewGuid());

        //    var purchase = Tuple.Create(_store1, (product1, 25));
        //    Assert.False(_systemManager.purchaseProduct(purchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        //    purchase = Tuple.Create(_store1, (product1, 20));
        //    Assert.True(_systemManager.purchaseProduct(purchase, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address));
        //}
    }
}