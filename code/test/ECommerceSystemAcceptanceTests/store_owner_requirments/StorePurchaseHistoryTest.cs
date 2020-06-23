using ECommerceSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.10 and 6.4.2
    [TestFixture()]
    internal class StorePurchaseHistoryTest : StoreOwnerTests
    {
        private Guid _productID;
        Guid productID1;
        Guid productID2;
        Guid productID3;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _bridge.openStore("store1");
            productID1 = _bridge.addProductInv("store1", "d", "product1", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            productID2 = _bridge.addProductInv("store1", "d", "product2", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            productID3 = _bridge.addProductInv("store1", "d", "product2", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            _bridge.logout();

            _bridge.login(_userName, _pswd);
            //bool susccess = _bridge.purchaseUserShoppingCart(_fname, _lname, "123456789", )
            _bridge.logout();
        }

        [TestCase()]
        public void storePurchaseHistorySuccess()
        {

            //List<Tuple<Guid, int>> products = new List<Tuple<Guid, int>>(); // product id --> quantity
            //products.Add(Tuple.Create(_productID, 1));
            //List<Tuple<string, List<Tuple<Guid, int>>, double>> expectedHistory = new List<Tuple<string, List<Tuple<Guid, int>>, double>>();
            //expectedHistory.Add(Tuple.Create(_userName, products, 80.0)); //user, product list, price

            ////owner
            //_bridge.login(_ownerUserName, _pswd);
            //CollectionAssert.AreEquivalent(expectedHistory, _bridge.storePurchaseHistory(_storeName), "fail to return purchase history of store");
            //_bridge.logout();

            ////permited manager
            //_bridge.login(_managerUserName, _pswd);
            //CollectionAssert.AreEquivalent(expectedHistory, _bridge.storePurchaseHistory(_storeName), "fail to return purchase history of store");
            //_bridge.logout();

            ////system admin
            //_bridge.login("admin", "4dMinnn");
            //CollectionAssert.AreEquivalent(expectedHistory, _bridge.storePurchaseHistory(_storeName), "fail to return purchase history of store");
            //_bridge.logout();
        }

        [TestCase()]
        public void storePurchaseHistoryFail()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.Null(_bridge.storePurchaseHistory("not exist"), "purchase history of not exist store successed");
            _bridge.logout();

            Assert.Null(_bridge.storePurchaseHistory(_storeName), "purchase history with guest successed");
            _bridge.login(_userName, _pswd);
            Assert.Null(_bridge.storePurchaseHistory(_storeName), "purchase history with regular user successed");
            _bridge.logout();
        }
    }
}