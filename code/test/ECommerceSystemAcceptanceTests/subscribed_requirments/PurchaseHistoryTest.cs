using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 3.7
    [TestFixture()]
    internal class PurchaseHistoryTest
    {
        private string store_uname, store_pswd;
        private string uname, pswd;
        private IBridgeAdapter _bridge;
        string firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            _bridge = Driver.getAcceptanceBridge();
            store_uname = "store_user1";
            store_pswd = "store_pSwd1";
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
            _bridge.register(store_uname, store_pswd, "user", "userlname", "mymail@mail.com");
            _bridge.login(store_uname, store_pswd);
            _bridge.openStoreWithProducts("store1", store_uname, new List<string>() { { "product1" }, { "product2" }, { "product3" }, { "product4" } });
            _bridge.logout();

            _bridge.register(uname, pswd, "user2", "userlname2", "mymail2@mail.com");
            _bridge.login(uname, pswd);
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
            _bridge.storesCleanUp();
        }

        //[TestCase()]
        //public void TestViewPurchaseHistory()
        //{
        //    _bridge.AddTocart(1, 10);
        //    _bridge.AddTocart(2, 5);
        //    _bridge.PurchaseProducts(null, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address);
        //    var h = _bridge.UserPurchaseHistory();
        //    Assert.True(h.SequenceEqual(new List<long>() { { 1 }, { 2 } }));
        //    Assert.IsEmpty(_bridge.ViewUserCart());
        //    _bridge.AddTocart(3, 20);
        //    _bridge.PurchaseProducts(null, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address);
        //    h = _bridge.UserPurchaseHistory();
        //    Assert.True(h.SequenceEqual(new List<long>() { { 1 }, { 2 }, { 3 } }));
        //    _bridge.AddTocart(1, 10);
        //    _bridge.AddTocart(2, 5);
        //    _bridge.AddTocart(4, 10);
        //    _bridge.PurchaseProducts(null, firstName, lastName, id, creditCardNumber, creditExpiration, cvv, address);
        //    Assert.True(_bridge.UserPurchaseHistory().SequenceEqual(new List<long>() { { 1 }, { 2 }, { 3 }, { 1 }, { 2 }, { 4 } }));
        //}
    }
}