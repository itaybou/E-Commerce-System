using NUnit.Framework;
using System;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.1.3
    [TestFixture()]
    internal class ModifyProductTest : StoreOwnerTests
    {
        private Guid _iphoneFirstGroupProductsID;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _bridge.login(_ownerUserName, _pswd);
            _iphoneFirstGroupProductsID =_bridge.addProductInv(_storeName,_description,_producInvName, _price, _quantity, _category, _keywords, _minQuantity, _maxQuantity, _imageURL);
            _bridge.logout();
        }

        [TestCase()]
        public void modifyProductAsGuest()
        {
            Assert.AreEqual(Guid.Empty, _bridge.addProduct(_storeName, _producInvName2, _quantity, _minQuantity, _maxQuantity), "add product group as guest success");
            Assert.False(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "delete product group as guest success");
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "new product name"), "modify product name as guest success");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, 500), "delete product price as guest success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group as guest success");

        }

        [TestCase()]
        public void modifyProductAsRegularUser()
        {
            _bridge.login(_userName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProduct(_storeName, _producInvName, _quantity, _minQuantity, _maxQuantity), "add product group as regular user success");
            Assert.False(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "delete product group as regular user success");
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "new product name"), "modify product name as regular user success");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, 500), "delete product price as regular user success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group as regular user success");
            _bridge.logout();
        }

        [TestCase()]
        public void modifyProductAsNotPermitedManager()
        {
            _bridge.login(_managerUserName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProduct(_storeName, _producInvName, _quantity, _minQuantity, _maxQuantity), "add product group as not permited manager success");
            Assert.False(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "delete product group as not permited manager success");
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "new product name"), "modify product name as not permited manager success");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, 500), "delete product price as not permited manager success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group asnot permited manager success");
            _bridge.logout();
        }

        [TestCase()]
        public void modifyNotExistProduct()
        {
            string notExistProduct = "not exist";
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProduct(_storeName, notExistProduct, _quantity, _minQuantity, _maxQuantity), "add product group of not exist product success");
            Assert.False(_bridge.deleteProduct(_storeName, notExistProduct, _iphoneFirstGroupProductsID), "delete product group of not exist product success");
            Assert.False(_bridge.modifyProductName(_storeName, notExistProduct, "new product name"), "modify product name of not exist product success");
            Assert.False(_bridge.modifyProductPrice(_storeName, notExistProduct, 500), "delete product price of not exist product success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, notExistProduct, _iphoneFirstGroupProductsID, 50), "modify quantity of product group of not exist product success");
            _bridge.logout();
        }

        [TestCase()]
        public void modifyNotExistStore()
        {
            string notExistStore = "not exist";
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreEqual(Guid.Empty, _bridge.addProduct(notExistStore, _producInvName, _quantity, _minQuantity, _maxQuantity), "add product group with with not exist store success");
            Assert.False(_bridge.deleteProduct(notExistStore, _productName, _iphoneFirstGroupProductsID), "delete product group with with not exist store success");
            Assert.False(_bridge.modifyProductName(notExistStore, _productName, "new product name"), "modify product name with with not exist store success");
            Assert.False(_bridge.modifyProductPrice(notExistStore, _productName, 500), "delete product price with with not exist store success");
            Assert.False(_bridge.modifyProductQuantity(notExistStore, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group with with not exist store success");
            
            _bridge.logout();
        }

        [TestCase()]
        public void modifyNotExistGroupProductsID()
        {
            Guid notExistGroupProductsID = Guid.NewGuid();
            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.deleteProduct(_storeName, _productName, notExistGroupProductsID), "delete product group as with not exist id success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, notExistGroupProductsID, 50), "modify quantity of product group with not exist id success");
            _bridge.logout();
        }

        [TestCase()]
        public void modifyProductAsOwner()
        {
            
            _bridge.login(_ownerUserName, _pswd);

            //add product
            Assert.AreNotEqual(Guid.Empty, _bridge.addProduct(_storeName, _producInvName, 50 ,_minQuantity,_maxQuantity), "fail to add product group ");
            Assert.AreEqual(Guid.Empty, _bridge.addProduct(_storeName, _productName, -5, _minQuantity, _maxQuantity), "add product group with with negative quantity success");

            //delete product:
            Assert.True(_bridge.deleteProduct(_storeName, _producInvName, _iphoneFirstGroupProductsID), "fail to delete product group ");
            //re add the product
            _iphoneFirstGroupProductsID = _bridge.addProduct(_storeName, _producInvName, 50 , _minQuantity, _maxQuantity);

            //modify name:
            Assert.True(_bridge.modifyProductName(_storeName, "new name", _producInvName), "fail modify product name");
            //re modify the name to the original name
            _bridge.modifyProductName(_storeName, _producInvName, "new name");
            _bridge.addProductInv(_storeName, _description, "second product name", _price, _quantity, _category, _keywords,_minQuantity, _maxQuantity, _imageURL);
            Assert.False(_bridge.modifyProductName(_storeName, _producInvName, "second product name"), "modify name of product with exist name success");
            _bridge.deleteProductInv(_storeName, "second product name");

            //modify price
            Assert.True(_bridge.modifyProductPrice(_storeName, _producInvName, 500), "fail to modify price");
            Assert.False(_bridge.modifyProductPrice(_storeName, _producInvName, -5), "modify to negative price successed");

            //modify quantity
            Assert.True(_bridge.modifyProductQuantity(_storeName, _producInvName, _iphoneFirstGroupProductsID, 50), "fail to modify quantity");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _producInvName, _iphoneFirstGroupProductsID, -5), "modify to negative quantity successed");

            _bridge.logout();
        }
    }
}