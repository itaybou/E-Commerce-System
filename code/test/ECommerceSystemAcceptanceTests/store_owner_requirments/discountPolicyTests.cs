
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.1.3
    [TestFixture()]
    internal class discountPolicyTests : StoreOwnerTests
    {
        
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
            productID1 = _bridge.addProductInv("store1", "d", "product1", 100, 100, Category.ART, new List<string> { "art" }, -1, -1, "");
            productID2 = _bridge.addProductInv("store1", "d", "product2", 50, 100, Category.AUTOMOTIVE, new List<string> { "auto", "prod2" }, -1, -1, "");
            productID3 = _bridge.addProductInv("store1", "d", "product3", 10, 100, Category.BABIES, new List<string> { "baby", "prod3" }, -1, -1, "");

            _bridge.openStore("store2");
            productID4 = _bridge.addProductInv("store2", "d", "product4", 20, 100, Category.CELLPHONES, new List<string>(), -1, -1, "");
            productID5 = _bridge.addProductInv("store2", "d", "product5", 100, 100, Category.BABIES, new List<string> { "baby" }, -1, -1, "");
            productID6 = _bridge.addProductInv("store2", "d", "product2", 40, 100, Category.AUTOMOTIVE, new List<string> { "prod2" }, -1, -1, "");

            _bridge.logout();
        }

        [TestCase()]
        public void addDiscountPolicy()
        {
            _bridge.login(_ownerUserName, _pswd);
            Guid visibalefID = _bridge.addVisibleDiscount("store1",productID1,10, DateTime.Today.AddDays(10));
            List<DiscountPolicyModel> discounts = _bridge.getAllDiscountsForCompose("store1");
            Assert.AreEqual(1, discounts.Count);
            Assert.AreEqual(visibalefID, discounts.ElementAt(0).ID);

            Guid storeDiscID = _bridge.addConditionalStoreDiscount("store1", 20, DateTime.Today.AddDays(10), 100);
            discounts = _bridge.getAllStoreLevelDiscounts("store1");
            Assert.AreEqual(1, discounts.Count);
            Assert.AreEqual(storeDiscID, discounts.ElementAt(0).ID);

            Guid prodQuantityDiscID = _bridge.addCondiotionalProcuctDiscount("store1", productID2, 20, DateTime.Today.AddDays(10), 2);
            discounts = _bridge.getAllDiscountsForCompose("store1");
            Assert.AreEqual(2, discounts.Count);
            Assert.AreEqual(prodQuantityDiscID, discounts.ElementAt(1).ID);

            Guid andDiscID = _bridge.addAndDiscountPolicy("store1",new List<Guid> { visibalefID , prodQuantityDiscID });
            discounts = _bridge.getAllDiscountsForCompose("store1");

            Assert.AreEqual(andDiscID, discounts.Select(a => a.ID = andDiscID).ElementAt(0));

            _bridge.logout();

                   

        }

        [TestCase()]
        public void removeDiscountPolicy()
        {

            _bridge.login(_ownerUserName, _pswd);
            Guid visibalefID = _bridge.addVisibleDiscount("store2", productID3, 10, DateTime.Today.AddDays(10));
            Assert.AreNotEqual(Guid.Empty, visibalefID);
            Guid visibalefID2 = _bridge.addVisibleDiscount("store2", productID4, 10, DateTime.Today.AddDays(10));
            Assert.AreNotEqual(Guid.Empty, visibalefID2);

            List<DiscountPolicyModel> discounts = _bridge.getAllDiscountsForCompose("store2");
            Guid storeDiscID = _bridge.addConditionalStoreDiscount("store2", 20, DateTime.Today.AddDays(10), 100);
            Assert.AreNotEqual(Guid.Empty, storeDiscID);

            Guid prodQuantityDiscID = _bridge.addCondiotionalProcuctDiscount("store2", productID5, 20, DateTime.Today.AddDays(10), 2);
            Assert.AreNotEqual(Guid.Empty, prodQuantityDiscID);

            Guid andDiscID = _bridge.addAndDiscountPolicy("store2", new List<Guid> { visibalefID, prodQuantityDiscID });
            Assert.AreNotEqual(Guid.Empty, andDiscID);


            Assert.IsTrue(_bridge.removeStoreLevelDiscount("store2", storeDiscID));

            discounts = _bridge.getAllStoreLevelDiscounts("store2");
            Assert.AreEqual(0, discounts.Count);

            Assert.IsTrue(_bridge.removeCompositeDiscount("store2", andDiscID));
            Assert.IsTrue(_bridge.removeProductDiscount("store2", visibalefID2, productID4));



            _bridge.logout();

        }

    }
}
