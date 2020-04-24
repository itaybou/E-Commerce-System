using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.8
    [TestFixture()]
    internal class PurchaseTest
    {
        private string uname, pswd;
        private IBridgeAdapter _bridge;
        string firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            _bridge = Driver.getAcceptanceBridge();
            uname = "test_user1";
            pswd = "Hell0World";

            firstName = "buyer";
            lastName = "buyerLast";
            id = "12312312";
            creditCardNumber = "123-123";
            creditExpiration = "May 2, 2020";
            cvv = "300";
            address = "my address, 14, NY";
        }

        [SetUp]
        public void setUp()
        {
            _bridge.register(uname, pswd, "user", "userlname", "mymail@mail.com");
            _bridge.login(uname, pswd);
            _bridge.openStoreWithProducts("store1", uname, new List<string>() { { "product1" }, { "product2" }, { "product3" } } );
            _bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product4" }, { "product5" }, { "product6" } });
            _bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product4" }, { "product5" }, { "product6" }, { "product7" } });
            _bridge.logout();
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
            _bridge.storesCleanUp();
        }

        //[TestCase()]
        //public void TestPurchaseCart()
        //{
        //    _bridge.AddTocart(1, 20);
        //    Assert.True(_bridge.PurchaseProducts(null, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //    _bridge.AddTocart(2, 20);
        //    _bridge.AddTocart(3, 20);
        //    Assert.IsNotEmpty(_bridge.ViewUserCart());
        //    Assert.True(_bridge.PurchaseProducts(null, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //    Assert.IsEmpty(_bridge.ViewUserCart());
        //}

        //[TestCase()]
        //public void TestPurchaseProductsFromCart()
        //{
        //    _bridge.AddTocart(1, 10);
        //    _bridge.AddTocart(2, 10);
        //    _bridge.AddTocart(3, 10);
        //    Assert.True(_bridge.PurchaseProducts(new Dictionary<long, int>() { { 1, 10 }, { 2, 10 } }, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //    Assert.IsNotEmpty(_bridge.ViewUserCart());
        //    Assert.True(_bridge.PurchaseProducts(new Dictionary<long, int>() { { 3, 10 } }, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //    Assert.IsEmpty(_bridge.ViewUserCart());
        //    _bridge.AddTocart(3, 10);
        //    _bridge.AddTocart(2, 10);
        //    Assert.True(_bridge.PurchaseProducts(new Dictionary<long, int>() { { 3, 3 }, { 2, 10 } }, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //    Assert.IsNotEmpty(_bridge.ViewUserCart());
        //}

        //[TestCase()]
        //public void TestPurchaseProduct()
        //{
        //    Assert.True(_bridge.PurchaseProducts(new Dictionary<long, int>() { { 4, 10 } }, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //    Assert.True(_bridge.PurchaseProducts(new Dictionary<long, int>() { { 4, 10 } }, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address));
        //}
    }
}