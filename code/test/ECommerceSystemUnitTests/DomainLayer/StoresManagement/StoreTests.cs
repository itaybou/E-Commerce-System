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
using ECommerceSystem.DomainLayer.StoresManagement.Discount.Tests;

namespace ECommerceSystem.DomainLayer.StoresManagement.Tests
{
    [TestFixture()]
    public class StoreTests
    {
        string _productName = "Iphone", _description = "description";
        PurchaseType _purchaseType = new ImmediatePurchase();
        double _price = 10;
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


        //**discounts**
        Guid _productID1;
        Guid _productID2;
        Guid _productID3;
        Guid _productID4;
        Guid _productID5;
        Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> _products;

        int _quantity1;
        int _quantity2;
        int _quantity3;
        int _quantity4;
        int _quantity5;

        double _totalPrice1;
        double _totalPrice2;
        double _totalPrice3;
        double _totalPrice4;
        double _totalPrice5;


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
            _store = new Store("owner", "store");

            _store.Inventory.addProductInv(_productName, _description, _price, _quantity, _category, _keywords);

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


            //**discounts**
            _quantity1 = 100;
            _quantity2 = 200;
            _quantity3 = 300;
            _quantity4 = 400;
            _quantity5 = 500;
            _totalPrice1 = _price * _quantity1;
            _totalPrice2 = _price * _quantity2;
            _totalPrice3 = _price * _quantity3;
            _totalPrice4 = _price * _quantity4;
            _totalPrice5 = _price * _quantity5;

            _productID1 = _store.Inventory.addProduct(_productName, _quantity1);
            _productID2 = _store.Inventory.addProduct(_productName, _quantity2);
            _productID3 = _store.Inventory.addProduct(_productName, _quantity3);
            _productID4 = _store.Inventory.addProduct(_productName, _quantity4);
            _productID5 = _store.Inventory.addProduct(_productName, _quantity5);

        }

        [TearDown]
        public void tearDown()
        {
            _store.Inventory.Products.Clear();
            _store.removeManager(_owner, "newManager");
            _store.removeManager(_owner, "nonPermitManager");
            _store.removeManager(_owner, "permitManager");
            _store.DiscountPolicyTree.Children.Clear();
            _store.StoreLevelDiscounts.Children.Clear();
            _store.NotInTreeProductDiscounts.Clear();
            _store.AllDiscountsMap.Clear();
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

        [Test()]
        public void addVisibleDiscountTest()
        {
            Assert.AreEqual(Guid.Empty, _store.addVisibleDiscount(Guid.NewGuid(), 10, DateTime.Today.AddDays(10))); // not exist product id
            Assert.AreEqual(Guid.Empty, _store.addVisibleDiscount(_productID1, -1, DateTime.Today.AddDays(10))); // negative percetage
            Assert.AreEqual(Guid.Empty, _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(-1))); // illegal date

            Guid visibleDiscountP1 = _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10));
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsTrue(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            Assert.AreEqual(Guid.Empty, _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10))); //cant add discount to product with discount

        }


        [Test()]
        public void addCondiotionalProcuctDiscountTest()
        {

            Assert.AreEqual(Guid.Empty, _store.addCondiotionalProcuctDiscount(Guid.NewGuid(), 10, DateTime.Today.AddDays(10), 10)); //not exist product ID
            Assert.AreEqual(Guid.Empty, _store.addCondiotionalProcuctDiscount(_productID2, 10, DateTime.Today.AddDays(10), -1)); // negative required quantity
            Assert.AreEqual(Guid.Empty, _store.addCondiotionalProcuctDiscount(_productID2, -1, DateTime.Today.AddDays(10), 10)); // negative percentage
            Assert.AreEqual(Guid.Empty, _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(-1), 2)); // illeget date


            Guid conditionalProductP2 = _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2);
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsTrue(_store.NotInTreeProductDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            Assert.AreEqual(Guid.Empty, _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2)); //cant add discount to product with discount

        }

        [Test()]
        public void addConditionalStoreDiscount()
        {

            Assert.AreEqual(Guid.Empty, _store.addConditionalStoreDiscount(-1, DateTime.Today.AddDays(10), 200)); //negative percentage
            Assert.AreEqual(Guid.Empty, _store.addConditionalStoreDiscount(10, DateTime.Today.AddDays(-10), 200)); //illegal date
            Assert.AreEqual(Guid.Empty, _store.addConditionalStoreDiscount(10, DateTime.Today.AddDays(10), -50)); //negative required price


            Guid conditionalStore = _store.addConditionalStoreDiscount(10, DateTime.Today.AddDays(10), 200);
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(conditionalStore));
            Assert.IsNotNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));
        }

        [Test()]
        public void addCompositeDiscountPolicyTest()
        {
            Guid visibleDiscountP1 = _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10));
            Guid conditionalProductP2 = _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2);
            Guid visibleDiscountP3 = _store.addVisibleDiscount(_productID3, 30, DateTime.Today.AddDays(10));
            Guid conditionalStore = _store.addConditionalStoreDiscount(10, DateTime.Today.AddDays(10), 200);


            //try to add with not exist policy
            Assert.AreEqual(Guid.Empty, _store.addAndDiscountPolicy(new List<Guid>() { Guid.NewGuid() }));


            //add store level discount in composite discount is forbidden
            Assert.AreEqual(Guid.Empty, _store.addAndDiscountPolicy(new List<Guid>() { visibleDiscountP3, conditionalStore }));

            //check the exist of visibleDiscountP3
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsTrue(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP3));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP3));

            //check the exist of conditionalStore
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(conditionalStore));
            Assert.IsNotNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));



            //add XOR discount between P1 and P2 discounts
            Guid XORDiscountP1P2 = _store.addXorDiscountPolicy(new List<Guid>() { visibleDiscountP1, conditionalProductP2 });

            //check the exist of visibleDiscountP1
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            //check the exist of conditionalProductP2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            //check the exist of XORDiscountP1P2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(XORDiscountP1P2));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(XORDiscountP1P2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XORDiscountP1P2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(XORDiscountP1P2));


            //add two level composite discount
            Guid XOrDiscountXORP1P2OrP3 = _store.addXorDiscountPolicy(new List<Guid>() { XORDiscountP1P2, visibleDiscountP3 });

            //check the exist of visibleDiscountP1
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            //check the exist of conditionalProductP2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            //check the exist of visibleDiscountP3
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP3));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(visibleDiscountP3));

            //check the exist of XORDiscountP1P2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(XORDiscountP1P2));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(XORDiscountP1P2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XORDiscountP1P2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(XORDiscountP1P2));
            Assert.IsFalse(_store.DiscountPolicyTree.Children.Contains(_store.DiscountPolicyTree.getByID(XORDiscountP1P2))); //check that the xor isn`t exist in the first level of the tree

            //check the exist of OrDiscountXORP1P2OrP3
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(XOrDiscountXORP1P2OrP3));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(XOrDiscountXORP1P2OrP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XOrDiscountXORP1P2OrP3));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(XOrDiscountXORP1P2OrP3));
        }

        [Test()]
        public void removeProductDiscountTest()
        {
            Guid visibleDiscountP1 = _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10));
            Guid conditionalProductP2 = _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2);
            Guid visibleDiscountP3 = _store.addVisibleDiscount(_productID3, 30, DateTime.Today.AddDays(10));


            Assert.IsFalse(_store.removeProductDiscount(Guid.NewGuid(), _productID1)); //not exist discount
            Assert.IsFalse(_store.removeProductDiscount(visibleDiscountP1, Guid.NewGuid())); //not exist product id
            Assert.IsFalse(_store.removeProductDiscount(visibleDiscountP1, _productID2)); //prouct id is not match with the product id of the discount

            //check the exist of visibleDiscountP1
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsTrue(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            //remove visibleDiscountP1
            Assert.IsTrue(_store.removeProductDiscount(visibleDiscountP1, _productID1));
            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));


            Guid XORDiscountP2P3 = _store.addXorDiscountPolicy(new List<Guid>() { visibleDiscountP3, conditionalProductP2 });
            Assert.IsFalse(_store.removeProductDiscount(XORDiscountP2P3, _productID1));



            Assert.IsTrue(_store.removeProductDiscount(visibleDiscountP3, _productID3));
            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP3));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP3));

        }

        [Test()]
        public void removeStoreLevelDiscountTest()
        {

            Guid conditionalStore = _store.addConditionalStoreDiscount(10, DateTime.Today.AddDays(10), 200);
            Guid visibleDiscountP1 = _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10));


            Assert.IsFalse(_store.removeStoreLevelDiscount(Guid.NewGuid())); //not exist discount
            Assert.IsFalse(_store.removeStoreLevelDiscount(visibleDiscountP1)); //remove not store discount

            //check the exist of conditionalStore
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(conditionalStore));
            Assert.IsNotNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));

            //remove visibleDiscountP1
            Assert.IsTrue(_store.removeStoreLevelDiscount(conditionalStore));
            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(conditionalStore));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));

        }


        [Test()]
        public void removeCompositeDiscountTest()
        {
            Guid visibleDiscountP1 = _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10));
            Guid conditionalProductP2 = _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2);
            Guid visibleDiscountP3 = _store.addVisibleDiscount(_productID3, 30, DateTime.Today.AddDays(10));

            Guid XORDiscountP2P3 = _store.addXorDiscountPolicy(new List<Guid>() { visibleDiscountP3, conditionalProductP2 });

            Assert.IsFalse(_store.removeCompositeDiscount(visibleDiscountP1)); //remove not composite
            Assert.IsFalse(_store.removeCompositeDiscount(Guid.NewGuid())); //remove not exist discount


            Assert.IsTrue(_store.removeCompositeDiscount(XORDiscountP2P3));

            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(XORDiscountP2P3));
            Assert.IsFalse(_store.NotInTreeProductDiscounts.ContainsKey(XORDiscountP2P3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XORDiscountP2P3));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(XORDiscountP2P3));

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsTrue(_store.NotInTreeProductDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsTrue(_store.NotInTreeProductDiscounts.ContainsKey(visibleDiscountP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP3));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP3));

        }


        [Test()]
        public void getTotalPriceTest()
        {
            int min200StoreDiscountPercentage = 10;
            int min500StoreDiscountPercentage = 20;
            Guid visibleDiscountP1 = _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10));
            Guid conditionalProductP2 = _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2);
            Guid visibleDiscountP3 = _store.addVisibleDiscount(_productID3, 30, DateTime.Today.AddDays(10));
            Guid conditionalProductP4 = _store.addCondiotionalProcuctDiscount(_productID4, 40, DateTime.Today.AddDays(10), 4);
            Guid conditionalStore1 = _store.addConditionalStoreDiscount(min200StoreDiscountPercentage, DateTime.Today.AddDays(10), 200);
            Guid conditionalStore2 = _store.addConditionalStoreDiscount(min500StoreDiscountPercentage, DateTime.Today.AddDays(10), 500);

            Product product1 = _store.Inventory.getProductById(_productID1); 
            Product product2 = _store.Inventory.getProductById(_productID2); 
            Product product3 = _store.Inventory.getProductById(_productID3); 
            Product product4 = _store.Inventory.getProductById(_productID4); 
            Product product5 = _store.Inventory.getProductById(_productID5);


            //test one product with visible discount and one without discount cart, no store discount
            Dictionary<Product, int> cart1 = new Dictionary<Product, int>();
            cart1.Add(product1, 5);
            cart1.Add(product5, 3);
            Assert.AreEqual((((_price * 5) * 0.9) + (3 * _price)), _store.getTotalPrice(cart1));


            //test one visible discount with store discount(total price between 200 to 500)
            Dictionary<Product, int> cart2 = new Dictionary<Product, int>();
            cart2.Add(product1, 30);
            Assert.AreEqual(((_price * 30) * 0.9) * 0.9, _store.getTotalPrice(cart2));


            //test one visible discount with store discount(total price > 500)
            Dictionary<Product, int> cart3 = new Dictionary<Product, int>();
            cart3.Add(product1, 90);
            Assert.AreEqual(((_price * 90) * 0.9) * 0.8, _store.getTotalPrice(cart3));


            //test satisfy conditional product wihtout store discount
            Dictionary<Product, int> cart4 = new Dictionary<Product, int>();
            cart4.Add(product2, 3);
            Assert.AreEqual(((_price *3) * 0.8), _store.getTotalPrice(cart4));


            //test not satisfy conditional product wihtout store discount
            Dictionary<Product, int> cart5 = new Dictionary<Product, int>();
            cart5.Add(product2, 1);
            Assert.AreEqual((_price * 1), _store.getTotalPrice(cart5));

            //******ADD AND*****
            Guid andP1P2Discount = _store.addAndDiscountPolicy(new List<Guid>() { visibleDiscountP1, conditionalProductP2 });

            //test not satisfy and(conditional product, visible) wihtout store discount
            Dictionary<Product, int> cart6 = new Dictionary<Product, int>();
            cart6.Add(product1, 5);
            cart6.Add(product2, 1);
            Assert.AreEqual((_price * 5) + ((_price * 1)), _store.getTotalPrice(cart6));


            //test satisfy and(conditional product, visible) wihtout store discount
            Dictionary<Product, int> cart7 = new Dictionary<Product, int>();
            cart7.Add(product1, 5);
            cart7.Add(product2, 3);
            Assert.AreEqual(((_price * 5) * 0.9) + (((_price * 3)) * 0.8), _store.getTotalPrice(cart7));

            //test one side of and not exist => not satisfy
            Dictionary<Product, int> cart8 = new Dictionary<Product, int>();
            cart8.Add(product1, 5);
            Assert.AreEqual(_price * 5, _store.getTotalPrice(cart8));


            //test one side of and not exist => but still satisfy
            Dictionary<Product, int> cart9 = new Dictionary<Product, int>();
            cart9.Add(product2, 5);
            Assert.AreEqual(_price * 5 * 0.8, _store.getTotalPrice(cart9));


            //******ADD XOR(andDiscount, conditionalProductP4)*****
            Guid XORP4AndDiscount = _store.addXorDiscountPolicy(new List<Guid>() { andP1P2Discount, conditionalProductP4 });

            //all true 
            Dictionary<Product, int> cart10 = new Dictionary<Product, int>();
            cart10.Add(product1, 5);
            cart10.Add(product2, 5);
            cart10.Add(product4, 5);
            Assert.AreEqual(((5+5) * _price) + (_price * 5 * 0.6) , _store.getTotalPrice(cart10));


            //product4 = false, and = true 
            Dictionary<Product, int> cart11 = new Dictionary<Product, int>();
            cart11.Add(product1, 5);
            cart11.Add(product2, 5);
            cart11.Add(product4, 2);
            Assert.AreEqual((5 * _price * 0.9) + (5 * _price * 0.8) + (_price * 2), _store.getTotalPrice(cart11));

            //product4 = false, and = false 
            Dictionary<Product, int> cart12 = new Dictionary<Product, int>();
            cart12.Add(product1, 5);
            cart12.Add(product2, 1);
            cart12.Add(product3, 5);
            cart12.Add(product4, 2);
            Assert.AreEqual(((5 + 1 + 2) * _price) + (5 * _price * 0.7), _store.getTotalPrice(cart12));


            //remove And
            _store.removeCompositeDiscount(andP1P2Discount);
            Assert.AreEqual((((_price * 5) * 0.9) + (3 * _price)), _store.getTotalPrice(cart1)); //cart1 should be priced like in the start
            Assert.AreEqual(((_price * 30) * 0.9) * 0.9, _store.getTotalPrice(cart2)); //cart2 should be priced like in the start
            Assert.AreEqual(((_price * 90) * 0.9) * 0.8, _store.getTotalPrice(cart3)); //cart3 should be priced like in the start
            Assert.AreEqual(((_price * 3) * 0.8), _store.getTotalPrice(cart4)); //cart4 should be priced like in the start
            Assert.AreEqual((_price * 1), _store.getTotalPrice(cart5)); //cart5 should be priced like in the start


        }


    }
}