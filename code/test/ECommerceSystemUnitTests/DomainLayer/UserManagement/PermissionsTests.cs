﻿using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement.Tests
{
    [TestFixture()]
    public class PermissionsTests
    {
        private string _productName = "Iphone", _description = "description";
        private PurchaseType _purchaseType = new ImmediatePurchase();
        private double _price = 100;
        private int _quantity = 5;
        private Category _category = Category.CELLPHONES;
        private List<string> _keywords = new List<string>();
        private Guid _productID = Guid.NewGuid();
        private Guid _productInvID = Guid.NewGuid();

        private SystemManager _systemManagement;
        private StoreManagement _storeManagement;

        private UsersManagement _userManagement;
        private User _owner;
        private User _regularUser;
        private User _nonPermitManager;
        private User _permitManager;
        private User _guest;
        private Store _store;
        private Permissions _permissions;
        private User _anotherOwner;
        private User _newManager;

        private Guid _regularUserGUID;
        private Guid _permitManagerGUID;
        private Guid _nonPermitManagerGUID;
        private Guid _ownerGUID;
        private Guid _anotherOwnerGUID;
        private Guid _newManagerGUID;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            // owner - owner of the store
            // nonPermitManager - manager with the default permissions
            // permitManager - manager with the default permissions, add, delete and modify productInv a
            // regularUser - not owner/manager of the store
            _regularUser = new User(new Subscribed("regularUser", "pA55word", "fname", "lname", "owner@gmail.com"));

            _anotherOwner = new User(new Subscribed("anotherOwner", "pA55word", "fname", "lname", "email@gmail.com"));
            _newManager = new User(new Subscribed("newManager", "pA55word", "fname", "lname", "email@gmail.com"));
            _guest = new User(new Guest());

            _userManagement = UsersManagement.Instance;
            _systemManagement = SystemManager.Instance;
            _storeManagement = StoreManagement.Instance;
            _userManagement.register("owner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("permitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("anotherOwner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("regularUser", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("newManager", "pA55word", "fname", "lname", "owner@gmail.com");

            _regularUserGUID = _regularUser.Guid;
            _permitManagerGUID = _userManagement.getUserByName("permitManager").Guid;
            _nonPermitManagerGUID = _userManagement.getUserByName("nonPermitManager").Guid;
            _ownerGUID = _userManagement.getUserByName("owner").Guid;
            _anotherOwnerGUID = _userManagement.getUserByName("anotherOwner").Guid;
            _newManagerGUID = _userManagement.getUserByName("newManager").Guid;

            _userManagement.login("owner", "pA55word");
            _storeManagement.openStore(_ownerGUID, "store");
            _owner = _userManagement.getUserByName("owner");
            _store = _storeManagement.getStoreByName("store");
            _permissions = _owner.getPermission("store");

            //_storeManagement.assignManager("nonPermitManager", "store");
            //_nonPermitManager = _userManagement.getUserByName("nonPermitManager");

            //_storeManagement.assignManager("permitManager", "store");
            //List<PermissionType> permissions = new List<PermissionType>();
            //permissions.Add(PermissionType.AddProductInv);
            //permissions.Add(PermissionType.DeleteProductInv);
            //permissions.Add(PermissionType.WatchPurchaseHistory);
            //permissions.Add(PermissionType.ModifyProduct);
            //_store.editPermissions("permitManager", permissions, "owner");
            //_permitManager = _userManagement.getUserByName("permitManager");
        }

        [SetUp]
        public void setUp()
        {
            _store.Inventory.addProductInv(_productName, _description, _price, _quantity, _category, _keywords);
        }

        [TearDown]
        public void tearDown()
        {
            _storeManagement.getStoreByName("store").Inventory.Products.Clear();
            _storeManagement.removeManager(_ownerGUID, "newManager", "store");
            _storeManagement.removeManager(_ownerGUID, "permitManager", "store");
            _storeManagement.removeManager(_ownerGUID, "nonPermitManager", "store");
            _storeManagement.removeManager(_ownerGUID, "anotherOwner", "store");
        }

        [Test()]
        public void addProductInvByPermitedUserTest()
        {
            Assert.AreNotEqual(Guid.Empty, _permissions.addProductInv("owner", "galaxy",
                _description, _price, _quantity,
                _category, _keywords, 0, 5), "fail to add productinv ");
        }

        [Test()]
        public void addProductInvByPermitedUserWithProductNameExist()
        {
            Assert.AreNotEqual(Guid.Empty, _permissions.addProductInv("owner", "galaxy",
                _description, _price, _quantity,
                _category, _keywords, 0, 5), "fail to add productinv ");
        }

        [Test()]
        public void addProductByPermitedUserTest()
        {
            Assert.AreNotEqual(Guid.Empty, _permissions.addProduct("owner", _productName, 20, 0, 5),
                   "Fail to add group of products ");
        }

        [Test()]
        public void deleteProductInventoryByPermitedUserTest()
        {
            Assert.True(_permissions.deleteProductInventory("permitManager", _productName),
                    "Fail to delete product inventory ");
        }

        [Test()]
        public void modifyProductNameTestByPermitedUserTest()
        {
            Assert.True(_permissions.modifyProductName("permitManager", "Galaxy", _productName),
                    "Fail to modify name of product inventory ");
        }

        [Test()]
        public void deleteProductTestByPermitedManagerTest()
        {
            
            Guid product = _store.Inventory.addProduct(_productName, _quantity);
            Assert.True(_permissions.deleteProduct("permitManager", _productName, product),
                    "Fail to delete group of products ");
        }

        [Test()]
        public void modifyProductPriceTestByPermitedUserTest()
        {
            Assert.True(_permissions.modifyProductPrice("permitManager", _productName, 200),
                    "Fail to modify price of product inventory by permited manager");
        }

        //[Test()]
        //public void modifyProductDiscountTypeByPermitedUserTest()
        //{
        //    Discount newDis = new VisibleDiscount(20, new DiscountPolicy());

        //    Guid guid1 = _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
        //    Assert.True(_permissions.modifyProductDiscountType("permitManager", _productName, guid1, newDis),
        //            "Fail to modify discount of group of products ");

        //}

        //[Test()]
        //public void modifyProductPurchaseTypeByPermitedUserTest()
        //{
        //    PurchaseType newPurchaseType = new ImmediatePurchase();

        //    Guid guid1 = _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
        //    Assert.True(_permissions.modifyProductPurchaseType("permitManager", _productName, guid1, newPurchaseType),
        //            "Fail to modify purchase type of group of products ");

        //}

        [Test()]
        public void modifyProductQuantityByPermitedUserTest()
        {
            Guid guid1 = _store.Inventory.addProduct(_productName, _quantity);
            Assert.True(_permissions.modifyProductQuantity("permitManager", _productName, guid1, 20),
                    "Fail to modify quantity of group of products ");
        }

        [Test()]
        public void assighOwnerdefultPermissionsTest()
        {
            _userManagement.login("owner", "pA55word");
            Assert.NotNull(_storeManagement.createOwnerAssignAgreement(_ownerGUID, "anotherOwner", "store"), "Fail to assign regular user as new owner");
            Assert.AreEqual(_userManagement.getUserByName("anotherOwner").getPermission("store").AssignedBy, _owner, "The user who assign the reg user as manager isn`t the assignee");
            //check defult permissions:
            _newManager = _userManagement.getUserByName("anotherOwner");
            Assert.True(_newManager.getPermission("store").canWatchAndomment(), "Assign new manager successed, but the manager dont have permission to watch and comment");
            Assert.True(_newManager.getPermission("store").canWatchPurchaseHistory(), "Assign new manager successed, but the manager dont have permission to watch purchase history");
            Assert.True(_newManager.getPermission("store").canAddProduct(), "Assign new manager successed, but the manager have permission to add product");
            Assert.True(_newManager.getPermission("store").canDeleteProduct(), "Assign new manager successed, but the manager have permission to delete product");
            Assert.True(_newManager.getPermission("store").canModifyProduct(), "Assign new manager successed, but the manager have permission to modify product");
        }

        [Test()]
        public void assighManagerdefultPermissionsTest()
        {
            _userManagement.login("owner", "pA55word");
            Assert.NotNull(_storeManagement.assignManager(_ownerGUID, "newManager", "store"), "Fail to assign regular user as new owner");
            Assert.AreEqual(_userManagement.getUserByName("newManager").getPermission("store").AssignedBy, _owner, "The user who assign the reg user as manager isn`t the assignee");
            //check defult permissions:
            _newManager = _userManagement.getUserByName("newManager");
            Assert.True(_newManager.getPermission("store").canWatchAndomment(), "Assign new manager successed, but the manager dont have permission to watch and comment");
            Assert.True(_newManager.getPermission("store").canWatchPurchaseHistory(), "Assign new manager successed, but the manager dont have permission to watch purchase history");
            Assert.False(_newManager.getPermission("store").canAddProduct(), "Assign new manager successed, but the manager have permission to add product");
            Assert.False(_newManager.getPermission("store").canDeleteProduct(), "Assign new manager successed, but the manager have permission to delete product");
            Assert.False(_newManager.getPermission("store").canModifyProduct(), "Assign new manager successed, but the manager have permission to modify product");
        }
    }
}