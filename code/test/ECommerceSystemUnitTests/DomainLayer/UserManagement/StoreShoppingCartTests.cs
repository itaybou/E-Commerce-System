using ECommerceSystem.DomainLayer.StoresManagement;
using NUnit.Framework;
using System;
using System.Linq;

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
            _store = new Store( "owner", "store1");
            _storeShoppingCart = new StoreShoppingCart(_store);
        }

        [TearDown]
        public void tearDown()
        {
            _storeShoppingCart.Products.Clear();       //clear the shopping carts after each test
        }

        [Test()]
        public void AddToCartTest()
        {
            var product = new Product(null, null,  20, 20, Guid.NewGuid());
            var product2 = new Product(null, null,  20, 20, Guid.NewGuid());
            _storeShoppingCart.AddToCart(product, 5);
            Assert.AreEqual(_storeShoppingCart.Products[product], 5);   //check if the product added to cart with requested quantity
            Assert.AreEqual(_storeShoppingCart.Products.Count(), 1);    //check if only 1 product added to cart
            _storeShoppingCart.AddToCart(product, 2);                         //add more quantity to existing product 
            Assert.AreEqual(_storeShoppingCart.Products[product], 7);   //check the quantity is updated 
            _storeShoppingCart.AddToCart(product2, 3);                        //add a new product to existing cart
            Assert.AreEqual(_storeShoppingCart.Products[product2], 3);  ////check if the new product added to cart with requested quantity
            Assert.AreEqual(_storeShoppingCart.Products.ElementAt(1).Key, product2);    //check that the second product in the existing cart is the new product
            Assert.AreEqual(_storeShoppingCart.Products.Count(), 2);                    //check that now the shopping cart include 2 products
        }

        [Test()]
        public void ChangeProductQuantityTest()
        {
            var product = new Product(null, null, 20, 20, Guid.NewGuid());
            var product2 = new Product(null, null,  20, 20, Guid.NewGuid());
            _storeShoppingCart.AddToCart(product, 5);
            Assert.AreEqual(_storeShoppingCart.Products[product], 5);
            _storeShoppingCart.ChangeProductQuantity(product, 2);               //check the quantity of existing product in the cart
            Assert.AreEqual(_storeShoppingCart.Products[product], 2);     //check that the quantity of the product was updated
            _storeShoppingCart.ChangeProductQuantity(product2, 3);              //changing the quantity of a product that does not exist in the shopping cart 
            _storeShoppingCart.AddToCart(product2, 5);
            Assert.AreEqual(_storeShoppingCart.Products[product2], 5);
        }

        [Test()]
        public void CheckProductRemovedFromCartAfterChangeQuantityToZero()
        {
            var product = new Product(null, null, 20, 20, Guid.NewGuid());
            _storeShoppingCart.AddToCart(product, 5);
            _storeShoppingCart.ChangeProductQuantity(product, 0);
            Assert.IsEmpty(_storeShoppingCart.Products);            //check that the shopping cart remain empty
        }

        [Test()]
        public void CheckChangeQuantityToProductDoesntExistInCart()
        {
            var product = new Product(null, null,  20, 20, Guid.NewGuid());
            _storeShoppingCart.ChangeProductQuantity(product, 5);
            Assert.IsEmpty(_storeShoppingCart.Products);
        }


        [Test()]
        public void RemoveFromCartTest()
        {
            var product = new Product(null, null, 20, 20, Guid.NewGuid());
            var product2 = new Product(null, null, 20, 20, Guid.NewGuid());
            _storeShoppingCart.AddToCart(product, 5);
            _storeShoppingCart.RemoveFromCart(product);         //remove a product from the shopping cart 
            Assert.IsEmpty(_storeShoppingCart.Products);        //check if the shopping cart is empty after the remove 
            _storeShoppingCart.AddToCart(product, 5);
            _storeShoppingCart.AddToCart(product2, 5);
            _storeShoppingCart.RemoveFromCart(product2);        //try to remove a new product from the shopping cart
            Assert.AreEqual(_storeShoppingCart.Products.Count(), 1);    //check if the shopping cart include only 1 product after the remove.
        }

        [Test()]
        public void getTotalCartPriceTest()
        {
            var product = new Product(null, null, 20, 20, Guid.NewGuid());
            var product2 = new Product(null, null, 20, 20, Guid.NewGuid());
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 0.0);       //check the total price of empty shopping cart
            _storeShoppingCart.AddToCart(product, 5);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 100.0);     //check the total price after adding a new product to cart 
            _storeShoppingCart.AddToCart(product2, 2);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 140.0);     //check the total price after adding second product to cart
            _storeShoppingCart.ChangeProductQuantity(product, 1);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 60.0);      //check the total price after changing the quantity of a product
            _storeShoppingCart.RemoveFromCart(product2);
            _storeShoppingCart.RemoveFromCart(product);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 0.0);       //check the total price after removing all the products to zero
            _storeShoppingCart.AddToCart(product, 5);
            _storeShoppingCart.AddToCart(product2, 2);
            _storeShoppingCart.RemoveFromCart(product2);
            Assert.AreEqual(_storeShoppingCart.getTotalCartPrice(), 100.0);     //check the total price after removing only 1 product


        }
    }
}