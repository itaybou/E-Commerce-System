using NUnit.Framework;
using System;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.1.1
    [TestFixture()]
    internal class AddProductInvTest : StoreOwnerTests
    {
        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
        }

        [TestCase()]
        public void addProductSuccess()
        {
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreNotEqual(-1, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords));
            _bridge.deleteProductInv(_storeName, _productName);
            _bridge.logout();
        }

        [TestCase()]
        public void addProductFail()
        {
            //Guest try to add productInv
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords), "guest add product inventory successed");

            //Regular user try to add productInv
            _bridge.login(_userName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords), "regulat user add product inventory successed");
            _bridge.logout();

            //not permited manager try to add productInv
            _bridge.login(_managerUserName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords), "not permited manager add product inventory successed");
            _bridge.logout();

            //wrong args
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv("not exist", _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords), "add product to not exist store name successed");
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, -5, _quantity, _category, _keywords), "add product with negative price successed");
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, -5, _category, _keywords), "add product with negative quantity successed");
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, -5, _purchaseType, _price, -5, _category, _keywords), "add product with negative discount percentage successed");

            //add same product twice
            _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords);
            Assert.AreEqual(Guid.Empty, _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords));
            _bridge.deleteProductInv(_storeName, _productName); //delete the added product
            _bridge.logout();
        }
    }
}