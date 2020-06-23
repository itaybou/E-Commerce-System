
using ECommerceSystem.DataAccessLayer;
using NUnit.Framework;
using System;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.3
    [TestFixture()]
    internal class removeOwnerTests : StoreOwnerTests
    {
        private string _newOwner1;
        private string _newOwner2;
        private string _newManager;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _newOwner1 = "new owner1";
            _newOwner2 = "new owner2";
            _newManager = "new manager1";

            _bridge.register(_newOwner1, _pswd, _fname, _lname, _email);
            _bridge.register(_newOwner2, _pswd, _fname, _lname, _email);
            _bridge.register(_newManager, _pswd, _fname, _lname, _email);
        }

        //[TearDown]
        //public new void teardown()
        //{
        //    DataAccess.Instance.DropTestDatabase();
        //}


            [TestCase()]
        public void removeOwnerRec()
        {


            //assign _newOwner1 to be owner
            _bridge.login(_ownerUserName, _pswd);
            _bridge.openStore("store2");
            Guid agreementID = _bridge.createOwnerAssignAgreement(_newOwner1, "store2");
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail to assign owner");
            _bridge.logout();

            _bridge.login(_newOwner1, _pswd);
            //open agreement for assigning _newOwner2 to be owner
            agreementID = _bridge.createOwnerAssignAgreement(_newOwner2, "store2");
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail open agreement");
            _bridge.logout();

            //_newOwner1 send approval to assign _newOwner2 to be owner
            _bridge.login(_ownerUserName, _pswd);
            Assert.IsTrue(_bridge.approveAssignOwnerRequest(agreementID, "store2"));
            //now _ownerUserName, _newOwner1,_newOwner2 are owners
            Assert.IsTrue(_bridge.removeOwner(_newOwner1, "store2"));
            _bridge.logout();

            //check permissions _newOwner2
            _bridge.login(_newOwner2, _pswd);
            Assert.IsFalse(_bridge.assignManager(_newManager, "store2"), "_newOwner2 didn't remove");
            Assert.IsFalse(_bridge.removeManager(_newManager, "store2"), "_newOwner2 didn't remove");
            _bridge.logout();

            //check permissions _newOwner1
            _bridge.login(_newOwner1, _pswd);
            Assert.IsFalse(_bridge.assignManager(_newManager, "store2"), "_newOwner1 didn't remove");
            Assert.IsFalse(_bridge.removeManager(_newManager, "store2"), "_newOwner1 didn't remove");
            _bridge.logout();
        }

        [TestCase()]
        public void removeOwnerNotAssignedBy()
        {
            //assign _newOwner1 to be owner
            _bridge.login(_ownerUserName, _pswd);
            Guid agreementID = _bridge.createOwnerAssignAgreement(_newOwner1, _storeName);
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail to assign owner");
            _bridge.logout();

            _bridge.login(_newOwner1, _pswd);
            //open agreement for assigning _newOwner2 to be owner
            agreementID = _bridge.createOwnerAssignAgreement(_newOwner2, _storeName);
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail open agreement");
            _bridge.logout();

            //_newOwner1 send approval to assign _newOwner2 to be owner
            _bridge.login(_ownerUserName, _pswd);
            Assert.IsTrue(_bridge.approveAssignOwnerRequest(agreementID, _storeName));
            //now _ownerUserName, _newOwner1,_newOwner2 are owners
            Assert.IsFalse(_bridge.removeOwner(_newOwner2, _storeName));
            _bridge.logout();

            //check permissions _newOwner2
            _bridge.login(_newOwner2, _pswd);
            Assert.IsTrue(_bridge.assignManager(_newManager, _storeName), "_newOwner2 didn't remove");
            Assert.IsTrue(_bridge.removeManager(_newManager, _storeName), "_newOwner2 didn't remove");
            _bridge.logout();

            //check permissions _newOwner1
            _bridge.login(_newOwner1, _pswd);
            Assert.IsTrue(_bridge.assignManager(_newManager, _storeName), "_newOwner1 didn't remove");
            Assert.IsTrue(_bridge.removeManager(_newManager, _storeName), "_newOwner1 didn't remove");
            _bridge.logout();
        }
    }
}




