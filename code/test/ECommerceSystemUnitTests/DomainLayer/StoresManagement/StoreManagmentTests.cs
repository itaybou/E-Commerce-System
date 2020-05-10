
using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystemUnitTests.DomainLayer.StoresManagement
{
    [TestFixture()]
    public class StoreManagmentTests
    {
        string _productName = "Iphone", _description = "description";
        PurchaseType _purchaseType = new ImmediatePurchase();
        double _price = 100;
        int _quantity = 5;
        Category _category = Category.CELLPHONES;
        List<string> _keywords = new List<string>();
        Guid _productID = Guid.NewGuid();
        Guid _productInvID = Guid.NewGuid();

        SystemManager _systemManagement;

        StoreManagement _storeManagement;
        UsersManagement _userManagement;
        User _owner;
        User _regularUser;
        User _nonPermitManager;
        User _permitManager;
        Store _store;

        User _anotherOwner;
        User _newManager;

        Guid _regularUserGUID;
        Guid _permitManagerGUID;
        Guid _nonPermitManagerGUID;
        Guid _ownerGUID;
        Guid _anotherOwnerGUID;
        Guid _newManagerGUID;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            // owner - owner of the store
            // nonPermitManager - manager with the default permissions
            // permitManager - manager with the default permissions, add, delete and modify productInv a
            // regularUser - not owner/manager of the store
            _regularUser = new User(new Subscribed("regularUser", "pA55word", "fname", "lname", "owner@gmail.com"));
            _permitManager = new User(new Subscribed("permitManager", "pA55word", "fname", "lname", "owner@gmail.com"));
            _nonPermitManager = new User(new Subscribed("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com"));
            _owner = new User(new Subscribed("owner", "pA55word", "fname", "lname", "owner@gmail.com"));
            _anotherOwner = new User(new Subscribed("anotherOwner", "pA55word", "fname", "lname", "email@gmail.com"));
            _newManager = new User(new Subscribed("newManager", "pA55word", "fname", "lname", "email@gmail.com"));



            _storeManagement = StoreManagement.Instance;
            _userManagement = UsersManagement.Instance;
            _systemManagement = SystemManager.Instance;
            _userManagement.register("owner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("permitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("regularUser", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("newManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("anotherOwner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.login("owner", "pA55word");
            
            _regularUserGUID = _userManagement.getUserByName("regularUser").Guid;
            _permitManagerGUID = _userManagement.getUserByName("permitManager").Guid;
            _nonPermitManagerGUID = _userManagement.getUserByName("nonPermitManager").Guid;
            _ownerGUID = _userManagement.getUserByName("owner").Guid;
            _anotherOwnerGUID = _userManagement.getUserByName("anotherOwner").Guid;
            _newManagerGUID = _userManagement.getUserByName("newManager").Guid;
            // make the managers permissions


            _keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            _userManagement.login("owner", "pA55word");
            _storeManagement.openStore(_ownerGUID ,"store");
            _store = _storeManagement.getStoreByName("store");
        }

        [TearDown]
        public void tearDown()
        {
            //_store.Inventory.Products.Clear();
            _storeManagement.removeManager(_newManagerGUID, "newManager","store");
            _storeManagement.removeManager(_nonPermitManagerGUID, "nonPermitManager", "store");
            _storeManagement.removeManager(_permitManagerGUID, "permitManager", "store");
        }

        [Test()]
        public void assignManagerByUnPermitedUserTest()
        {
            _userManagement.login("regularUser", "pA55word");
            Assert.False(_storeManagement.assignManager(_regularUserGUID, "newManager", "store"), "Assign regular user as manager by another regular user successed");
            _userManagement.logout(_regularUser.Guid);
            _userManagement.login("nonPermitManager", "pA55word");
            Assert.False(_storeManagement.assignManager(_nonPermitManagerGUID, "newManager", "store"), "Assign regular user as manager by manager with default permissions successed");
            _userManagement.logout(_nonPermitManager.Guid);
            _userManagement.login("permitManager", "pA55word");
            Assert.False(_storeManagement.assignManager(_permitManagerGUID, "newManager", "store"), "Assign regular user as manager by manager with full permissions successed");
            _userManagement.logout(_permitManager.Guid);



        }

        [Test()]
        public void assignManagerByPermitedUserTest()
        {
            
            Assert.True(_storeManagement.assignManager(_ownerGUID, "newManager", "store"), "Fail to assign regular user as new manager");
            Assert.False(_storeManagement.assignManager(_ownerGUID, "newManager","store"), "Assign already manager user as new manager successed");
            _userManagement.logout(_ownerGUID);
           
        }

        [Test()]
        public void assignOwnerByUnPermitedUserTest()
        {
            _userManagement.login("regularUser", "pA55word");
            Assert.False(_storeManagement.assignOwner(_regularUserGUID, "anotherOwner", "store"), "Assign regular user as owner by another regular user successed");
            _userManagement.logout(_regularUser.Guid);
            _userManagement.login("nonPermitManager", "pA55word");
            Assert.False(_storeManagement.assignOwner(_nonPermitManagerGUID, "anotherOwner", "store"), "Assign regular user as owner by manager with default permissions successed");
            _userManagement.logout(_nonPermitManager.Guid);
            _userManagement.login("permitManager", "pA55word");
            Assert.False(_storeManagement.assignOwner(_permitManagerGUID, "anotherOwner", "store"), "Assign regular user as owner by manager with full permissions successed");
            _userManagement.logout(_permitManager.Guid);



        }

        [Test()]
        public void assignOwnerByPermitedUserTest()
        {

            Assert.True(_storeManagement.assignOwner(_ownerGUID, "anotherOwner", "store"), "Fail to assign regular user as new owner");
            Assert.False(_storeManagement.assignManager(_ownerGUID, "anotherOwner", "store"), "Assign already owner user as new manager successed");
            _userManagement.logout(_ownerGUID);
            
        }

        //[Test()]
        //public void purchaseHistoryTest()
        //{
        //    StorePurchase purchase = new StorePurchase(_regularUser, 80.0, new List<Product>() { new Product(_productName, _description, _discount, _purchaseType, _quantity, _price, _productID) });
        //    _store.PurchaseHistory.Add(purchase);

        //    List<StorePurchase> expected = new List<StorePurchase>();
        //    expected.Add(purchase);

            //succcess:
            //Assert.AreEqual(expected, _storeManagement.purchaseHistory("store"), "fail to view store history");
            //Assert.AreEqual(expected, _storeManagement.purchaseHistory("permitManager"), "fail to view store history");

            //User admin = new User(new SystemAdmin("admin", "4dMinnn", "fname", "lname", "email"));
            //Assert.AreEqual(expected, _storeManagement.purchaseHistory("admin"), "fail to view store history");

            ////fail:

            //Assert.Null(_storeManagement.purchaseHistory("regularUser"), "view history of a store successed with regular user");
            
            //Assert.Null(_storeManagement.purchaseHistory("nonExsist"), "view history of a store successed with non exist user");
        //}





    }
}
