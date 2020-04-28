
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement.Tests
{
    [TestFixture()]
    public class PermissionsTests
    {


        string _productName = "Iphone", _description = "description";
        Discount _discount = new VisibleDiscount(10, new DiscountPolicy());
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
        User _guest;
        Store _store;
        Permissions _permissions;
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

            _userManagement.login("owner", "pA55word");
            _storeManagement.openStore("store", new DiscountPolicy(), new PurchasePolicy());
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
           
            _store.Inventory.addProductInv(_productName, _description, _discount, _purchaseType, _price, _quantity, _category, _keywords);
            




        }

        [TearDown]
        public void tearDown()
        {
            _storeManagement.getStoreByName("store").Inventory.Products.Clear();
            _storeManagement.removeManager( "newManager","store");
            _storeManagement.removeManager("permitManager", "store");
            _storeManagement.removeManager("nonPermitManager", "store");
            _storeManagement.removeManager("anotherOwner", "store");
            
        }

        [Test()]
        public void addProductInvByPermitedUserTest()
        {

            Assert.AreNotEqual(Guid.Empty, _permissions.addProductInv("owner", "galaxy",
                _description, _discount, _purchaseType, _price, _quantity,
                _category, _keywords), "fail to add productinv ");


        }

        [Test()]
        public void addProductInvByPermitedUserWithProductNameExist()
        {

            Assert.AreNotEqual(Guid.Empty, _permissions.addProductInv("owner", "galaxy",
                _description, _discount, _purchaseType, _price, _quantity,
                _category, _keywords), "fail to add productinv ");


        }

        [Test()]
        public void addProductByPermitedUserTest()
        {
            Assert.AreNotEqual(Guid.Empty, _permissions.addProduct("owner", _productName, _discount, _purchaseType, 20),
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
        public void deleteProductTestByPermitedUserTest()
        {
            
            Guid guid1 = _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
            Assert.True(_permissions.deleteProduct("permitManager", _productName, guid1),
                    "Fail to delete group of products ");

        }

        [Test()]
        public void modifyProductPriceTestByPermitedUserTest()
        {
            Assert.True(_permissions.modifyProductPrice("permitManager", _productName, 200),
                    "Fail to modify price of product inventory by permited manager");
        }

        [Test()]
        public void modifyProductDiscountTypeByPermitedUserTest()
        {
            Discount newDis = new VisibleDiscount(20, new DiscountPolicy());

           
            Guid guid1 = _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
            Assert.True(_permissions.modifyProductDiscountType("permitManager", _productName, guid1, newDis),
                    "Fail to modify discount of group of products ");

        }

        [Test()]
        public void modifyProductPurchaseTypeByPermitedUserTest()
        {
            PurchaseType newPurchaseType = new ImmediatePurchase();

           
            Guid guid1 = _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
            Assert.True(_permissions.modifyProductPurchaseType("permitManager", _productName, guid1, newPurchaseType),
                    "Fail to modify purchase type of group of products ");

        }

        [Test()]
        public void modifyProductQuantityByPermitedUserTest()
        {
           
            Guid guid1 = _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
            Assert.True(_permissions.modifyProductQuantity("permitManager", _productName, guid1, 20),
                    "Fail to modify quantity of group of products ");
        }

        [Test()]
        public void assighOwnerdefaltPermissionsTest()
        {
            
            _userManagement.login("owner", "pA55word");
            Assert.NotNull(_storeManagement.assignOwner("anotherOwner", "store"), "Fail to assign regular user as new owner");
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
        public void assighManagerdefaltPermissionsTest()
        {
            _userManagement.login("owner", "pA55word");
            Assert.NotNull(_storeManagement.assignManager("newManager", "store"), "Fail to assign regular user as new owner");
            Assert.AreEqual(_userManagement.getUserByName("newManager").getPermission("store").AssignedBy,_owner, "The user who assign the reg user as manager isn`t the assignee");
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


