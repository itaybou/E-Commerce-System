using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{

    // Requirment 3.2
    [TestFixture()]
    class OpenStoreTest : StoreOwnerTests
    {
        string _newStoreName;
        string _discountPolicy;
        string _purchasePolicy;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _newStoreName = "new store name";
            _discountPolicy = "discount policy";
            _purchasePolicy = "purchase policy";
        }

        [OneTimeTearDown]
        public new void tearDown()
        {
            _bridge.storesCleanUp();
            _bridge.usersCleanUp();
        }

        [TestCase()]
        public void openStoreSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.openStore(_newStoreName, _discountPolicy, _purchasePolicy), "fail to open store");
            _bridge.logout();
        }


        [TestCase()]
        public void openStoreFail()
        {
            Assert.False(_bridge.openStore(_newStoreName, _discountPolicy, _purchasePolicy), "open store as a guest successed");
            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.openStore(_storeName, _discountPolicy, _purchasePolicy), "open store with exist store name successed");
            _bridge.logout();
        }

    }
}
