using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.7
    [TestFixture()]
    class RemoveManagerTest : StoreOwnerTests
    {
        string _otherManager;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _bridge.register(_otherManager, _pswd, _fname, _lname, _email);
            _bridge.login(_ownerUserName, _pswd);
            _bridge.assignManager(_otherManager, _storeName);
            _bridge.logout();
        }


        [TestCase()]
        public void removeManagerSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.removeManager(_otherManager, _storeName), "fail to remove manager");
            _bridge.assignManager(_otherManager, _storeName); // re assign in case of success remove
            _bridge.logout();
        }

        [TestCase()]
        public void removeManagerFail()
        {

            Assert.False(_bridge.removeManager(_otherManager, _storeName), "remove manager as a guest successed");

            _bridge.login(_userName, _pswd);
            Assert.False(_bridge.removeManager(_otherManager, _storeName), "remove manager as a regular user successed");
            _bridge.logout();

            _bridge.login(_managerUserName, _pswd);
            Assert.False(_bridge.removeManager(_otherManager, _storeName), "remove manager as a manager successed");
            _bridge.logout();

            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.removeManager("not exist", _storeName), "remove manager of not exist user name successed");
            Assert.False(_bridge.removeManager(_otherManager, "not exist"), "remove manager of not exist store successed");

            string otherOwmer = "other owner";
            _bridge.register(otherOwmer, _pswd, _fname, _lname, _email);
            Assert.False(_bridge.removeManager(otherOwmer, _storeName), "remove manager successed on owner");
            _bridge.logout();

            _bridge.login(otherOwmer, _pswd);
            Assert.False(_bridge.removeManager(_otherManager, _storeName), "remove manager by not the assignee owner successed");

            _bridge.logout();

        }

    }
}
