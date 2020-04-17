using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.store_owner_requirments
{
    // Requirment 4.1.3
    [TestFixture()]

    class ModifyProductTest : StoreOwnerTests
    {
        long _iphoneFirstGroupProductsID;

        [OneTimeSetUp]
        public new void oneTimeSetup()
        {
            base.oneTimeSetup();
            _iphoneFirstGroupProductsID = _bridge.addProductInv(_storeName, _productName, _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords);
        }

        [TestCase()]
        public void modifyProductAsGuest()
        {
            Assert.AreEqual(-1, _bridge.addProduct(_storeName, _productName, _discontType, _discountPercentage, _purchaseType, 50), "add product group as guest success");
            Assert.False(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "delete product group as guest success");
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "new product name"), "modify product name as guest success");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, 500), "delete product price as guest success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group as guest success");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, _productName, _iphoneFirstGroupProductsID, "visible", 15), "modify discount type of product group as guest success");
            Assert.False(_bridge.modifyProductPurchaseType(_storeName, _productName, _iphoneFirstGroupProductsID, "immediate"), "modify purchase type of product group as guest success");
        }


        [TestCase()]
        public void modifyProductAsRegularUser()
        {
            _bridge.login(_userName, _pswd);
            Assert.AreEqual(-1, _bridge.addProduct(_storeName, _productName, _discontType, _discountPercentage, _purchaseType, 50), "add product group as regular user success");
            Assert.False(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "delete product group as regular user success");
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "new product name"), "modify product name as regular user success");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, 500), "delete product price as regular user success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group as regular user success");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, _productName, _iphoneFirstGroupProductsID, "visible", 15), "modify discount type of product group as regular user success");
            Assert.False(_bridge.modifyProductPurchaseType(_storeName, _productName, _iphoneFirstGroupProductsID, "immediate"), "modify purchase type of product group as regular user success");
            _bridge.logout():
        }


        [TestCase()]
        public void modifyProductAsNotPermitedManager()
        {
            _bridge.login(_managerUserName, _pswd);
            Assert.AreEqual(-1, _bridge.addProduct(_storeName, _productName, _discontType, _discountPercentage, _purchaseType, 50), "add product group as not permited manager success");
            Assert.False(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "delete product group as not permited manager success");
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "new product name"), "modify product name as not permited manager success");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, 500), "delete product price as not permited manager success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group asnot permited manager success");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, _productName, _iphoneFirstGroupProductsID, "visible", 15), "modify discount type of product group as not permited manager success");
            Assert.False(_bridge.modifyProductPurchaseType(_storeName, _productName, _iphoneFirstGroupProductsID, "immediate"), "modify purchase type of product group as not permited manager success");
            _bridge.logout():
        }

        [TestCase()]
        public void modifyNotExistProduct()
        {
            string notExistProduct = "not exist";
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreNotEqual(-1, _bridge.addProduct(_storeName, notExistProduct, _discontType, _discountPercentage, _purchaseType, 50), "add product group of not exist product success");
            Assert.False(_bridge.deleteProduct(_storeName, notExistProduct, _iphoneFirstGroupProductsID), "delete product group of not exist product success");
            Assert.False(_bridge.modifyProductName(_storeName, notExistProduct, "new product name"), "modify product name of not exist product success");
            Assert.False(_bridge.modifyProductPrice(_storeName, notExistProduct, 500), "delete product price of not exist product success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, notExistProduct, _iphoneFirstGroupProductsID, 50), "modify quantity of product group of not exist product success");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, notExistProduct, _iphoneFirstGroupProductsID, "visible", 15), "modify discount type of product group of not exist product success");
            Assert.False(_bridge.modifyProductPurchaseType(_storeName, notExistProduct, _iphoneFirstGroupProductsID, "immediate"), "modify purchase type of product group of not exist product success");
            _bridge.logout():
        }

        [TestCase()]
        public void modifyNotExistStore()
        {
            string notExistStore = "not exist";
            _bridge.login(_ownerUserName, _pswd);
            Assert.AreEqual(-1, _bridge.addProduct(notExistStore, _productName, _discontType, _discountPercentage, _purchaseType, 50), "add product group with with not exist store success");
            Assert.False(_bridge.deleteProduct(notExistStore, _productName, _iphoneFirstGroupProductsID), "delete product group with with not exist store success");
            Assert.False(_bridge.modifyProductName(notExistStore, _productName, "new product name"), "modify product name with with not exist store success");
            Assert.False(_bridge.modifyProductPrice(notExistStore, _productName, 500), "delete product price with with not exist store success");
            Assert.False(_bridge.modifyProductQuantity(notExistStore, _productName, _iphoneFirstGroupProductsID, 50), "modify quantity of product group with with not exist store success");
            Assert.False(_bridge.modifyProductDiscountType(notExistStore, _productName, _iphoneFirstGroupProductsID, "visible", 15), "modify discount type of product group with with not exist store success");
            Assert.False(_bridge.modifyProductPurchaseType(notExistStore, _productName, _iphoneFirstGroupProductsID, "immediate"), "modify purchase type of product group with with not exist store success");
            _bridge.logout();
        }

        [TestCase()]
        public void modifyNotExistGroupProductsID()
        {
            long notExistGroupProductsID = 10;
            _bridge.login(_ownerUserName, _pswd);
            Assert.False(_bridge.deleteProduct(_storeName, _productName, notExistGroupProductsID), "delete product group as with not exist id success");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, notExistGroupProductsID, 50), "modify quantity of product group with not exist id success");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, _productName, notExistGroupProductsID, "visible", 15), "modify discount type of product group with not exist id success");
            Assert.False(_bridge.modifyProductPurchaseType(_storeName, _productName, notExistGroupProductsID, "immediate"), "modify purchase type of product group with not exist id success");
            _bridge.logout();
        }

        [TestCase()]
        public void modifyProductAsOwner()
        {
            _bridge.login(_ownerUserName, _pswd);

            //add product
            Assert.AreNotEqual(-1, _bridge.addProduct(_storeName, _productName, _discontType, _discountPercentage, _purchaseType, 50), "fail to add product group ");
            Assert.AreEqual(-1, _bridge.addProduct(_storeName, _productName, _discontType, _discountPercentage, _purchaseType, -5), "add product group with with negative quantity success");

            //delete product:
            Assert.True(_bridge.deleteProduct(_storeName, _productName, _iphoneFirstGroupProductsID), "fail to delete product group ");
            //re add the product
            _iphoneFirstGroupProductsID = _bridge.addProduct(_storeName, _productName, _discontType, _discountPercentage, _purchaseType, 50);

            //modify name:
            Assert.True(_bridge.modifyProductName(_storeName, "new name", _productName), "fail modify product name");
            //re modify the name to the original name
            _bridge.modifyProductName(_storeName, _productName, "new name");
            _bridge.addProductInv(_storeName, "second product name", _description, _discontType, _discountPercentage, _purchaseType, _price, _quantity, _category, _keywords);
            Assert.False(_bridge.modifyProductName(_storeName, _productName, "second product name"), "modify name of product with exist name success");
            _bridge.deleteProductInv(_storeName, "second product name");

            //modify price
            Assert.True(_bridge.modifyProductPrice(_storeName, _productName, 500), "fail to modify price");
            Assert.False(_bridge.modifyProductPrice(_storeName, _productName, -5), "modify to negative price successed");

            //modify quantity
            Assert.True(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, 50), "fail to modify quantity");
            Assert.False(_bridge.modifyProductQuantity(_storeName, _productName, _iphoneFirstGroupProductsID, -5), "modify to negative quantity successed");

            //modify discout type
            Assert.True(_bridge.modifyProductDiscountType(_storeName, _productName, _iphoneFirstGroupProductsID, "visible", 15), "fail to modify discount type");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, _productName, _iphoneFirstGroupProductsID, "visible", -5), "modify to negative discount percentage successed");
            Assert.False(_bridge.modifyProductDiscountType(_storeName, _productName, _iphoneFirstGroupProductsID, "not exist discount", 15), "modify to not exist discount type successed");

            //modify discout type
            Assert.True(_bridge.modifyProductPurchaseType(_storeName, _productName, _iphoneFirstGroupProductsID, "immediate"), "fail to modify purchase type");
            Assert.False(_bridge.modifyProductPurchaseType(_storeName, _productName, _iphoneFirstGroupProductsID, "not exist purchase type"), "modify to not exist purchase type successed");


            _bridge.logout();
        }



    }
}
