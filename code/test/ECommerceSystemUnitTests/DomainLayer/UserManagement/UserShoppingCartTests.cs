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
    public class UserShoppingCartTests
    {

        private UserShoppingCart _userShoppingCart;
        private StoreShoppingCart _storeShoppingCart1;
        private StoreShoppingCart _storeShoppingCart2;
        private Store _store1;
        private Store _store2;

        [OneTimeSetUp]
        public void setUp()
        {
            _store1 = new Store(null, null, "owner", "store1");
            _store2 = new Store(null, null, "owner", "store2");
            _storeShoppingCart1 = new StoreShoppingCart(_store1);
            _storeShoppingCart2 = new StoreShoppingCart(_store2);
            _userShoppingCart = new UserShoppingCart();
            _userShoppingCart.StoreCarts.Add(_storeShoppingCart1);
            _userShoppingCart.StoreCarts.Add(_storeShoppingCart2);
        }

        [Test()]
        public void getTotalACartPriceTest()
        {
            var product1 = new Product(null, null, 10, 30.0, 5);
            var product2 = new Product(null, null, 10, 20.0, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 0.0);
            _storeShoppingCart1.AddToCart(product1, 5);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 150.0);
            _storeShoppingCart2.AddToCart(product2, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 190.0);
            _storeShoppingCart1.AddToCart(product1, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 250.0);
            _storeShoppingCart1.RemoveFromCart(product1);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 40.0);
            _storeShoppingCart2.AddToCart(product2, 2);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 80.0);
            _storeShoppingCart1.AddToCart(product1, 4);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 200.0);
            _storeShoppingCart1.ChangeProductQuantity(product1, 1);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 110.0);
            _storeShoppingCart1.ChangeProductQuantity(product1, 0);
            _storeShoppingCart2.ChangeProductQuantity(product2, 0);
            Assert.AreEqual(_userShoppingCart.getTotalACartPrice(), 0.0);
        }
    }
}