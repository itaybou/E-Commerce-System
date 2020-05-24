using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement.Tests
{
    [TestFixture()]
    public class UsersManagementTests
    {
        private UsersManagement _userManagement;
        private StoreManagement _storeManagement;
        private Product product;
        private Product product2;
        private Store store;
        private Store store2;
        private Guid userID;

        private string uname = "test", bad_pswd = "password", good_pswd = "passwordA5",
            fname = "name", lname = "lname", email = "email@email.com", bad_email = "helloworld";

        [SetUp]
        public void setUpFixture()
        {
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
            product = new Product(null, null, 20, 20, Guid.NewGuid());
            product2 = new Product(null, null, 20, 20, Guid.NewGuid());
            store = new Store("owner", "store1");
            store2 = new Store("owner", "store2");
            store.Inventory.Products = new List<ProductInventory>()
            {
                {new ProductInventory(null,null,20.0,Category.CELLPHONES,new List<string>(),Guid.NewGuid())}
            };
            store2.Inventory.Products = new List<ProductInventory>()
            {
                {new ProductInventory(null,null,20.0,Category.CELLPHONES,new List<string>(),Guid.NewGuid())}
            };
            store.Inventory.Products.First().ProductList.Add(product);
            store.Inventory.Products.First().ProductList.Add(product2);
            store2.Inventory.Products.First().ProductList.Add(product);
            store2.Inventory.Products.First().ProductList.Add(product2);
            _storeManagement.Stores.Add(store);
            _storeManagement.Stores.Add(store2);
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
            userID = _userManagement.getUserByName(uname).Guid;
        }

        [TearDown]
        public void tearDown()
        {
            _userManagement.logout(userID);
            _userManagement.UserCarts.Clear();
            _storeManagement.Stores.Clear();
        }

        [Test()]
        public void registerTest()
        {
            Assert.NotNull(_userManagement.register(uname + "1", bad_pswd, fname, lname, email)); // Test register method not passed due to bad password
            Assert.NotNull(_userManagement.register(uname + "1", bad_pswd, fname, lname, bad_email)); // Test register method not passed due to bad email
            Assert.Null(_userManagement.register(uname + "1", good_pswd, fname, lname, email)); // Test register method passes with good password
            Assert.True(_userManagement.UserCarts.Any() && _userManagement.UserCarts.Count.Equals(2));   // Test users list has
            Assert.IsEmpty(_userManagement.UserCarts.First().Value);   // Test user initialized with empty shopping cart
        }

        [Test()]
        public void loginTest()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            Assert.True(_userManagement.login(uname, good_pswd).Item1); // Check able to login registered user
            var userID = _userManagement.getUserByName(uname).Guid;
            _userManagement.logout(userID);
            _userManagement.register(uname + "2", good_pswd, fname, lname, "1" + email);
            Assert.True(_userManagement.login(uname + "2", good_pswd).Item1); // Check able to login registered user
            _userManagement.logout(userID);
            Assert.True(_userManagement.login(uname, good_pswd).Item1);       // Check able to login again registered user
            _userManagement.logout(userID);
            Assert.False(_userManagement.login(uname, "1Apassword").Item1);       // Check unable to login with different password
        }

        [Test()]
        public void checkUnableToLoginBeforeRegister()
        {
            Assert.False(_userManagement.login(uname + "1", good_pswd).Item1); // Check unable to login with unregistered user
        }

        [TestCase()]
        public void checkLoginedUserPasswordIsEncrypted()
        {
            _userManagement.register(uname, good_pswd, fname, lname, email);
            _userManagement.login(uname, good_pswd);
        }

        [Test()]
        public void logoutTest()
        {
            Assert.AreEqual(_userManagement.logout(userID), true);
        }

        [Test()]
        public void getUserCartTest()
        {
            var registered = _userManagement.UserCarts.First();
            var storeCart = new StoreShoppingCart(null);
            storeCart.AddToCart(new Product(null, null, 20, 20, Guid.NewGuid()), 5);
            registered.Value.StoreCarts.Add(storeCart);
            Assert.IsNotEmpty(registered.Value);        // Test current user cart is not empty
        }

        [Test()]
        public void getNotRegisteredUserCartIsEmpty()
        {
            var notRegistered = new User(new Guest());
            Assert.IsEmpty(_userManagement.getUserCart(notRegistered)); // Test cart for unregistered user is empty
        }

        [Test()]
        public void addProductToCartTest()
        {
            _userManagement.addProductToCart(userID, product.Id, store.Name, 1);
            var cart = _userManagement.getUserCart(_userManagement.getUserByName(uname));
            Assert.AreEqual(cart.StoreCarts.Count, 1);                            // Test only one new store cart added
            Assert.AreEqual(cart.StoreCarts.First().Products.Count, 1);           // Test only one new prodcut added
            Assert.AreEqual(cart.StoreCarts.First().Products[product], 1);        // Test product quantity added is one
            _userManagement.addProductToCart(userID, product.Id, store.Name, 2);
            Assert.AreEqual(cart.StoreCarts.Count, 1);                            // Test adding same product from same store, store cart list remains of size one
            Assert.AreEqual(cart.StoreCarts.First().Products.Count, 1);           // Test adding same product from same store, product count remains one
            Assert.AreEqual(cart.StoreCarts.First().Products[product], 3);        // Test adding same product from same store, increases accumulated qunatity
            _userManagement.addProductToCart(userID, product2.Id, store.Name, 1);
            Assert.AreEqual(cart.StoreCarts.Count, 1);                            // Test no new store carts added after adding product from same store
            Assert.AreEqual(cart.StoreCarts.First().Products.Count, 2);           // Test prodcuts from store count is increased
            Assert.AreEqual(cart.StoreCarts.First().Products[product2], 1);       // Test product quantity added is one
            _userManagement.addProductToCart(userID, product2.Id, store2.Name, 1);
            Assert.AreEqual(cart.StoreCarts.Count, 2);                            // Test adding product from new store increase store cart count
            Assert.AreEqual(cart.StoreCarts.ElementAt(1).Products.Count, 1);      // Test new store cart containd only one product
            Assert.AreEqual(cart.StoreCarts.ElementAt(1).Products[product2], 1);  // Test new store cart contains the new product with quantity one
        }

        [Test()]
        public void CartisNotEmptyAfterAddingProductTest()
        {
            _userManagement.addProductToCart(userID, product.Id, store.Name, 1);
            var cart = _userManagement.getUserCart(_userManagement.getUserByName(uname));
            Assert.IsNotEmpty(cart);                                              // Test cart is not empty after adding first product
        }

        [Test()]
        public void InitialStoreCartsEmptyTest()
        {
            var cart = _userManagement.getUserCart(_userManagement.getUserByName(uname));
            Assert.IsEmpty(cart);                                               // Test inital cart is empty
            Assert.IsEmpty(cart.StoreCarts);                                    // Test inital store carts list is empty
        }

        [Test()]
        public void CheckAddToCartIncorrectValuesTest()
        {
            Assert.False(_userManagement.addProductToCart(userID, product.Id, store.Name, 0)); // Test not adding 0 products to cart
            Assert.False(_userManagement.addProductToCart(userID, product.Id, store.Name, -2)); // Test not adding negative quantity products to cart
        }

        [Test()]
        public void changeProductQuantityTest()
        {
            _userManagement.addProductToCart(userID, product.Id, store.Name, 1);
            Assert.True(_userManagement.changeProductQuantity(userID, product.Id, 8));         // Test returns true after changing to available amount
            var cart = _userManagement.getUserCart(_userManagement.getUserByName(uname));
            Assert.AreEqual(cart.StoreCarts.First().Products.First().Value, 8);   // Test product quantity changed to 8 (available amount)
            Assert.True(_userManagement.changeProductQuantity(userID, product.Id, 0));         // Test returns true after changing to zero amount
            Assert.IsEmpty(cart);            // Test product removed after changing qunatity to 0
        }

        [Test()]
        public void CheckCantChangeToNegativeQuantityTest()
        {
            Assert.False(_userManagement.changeProductQuantity(userID, product.Id, -1));       // Test change to negative quantity fails
        }

        [Test()]
        public void CheckCantChangeMoreThenQuantityOrProductNotExistTest()
        {
            Assert.False(_userManagement.changeProductQuantity(userID, product.Id, 21));       // Test trying to change to quantity above available
            Assert.False(_userManagement.changeProductQuantity(userID, product2.Id, 2));       // Tests fails on trying to change product quantity that does not exists in cart
        }

        [Test()]
        public void removeProdcutFromCartTest()
        {
            _userManagement.addProductToCart(userID, product.Id, store.Name, 1);
            Assert.True(_userManagement.removeProdcutFromCart(userID, product.Id));    // Test passed on trying to remove existing product
            Assert.IsEmpty(_userManagement.getUserCart(_userManagement.getUserByName(uname)));    // Test prodcut was removed
            _userManagement.addProductToCart(userID, product.Id, store.Name, 1);
            _userManagement.addProductToCart(userID, product2.Id, store.Name, 1);
            Assert.True(_userManagement.removeProdcutFromCart(userID, product.Id));    // Test passed on trying to remove existing product
        }

        [Test()]
        public void removeProductNotExistInCartTest()
        {
            Assert.False(_userManagement.removeProdcutFromCart(userID, product2.Id));  // Test fail on trying to remove non existing product
        }

        [Test()]
        public void logUserPurchaseTest()
        {
            var prodcutQunatities = new Dictionary<Product, int>() { { product, 1 }, { product2, 2 } };
            _userManagement.logUserPurchase(userID, 41, prodcutQunatities, "fname", "lname", 305278384, "343-434", DateTime.Now, 300, "BGU University");
            var purchaseHistory = ((Subscribed)(_userManagement.getUserByName(uname)._state)).PurchaseHistory;
            Assert.AreEqual(purchaseHistory.First().ProductsPurchased.Count, 2);            // Test both proudcts added to user purchase on same purchase
            Assert.True(purchaseHistory.First().ProductsPurchased.First() != product);      // Test not same product was stored in history(indifferent to changes)
            Assert.True(purchaseHistory.First().ProductsPurchased.ElementAt(1) != product2); // Test not same product was stored in history(indifferent to changes)
        }

        [Test()]
        public void getUserByNameTest()
        {
            _userManagement.logout(userID);
            _userManagement.register("user2", good_pswd, fname, lname, "1" + email);
            _userManagement.login("user2", good_pswd);
        }

        [Test()]
        public void isSubscribedTest()
        {
            Assert.True(_userManagement.isSubscribed(uname)); // Test user registered is subscribed
        }
    }
}