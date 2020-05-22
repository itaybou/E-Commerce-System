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
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;

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


        //**purchase policy**


        List<string> _bannedLocationsIran;
        List<string> _bannedLocationsIraq;
        List<string> _bannedLocationsTurkey;
        List<string> _bannedLocationsEgypt;
        List<string> _bannedLocationsLebanon;

        Guid _banIranPolicyID;
        Guid _banIraqPolicyID;
        Guid _banEgyptPolicyID;
        Guid _banTurkeyPolicyID;
        Guid _banLebanonPolicyID;
        Guid _minPricePerStorePolicyID;

        double _requireTotalPrice;
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


            //**purchase policy**
            _requireTotalPrice = 200;
            _bannedLocationsIran = new List<string>() { "iran" };
            _bannedLocationsIraq = new List<string>() { "iraq" };
            _bannedLocationsTurkey = new List<string>() { "turkey" };
            _bannedLocationsEgypt = new List<string>() { "egypt" };
            _bannedLocationsLebanon = new List<string>() { "lebanon" };

            _banIranPolicyID = _store.addLocationPolicy(_bannedLocationsIran);
            _banTurkeyPolicyID = _store.addLocationPolicy(_bannedLocationsTurkey);
            _banIraqPolicyID = _store.addLocationPolicy(_bannedLocationsIraq);
            _banEgyptPolicyID = _store.addLocationPolicy(_bannedLocationsEgypt);
            _banLebanonPolicyID = _store.addLocationPolicy(_bannedLocationsLebanon);
            _minPricePerStorePolicyID = _store.addMinPriceStorePolicy(_requireTotalPrice);

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


        // *************PURCHASE POLICY TESTS********* //
        [Test()]
        public void addCompositePurchasePolicyTest()
        {
            //try to add with not exist policy

            Assert.AreEqual(Guid.Empty, _store.addAndPurchasePolicy(_banIranPolicyID, Guid.NewGuid()));

            //add first xor
            Guid xorPolicy1 = _store.addXorPurchasePolicy(_banIranPolicyID, _banIraqPolicyID);

            //check that the simple policies exist
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIranPolicyID));
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIraqPolicyID));

            Assert.IsTrue(_store.canBuy(null, _requireTotalPrice + 1, "iran"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice + 1, "iran iraq"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice - 1, "iran"));



            //add second xor

            Guid xorPolicy2 = _store.addXorPurchasePolicy(_banIranPolicyID, _banEgyptPolicyID);
            //check that the simple policies exist
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIraqPolicyID));
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banEgyptPolicyID));

            Assert.IsTrue(_store.canBuy(null, _requireTotalPrice + 1, "iran"));
            Assert.IsTrue(_store.canBuy(null, _requireTotalPrice + 1, "iraq egypt"));

            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice - 1, "iran"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice + 1, ""));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice + 1, "iran egypt"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice + 1, "iran iraq"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice + 1, "iraq"));
        }


        [Test()]
        public void removePolicyTest()
        {

            //remove first level simple policy
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIranPolicyID));
            _store.removePurchasePolicy(_banIranPolicyID);
            Assert.IsNull(_store.PurchasePolicy.getByID(_banIranPolicyID));


            //remove composite from first level
            Guid andPolicy = _store.addOrPurchasePolicy(_banEgyptPolicyID, _banIraqPolicyID);
            _store.removePurchasePolicy(_banIraqPolicyID);
            Assert.IsNull(_store.PurchasePolicy.getByID(_banIraqPolicyID));
            _store.removePurchasePolicy(andPolicy);
            Assert.IsNull(_store.PurchasePolicy.getByID(andPolicy));
            Assert.IsNotNull(_banEgyptPolicyID);

        }
        // *************DISCOUNT TESTS**************** //
    }
}