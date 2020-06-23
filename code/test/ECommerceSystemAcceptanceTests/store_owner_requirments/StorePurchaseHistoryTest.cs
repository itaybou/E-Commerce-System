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
    }
}