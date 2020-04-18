using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.3
    [TestFixture()]
    class AssignOwnerTest : StoreOwnerTests
    {
        string _newOwner;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _newOwner = "new owner";
            _bridge.register(_newOwner, _pswd, _fname, _lname, _email);
        }

        [TestCase()]
        public void assignOwnerSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.assignOwner(_newOwner, _storeName), "fail to assign owner");
            _bridge.logout();
        }

        [TestCase()]
        public void assignOwnerFail()
        {
            Assert.False(_bridge.assignOwner(_newOwner, _storeName), "assign owner as a guest successed");

            _bridge.login(_userName, _pswd);
            Assert.False(_bridge.assignOwner(_newOwner, _storeName), "assign owner as a regular user successed");
            _bridge.logout();

            _bridge.login(_managerUserName, _pswd);
            Assert.False(_bridge.assignOwner(_newOwner, _storeName), "assign owner as a manager successed");
            _bridge.logout();

            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.assignOwner("not exist", _storeName), "assign not exist user to owner successed");
            Assert.False(_bridge.assignOwner(_newOwner, "not exist"), "assign owner for not exist store successed");
            _bridge.assignOwner(_newOwner, _storeName);
            Assert.False(_bridge.assignOwner(_newOwner, _storeName), "assign already store owner for owner successed");
            _bridge.logout();
        }
    }
}
