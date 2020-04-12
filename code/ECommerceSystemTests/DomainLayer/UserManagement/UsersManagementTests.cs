using NUnit.Framework;
using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;

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
            _userManagement.register(uname, good_pswd, fname, lname, email);
            Assert.Null(_userManagement.getLoggedInUser());                                 // Test logged in user is null before login
            _userManagement.login(uname, good_pswd);
            Assert.True(_userManagement._activeUser == _userManagement.getLoggedInUser()); // Test connected user is the active user
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
            Assert.True(_userManagement.login(uname, good_pswd)); // Check able to login registered user
            var activeUser = _userManagement._activeUser;
            Assert.AreEqual(activeUser.Name(), uname);                      // check that login changed current active user
            Assert.AreNotEqual(activeUser._state.Password(), good_pswd);    // check password is encrypted
            Assert.IsEmpty(activeUser._cart);                               // Test user cart is empty
            _userManagement.logout();
            _userManagement.register(uname + "2", good_pswd, fname, lname, "1" + email);
            Assert.True(_userManagement.login(uname + "2", good_pswd)); // Check able to login registered user
            _userManagement.logout();
            Assert.True(_userManagement.login(uname, good_pswd));       // Check able to login again registered user
            _userManagement.logout();
            Assert.False(_userManagement.login(uname, "1Apassword"));       // Check unable to login with different password
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
            activeUser = _userManagement._activeUser;
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
            Assert.AreEqual(_userManagement.getUserCart(registered.Key).Count(), 1); // Test getting registered user cart contains the added product
            Assert.IsEmpty(_userManagement.getUserCart(notRegistered)); // Test cart for unregistered user is empty
        }

        [Test()]
        public void addProductToCartTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            var store = new Store(null, null, "owner", "store1");
            var store2 = new Store(null, null, "owner", "store2");
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            Assert.False(_userManagement.addProductToCart(product, store, 0)); // Test not adding 0 products to cart
            Assert.False(_userManagement.addProductToCart(product, store, -2)); // Test not adding negative quantity products to cart
            var cart = _userManagement.getActiveUserShoppingCart();
            Assert.IsEmpty(cart);                                               // Test inital cart is empty
            Assert.IsEmpty(cart.StoreCarts);                                    // Test inital store carts list is empty
            _userManagement.addProductToCart(product, store, 1);                
            Assert.IsNotEmpty(cart);                                              // Test cart is not empty after adding first product
            Assert.AreEqual(cart.StoreCarts.Count, 1);                            // Test only one new store cart added
            Assert.AreEqual(cart.StoreCarts.First().Products.Count, 1);           // Test only one new prodcut added
            Assert.AreEqual(cart.StoreCarts.First().Products[product], 1);        // Test product quantity added is one
            _userManagement.addProductToCart(product, store, 2);                
            Assert.AreEqual(cart.StoreCarts.Count, 1);                            // Test adding same product from same store, store cart list remains of size one
            Assert.AreEqual(cart.StoreCarts.First().Products.Count, 1);           // Test adding same product from same store, product count remains one
            Assert.AreEqual(cart.StoreCarts.First().Products[product], 3);        // Test adding same product from same store, increases accumulated qunatity
            _userManagement.addProductToCart(product2, store, 1);
            Assert.AreEqual(cart.StoreCarts.Count, 1);                            // Test no new store carts added after adding product from same store
            Assert.AreEqual(cart.StoreCarts.First().Products.Count, 2);           // Test prodcuts from store count is increased
            Assert.AreEqual(cart.StoreCarts.First().Products[product2], 1);       // Test product quantity added is one
            _userManagement.addProductToCart(product2, store2, 1);
            Assert.AreEqual(cart.StoreCarts.Count, 2);                            // Test adding product from new store increase store cart count
            Assert.AreEqual(cart.StoreCarts.ElementAt(1).Products.Count, 1);      // Test new store cart containd only one product
            Assert.AreEqual(cart.StoreCarts.ElementAt(1).Products[product2], 1);  // Test new store cart contains the new product with quantity one
        }

        [Test()]
        public void ShoppingCartDetailsTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            Assert.AreEqual(_userManagement.getActiveUserShoppingCart(), _userManagement.ShoppingCartDetails()); // compare logged user cart to returned cart
        }

        [Test()]
        public void changeProductQuantityTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            var store = new Store(null, null, "owner", "store1");
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            Assert.False(_userManagement.changeProductQuantity(product, -1));       // Test change to negative quantity fails
            _userManagement.addProductToCart(product, store, 1);
            Assert.False(_userManagement.changeProductQuantity(product2, 2));       // Tests fails on trying to change product quantity that does not exists in cart
            Assert.False(_userManagement.changeProductQuantity(product, 12));       // Test trying to change to quantity above available
            Assert.True(_userManagement.changeProductQuantity(product, 8));         // Test returns true after changing to available amount
            Assert.AreEqual(_userManagement.getActiveUserShoppingCart().StoreCarts.First().Products.First().Value, 8);   // Test product quantity changed to 8 (available amount)
            Assert.True(_userManagement.changeProductQuantity(product, 0));         // Test returns true after changing to zero amount
            Assert.IsEmpty(_userManagement.getActiveUserShoppingCart());            // Test product removed after changing qunatity to 0
        }

        [Test()]
        public void removeProdcutFromCartTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            var store = new Store(null, null, "owner", "store1");
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            _userManagement.addProductToCart(product, store, 1);
            Assert.False(_userManagement.removeProdcutFromCart(product2));  // Test fail on trying to remove non existing product
            Assert.True(_userManagement.removeProdcutFromCart(product));    // Test passed on trying to remove existing product
            Assert.IsEmpty(_userManagement.getActiveUserShoppingCart());    // Test prodcut was removed
            _userManagement.addProductToCart(product, store, 1);
            _userManagement.addProductToCart(product2, store, 1);
            Assert.True(_userManagement.removeProdcutFromCart(product));    // Test passed on trying to remove existing product
            Assert.AreEqual(_userManagement.getActiveUserShoppingCart().Count(), 1);    // Test only one product was removed
            Assert.AreEqual(_userManagement.getActiveUserShoppingCart().First(), product2); // Test remaining product is the one that was not removed

        }

        [Test()]
        public void getActiveUserShoppingCartTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            var activeUser = _userManagement.getLoggedInUser();
            Assert.True(activeUser._cart == _userManagement.getActiveUserShoppingCart());   // Test logged in user cart is equal to returned cart
        }

        [Test()]
        public void logUserPurchaseTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            var prodcutQunatities = new Dictionary<Product, int>(){{product, 1}, {product2, 2}};
            _userManagement.logUserPurchase(41, prodcutQunatities, "fname", "lname", 305278384, "343-434", DateTime.Now, 300, "BGU University");
            var purchaseHistory = ((Subscribed)(_userManagement.getLoggedInUser()._state)).PurchaseHistory;
            Assert.AreEqual(purchaseHistory.First().ProductsPurchased.Count, 2);            // Test both proudcts added to user purchase on same purchase
            Assert.True(purchaseHistory.First().ProductsPurchased.First() != product);      // Test not same product was stored in history(indifferent to changes)
            Assert.True(purchaseHistory.First().ProductsPurchased.ElementAt(1) != product2); // Test not same product was stored in history(indifferent to changes)
        }

        [Test()]
        public void getUserByNameTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            Assert.True(_userManagement.getUserByName(uname) == _userManagement.getLoggedInUser()); // Test returned same user as logged in
            _userManagement.logout();
            _userManagement.register("user2", good_pswd, fname, lname, "1" + email);
            _userManagement.login("user2", good_pswd);
            Assert.True(_userManagement.getUserByName("user2") == _userManagement.getLoggedInUser()); // Test returned same user as logged in
        }

        [Test()]
        public void isSubscribedTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            Assert.True(_userManagement.isSubscribed(uname)); // Test user registered is subscribed
        }
    }
}