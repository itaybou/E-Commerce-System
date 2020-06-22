using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.5
    [TestFixture()]
    internal class AssignManagerTest : StoreOwnerTests
    {
        private string _newManager;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _newManager = "new manager";
            _bridge.register(_newManager, _pswd, _fname, _lname, _email);
        }

        [TestCase()]
        public void assignManagerSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.assignManager(_newManager, _storeName), "fail to assign manager");
            _bridge.logout();
        }

        [TestCase()]
        public void assignManagerFail()
        {
            string otherOwner = "other owner";
            _bridge.register(otherOwner, _pswd, _fname, _lname, _email);

            Assert.False(_bridge.assignManager(_newManager, _storeName), "assign manager as a guest successed");

            _bridge.login(_userName, _pswd);
            Assert.False(_bridge.assignManager(_newManager, _storeName), "assign manager as a regular user successed");
            _bridge.logout();

            _bridge.login(_managerUserName, _pswd);
            Assert.False(_bridge.assignManager(_newManager, _storeName), "assign manager as a manager successed");
            _bridge.logout();

            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.assignManager("not exist", _storeName), "assign not exist user as manager successed");
            Assert.False(_bridge.assignManager(_newManager, "not exist"), "assign manager to not exist store successed");
            Assert.False(_bridge.assignManager(_managerUserName, _storeName), "assign already manager to manager successed");

            _bridge.createOwnerAssignAgreement(otherOwner, _storeName);
            Assert.False(_bridge.assignManager(otherOwner, _storeName), "assign owner to manager successed");
            _bridge.logout();
        }
    }
}