using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using NUnit.Framework;
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

        private SystemManager _systemManagement;
        private StoreManagement _storeManagment;
        private UsersManagement _userManagement;
        private User _owner;
        private User _regularUser;
        private User _nonPermitManager;
        private User _permitManager;
        private Store _store;

        private User _anotherOwner;
        private User _newManager;


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

            ECommerceSystem.DataAccessLayer.DataAccess.Instance.SetTestContext();

            _regularUser = new User(new Subscribed("regularUser", "pA55word", "fname", "lname", "owner@gmail.com"));
            _permitManager = new User(new Subscribed("permitManager", "pA55word", "fname", "lname", "owner@gmail.com"));
            _nonPermitManager = new User(new Subscribed("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com"));
            _owner = new User(new Subscribed("owner", "pA55word", "fname", "lname", "owner@gmail.com"));
            _anotherOwner = new User(new Subscribed("anotherOwner", "pA55word", "fname", "lname", "email@gmail.com"));
            _newManager = new User(new Subscribed("newManager", "pA55word", "fname", "lname", "email@gmail.com"));

            _userManagement = UsersManagement.Instance;
            _systemManagement = SystemManager.Instance;
            _storeManagment = StoreManagement.Instance;
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

            _store.Inventory.addProductInv(_productName, _description, _price, _quantity, _category, _keywords, "", "store");

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
            _store.NotInTreeDiscounts.Clear();
            _store.AllDiscountsMap.Clear();
        }

        [OneTimeTearDown]
        public void tearDownFixture()
        {
            ECommerceSystem.DataAccessLayer.DataAccess.Instance.DropTestDatabase();
        }

        //[Test()]
        //public void editPermissionsTest()
        //{
        //    _owner = _userManagement.getUserByGUID(_owner.Guid, true);
        //    _storeManagment.assignManager(_owner.Guid, "newManager", "store");

        //    List<PermissionType> emptyPermissions = new List<PermissionType>();

        //    List<PermissionType> fewPermissions = new List<PermissionType>();
        //    fewPermissions.Add(PermissionType.AddProductInv);
        //    fewPermissions.Add(PermissionType.DeleteProductInv);
        //    fewPermissions.Add(PermissionType.WatchAndComment);

        //    _store = _storeManagment.getStoreByName("store");
        //    Assert.Null(_store.editPermissions("newManager", emptyPermissions, "regularUser"), "Edit permiossions for manager by regular successed");
        //    Assert.Null(_store.editPermissions("newManager", emptyPermissions, "regularUser"), "Edit permiossions for manager by another manager successed");
        //    Assert.Null(_store.editPermissions("newManager", emptyPermissions, "regularUser"), "Edit permiossions for manager by owner who isn`t his asignee successed");

        //    Assert.NotNull(_store.editPermissions("newManager", emptyPermissions, "owner"), "Fail to edit permissions to empty permissions list");
        //    _store = _storeManagment.getStoreByName("store");

        //    //check that newManager dont have permissions:
        //    Assert.False(_store.getPermissionByName("newManager").canAddProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to add product");
        //    Assert.False(_store.getPermissionByName("newManager").canDeleteProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to delete product");
        //    Assert.False(_store.getPermissionByName("newManager").canModifyProduct(), "Permissions edited to empty permissions list successed but the manager still have permission to modify product");
        //    Assert.False(_store.getPermissionByName("newManager").canWatchAndomment(), "Permissions edited to empty permissions list successed but the manager still have permission to watch and comment");
        //    Assert.False(_store.getPermissionByName("newManager").canWatchPurchaseHistory(), "Permissions edited to empty permissions list successed but the manager still have permission to watch history");

        //    Assert.NotNull(_store.editPermissions("newManager", fewPermissions, "owner"), "Fail to edit permissions to empty permissions list");

        //    _store = _storeManagment.getStoreByName("store");
        //    //check that newManager dont have permissions:
        //    Assert.True(_store.getPermissionByName("newManager").canAddProduct(), "Permissions edited to esuccessed but the manager dont have permission to add product");
        //    Assert.True(_store.getPermissionByName("newManager").canDeleteProduct(), "Permissions edited to esuccessed but the manager dont have permission to delete product");
        //    Assert.False(_store.getPermissionByName("newManager").canModifyProduct(), "Permissions edited to successed but the manager have permission to modify product");
        //    Assert.True(_store.getPermissionByName("newManager").canWatchAndomment(), "Permissions edited to esuccessed but the manager dont have permission to watch and comment");
        //    Assert.False(_store.getPermissionByName("newManager").canWatchPurchaseHistory(), "Permissions edited to successed but the manager have permission to watch history");
        //}








       




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
            Assert.IsTrue(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            //remove visibleDiscountP1
            Assert.IsTrue(_store.removeProductDiscount(visibleDiscountP1, _productID1));
            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));


            Guid XORDiscountP2P3 = _store.addXorDiscountPolicy(new List<Guid>() { visibleDiscountP3, conditionalProductP2 });
            Assert.IsFalse(_store.removeProductDiscount(XORDiscountP2P3, _productID1));



            Assert.IsTrue(_store.removeProductDiscount(visibleDiscountP3, _productID3));
            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP3));
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
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(conditionalStore));
            Assert.IsNotNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));

            //remove visibleDiscountP1
            Assert.IsTrue(_store.removeStoreLevelDiscount(conditionalStore));
            Assert.IsFalse(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(conditionalStore));
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
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(XORDiscountP2P3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XORDiscountP2P3));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(XORDiscountP2P3));

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsTrue(_store.NotInTreeDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsTrue(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP3));
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
            Dictionary<Guid, Tuple<Product, int>> cart1 = new Dictionary<Guid, Tuple<Product, int>>();
            cart1.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            cart1.Add(Guid.NewGuid(), Tuple.Create(product5, 3));
            Assert.AreEqual((((_price * 5) * 0.9) + (3 * _price)), _store.getTotalPrice(cart1));


            //test one visible discount with store discount(total price between 200 to 500)
            Dictionary<Guid, Tuple<Product, int>> cart2 = new Dictionary<Guid, Tuple<Product, int>>();
            cart2.Add(Guid.NewGuid(), Tuple.Create(product1, 30));
            Assert.AreEqual(((_price * 30) * 0.9) * 0.9, _store.getTotalPrice(cart2));


            //test one visible discount with store discount(total price > 500)
            Dictionary<Guid, Tuple<Product, int>> cart3 = new Dictionary<Guid, Tuple<Product, int>>();
            cart3.Add(Guid.NewGuid(), Tuple.Create(product1, 90));
            Assert.AreEqual(((_price * 90) * 0.9) * 0.8, _store.getTotalPrice(cart3));


            //test satisfy conditional product wihtout store discount
            Dictionary<Guid, Tuple<Product, int>> cart4 = new Dictionary<Guid, Tuple<Product, int>>();
            cart4.Add(Guid.NewGuid(), Tuple.Create(product2, 3));
            Assert.AreEqual(((_price *3) * 0.8), _store.getTotalPrice(cart4));


            //test not satisfy conditional product wihtout store discount
            Dictionary<Guid, Tuple<Product, int>> cart5 = new Dictionary<Guid, Tuple<Product, int>>();
            cart5.Add(Guid.NewGuid(), Tuple.Create(product2, 1));
            Assert.AreEqual((_price * 1), _store.getTotalPrice(cart5));

            //******ADD AND*****
            Guid andP1P2Discount = _store.addAndDiscountPolicy(new List<Guid>() { visibleDiscountP1, conditionalProductP2 });

            //test not satisfy and(conditional product, visible) wihtout store discount
            Dictionary<Guid, Tuple<Product, int>> cart6 = new Dictionary<Guid, Tuple<Product, int>>();
            cart6.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            cart6.Add(Guid.NewGuid(), Tuple.Create(product2, 1));
            Assert.AreEqual((_price * 5) + ((_price * 1)), _store.getTotalPrice(cart6));


            //test satisfy and(conditional product, visible) wihtout store discount
            Dictionary<Guid, Tuple<Product, int>> cart7 = new Dictionary<Guid, Tuple<Product, int>>();
            cart7.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            cart7.Add(Guid.NewGuid(), Tuple.Create(product2, 3));
            Assert.AreEqual(((_price * 5) * 0.9) + (((_price * 3)) * 0.8), _store.getTotalPrice(cart7));

            //test one side of and not exist => not satisfy
            Dictionary<Guid, Tuple<Product, int>> cart8 = new Dictionary<Guid, Tuple<Product, int>>();
            cart8.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            Assert.AreEqual(_price * 5, _store.getTotalPrice(cart8));


            //test one side of and not exist => but still satisfy
            Dictionary<Guid, Tuple<Product, int>> cart9 = new Dictionary<Guid, Tuple<Product, int>>();
            cart9.Add(Guid.NewGuid(), Tuple.Create(product2, 5));
            Assert.AreEqual(_price * 5 * 0.8, _store.getTotalPrice(cart9));


            //******ADD XOR(andDiscount, conditionalProductP4)*****
            Guid XORP4AndDiscount = _store.addXorDiscountPolicy(new List<Guid>() { andP1P2Discount, conditionalProductP4 });

            //all true 
            Dictionary<Guid, Tuple<Product, int>> cart10 = new Dictionary<Guid, Tuple<Product, int>>();
            cart10.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            cart10.Add(Guid.NewGuid(), Tuple.Create(product2, 5));
            cart10.Add(Guid.NewGuid(), Tuple.Create(product4, 5));
            Assert.AreEqual(((5+5) * _price) + (_price * 5 * 0.6) , _store.getTotalPrice(cart10));


            //product4 = false, and = true 
            Dictionary<Guid, Tuple<Product, int>> cart11 = new Dictionary<Guid, Tuple<Product, int>>();
            cart11.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            cart11.Add(Guid.NewGuid(), Tuple.Create(product2, 5));
            cart11.Add(Guid.NewGuid(), Tuple.Create(product4, 2));
            Assert.AreEqual((5 * _price * 0.9) + (5 * _price * 0.8) + (_price * 2), _store.getTotalPrice(cart11));

            //product4 = false, and = false 
            Dictionary<Guid, Tuple<Product, int>> cart12 = new Dictionary<Guid, Tuple<Product, int>>();
            cart12.Add(Guid.NewGuid(), Tuple.Create(product1, 5));
            cart12.Add(Guid.NewGuid(), Tuple.Create(product2, 1));
            cart12.Add(Guid.NewGuid(), Tuple.Create(product3, 5));
            cart12.Add(Guid.NewGuid(), Tuple.Create(product4, 2));
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