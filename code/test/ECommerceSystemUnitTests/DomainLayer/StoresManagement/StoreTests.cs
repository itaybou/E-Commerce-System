using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
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
        Discount _discount = new VisibleDiscount(10, new DiscountPolicy());
        PurchaseType _purchaseType = new ImmediatePurchase();
        double _price = 100;
        int _quantity = 5;
        Category _category = Category.CELLPHONES;
        List<string> _keywords = new List<string>();
        long _productID = 1;
        long _productInvID = 0;

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
            _regularUser = new User(new Subscribed("regularUser", "123456", "fname", "lname", "owner@gmail.com"));
            _permitManager = new User(new Subscribed("permitManager", "123456", "fname", "lname", "owner@gmail.com"));
            _nonPermitManager = new User(new Subscribed("nonPermitManager", "123456", "fname", "lname", "owner@gmail.com"));
            _owner = new User(new Subscribed("owner", "123456", "fname", "lname", "owner@gmail.com"));
            _anotherOwner = new User(new Subscribed("anotherOwner", "123456", "fname", "lname", "email@gmail.com"));
            _newManager = new User(new Subscribed("newManager", "123456", "fname", "lname", "email@gmail.com"));



            _userManagement = UsersManagement.Instance;
            _systemManagement = SystemManager.Instance; 
            _userManagement.register("owner", "123456", "fname", "lname", "owner@gmail.com");
            _userManagement.register("nonPermitManager", "123456", "fname", "lname", "owner@gmail.com");
            _userManagement.register("permitManager", "123456", "fname", "lname", "owner@gmail.com");
            _userManagement.register("regularUser", "123456", "fname", "lname", "owner@gmail.com");
            _userManagement.login("owner", "123456");

            // make the managers permissions


            _keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            _store = new Store(new DiscountPolicy(), new PurchasePolicy(), "owner", "store");
            _store.Inventory.addProductInv(_productName, _description, _discount, _purchaseType, _price, _quantity, _category, _keywords, _productInvID);


            _store.assignOwner(_owner, "anotherOwner");
            //_store.assignManager(_owner, "newManager");
            _store.assignManager(_owner, "nonPermitManager");

            //give permit manager the permissions
            _store.assignManager(_owner, "permitManager");
            List<permissionType> permissions = new List<permissionType>();
            permissions.Add(permissionType.AddProductInv);
            permissions.Add(permissionType.DeleteProductInv);
            permissions.Add(permissionType.ModifyProduct);
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
        public void addProductInvTest()
        {
            Assert.AreEqual(-1, _store.addProductInv("regularUser" ,"Galaxy" , _description, _discount, _purchaseType, _price, _quantity,
                         _category, _keywords, 2), "Add productInv successed while the user isn`t owner/manager");
            Assert.AreEqual(-1, _store.addProductInv("nonPermitManager", "Galaxy", _description, _discount, _purchaseType, _price, _quantity,
                        _category, _keywords, 2), "Add productInv successed while the user is manager without permission");

            Assert.AreNotEqual(-1, _store.addProductInv("owner", "Galaxy", _description, _discount, _purchaseType, _price, _quantity,
                         _category, _keywords, 2), "Fail to add productInv by the owner"); 
            Assert.AreNotEqual(-1, _store.addProductInv("permitManager", "Galaxy2", _description, _discount, _purchaseType, _price, _quantity,
                         _category, _keywords, 3), "Fail to add productInv by permited manager");
        }

        [Test()]
        public void addProductTest()
        {
            Assert.AreEqual(-1, _store.addProduct("regularUser", _productName, _discount, _purchaseType, 20),
                                "Add group of products successed while the user isn`t owner/manager");
            Assert.AreEqual(-1, _store.addProduct("nonPermitManager", _productName, _discount, _purchaseType, 20),
                    "Add group of products successed while the user is manager without permission");

            Assert.AreNotEqual(-1, _store.addProduct("permitManager", _productName, _discount, _purchaseType, 20),
                    "Fail to add group of products by permited manager");
            Assert.AreNotEqual(-1, _store.addProduct("owner", _productName, _discount, _purchaseType, 20),
                    "Fail to add group of products by the owner");

        }

        [Test()]
        public void deleteProductInventoryTest()
        {
            Assert.False(_store.deleteProductInventory("regularUser", _productName),
                    "Delete product inventory successed while the user isn`t owner/manager");
            Assert.False(_store.deleteProductInventory("nonPermitManager", _productName),
                    "Delete product inventory successed while the user is manager without permission");

            Assert.True(_store.deleteProductInventory("permitManager", _productName),
                    "Fail to delete product inventory by permited manager");

            //re add the deleted product
            _store.Inventory.addProductInv(_productName, _description, _discount, _purchaseType, _price, _quantity, _category, _keywords, _productInvID);

            Assert.True(_store.deleteProductInventory("owner", _productName),
                    "Fail to delete product inventory by the owner");
        }

        [Test()]
        public void deleteProductTest()
        {
            Assert.False(_store.deleteProduct("regularUser", _productName, _productID),
                    "Delete group of products successed while the user isn`t owner/manager");
            Assert.False(_store.deleteProduct("nonPermitManager", _productName, _productID),
                    "Delete group of products successed while the user is manager without permission");

            Assert.True(_store.deleteProduct("permitManager", _productName, _productID),
                    "Fail to delete group of products by permited manager");

            //re add the deleted product
            _store.Inventory.addProduct(_productName, _discount, _purchaseType, _quantity);
            Assert.True(_store.deleteProduct("owner", _productName, 2),
                    "Fail to delete group of products by the owner");
        }

        [Test()]
        public void modifyProductPriceTest()
        {
            Assert.False(_store.modifyProductPrice("regularUser", _productName, 200),
                    "Modify price of product inventory successed while the user isn`t owner/manager");
            Assert.False(_store.modifyProductPrice("nonPermitManager", _productName, 200),
                    "Modify price of product inventory successed while the user is manager without permission");

            Assert.True(_store.modifyProductPrice("permitManager", _productName, 200),
                    "Fail to modify price of product inventory by permited manager");

            Assert.True(_store.modifyProductPrice("owner", _productName, 300),
                    "Fail to modify price of product inventory by the owner");
        }

        [Test()]
        public void modifyProductDiscountTypeTest()
        {
            Discount newDis = new VisibleDiscount(20, new DiscountPolicy());

            Assert.False(_store.modifyProductDiscountType("regularUser", _productName, _productID, newDis),
                    "Modify discount of group of products successed while the user isn`t owner/manager");
            Assert.False(_store.modifyProductDiscountType("nonPermitManager", _productName, _productID, newDis),
                    "Modify discount of group of products successed while the user is manager without permission");

            Assert.True(_store.modifyProductDiscountType("permitManager", _productName, _productID, newDis),
                    "Fail to modify discount of group of products by permited manager");
            Assert.True(_store.modifyProductDiscountType("owner", _productName, _productID, newDis),
                    "Fail to modify discount of group of products by the owner");
        }

        [Test()]
        public void modifyProductPurchaseTypeTest()
        {
            PurchaseType newPurchaseType = new ImmediatePurchase();

            Assert.False(_store.modifyProductPurchaseType("regularUser", _productName, _productID, newPurchaseType),
                    "Modify purchase type of group of products successed while the user isn`t owner/manager");
            Assert.False(_store.modifyProductPurchaseType("nonPermitManager", _productName, _productID, newPurchaseType),
                    "Modify purchase type of group of products successed while the user is manager without permission");

            Assert.True(_store.modifyProductPurchaseType("permitManager", _productName, _productID, newPurchaseType),
                    "Fail to modify purchase type of group of products by permited manager");
            Assert.True(_store.modifyProductPurchaseType("owner", _productName, _productID, newPurchaseType),
                    "Fail to modify purchase type of group of products by the owner");
        }

        [Test()]
        public void modifyProductQuantityTest()
        {
            Assert.False(_store.modifyProductQuantity("regularUser", _productName, _productID, 20),
                    "Modify quantity of group of products successed while the user isn`t owner/manager");
            Assert.False(_store.modifyProductQuantity("nonPermitManager", _productName, _productID, 20),
                    "Modify quantity of group of products successed while the user is manager without permission");

            Assert.True(_store.modifyProductQuantity("permitManager", _productName, _productID, 20),
                    "Fail to modify quantity of group of products by permited manager");
            Assert.True(_store.modifyProductQuantity("owner", _productName, _productID, 30),
                    "Fail to modify quantity of group of products by the owner");
        }

        [Test()]
        public void modifyProductNameTest()
        {
            Assert.False(_store.modifyProductName("regularUser", "Galaxy", _productName),
                    "Modify name of product inventory successed while the user isn`t owner/manager");
            Assert.False(_store.modifyProductName("nonPermitManager", "Galaxy", _productName),
                    "Modify name of product inventory successed while the user is manager without permission");

            Assert.True(_store.modifyProductName("permitManager", "Galaxy", _productName),
                    "Fail to modify name of product inventory by permited manager");
            Assert.True(_store.modifyProductName("owner", "Galaxy2", "Galaxy"),
                    "Fail to modify name of product inventory by the owner");
        }

        [Test()]
        public void assignOwnerTest()
        {
            User newOwner = new User(new Subscribed("newOwner", "123456", "fname", "lname", "email@gmail.com"));
            _userManagement.register("newOwner", "123456", "fname", "lname", "email@gmail.com");

            Assert.False(_store.assignOwner(_regularUser, "newOwner"), "Assign regular user as owner by another regular user successed");
            Assert.False(_store.assignOwner(_nonPermitManager, "newOwner"), "Assign regular user as owner by manager with default permissions successed");
            Assert.False(_store.assignOwner(_permitManager, "newOwner"), "Assign regular user as owner by manager with full permissions successed");
           
            Assert.True(_store.assignOwner(_owner, "newOwner"), "Fail to assign regular user as new owner");
            Assert.True(_store.getPermissionByName("newOwner").isOwner(), "Fail to assign regular user as new owner");
            Assert.AreEqual(_owner, _store.getPermissionByName("newOwner").AssignedBy, "The user who assign the reg user as owner isn`t the assignee ");

            Assert.False(_store.assignOwner(_anotherOwner, "newOwner"), "Assign already owner user as owner by another owner successed");
        }

        [Test()]
        public void assignManagerTest()
        {
            Assert.False(_store.assignManager(_regularUser, "newManager"), "Assign regular user as manager by another regular user successed");
            Assert.False(_store.assignManager(_nonPermitManager, "newManager"), "Assign regular user as manager by manager with default permissions successed");
            Assert.False(_store.assignManager(_permitManager, "newManager"), "Assign regular user as manager by manager with full permissions successed");


            Assert.True(_store.assignManager(_owner, "newManager"), "Fail to assign regular user as new owner");
            //check defult permissions:
            Assert.True(_store.getPermissionByName("newManager").canWatchAndomment(), "Assign new manager successed, but the manager dont have permission to watch and comment");
            Assert.True(_store.getPermissionByName("newManager").canWatchPurchaseHistory(), "Assign new manager successed, but the manager dont have permission to watch purchase history");
            Assert.False(_store.getPermissionByName("newManager").canAddProduct(), "Assign new manager successed, but the manager have permission to add product");
            Assert.False(_store.getPermissionByName("newManager").canDeleteProduct(), "Assign new manager successed, but the manager have permission to delete product");
            Assert.False(_store.getPermissionByName("newManager").canModifyProduct(), "Assign new manager successed, but the manager have permission to modify product");
            Assert.AreEqual(_owner, _store.getPermissionByName("newManager").AssignedBy, "The user who assign the reg user as manager isn`t the assignee");

            Assert.False(_store.assignManager(_owner, "newManager"), "Assign already manager user as new manager successed");
            Assert.False(_store.assignManager(_anotherOwner, "newManager"), "Assign already manager as manager by another owner successed");
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

            List<permissionType> emptyPermissions = new List<permissionType>();

            List<permissionType> fewPermissions = new List<permissionType>();
            fewPermissions.Add(permissionType.AddProductInv);
            fewPermissions.Add(permissionType.DeleteProductInv);
            fewPermissions.Add(permissionType.WatchAndComment);


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
        public void purchaseHistory()
        {
            StorePurchase purchase = new StorePurchase(_regularUser, 80.0, new List<Product>(){ new Product(_discount, _purchaseType, _quantity, _price, 1) });
            _store.PurchaseHistory.Add(purchase);

            List<StorePurchase> expected = new List<StorePurchase>();
            expected.Add(purchase);

            //succcess:
            Assert.AreEqual(expected, _store.purchaseHistory(_owner), "fail to view store history");
            Assert.AreEqual(expected, _store.purchaseHistory(_permitManager), "fail to view store history");

            User admin = new User(new SystemAdmin("admind", "123456", "fname", "lname", "email"));
            Assert.AreEqual(expected, _store.purchaseHistory(admin), "fail to view store history");

            //fail:

            Assert.Null(_store.purchaseHistory(_regularUser), "view history of a store successed with regular user");
            Assert.Null(_store.purchaseHistory(new User(new Guest())), "view history of a store successed with guest");
        }


        [Test()]
        public void rateStoreTest()
        {
            _store.rateStore(6.0);
            Assert.AreEqual(5.0, _store.Rating);
            _store.rateStore(2.0);
            Assert.AreEqual(3.5, _store.Rating);
        }

        [Test()]
        public void logPurchaseTest()
        {
            List<Product> products = _store.Inventory.Products.ElementAt(0).ProductList; // = {"Iphone"}
            _store.logPurchase(new StorePurchase(_regularUser, 1000, products));
            Assert.True(true);
        }
    }
}