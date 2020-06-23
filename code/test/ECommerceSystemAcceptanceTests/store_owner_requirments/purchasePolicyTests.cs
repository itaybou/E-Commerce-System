
using ECommerceSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.1.3
    [TestFixture()]
    internal class purchasePolicyTests : StoreOwnerTests
    {
        private Guid _iphoneFirstGroupProductsID;
        Guid productID1;
        Guid productID2;
        Guid productID3;
        Guid productID4;
        Guid productID5;
        Guid productID6;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _bridge.login(_ownerUserName, _pswd);

            _bridge.openStore("store1");
            productID1= _bridge.addProductInv("store1", "d", "product1", 100, 100, Category.ART, new List<string> { "art" }, -1, -1, "");
            productID2= _bridge.addProductInv("store1", "d", "product2", 50, 100, Category.AUTOMOTIVE, new List<string> { "auto", "prod2" }, -1, -1, "");
            productID3=_bridge.addProductInv("store1", "d", "product3", 10, 100, Category.BABIES, new List<string> { "baby", "prod3" }, -1, -1, "");

            _bridge.openStore("store2");
            productID4= _bridge.addProductInv("store2", "d", "product4", 20, 100, Category.CELLPHONES, new List<string>(), -1, -1, "");
            productID5= _bridge.addProductInv("store2", "d", "product5", 100, 100, Category.BABIES, new List<string> { "baby" }, -1, -1, "");
            productID6= _bridge.addProductInv("store2", "d", "product2", 40, 100, Category.AUTOMOTIVE, new List<string> { "prod2" }, -1, -1, "");

            _bridge.logout();
        }
    }
}
