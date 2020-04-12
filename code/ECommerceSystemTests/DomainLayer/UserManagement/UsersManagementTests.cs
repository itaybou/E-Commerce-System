using NUnit.Framework;
using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement.Tests
{
    [TestFixture()]
    public class UsersManagementTests
    {
        private UsersManagement _userManagement;
        private string uname = "test", bad_pswd = "password", good_pswd = "passwordA5",
            fname = "name", lname = "lname", email = "email@email.com", bad_email = "helloworld";

        [OneTimeSetUp]
        public void setUpFixture()
        {
            _userManagement = UsersManagement.Instance;
        }

        [SetUp]
        public void setUp()
        {

        }

        [TearDown]
        public void tearDown()
        {
            _userManagement.Users.Clear();
            _userManagement._activeUser = null;
        }

        [Test()]
        public void getLoggedInUserTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void registerTest()
        {
            Assert.NotNull(_userManagement.register(uname, bad_pswd, fname, lname, email)); // Test register method not passed due to bad password
            Assert.NotNull(_userManagement.register(uname, bad_pswd, fname, lname, bad_email)); // Test register method not passed due to bad email
            Assert.Null(_userManagement.register(uname, good_pswd, fname, lname, email)); // Test register method passes with good password
            Assert.True(_userManagement.Users.Any() && _userManagement.Users.Count.Equals(1));   // Test users list has 
            Assert.IsEmpty(_userManagement.Users.First().Value);   // Test user initialized with empty shopping cart
            var registered = _userManagement.Users.First().Key;
            Assert.AreEqual(registered.Name(), uname);
            Assert.AreNotEqual(registered._state.Password(), good_pswd); // Test password store as encrypted after registration
        }

        [Test()]
        public void loginTest()
        {
            Assert.False(_userManagement.login(uname, good_pswd)); // Check unable to login with unregistered user
            _userManagement.register(uname, good_pswd, fname, lname, email);
            Assert.True(_userManagement.login(uname, good_pswd)); // Check unable to login with unregistered user
            var activeUser = _userManagement._activeUser;
            Assert.AreEqual(activeUser.Name(), uname);                      // check that login changed current active user
            Assert.AreNotEqual(activeUser._state.Password(), good_pswd);    // check password is encrypted
            Assert.IsEmpty(activeUser._cart);                               // Test user cart is empty
        }

        [Test()]
        public void logoutTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);  
            _userManagement.login(uname, good_pswd);
            var activeUser = _userManagement._activeUser;
            var subscribedCart = activeUser._cart;
            Assert.That(activeUser._state, Is.InstanceOf<Subscribed>());     // Test logged user is instance of Subscribed user
            _userManagement.logout();
            Assert.That(activeUser._state, Is.InstanceOf<Guest>());         // Test logged out user is instance of Guest user
            Assert.False(subscribedCart == activeUser._cart);               // Test new cart provided to guest after logout
        }

        [Test()]
        public void getUserCartTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            var notRegistered = new User(new Guest());
            var registered = _userManagement.Users.First();
            var storeCart = new StoreShoppingCart(null);
            storeCart.AddToCart(new StoresManagement.Product(null, null, 2, 2.0, 4), 1);
            registered.Value.StoreCarts.Add(storeCart);
            Assert.IsNotEmpty(registered.Value);        // Test current user cart is not empty
            _userManagement.logout();
            Assert.AreEqual(_userManagement.getUserCart(registered.Key).Count(), 1); // Test getting registered user cart contains the added product
            Assert.IsEmpty(_userManagement.getUserCart(notRegistered)); // Test cart for unregistered user is empty
        }

        [Test()]
        public void addProductToCartTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ShoppingCartDetailsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void changeProductQuantityTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void removeProdcutFromCartTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void getActiveUserShoppingCartTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void logUserPurchaseTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void addOwnStoreTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void addManagerStoreTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void removeManagerStoreTest()
        {
            Assert.Fail();
        }
    }
}