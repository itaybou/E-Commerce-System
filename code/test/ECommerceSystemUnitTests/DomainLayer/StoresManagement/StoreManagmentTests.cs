using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ECommerceSystemUnitTests.DomainLayer.StoresManagement
{
    [TestFixture()]
    public class StoreManagmentTests
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
        private DataAccess _data;

        private StoreManagement _storeManagement;
        private UsersManagement _userManagement;
        private User _owner;
        private User _regularUser;
        private User _nonPermitManager;
        private User _permitManager;
        private Store _store;

        private User _anotherOwner;
        private User _newManager;

        private Guid _regularUserGUID;
        private Guid _permitManagerGUID;
        private Guid _nonPermitManagerGUID;
        private Guid _ownerGUID;
        private Guid _anotherOwnerGUID;
        private Guid _anotherOwner3GUID;
        private Guid _newManagerGUID;

    
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
            //_data = DataAccess.Instance;
            ////_data.DropTestDatabase();

            //_data.SetTestContext();
            DataAccess.Instance.SetTestContext();

            _storeManagement = StoreManagement.Instance;
            _userManagement = UsersManagement.Instance;
            _systemManagement = SystemManager.Instance;
            _userManagement.register("owner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("nonPermitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("permitManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("regularUser", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("newManager", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("anotherOwner", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.register("anotherOwner3", "pA55word", "fname", "lname", "owner@gmail.com");
            _userManagement.login("owner", "pA55word");


            //_regularUser = _userManagement.getUserByName("regularUser");
            //_permitManager = _userManagement.getUserByName("permitManager");
            //_nonPermitManager = _userManagement.getUserByName("nonPermitManager");
            //_owner = _userManagement.getUserByName("owner");
            //_anotherOwner = _userManagement.getUserByName("anotherOwner");
            //_newManager = _userManagement.getUserByName("newManager");


            _regularUserGUID = _userManagement.getUserByName("regularUser").Guid;
            _permitManagerGUID = _userManagement.getUserByName("permitManager").Guid;
            _nonPermitManagerGUID = _userManagement.getUserByName("nonPermitManager").Guid;
            _ownerGUID = _userManagement.getUserByName("owner").Guid;
            _anotherOwnerGUID = _userManagement.getUserByName("anotherOwner").Guid;
            _anotherOwner3GUID = _userManagement.getUserByName("anotherOwner3").Guid;
            _newManagerGUID = _userManagement.getUserByName("newManager").Guid;
            // make the managers permissions

            _keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            _userManagement.login("owner", "pA55word");
            _storeManagement.openStore(_ownerGUID ,"store");
            _storeManagement.assignManager(_ownerGUID, "permitManager", "store");
            _storeManagement.editPermissions(_ownerGUID, "store", "permitManager", new List<PermissionType> { PermissionType.AddProductInv, PermissionType.DeleteProductInv, PermissionType.ManageDiscounts, PermissionType.ManagePurchasePolicy, PermissionType.ModifyProduct, PermissionType.WatchAndComment, PermissionType.WatchPurchaseHistory });
            _storeManagement.assignManager(_ownerGUID, "nonPermitManager", "store");
            //_store = _storeManagement.getStoreByName("store");

            _storeManagement.addProductInv(_ownerGUID, "store",_description, _productName,  _price, _quantity, _category, _keywords, -1,-1,"");



            //**purchase policy**
            _requireTotalPrice = 200;
            _bannedLocationsIran = new List<string>() { "iran" };
            _bannedLocationsIraq = new List<string>() { "iraq" };
            _bannedLocationsTurkey = new List<string>() { "turkey" };
            _bannedLocationsEgypt = new List<string>() { "egypt" };
            _bannedLocationsLebanon = new List<string>() { "lebanon" };

            _banIranPolicyID = _storeManagement.addLocationPolicy(_ownerGUID, "store", _bannedLocationsIran);
            _banTurkeyPolicyID = _storeManagement.addLocationPolicy(_ownerGUID, "store", _bannedLocationsTurkey); 
            _banIraqPolicyID = _storeManagement.addLocationPolicy(_ownerGUID, "store", _bannedLocationsIraq); 
            _banEgyptPolicyID = _storeManagement.addLocationPolicy(_ownerGUID, "store", _bannedLocationsEgypt); 
            _banLebanonPolicyID = _storeManagement.addLocationPolicy(_ownerGUID, "store", _bannedLocationsLebanon); 
            _minPricePerStorePolicyID = _storeManagement.addMinPriceStorePolicy(_ownerGUID, "store", _requireTotalPrice);


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

            _productID1 = _storeManagement.addProduct(_ownerGUID, "store", _productName, _quantity1, -1, -1);
            _productID2 = _storeManagement.addProduct(_ownerGUID, "store", _productName, _quantity2, -1, -1);
            _productID3 = _storeManagement.addProduct(_ownerGUID, "store", _productName, _quantity3, -1, -1);
            _productID4 = _storeManagement.addProduct(_ownerGUID, "store", _productName, _quantity4, -1, -1);
            _productID5 = _storeManagement.addProduct(_ownerGUID, "store", _productName, _quantity5, -1, -1);

        }

        [TearDown]
        public void tearDown()
        {
            //_store.Inventory.Products.Clear();
            _userManagement.login("owner", "pA55word");

            _store = _storeManagement.getStoreByName("store");
            _store.AssignerOwnerAgreement.Clear();
            _storeManagement.removeManager(_ownerGUID, "newManager", "store");
            _storeManagement.removeManager(_ownerGUID, "nonPermitManager", "store");
            _storeManagement.removeManager(_ownerGUID, "permitManager", "store");
            _storeManagement.removeOwner(_ownerGUID, "anotherOwner", "store");
            _storeManagement.removeOwner(_ownerGUID, "anotherOwner3", "store");


            //reset db
            _store = _storeManagement.getStoreByName("store");
            _storeManagement.deleteProductInventory(_ownerGUID, "store", _productName);
            _store.Inventory.Products.Clear();
            _store.PurchasePolicy.Children.Clear();
            _store.DiscountPolicyTree.Children.Clear();
            _store.StoreLevelDiscounts.Children.Clear();
            _store.NotInTreeDiscounts.Clear();
            _store.AllDiscountsMap.Clear();

            DataAccess.Instance.Stores.Update(_store, "store", s => s.Name);
            
        }


    

        [OneTimeTearDown]
        public void tearDownFixture()
        {
           DataAccess.Instance.DropTestDatabase();
        }


        [Test()]
        public void OpenStoreParallelTest()
        {

            Guid[] usersID = { _ownerGUID, _anotherOwnerGUID, _anotherOwner3GUID };
            Thread[] threads = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                Thread t = new Thread(() =>
                {
                    _storeManagement.openStore(usersID[i], "newStore");
                });
                threads[i] = t;
                t.Start();
                //Thread.Sleep(100);
            }

            for (int i = 0; i < 3; i++)
            {
                threads[i].Join();
            }

            Store store = _storeManagement.getStoreByName("newStore");
            Assert.IsNotNull(store);
            Assert.AreEqual(1, store.StorePermissions.Count);
            User owner = _userManagement.getUserByGUID(_ownerGUID, false);
            User anotherOwner = _userManagement.getUserByGUID(_anotherOwnerGUID, false);
            User anotherOwner3 = _userManagement.getUserByGUID(_anotherOwner3GUID, false);

            bool isOwnerSuccess = ((Subscribed)owner.State).Permissions.ContainsKey("newStore");
            bool isAnotherOwnerSuccess = ((Subscribed)anotherOwner.State).Permissions.ContainsKey("newStore");
            bool isAnotherOwner3Success = ((Subscribed)anotherOwner3.State).Permissions.ContainsKey("newStore");
            Console.WriteLine(isOwnerSuccess + " " + isAnotherOwnerSuccess + " " + isAnotherOwner3Success);
            Assert.IsTrue((isOwnerSuccess ^ isAnotherOwnerSuccess ^ isAnotherOwner3Success) && !(isOwnerSuccess && isAnotherOwnerSuccess && isAnotherOwner3Success));
        }


        [Test()]
        public void assignManagerByUnPermitedUserTest()
        {
            _userManagement.login("regularUser", "pA55word");
            Assert.False(_storeManagement.assignManager(_regularUserGUID, "newManager", "store"), "Assign regular user as manager by another regular user successed");
            _userManagement.logout(_regularUserGUID);
            _userManagement.login("nonPermitManager", "pA55word");
            Assert.False(_storeManagement.assignManager(_nonPermitManagerGUID, "newManager", "store"), "Assign regular user as manager by manager with default permissions successed");
            _userManagement.logout(_nonPermitManagerGUID);
            _userManagement.login("permitManager", "pA55word");
            Assert.False(_storeManagement.assignManager(_permitManagerGUID, "newManager", "store"), "Assign regular user as manager by manager with full permissions successed");
            _userManagement.logout(_permitManagerGUID);
        }

        [Test()]
        public void removeOwnerTest()
        {
            _storeManagement.createOwnerAssignAgreement(_ownerGUID, "anotherOwner", "store");
            _userManagement.register("ownerAssignedByAnotherOwner", "pA55word", "fname", "lname", "owner@gmail.com");
             Guid agreementID = _storeManagement.createOwnerAssignAgreement(_anotherOwnerGUID, "ownerAssignedByAnotherOwner", "store");
            _storeManagement.approveAssignOwnerRequest(_ownerGUID, agreementID, "store");
            User ownerAssignedByAnotherOwner = _userManagement.getUserByName("ownerAssignedByAnotherOwner");


            Assert.False(_storeManagement.removeOwner(_regularUserGUID, "anotherOwner", "store")); //regular user try to remove owner
            Assert.False(_storeManagement.removeOwner(_permitManagerGUID, "anotherOwner", "store")); //permited manager try to remove owner
            Assert.False(_storeManagement.removeOwner(_nonPermitManagerGUID, "anotherOwner", "store")); //non permited manager try to remove owner
            Assert.False(_storeManagement.removeOwner(Guid.NewGuid(), "anotherOwner", "store")); //non exist id user try to remove owner
            Assert.False(_storeManagement.removeOwner(_ownerGUID, "anotherOwner", "notExiststore")); //non exist store
            Assert.False(_storeManagement.removeOwner(_ownerGUID, "permitManager", "store")); //try to remove not owner user(manager)
            Assert.False(_storeManagement.removeOwner(_ownerGUID, "ownerAssignedByAnotherOwner", "store")); //try to owner that was not assigned by "_owner"


            Assert.True(_storeManagement.removeOwner(_ownerGUID, "anotherOwner", "store"));
            //check that the another owner and the owner that assined by abother owner dont have permissions to the store
            _anotherOwner = _userManagement.getUserByName("anotherOwner");
            Assert.IsNull(_anotherOwner.getPermission("store"));
            ownerAssignedByAnotherOwner = _userManagement.getUserByName("ownerAssignedByAnotherOwner");
            Assert.IsNull(ownerAssignedByAnotherOwner.getPermission("store"));
            Assert.IsFalse(_store.StorePermissions.ContainsKey("anotherOwner"));
            Assert.IsFalse(_store.StorePermissions.ContainsKey(ownerAssignedByAnotherOwner.Name));

            _owner = _userManagement.getUserByName("owner");
            //check that owner dont have other owner as asignee
            Assert.IsFalse(_owner.getAssigneesOfStore("store").Contains(_anotherOwnerGUID));
            _anotherOwner = _userManagement.getUserByName("anotherOwner");
            //check that another owner dont have any assignees
            Assert.IsNull(_anotherOwner.getAssigneesOfStore("store"));


        }

        [Test()]
        public void assignManagerByPermitedUserTest()
        {
            Assert.True(_storeManagement.assignManager(_ownerGUID, "newManager", "store"), "Fail to assign regular user as new manager");
            Assert.False(_storeManagement.assignManager(_ownerGUID, "newManager", "store"), "Assign already manager user as new manager successed");
            _userManagement.logout(_ownerGUID);
        }

        [Test()]
        public void assignOwnerByUnPermitedUserTest()
        {
            _userManagement.login("regularUser", "pA55word");
            Assert.AreEqual(Guid.Empty, _storeManagement.createOwnerAssignAgreement(_regularUserGUID, "anotherOwner", "store"), "Assign anotherOwner wich is regular user as owner by another regular user successed");
            _userManagement.logout(_regularUserGUID);
            _userManagement.login("nonPermitManager", "pA55word");
            Assert.AreEqual(Guid.Empty, _storeManagement.createOwnerAssignAgreement(_nonPermitManagerGUID, "anotherOwner", "store"), "Assign regular user as owner by manager with default permissions successed");
            _userManagement.logout(_nonPermitManagerGUID);
            _userManagement.login("permitManager", "pA55word");
            Assert.AreEqual(Guid.Empty, _storeManagement.createOwnerAssignAgreement(_permitManagerGUID, "anotherOwner", "store"), "Assign regular user as owner by manager with full permissions successed");
            _userManagement.logout(_permitManagerGUID);
        }

        [Test()]
        public void assignOwnerWithoutAgreemntNeedByPermitedUserTest()
        {
            Assert.AreNotEqual(Guid.Empty, _storeManagement.createOwnerAssignAgreement(_ownerGUID, "anotherOwner", "store"), "Fail to assign regular user as new owner");
            Assert.False(_storeManagement.assignManager(_ownerGUID, "anotherOwner", "store"), "Assign already owner user as new manager successed");
            _storeManagement.removeOwner(_ownerGUID, "anotherOwner", "store");
            _userManagement.logout(_ownerGUID);
        }

        [Test()]
        public void assignOwnerWithAgreemntByPermitedUserTest()
        {


            Assert.AreNotEqual(Guid.Empty, _storeManagement.createOwnerAssignAgreement(_ownerGUID, "anotherOwner3", "store"));

            _userManagement.register("ownerAssignedByAnotherOwner", "pA55word", "fname", "lname", "owner@gmail.com");
            Guid disapproveAgreementID = _storeManagement.createOwnerAssignAgreement(_ownerGUID, "ownerAssignedByAnotherOwner", "store");
            Assert.AreNotEqual(Guid.Empty, disapproveAgreementID);

            //cant create agree when there is open agree for this user and store
            Assert.AreEqual(Guid.Empty, _storeManagement.createOwnerAssignAgreement(_ownerGUID, "ownerAssignedByAnotherOwner", "store"));

            //disapprove

            Assert.IsTrue(_storeManagement.disApproveAssignOwnerRequest(_anotherOwner3GUID, disapproveAgreementID, "store"));
            Assert.IsNull(_store.getAgreementByID(disapproveAgreementID)); //agreement revmoved after disapprove
            Assert.IsFalse(_store.getOWners().Contains("ownerAssignedByAnotherOwner"));
            User ownerAssignedByAnotherOwner = _userManagement.getUserByName("ownerAssignedByAnotherOwner");

            Assert.IsNull(ownerAssignedByAnotherOwner.getPermission("store"));


            //approve
            Guid approveAgreemntID = _storeManagement.createOwnerAssignAgreement(_ownerGUID, "ownerAssignedByAnotherOwner", "store");
            Assert.IsTrue(_storeManagement.approveAssignOwnerRequest(_anotherOwner3GUID, approveAgreemntID, "store"));
            Assert.IsNull(_store.getAgreementByID(approveAgreemntID)); //agreement revmoved after approval
            Assert.IsTrue(_store.getOWners().Contains("ownerAssignedByAnotherOwner"));
            ownerAssignedByAnotherOwner= _userManagement.getUserByName("ownerAssignedByAnotherOwner");
            Assert.IsNotNull(ownerAssignedByAnotherOwner.getPermission("store"));
            Assert.IsTrue(ownerAssignedByAnotherOwner.getPermission("store").isOwner());

            _storeManagement.removeOwner(_ownerGUID, "anotherOwner", "store");
            _storeManagement.removeOwner(_ownerGUID, "ownerAssignedByAnotherOwner", "store");
        }


        

        // *************PURCHASE POLICY TESTS********* //
        [Test()]
        public void addCompositePurchasePolicyTest()
        {
            //try to add with not exist policy
           

            Assert.AreEqual(Guid.Empty, _storeManagement.addAndPurchasePolicy(_ownerGUID,"store", _banIranPolicyID, Guid.NewGuid()));

            //add first xor
            Guid xorPolicy1 = _storeManagement.addXorPurchasePolicy(_ownerGUID, "store", _banIranPolicyID, _banIraqPolicyID);

            //check that the simple policies exist
            _store = _storeManagement.getStoreByName("store");
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIranPolicyID));
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIraqPolicyID));

            Assert.IsTrue(_store.canBuy(null, _requireTotalPrice + 1, "iran"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice + 1, "iran iraq"));
            Assert.IsFalse(_store.canBuy(null, _requireTotalPrice - 1, "iran"));



            //add second xor

            Guid xorPolicy2 = _storeManagement.addXorPurchasePolicy(_ownerGUID, "store",_banIranPolicyID, _banEgyptPolicyID);
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
            _store = _storeManagement.getStoreByName("store");

            //remove first level simple policy
            Assert.IsNotNull(_store.PurchasePolicy.getByID(_banIranPolicyID));
            _storeManagement.removePurchasePolicy(_ownerGUID,"store",  _banIranPolicyID);
            Assert.IsNull(_store.PurchasePolicy.getByID(_banIranPolicyID));


            //remove composite from first level
            Guid andPolicy = _store.addOrPurchasePolicy(_banEgyptPolicyID, _banIraqPolicyID);
            _storeManagement.removePurchasePolicy(_ownerGUID, "store", _banIraqPolicyID);
            Assert.IsNull(_store.PurchasePolicy.getByID(_banIraqPolicyID));
            _storeManagement.removePurchasePolicy(_ownerGUID, "store", andPolicy);
            Assert.IsNull(_store.PurchasePolicy.getByID(andPolicy));
            Assert.IsNotNull(_banEgyptPolicyID);

        }

        // *************DISCOUNT TESTS**************** //

        [Test()]
        public void addVisibleDiscountTest()
        {

            Assert.AreEqual(Guid.Empty, _storeManagement.addVisibleDiscount(_ownerGUID, "store",Guid.NewGuid(), 10, DateTime.Today.AddDays(10))); // not exist product id
            Assert.AreEqual(Guid.Empty, _storeManagement.addVisibleDiscount(_ownerGUID, "store", _productID1, -1, DateTime.Today.AddDays(10))); // negative percetage
            Assert.AreEqual(Guid.Empty, _storeManagement.addVisibleDiscount(_ownerGUID, "store", _productID1, 10, DateTime.Today.AddDays(-1))); // illegal date

            Guid visibleDiscountP1 = _storeManagement.addVisibleDiscount(_ownerGUID, "store", _productID1, 10, DateTime.Today.AddDays(10));
            _store = _storeManagement.getStoreByName("store");

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsTrue(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            Assert.AreEqual(Guid.Empty, _store.addVisibleDiscount(_productID1, 10, DateTime.Today.AddDays(10))); //cant add discount to product with discount

        }


        [Test()]
        public void addCondiotionalProcuctDiscountTest()
        {

            Assert.AreEqual(Guid.Empty, _storeManagement.addCondiotionalProcuctDiscount(_ownerGUID, "store",Guid.NewGuid(), 10, DateTime.Today.AddDays(10), 10)); //not exist product ID
            Assert.AreEqual(Guid.Empty, _storeManagement.addCondiotionalProcuctDiscount(_ownerGUID, "store", _productID2, 10, DateTime.Today.AddDays(10), -1)); // negative required quantity
            Assert.AreEqual(Guid.Empty, _storeManagement.addCondiotionalProcuctDiscount(_ownerGUID, "store", _productID2, -1, DateTime.Today.AddDays(10), 10)); // negative percentage
            Assert.AreEqual(Guid.Empty, _storeManagement.addCondiotionalProcuctDiscount(_ownerGUID, "store", _productID2, 20, DateTime.Today.AddDays(-1), 2)); // illeget date


            Guid conditionalProductP2 = _storeManagement.addCondiotionalProcuctDiscount(_ownerGUID, "store", _productID2, 20, DateTime.Today.AddDays(10), 2);

            Assert.AreNotEqual(Guid.Empty, conditionalProductP2);

            _store = _storeManagement.getStoreByName("store");

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsTrue(_store.NotInTreeDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            Assert.AreEqual(Guid.Empty, _store.addCondiotionalProcuctDiscount(_productID2, 20, DateTime.Today.AddDays(10), 2)); //cant add discount to product with discount

        }


        [Test()]
        public void addConditionalStoreDiscount()
        {

            Assert.AreEqual(Guid.Empty, _storeManagement.addConditionalStoreDiscount(_ownerGUID, "store", -1, DateTime.Today.AddDays(10), 200)); //negative percentage
            Assert.AreEqual(Guid.Empty, _storeManagement.addConditionalStoreDiscount(_ownerGUID, "store", 10, DateTime.Today.AddDays(-10), 200)); //illegal date
            Assert.AreEqual(Guid.Empty, _storeManagement.addConditionalStoreDiscount(_ownerGUID, "store", 10, DateTime.Today.AddDays(10), -50)); //negative required price


            Guid conditionalStore = _storeManagement.addConditionalStoreDiscount(_ownerGUID, "store",10, DateTime.Today.AddDays(10), 200);

            _store = _storeManagement.getStoreByName("store");

            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(conditionalStore));
            Assert.IsNotNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));
        }

        [Test()]
        public void addCompositeDiscountPolicyTest()
        {
            Guid visibleDiscountP1 = _storeManagement.addVisibleDiscount(_ownerGUID, "store", _productID1, 10, DateTime.Today.AddDays(10));
            Guid conditionalProductP2 = _storeManagement.addCondiotionalProcuctDiscount(_ownerGUID, "store", _productID2, 20, DateTime.Today.AddDays(10), 2);
            Guid visibleDiscountP3 = _storeManagement.addVisibleDiscount(_ownerGUID, "store", _productID3, 30, DateTime.Today.AddDays(10));
            Guid conditionalStore = _storeManagement.addConditionalStoreDiscount(_ownerGUID, "store", 10, DateTime.Today.AddDays(10), 200);


            //try to add with not exist policy
            Assert.AreEqual(Guid.Empty, _storeManagement.addAndDiscountPolicy(_ownerGUID, "store", new List<Guid>() { Guid.NewGuid() }));


            //add store level discount in composite discount is forbidden
            Assert.AreEqual(Guid.Empty, _storeManagement.addAndDiscountPolicy(_ownerGUID, "store", new List<Guid>() { visibleDiscountP3, conditionalStore }));


            _store = _storeManagement.getStoreByName("store");

            //check the exist of visibleDiscountP3
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsTrue(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP3));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(visibleDiscountP3));

            //check the exist of conditionalStore
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalStore));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(conditionalStore));
            Assert.IsNotNull(_store.StoreLevelDiscounts.getByID(conditionalStore));
            Assert.IsNull(_store.DiscountPolicyTree.getByID(conditionalStore));



            //add XOR discount between P1 and P2 discounts
            Guid XORDiscountP1P2 = _storeManagement.addXorDiscountPolicy(_ownerGUID, "store", new List<Guid>() { visibleDiscountP1, conditionalProductP2 });
            _store = _storeManagement.getStoreByName("store");

            //check the exist of visibleDiscountP1
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            //check the exist of conditionalProductP2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            //check the exist of XORDiscountP1P2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(XORDiscountP1P2));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(XORDiscountP1P2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XORDiscountP1P2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(XORDiscountP1P2));


            //add two level composite discount
            Guid XOrDiscountXORP1P2OrP3 = _storeManagement.addXorDiscountPolicy(_ownerGUID, "store", new List<Guid>() { XORDiscountP1P2, visibleDiscountP3 });
            _store = _storeManagement.getStoreByName("store");

            //check the exist of visibleDiscountP1
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP1));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP1));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP1));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(visibleDiscountP1));

            //check the exist of conditionalProductP2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(conditionalProductP2));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(conditionalProductP2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(conditionalProductP2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(conditionalProductP2));

            //check the exist of visibleDiscountP3
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(visibleDiscountP3));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(visibleDiscountP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(visibleDiscountP3));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(visibleDiscountP3));

            //check the exist of XORDiscountP1P2
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(XORDiscountP1P2));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(XORDiscountP1P2));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XORDiscountP1P2));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(XORDiscountP1P2));
            Assert.IsFalse(_store.DiscountPolicyTree.Children.Contains(_store.DiscountPolicyTree.getByID(XORDiscountP1P2))); //check that the xor isn`t exist in the first level of the tree

            //check the exist of OrDiscountXORP1P2OrP3
            Assert.IsTrue(_store.AllDiscountsMap.ContainsKey(XOrDiscountXORP1P2OrP3));
            Assert.IsFalse(_store.NotInTreeDiscounts.ContainsKey(XOrDiscountXORP1P2OrP3));
            Assert.IsNull(_store.StoreLevelDiscounts.getByID(XOrDiscountXORP1P2OrP3));
            Assert.IsNotNull(_store.DiscountPolicyTree.getByID(XOrDiscountXORP1P2OrP3));
        }


        [Test()]
        public void removeProductDiscountTest()
        {
            _store = _storeManagement.getStoreByName("store");
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
            _store = _storeManagement.getStoreByName("store");
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
            _store = _storeManagement.getStoreByName("store");
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
            _store = _storeManagement.getStoreByName("store");
            _price = 10;
            int min200StoreDiscountPercentage = 10;
            int min500StoreDiscountPercentage = 20;

            //modify price to sync this test that came from storeTest
            _storeManagement.modifyProductPrice(_ownerGUID, "store", _productName, 10);

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

            _storeManagement.modifyProductPrice(_ownerGUID, "store", _productName, 100);
            _price = 100;
        }
    }
}