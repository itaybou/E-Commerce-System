using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Models;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.TransactionManagement;
using ECommerceSystemUnitTests.DomainLayer.SystemManagement;
using System.Threading;

namespace ECommerceSystem.DomainLayer.SystemManagement.Tests
{
    [TestFixture()]
    public class SystemManagerTests
    {
        private SystemManager _systemManager;
        private UsersManagement _userManagement;
        private StoreManagement _storeManagement;
        private IExternalSupplyPayment externalStub;
        private Store _store1, _store2;
        private double _totalPrice;
        private Dictionary<Product, int> _allProducts;
        private ICollection<(Store, double, IDictionary<Product, int>)> _storeProducts;
        private string _firstName = "fname", _lastName = "lname", _address = "address,city,country,zip", _creditCardNumber = "413-547";
        private int _id = 54362432, _CVV = 300;
        private DateTime _expirationCreditCard = DateTime.Now.AddDays(2.0);
        private Product product1;
        private Product product2;
        private Product product3;
        private Product product4;
        private Guid _prod1ID;
        private Guid _prod2ID;
        private Guid _prod3ID;
        private Guid _prod4ID;

        private Guid _userID;
        private Guid _userID2;
        private Guid _userID3;
        private Guid _userID4;
        private Guid _userID5;
        private Guid _owner1ID;
        private Guid _owner2ID;


        [OneTimeSetUp]
        public void setUpFixture()
        {
            DataAccess.Instance.SetTestContext();

            //To make pay fail you should use the pay method with null card number
            //To make supply fail you should use the supply method with empty fname and lname
            externalStub = new ExternalSystemsStub();
            TransactionManager.Instance.setTestExternalSystems(externalStub);

            _systemManager = SystemManager.Instance;
            _userManagement = UsersManagement.Instance;
            _storeManagement = StoreManagement.Instance;
            _userManagement.register("user1", "pA55word", "user", "last", "mail@mail");
            _userManagement.register("user2", "pA55word", "user", "last", "mail@mail");
            _userManagement.register("owner1", "pA55word", "user", "last", "mail@mail");
            _userManagement.register("owner2", "pA55word", "user", "last", "mail@mail");
            _userManagement.register("user3", "pA55word", "user", "last", "mail@mail");
            _userManagement.register("user4", "pA55word", "user", "last", "mail@mail");
            _userManagement.register("user5", "pA55word", "user", "last", "mail@mail");
            _userID = _userManagement.getUserByName("user1").Guid;
            _userID2 = _userManagement.getUserByName("user2").Guid;
            _userID3 = _userManagement.getUserByName("user3").Guid;
            _userID4 = _userManagement.getUserByName("user4").Guid;
            _userID5 = _userManagement.getUserByName("user5").Guid;
            _owner1ID = _userManagement.getUserByName("owner1").Guid;
            _owner2ID = _userManagement.getUserByName("owner2").Guid;

            _storeManagement.openStore(_owner1ID, "store1");
            _storeManagement.openStore(_owner2ID, "store2");
        }

        [SetUp]
        public void setUp()
        {
            _prod1ID = _storeManagement.addProductInv(_owner1ID, "store1", "d", "prod1", 25.0, 20, Category.ART, new List<string>(), -1, -1, "");
            _prod2ID = _storeManagement.addProductInv(_owner1ID, "store1", "d", "prod2", 10.0, 20, Category.ART, new List<string>(), -1, -1, "");
            _prod3ID = _storeManagement.addProductInv(_owner2ID, "store2", "d", "prod3", 12.5, 20, Category.ART, new List<string>(), -1, -1, "");
            _prod4ID = _storeManagement.addProductInv(_owner2ID, "store2", "d", "prod4", 50.0, 20, Category.ART, new List<string>(), -1, -1, "");

            _store1 = _storeManagement.getStoreByName("store1");
            _store2 = _storeManagement.getStoreByName("store2");

            product1 = _store1.Inventory.getProductByName("prod1").getProducByID(_prod1ID);
            product2 = _store1.Inventory.getProductByName("prod2").getProducByID(_prod2ID);
            product3 = _store2.Inventory.getProductByName("prod3").getProducByID(_prod3ID);
            product4 = _store2.Inventory.getProductByName("prod4").getProducByID(_prod4ID);


        }

        [TearDown]
        public void tearDown()
        {
            _userManagement.resetUserShoppingCart(_userID);
            _userManagement.resetUserShoppingCart(_userID2);
            _userManagement.resetUserShoppingCart(_userID3);
            _userManagement.resetUserShoppingCart(_userID4);
            _userManagement.resetUserShoppingCart(_userID5);
            _storeManagement.deleteProductInventory(_owner1ID, "store1", "prod1");
            _storeManagement.deleteProductInventory(_owner1ID, "store1", "prod2");
            _storeManagement.deleteProductInventory(_owner2ID, "store2", "prod3");
            _storeManagement.deleteProductInventory(_owner2ID, "store2", "prod4");
        }

        [OneTimeTearDown]
        public void tearDownFixture()
        {
            TransactionManager.Instance.setRealExternalSystems();
            DataAccess.Instance.DropTestDatabase();
        }

        public void purchaseUserShoppingCartTestSuccess()
        {
            _userManagement.addProductToCart(_userID, _prod1ID, "store1", 18);
            _userManagement.addProductToCart(_userID, _prod2ID, "store1", 15);
            _userManagement.addProductToCart(_userID, _prod3ID, "store2", 12);
            _userManagement.addProductToCart(_userID, _prod4ID, "store2", 9);

            var ans = _systemManager.purchaseUserShoppingCart(_userID, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.IsNull(ans.Result);
            Assert.AreEqual(product1.Quantity, 2);
            Assert.AreEqual(product2.Quantity, 5);
            Assert.AreEqual(product3.Quantity, 8);
            Assert.AreEqual(product4.Quantity, 11);
        }

        [Test()]
        public void purchaseUserShoppingCartCheckStorePurchaseHistoryUpdateAfterPurchaseTest()
        {
            purchaseUserShoppingCartTestSuccess();
            double totalPriceStore1 = (product1.BasePrice * 18) + (product2.BasePrice * 15);
            double totalPriceStore2 = (product3.BasePrice * 12) + (product4.BasePrice * 9);
            Assert.AreEqual(totalPriceStore1, _store1.PurchaseHistory.ElementAt(_store1.PurchaseHistory.Count() - 1).TotalPrice);
            Assert.AreEqual("user1", _store1.PurchaseHistory.ElementAt(_store1.PurchaseHistory.Count() - 1).Username);
            Assert.AreEqual("user1", _store2.PurchaseHistory.ElementAt(_store2.PurchaseHistory.Count() - 1).Username);
            Assert.True(_store1.PurchaseHistory.ElementAt(_store1.PurchaseHistory.Count() - 1).ProductsPurchased.ToList().Exists(p => p.Id.Equals(product1.Id)));
            Assert.True(_store1.PurchaseHistory.ElementAt(_store1.PurchaseHistory.Count() - 1).ProductsPurchased.ToList().Exists(p => p.Id.Equals(product2.Id)));
            Assert.True(_store2.PurchaseHistory.ElementAt(_store2.PurchaseHistory.Count() - 1).ProductsPurchased.ToList().Exists(p => p.Id.Equals(product3.Id)));
            Assert.True(_store2.PurchaseHistory.ElementAt(_store2.PurchaseHistory.Count() - 1).ProductsPurchased.ToList().Exists(p => p.Id.Equals(product4.Id)));
        }

        [Test()]
        public void purchaseUserShoppingCartCheckUserPurchaseHistoryUpdateAfterPurchaseTest()
        {
            purchaseUserShoppingCartTestSuccess();
            double totalPriceStore = (product1.BasePrice * 18) + (product2.BasePrice * 15) + (product3.BasePrice * 12) + (product4.BasePrice * 9);
            User user = _userManagement.getUserByName("user1");
            UserPurchase purchase = ((Subscribed)user.State).PurchaseHistory.Last();
            

            Assert.AreEqual(totalPriceStore, purchase.TotalPrice);
            Assert.True(purchase.ProductsPurchased.Exists(p => p.Id.Equals(product1.Id)));
            Assert.True(purchase.ProductsPurchased.Exists(p => p.Id.Equals(product2.Id)));
            Assert.True(purchase.ProductsPurchased.Exists(p => p.Id.Equals(product3.Id)));
            Assert.True(purchase.ProductsPurchased.Exists(p => p.Id.Equals(product4.Id)));
        }


        [Test()]
        public void purchaseUserShoppingCartWithExceededProductQuantityTest()
        {
            //userID cart:
            _userManagement.addProductToCart(_userID, _prod1ID, "store1", 18);
            _userManagement.addProductToCart(_userID, _prod2ID, "store1", 15);
            _userManagement.addProductToCart(_userID, _prod3ID, "store2", 12);
            _userManagement.addProductToCart(_userID, _prod4ID, "store2", 9);

            //userID cart:
            _userManagement.addProductToCart(_userID2, _prod1ID, "store1", 1);
            _userManagement.addProductToCart(_userID2, _prod2ID, "store1", 4);
            _userManagement.addProductToCart(_userID2, _prod3ID, "store2", 10); //exceed (10 + 12) > 20
            _userManagement.addProductToCart(_userID2, _prod4ID, "store2", 9);

            //first user buy his cart should success
            var ans = _systemManager.purchaseUserShoppingCart(_userID, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.IsNull(ans.Result);
            var ans2 = _systemManager.purchaseUserShoppingCart(_userID2, _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
            ans2.Wait();
            List<ProductModel> unavailablePRoducts = ans2.Result;
            Console.WriteLine("prod1 = " + product1.Id);
            Console.WriteLine("prod2 = " + product2.Id);
            Console.WriteLine("prod3 = " + product3.Id);
            Console.WriteLine("prod4 = " + product4.Id);
            Console.WriteLine(unavailablePRoducts.ElementAt(0).Id);
            Assert.AreEqual(product3.Id, unavailablePRoducts.ElementAt(0).Id);

            Assert.AreEqual(product1.Quantity, 2);
            Assert.AreEqual(product2.Quantity, 5);
            Assert.AreEqual(product3.Quantity, 8);
            Assert.AreEqual(product4.Quantity, 11);
        }

        [Test()]
        public void purchaseUserShoppingCartFailToPayTest()
        {
            //To make pay fail you should use the pay method with null card number
            _userManagement.addProductToCart(_userID, _prod1ID, "store1", 18);
            _userManagement.addProductToCart(_userID, _prod2ID, "store1", 15);
            _userManagement.addProductToCart(_userID, _prod3ID, "store2", 12);
            _userManagement.addProductToCart(_userID, _prod4ID, "store2", 9);
            var ans = _systemManager.purchaseUserShoppingCart(_userID, _firstName, _lastName, _id, null, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.IsNotNull(ans.Result);
            Assert.AreEqual(0, ans.Result.Count);

            Assert.AreEqual(product1.Quantity, 20);
            Assert.AreEqual(product2.Quantity, 20);
            Assert.AreEqual(product3.Quantity, 20);
            Assert.AreEqual(product4.Quantity, 20);

        }

        [Test()]
        public void purchaseUserShoppingCartFailToSupplyTest()
        {
            int oldCancelPayCount = ((ExternalSystemsStub)externalStub).CancelPayCounter;
            //To make supply fail you should use the supply method with empty fname and lname
            _userManagement.addProductToCart(_userID, _prod1ID, "store1", 18);
            _userManagement.addProductToCart(_userID, _prod2ID, "store1", 15);
            _userManagement.addProductToCart(_userID, _prod3ID, "store2", 12);
            _userManagement.addProductToCart(_userID, _prod4ID, "store2", 9);
            var ans = _systemManager.purchaseUserShoppingCart(_userID, "", "", _id, _address, _expirationCreditCard, _CVV, _address);
            ans.Wait();
            Assert.IsNotNull(ans.Result);
            Assert.AreEqual(0, ans.Result.Count);
            Assert.AreEqual(oldCancelPayCount + 1, ((ExternalSystemsStub)externalStub).CancelPayCounter);
            Assert.AreEqual(product1.Quantity, 20);
            Assert.AreEqual(product2.Quantity, 20);
            Assert.AreEqual(product3.Quantity, 20);
            Assert.AreEqual(product4.Quantity, 20);
        }

        [Test()]
        public void FewUsersPurchaseUserShoppingCartWithOneProductParallelTest()
        {
            int oldPayCount = ((ExternalSystemsStub)externalStub).PayCounter;

            //add products to all users carts
            _userManagement.addProductToCart(_userID, _prod1ID, "store1", 8);
            _userManagement.addProductToCart(_userID2, _prod1ID, "store1", 8);
            _userManagement.addProductToCart(_userID3, _prod1ID, "store1", 8);
            _userManagement.addProductToCart(_userID4, _prod1ID, "store1", 8);
            _userManagement.addProductToCart(_userID5, _prod1ID, "store1", 8);

            

            Guid[] usersID = {_userID, _userID2, _userID3, _userID4, _userID5};
            Thread[] threads = new Thread[5];
            for(int i = 0; i < 5; i++)
            {
                Thread t = new Thread(() =>
                { 
                    var ans = _systemManager.purchaseUserShoppingCart(usersID[i], _firstName, _lastName, _id, _creditCardNumber, _expirationCreditCard, _CVV, _address);
                    ans.Wait();
                });
                threads[i] = t;
                t.Start();
            }
            
            for(int i = 0; i < 5; i++)
            {
                threads[i].Join();
            }
            //only two users shouold success to make the purchase;
            Assert.AreEqual(4, product1.Quantity);
            Assert.AreEqual(oldPayCount + 2, ((ExternalSystemsStub)externalStub).PayCounter);
        }

    }
}