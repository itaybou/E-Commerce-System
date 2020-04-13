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
    public class StoreShoppingCartTests
    {
        private StoreShoppingCart _storeShoppingCart;
        private Store _store;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            _store = new Store(null, null, "owner", "store1");
            _storeShoppingCart = new StoreShoppingCart(_store);
        }

        [TearDown]
        public void tearDown()
        {
            _storeShoppingCart.Products.Clear();
        }

        [Test()]
        public void AddToCartTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            _storeShoppingCart.AddToCart(product, 5);
            Assert.AreEqual(_storeShoppingCart.Products[product],5);
            Assert.AreEqual(_storeShoppingCart.Products.Count(), 1);
            _storeShoppingCart.AddToCart(product, 2);
            Assert.AreEqual(_storeShoppingCart.Products[product], 7);
            _storeShoppingCart.AddToCart(product2, 3);
            Assert.AreEqual(_storeShoppingCart.Products[product2], 3);
            Assert.AreEqual(_storeShoppingCart.Products.ElementAt(1).Key,product2);
            Assert.AreEqual(_storeShoppingCart.Products.Count(), 2);
        }

        [Test()]
        public void ChangeProductQuantityTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            _storeShoppingCart.AddToCart(product, 5);
            Assert.AreEqual(_storeShoppingCart.Products[product], 5);
            _storeShoppingCart.ChangeProductQuantity(product, 2);
            Assert.AreEqual(_storeShoppingCart.Products[product], 2);
            _storeShoppingCart.ChangeProductQuantity(product, 0);
            Assert.IsEmpty(_storeShoppingCart.Products);
        }

        [Test()]
        public void RemoveFromCartTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            _storeShoppingCart.AddToCart(product, 5);
            _storeShoppingCart.RemoveFromCart(product);
            Assert.IsEmpty(_storeShoppingCart.Products);
            _storeShoppingCart.AddToCart(product, 5);
            _storeShoppingCart.AddToCart(product2, 5);
            _storeShoppingCart.RemoveFromCart(product2);
            Assert.AreEqual(_storeShoppingCart.Products.Count(),1);

        }

        [Test()]
        public void getTotalCartPriceTest()
        {
            var product = new Product(null, null, 10, 25.5, 5);
            var product2 = new Product(null, null, 10, 15.5, 2);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 0.0);
            _storeShoppingCart.AddToCart(product, 5);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(),127.5);
            _storeShoppingCart.AddToCart(product2,2);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 158.5);
            _storeShoppingCart.ChangeProductQuantity(product, 1);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(),56.5);
        }

    }
}