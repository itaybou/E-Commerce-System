using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.10 and 6.4.2
    [TestFixture()]
    class StorePurchaseHistoryTest : StoreOwnerTests
    {

        Guid _productID;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _bridge.login(_ownerUserName, _pswd);
            _productID =_bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords);
            _bridge.logout();

            _bridge.login(_userName, _pswd);
            bool susccess =_bridge.PurchaseProducts(new Dictionary<Guid, int>() { {_productID, 1} }, _fname, _lname, "123456789", "1234123412341234", DateTime.Today.ToString(), "123", "address");
            _bridge.logout();
        }

        [TestCase()]
        public void storePurchaseHistorySuccess()
        {

            List<Tuple<Guid, int>> products = new List<Tuple<Guid, int>>(); // product id --> quantity
            products.Add(Tuple.Create(_productID, 1));
            List<Tuple<string, List<Tuple<Guid, int>>, double>> expectedHistory = new List<Tuple<string, List<Tuple<Guid, int>>, double>>();
            expectedHistory.Add(Tuple.Create(_userName, products, 80.0)); //user, product list, price

            //owner
            _bridge.login(_ownerUserName, _pswd);
            CollectionAssert.AreEquivalent(expectedHistory, _bridge.StorePurchaseHistory(_storeName), "fail to return purchase history of store");
            _bridge.logout();

            //permited manager
            _bridge.login(_managerUserName, _pswd);
            CollectionAssert.AreEquivalent(expectedHistory, _bridge.StorePurchaseHistory(_storeName), "fail to return purchase history of store");
            _bridge.logout();


            //system admin
            _bridge.login("admin", "4dMinnn");
            CollectionAssert.AreEquivalent(expectedHistory, _bridge.StorePurchaseHistory(_storeName), "fail to return purchase history of store");
            _bridge.logout();
        }


        [TestCase()]
        public void storePurchaseHistoryFail()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.Null(_bridge.StorePurchaseHistory("not exist"), "purchase history of not exist store successed");
            _bridge.logout();


            Assert.Null(_bridge.StorePurchaseHistory(_storeName), "purchase history with guest successed");
            _bridge.login(_userName, _pswd);
            Assert.Null(_bridge.StorePurchaseHistory(_storeName), "purchase history with regular user successed");
            _bridge.logout();
        }


    }
}
