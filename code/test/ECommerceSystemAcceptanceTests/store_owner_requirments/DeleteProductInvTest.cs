using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{

    // Requirment 4.1.2
    [TestFixture()]
    class DeleteProductInvTest : StoreOwnerTests
    {

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _bridge.login(_ownerUserName, _pswd);
            //long success = _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords);
            _bridge.logout();
        }

        [OneTimeTearDown]
        public new void tearDown()
        {
            _bridge.storesCleanUp();
            _bridge.usersCleanUp();
        }

        [TestCase()]
        public void deleteProductSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.True(_bridge.deleteProductInv(_storeName, _productName), "Fail to delete product");
            _bridge.logout();
        }

        [TestCase()]
        public void deleteProductFail()
        {
            //not permited try do delete:
            Assert.False(_bridge.deleteProductInv(_storeName, _productName), "delete product by guest success");
            _bridge.login(_userName, _pswd);
            Assert.False(_bridge.deleteProductInv(_storeName, _productName), "delete product by regular user success");
            _bridge.logout();
            _bridge.login(_managerUserName, _pswd);
            Assert.False(_bridge.deleteProductInv(_storeName, _productName), "delete product by not permited manager success");
            _bridge.logout();


            //not exist product and store:
            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.deleteProductInv(_storeName, "not exist"), "delete not exist product in store success");
            Assert.False(_bridge.deleteProductInv("not exist", _productName), "delete not exist product in store success");
            _bridge.logout();
        }

    }
}
