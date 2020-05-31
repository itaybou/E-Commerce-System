using ECommerceSystem.DomainLayer.StoresManagement;
using NUnit.Framework;
using System;

namespace ECommerceSystem.DomainLayer.UserManagement.Tests
{
    [TestFixture()]
    public class UserShoppingCartTests
    {
        private UserShoppingCart _userShoppingCart;
        private StoreShoppingCart _storeShoppingCart1;
        private StoreShoppingCart _storeShoppingCart2;
        private Store _store1;
        private Store _store2;

        [SetUp]
        public void setUp()
        {
            _store1 = new Store("owner", "store1");
            _store2 = new Store("owner", "store2");
            _storeShoppingCart1 = new StoreShoppingCart(_store1);
            _storeShoppingCart2 = new StoreShoppingCart(_store2);
            _userShoppingCart = new UserShoppingCart();
            _userShoppingCart.StoreCarts.Add(_storeShoppingCart1);
            _userShoppingCart.StoreCarts.Add(_storeShoppingCart2);
        }

        [Test()]
        public void getTotalACartPriceTest()
        {
            var product1 = new Product(null, null, 20, 20, Guid.NewGuid());
            var product2 = new Product(null, null, 20, 20, Guid.NewGuid());
            _storeShoppingCart1.AddToCart(product1, 5);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 100.0);     //check the total price after adding a product to cart
            _storeShoppingCart2.AddToCart(product2, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 140.0);     //check the total price after adding a new product to cart
            _storeShoppingCart1.AddToCart(product1, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 180.0);     //check the total price after adding a new product to cart
            _storeShoppingCart1.RemoveFromCart(product1);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 40.0);      //check the total price after removing a product from a cart
            _storeShoppingCart2.AddToCart(product2, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 80.0);
            _storeShoppingCart1.AddToCart(product1, 4);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 160.0);
            _storeShoppingCart1.ChangeProductQuantity(product1, 1);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 100.0);     //check the total price after changing the quantity of a product
        }

        [Test()]
        public void getTotalCartPriceOfEmptyCart()
        {
            var product1 = new Product(null, null, 20, 20, Guid.NewGuid());
            var product2 = new Product(null, null, 20, 20, Guid.NewGuid());
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 0.0);      //check the total price of empty shopping cart
            _storeShoppingCart1.AddToCart(product1, 4);
            _storeShoppingCart1.ChangeProductQuantity(product1, 0);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 0.0);       //check the total price after changing the quantity of the product to zero
        }
    }
}