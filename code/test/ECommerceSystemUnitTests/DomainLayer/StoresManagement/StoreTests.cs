//using ECommerceSystem.DomainLayer.SystemManagement;
//using ECommerceSystem.DomainLayer.UserManagement;
//using ECommerceSystem.Models;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
//using ECommerceSystem.DomainLayer.StoresManagement.Discount.Tests;

//namespace ECommerceSystem.DomainLayer.StoresManagement.Tests
//{
//    [TestFixture()]
//    public class StoreTests
//    {
//        string _productName = "Iphone", _description = "description";
//        PurchaseType _purchaseType = new ImmediatePurchase();
//        double _price = 10;
//        int _quantity = 5;
//        Category _category = Category.CELLPHONES;
//        List<string> _keywords = new List<string>();
//        Guid _productID = Guid.NewGuid();
//        Guid _productInvID = Guid.NewGuid();

//        private SystemManager _systemManagement;
//        private StoreManagement _storeManagment;
//        private UsersManagement _userManagement;
//        private User _owner;
//        private User _regularUser;
//        private User _nonPermitManager;
//        private User _permitManager;
//        private Store _store;

//        private User _anotherOwner;
//        private User _newManager;


//        //**purchase policy**


//        List<string> _bannedLocationsIran;
//        List<string> _bannedLocationsIraq;
//        List<string> _bannedLocationsTurkey;
//        List<string> _bannedLocationsEgypt;
//        List<string> _bannedLocationsLebanon;

//        Guid _banIranPolicyID;
//        Guid _banIraqPolicyID;
//        Guid _banEgyptPolicyID;
//        Guid _banTurkeyPolicyID;
//        Guid _banLebanonPolicyID;
//        Guid _minPricePerStorePolicyID;

//        double _requireTotalPrice;


//        //**discounts**
//        Guid _productID1;
//        Guid _productID2;
//        Guid _productID3;
//        Guid _productID4;
//        Guid _productID5;
//        Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> _products;

//        int _quantity1;
//        int _quantity2;
//        int _quantity3;
//        int _quantity4;
//        int _quantity5;

//        double _totalPrice1;
//        double _totalPrice2;
//        double _totalPrice3;
//        double _totalPrice4;
//        double _totalPrice5;


//        [OneTimeSetUp]
//        public void setUpFixture()
//        {
//            // owner - owner of the store
//            // nonPermitManager - manager with the default permissions
//            // permitManager - manager with the default permissions, add, delete and modify productInv a
//            // regularUser - not owner/manager of the store

//            ECommerceSystem.DataAccessLayer.DataAccess.Instance.SetTestContext();

//            _regularUser = new User(new Subscribed("regularUser", "pA55word", "fname", "lname", "owner@gmail.com"));
//            _permitManager = new User(new Subscribed("permitManager", "pA55word", "fname", "lname", "owner@gmail.com"));
//            _nonPermitManager = new User(new Subscribed("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com"));
//            _owner = new User(new Subscribed("owner", "pA55word", "fname", "lname", "owner@gmail.com"));
//            _anotherOwner = new User(new Subscribed("anotherOwner", "pA55word", "fname", "lname", "email@gmail.com"));
//            _newManager = new User(new Subscribed("newManager", "pA55word", "fname", "lname", "email@gmail.com"));

//            _userManagement = UsersManagement.Instance;
//            _systemManagement = SystemManager.Instance;
//            _storeManagment = StoreManagement.Instance;
//            _userManagement.register("owner", "pA55word", "fname", "lname", "owner@gmail.com");
//            _userManagement.register("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com");
//            _userManagement.register("permitManager", "pA55word", "fname", "lname", "owner@gmail.com");
//            _userManagement.register("regularUser", "pA55word", "fname", "lname", "owner@gmail.com");
//            _userManagement.login("owner", "pA55word");

//            // make the managers permissions

//            _keywords.Add("phone");


//        }

//        [SetUp]
//        public void setUp()
//        {
//            _store = new Store("owner", "store");

//            _store.Inventory.addProductInv(_productName, _description, _price, _quantity, _category, _keywords, "", "store");

//            _store.assignOwner(_owner, "anotherOwner");
//            //_store.assignManager(_owner, "newManager");
//            _store.assignManager(_owner, "nonPermitManager");

//            //give permit manager the permissions
//            _store.assignManager(_owner, "permitManager");
//            List<PermissionType> permissions = new List<PermissionType>();
//            permissions.Add(PermissionType.AddProductInv);
//            permissions.Add(PermissionType.DeleteProductInv);
//            permissions.Add(PermissionType.WatchPurchaseHistory);
//            permissions.Add(PermissionType.ModifyProduct);
//            _store.editPermissions("permitManager", permissions, "owner");


//            //**purchase policy**
//            _requireTotalPrice = 200;
//            _bannedLocationsIran = new List<string>() { "iran" };
//            _bannedLocationsIraq = new List<string>() { "iraq" };
//            _bannedLocationsTurkey = new List<string>() { "turkey" };
//            _bannedLocationsEgypt = new List<string>() { "egypt" };
//            _bannedLocationsLebanon = new List<string>() { "lebanon" };

//            _banIranPolicyID = _store.addLocationPolicy(_bannedLocationsIran);
//            _banTurkeyPolicyID = _store.addLocationPolicy(_bannedLocationsTurkey);
//            _banIraqPolicyID = _store.addLocationPolicy(_bannedLocationsIraq);
//            _banEgyptPolicyID = _store.addLocationPolicy(_bannedLocationsEgypt);
//            _banLebanonPolicyID = _store.addLocationPolicy(_bannedLocationsLebanon);
//            _minPricePerStorePolicyID = _store.addMinPriceStorePolicy(_requireTotalPrice);


//            //**discounts**
//            _quantity1 = 100;
//            _quantity2 = 200;
//            _quantity3 = 300;
//            _quantity4 = 400;
//            _quantity5 = 500;
//            _totalPrice1 = _price * _quantity1;
//            _totalPrice2 = _price * _quantity2;
//            _totalPrice3 = _price * _quantity3;
//            _totalPrice4 = _price * _quantity4;
//            _totalPrice5 = _price * _quantity5;

//            _productID1 = _store.Inventory.addProduct(_productName, _quantity1);
//            _productID2 = _store.Inventory.addProduct(_productName, _quantity2);
//            _productID3 = _store.Inventory.addProduct(_productName, _quantity3);
//            _productID4 = _store.Inventory.addProduct(_productName, _quantity4);
//            _productID5 = _store.Inventory.addProduct(_productName, _quantity5);

//        }

//        [TearDown]
//        public void tearDown()
//        {
//            _store.Inventory.Products.Clear();
//            _store.removeManager(_owner, "newManager");
//            _store.removeManager(_owner, "nonPermitManager");
//            _store.removeManager(_owner, "permitManager");
//            _store.DiscountPolicyTree.Children.Clear();
//            _store.StoreLevelDiscounts.Children.Clear();
//            _store.NotInTreeDiscounts.Clear();
//            _store.AllDiscountsMap.Clear();
//        }

//        [OneTimeTearDown]
//        public void tearDownFixture()
//        {
//            ECommerceSystem.DataAccessLayer.DataAccess.Instance.DropTestDatabase();
//        }

//        //[Test()]
//        //public void editPermissionsTest()
//        //{
//        //    _owner = _userManagement.getUserByGUID(_owner.Guid, true);
//        //    _storeManagment.assignManager(_owner.Guid, "newManager", "store");

//        //    List<PermissionType> emptyPermissions = new List<PermissionType>();

//        //    List<PermissionType> fewPermissions = new List<PermissionType>();
//        //    fewPermissions.Add(PermissionType.AddProductInv);
//        //    fewPermissions.Add(PermissionType.DeleteProductInv);
//        //    fewPermissions.Add(PermissionType.WatchAndComment);

//        //    _store = _storeManagment.getStoreByName("store");
//        //    Assert.Null(_store.editPermissions("newManager", emptyPermissions, "regularUser"), "Edit permiossions for manager by regular successed");
//        //    Assert.Null(_store.editPermissions("newManager", emptyPermissions, "regularUser"), "Edit permiossions for manager by another manager successed");
//        //    Assert.Null(_store.editPermissions("newManager", emptyPermissions, "regularUser"), "Edit permiossions for manager by owner who isn`t his asignee successed");

//        //    Assert.NotNull(_store.editPermissions("newManager", emptyPermissions, "owner"), "Fail to edit permissions to empty permissions list");
//        //    _store = _storeManagment.getStoreByName("store");

//        //    //check that newManager dont have permissions:
//        //    Assert.False(_store.getPermissionByName("newManager").canAddProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to add product");
//        //    Assert.False(_store.getPermissionByName("newManager").canDeleteProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to delete product");
//        //    Assert.False(_store.getPermissionByName("newManager").canModifyProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to modify product");
//        //    Assert.False(_store.getPermissionByName("newManager").canWatchAndomment(), "Permissions edited to empty permissions list successed but the manager still have permission to watch and comment");
//        //    Assert.False(_store.getPermissionByName("newManager").canWatchPurchaseHistory(), "Permissions edited to empty permissions list successed but the manager still have permission to watch history");

//        //    Assert.NotNull(_store.editPermissions("newManager", fewPermissions, "owner"), "Fail to edit permissions to empty permissions list");

//        //    _store = _storeManagment.getStoreByName("store");
//        //    //check that newManager dont have permissions:
//        //    Assert.True(_store.getPermissionByName("newManager").canAddProduct(), "Permissions edited to esuccessed but the manager dont have permission to add product");
//        //    Assert.True(_store.getPermissionByName("newManager").canDeleteProduct(), "Permissions edited to esuccessed but the manager dont have permission to delete product");
//        //    Assert.False(_store.getPermissionByName("newManager").canModifyProduct(), "Permissions edited to successed but the manager have permission to modify product");
//        //    Assert.True(_store.getPermissionByName("newManager").canWatchAndomment(), "Permissions edited to esuccessed but the manager dont have permission to watch and comment");
//        //    Assert.False(_store.getPermissionByName("newManager").canWatchPurchaseHistory(), "Permissions edited to successed but the manager have permission to watch history");
//        //}








       














//    }
//}