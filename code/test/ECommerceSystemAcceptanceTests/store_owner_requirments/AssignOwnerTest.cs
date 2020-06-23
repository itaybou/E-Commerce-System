using ECommerceSystem.DataAccessLayer;
using NUnit.Framework;
using System;
using System.Linq;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.3
    [TestFixture()]
    internal class AssignOwnerTest : StoreOwnerTests
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
        public void assignOwnerSuccess()
        {
            //not allowed to assign meneger
            _bridge.login(_newOwner1, _pswd);
            Assert.IsFalse(_bridge.assignManager(_newManager, _storeName));
            Assert.IsFalse(_bridge.removeManager(_newManager, _storeName));
            _bridge.logout();

            //assign _newOwner1 to be owner
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreNotEqual(Guid.Empty, _bridge.createOwnerAssignAgreement(_newOwner1, _storeName), "fail to assign owner");
            _bridge.logout();

            //try to use owner permissions
            _bridge.login(_newOwner1, _pswd);
            Assert.IsTrue(_bridge.assignManager(_newManager, _storeName));
            Assert.IsTrue(_bridge.removeManager(_newManager, _storeName));
            _bridge.logout();

            //remove owner _newOwner1
            _bridge.login(_ownerUserName, _pswd);
            Assert.IsTrue(_bridge.removeOwner(_newOwner1, _storeName));
            _bridge.logout();

            

        }

        [TestCase()]
        public void assignOwnerWithApprovals()
        {
            //assign _newOwner1 to be owner
            _bridge.login(_ownerUserName, _pswd);
            Guid agreementID = _bridge.createOwnerAssignAgreement(_newOwner1, _storeName);
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail to assign owner");

            //open agreement for assigning _newOwner2 to be owner
            agreementID = _bridge.createOwnerAssignAgreement(_newOwner2, _storeName);
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail open agreement");
            _bridge.logout();

            //_newOwner1 send approval to assign _newOwner2 to be owner
            _bridge.login(_newOwner1, _pswd);
            Assert.IsTrue(_bridge.approveAssignOwnerRequest(agreementID, _storeName));
            _bridge.logout();

            //_newOwner2 should be owner now
            //_newOwner2 try to use owner permissions
            _bridge.login(_newOwner2, _pswd);
            Assert.IsTrue(_bridge.assignManager(_newManager, _storeName));
            Assert.IsTrue(_bridge.removeManager(_newManager, _storeName));
            _bridge.logout();

            //remove owners _newOwner1 and _newOwner2
            _bridge.login(_ownerUserName, _pswd);
            Assert.IsTrue(_bridge.removeOwner(_newOwner2, _storeName));
            Assert.IsTrue(_bridge.removeOwner(_newOwner1, _storeName));
            _bridge.logout();

        }

        [TestCase()]
        public void assignOwnerWithDisapproval()
        {
            //assign _newOwner1 to be owner
            _bridge.login(_ownerUserName, _pswd);
            Guid agreementID = _bridge.createOwnerAssignAgreement(_newOwner1, _storeName);
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail to assign owner");

            //open agreement for assigning _newOwner2 to be owner
            agreementID = _bridge.createOwnerAssignAgreement(_newOwner2, _storeName);
            Assert.AreNotEqual(Guid.Empty, agreementID, "fail open agreement");
            _bridge.logout();

            //_newOwner1 send approval to assign _newOwner2 to be owner
            _bridge.login(_newOwner1, _pswd);
            Assert.IsTrue(_bridge.disApproveAssignOwnerRequest(agreementID, _storeName));
            _bridge.logout();

            //_newOwner2 should be owner now
            //_newOwner2 try to use owner permissions and eccept to not succed
            _bridge.login(_newOwner2, _pswd);
            Assert.IsFalse(_bridge.assignManager(_newManager, _storeName));
            _bridge.logout();

            //remove owners _newOwner1
            _bridge.login(_ownerUserName, _pswd);
            Assert.IsTrue(_bridge.removeOwner(_newOwner1, _storeName));
            _bridge.removeOwner(_newOwner2, _storeName);
            _bridge.logout();

        }
    }
}