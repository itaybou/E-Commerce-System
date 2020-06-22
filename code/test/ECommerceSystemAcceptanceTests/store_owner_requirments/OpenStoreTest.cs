using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 3.2
    [TestFixture()]
    internal class OpenStoreTest : StoreOwnerTests
    {
        private string _newStoreName;
        private string _discountPolicy;
        private string _purchasePolicy;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _newStoreName = "new store name";
            _discountPolicy = "discount policy";
            _purchasePolicy = "purchase policy";
        }

        [TestCase()]
        public void openStoreSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.openStore(_newStoreName), "fail to open store");
            _bridge.logout();
        }

        [TestCase()]
        public void openStoreFail()
        {
            Assert.False(_bridge.openStore(_newStoreName), "open store as a guest successed");
            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.openStore(_storeName), "open store with exist store name successed");
            _bridge.logout();
        }
    }
}