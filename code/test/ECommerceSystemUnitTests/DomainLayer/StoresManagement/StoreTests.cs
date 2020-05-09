using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Tests
{
    [TestFixture()]
    public class StoreTests
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


        UsersManagement _userManagement;
        User _owner;
        User _regularUser;
        User _nonPermitManager;
        User _permitManager;
        Store _store;

        User _anotherOwner;
        User _newManager;

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



            _userManagement = UsersManagement.Instance;
            _systemManagement = SystemManager.Instance; 
            _userManagement.register("owner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("permitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("regularUser", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.login("owner", "pA55word");

            // make the managers permissions


            _keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            _store = new Store( "owner", "store");
            _store.Inventory.addProductInv(_productName, _description,  _price, _quantity, _category, _keywords);


            _store.assignOwner(_owner, "anotherOwner");
            //_store.assignManager(_owner, "newManager");
            _store.assignManager(_owner, "nonPermitManager");

            //give permit manager the permissions
            _store.assignManager(_owner, "permitManager");
            List<PermissionType> permissions = new List<PermissionType>();
            permissions.Add(PermissionType.AddProductInv);
            permissions.Add(PermissionType.DeleteProductInv);
            permissions.Add(PermissionType.WatchPurchaseHistory);
            permissions.Add(PermissionType.ModifyProduct);
            _store.editPermissions("permitManager", permissions, "owner");


        }

        [TearDown]
        public void tearDown()
        {
            _store.Inventory.Products.Clear();
            _store.removeManager(_owner, "newManager");
            _store.removeManager(_owner, "nonPermitManager");
            _store.removeManager(_owner, "permitManager");
        }


       

        

        [Test()]
        public void removeManagerTest()
        {
            _store.assignManager(_owner, _newManager.Name());

            Assert.False(_store.removeManager(_permitManager, "newManager"), "Remove manager by another manager successed");
            Assert.False(_store.removeManager(_regularUser, "newManager"), "Remove manager by regular user successed");
            Assert.False(_store.removeManager(_anotherOwner, "newManager"), "Remove manager by owner who isn`t his asignee successed");

            Assert.True(_store.removeManager(_owner, "newManager"), "Fail to remove manager");
            Assert.Null(_store.getPermissionByName("newManager"), "Remove manager successed, but the manager still have permissions"); 
        }

        [Test()]
        public void editPermissionsTest()
        {
            _store.assignManager(_owner, _newManager.Name());

            List<PermissionType> emptyPermissions = new List<PermissionType>();

            List<PermissionType> fewPermissions = new List<PermissionType>();
            fewPermissions.Add(PermissionType.AddProductInv);
            fewPermissions.Add(PermissionType.DeleteProductInv);
            fewPermissions.Add(PermissionType.WatchAndComment);


            Assert.False(_store.editPermissions(_newManager.Name(), emptyPermissions, _regularUser.Name()), "Edit permiossions for manager by regular successed");
            Assert.False(_store.editPermissions(_newManager.Name(), emptyPermissions, _permitManager.Name()), "Edit permiossions for manager by another manager successed");
            Assert.False(_store.editPermissions(_newManager.Name(), emptyPermissions, _anotherOwner.Name()), "Edit permiossions for manager by owner who isn`t his asignee successed");

            Assert.True(_store.editPermissions(_newManager.Name(), emptyPermissions, _owner.Name()), "Fail to edit permissions to empty permissions list");
            //check that newManager dont have permissions:
            Assert.False(_store.getPermissionByName(_newManager.Name()).canAddProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to add product");
            Assert.False(_store.getPermissionByName(_newManager.Name()).canDeleteProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to delete product");
            Assert.False(_store.getPermissionByName(_newManager.Name()).canModifyProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to modify product");
            Assert.False(_store.getPermissionByName(_newManager.Name()).canWatchAndomment(), "Permissions edited to empty permissions list successed but the manager still have permission to watch and comment");
            Assert.False(_store.getPermissionByName(_newManager.Name()).canWatchPurchaseHistory(), "Permissions edited to empty permissions list successed but the manager still have permission to watch history");

            Assert.True(_store.editPermissions(_newManager.Name(), fewPermissions, _owner.Name()), "Fail to edit permissions to empty permissions list");
            //check that newManager dont have permissions:
            Assert.True(_store.getPermissionByName(_newManager.Name()).canAddProduct(), "Permissions edited to esuccessed but the manager dont have permission to add product");
            Assert.True(_store.getPermissionByName(_newManager.Name()).canDeleteProduct(), "Permissions edited to esuccessed but the manager dont have permission to delete product");
            Assert.False(_store.getPermissionByName(_newManager.Name()).canModifyProduct(), "Permissions edited to successed but the manager have permission to modify product");
            Assert.True(_store.getPermissionByName(_newManager.Name()).canWatchAndomment(), "Permissions edited to esuccessed but the manager dont have permission to watch and comment");
            Assert.False(_store.getPermissionByName(_newManager.Name()).canWatchPurchaseHistory(), "Permissions edited to successed but the manager have permission to watch history");

        }

       


        [Test()]
        public void rateStoreTest()
        {
            _store.rateStore(6.0);
            Assert.AreEqual(5.0, _store.Rating);
            _store.rateStore(2.0);
            Assert.AreEqual(3.5, _store.Rating);
        }

        //[Test()]
        //public void logPurchaseTest()
        //{
        //    List<Product> products = _store.Inventory.Products.ElementAt(0).ProductList; // = {"Iphone"}
        //    _store.logPurchase(new StorePurchaseModel(_regularUser.Name(), 1000, products);
        //    Assert.True(true);
        //}
    }
}