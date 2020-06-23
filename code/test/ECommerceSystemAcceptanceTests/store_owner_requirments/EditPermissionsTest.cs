using ECommerceSystem.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.6
    [TestFixture()]
    internal class EditPermissionsTest : StoreOwnerTests
    {
        private List<PermissionType> _permissions;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _permissions = new List<PermissionType>();
            _permissions.Add(PermissionType.AddProductInv);
            _permissions.Add(PermissionType.DeleteProductInv);
        }

        [TestCase()]
        public void editPermissionsSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.editPermissions(_storeName, _managerUserName, _permissions), "fail to edit permissions");
            _bridge.logout();
        }

        [TestCase()]
        public void editPermissionsFail()
        {
            Assert.False(_bridge.editPermissions(_storeName, _managerUserName, _permissions), "edit permissions as a guest successed");

            _bridge.login(_userName, _pswd);
            Assert.False(_bridge.editPermissions(_storeName, _managerUserName, _permissions), "edit permissions as a regular user successed");
            _bridge.logout();

            _bridge.login(_managerUserName, _pswd);
            Assert.False(_bridge.editPermissions(_storeName, _managerUserName, _permissions), "edit permissions as a manager successed");
            _bridge.logout();

            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.editPermissions("not exist", _managerUserName, _permissions), "edit permissions of not exist store successed");
            Assert.False(_bridge.editPermissions(_storeName, "not exist", _permissions), "edit permissions to not exist user successed");
            Assert.False(_bridge.editPermissions(_storeName, _userName, _permissions), "edit permissions to regular user(not manager) successed");

            string otherOwmer = "other owner";
            _bridge.register(otherOwmer, _pswd, _fname, _lname, _email);
            Assert.False(_bridge.editPermissions(_storeName, otherOwmer, _permissions), "edit permissions to owner successed");
            _bridge.logout();

            _bridge.login(otherOwmer, _pswd);
            Assert.False(_bridge.editPermissions(_storeName, _managerUserName, _permissions), "edit permissions not the assignee successed");
            _bridge.logout();
        }
    }
}