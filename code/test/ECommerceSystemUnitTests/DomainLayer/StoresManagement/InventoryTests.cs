using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.Tests
{
    [TestFixture()]
    public class InventoryTests
    {
        string productName = "Iphone", description = "description";
        PurchaseType purchaseType = new ImmediatePurchase();
        double price = 100;
        int quantity = 5;
        Category category = Category.CELLPHONES;
        List<string> keywords = new List<string>();
        private Guid productID;
        private Guid productInvID;
        Inventory inventory;
        private ProductInventory pInv;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            inventory = new Inventory();
            pInv = ProductInventory.Create(productName, description, price, quantity, category, keywords);
            productInvID = pInv.ID;
            productID = pInv.ProductList.First().Id;
            inventory.Products.Add(pInv);
        }

        [TearDown]
        public void tearDown()
        {
            inventory.Products.Clear();
        }

        [Test()]
        public void addProductInvNotSuccessTest()
        {
            Assert.AreEqual(Guid.Empty, inventory.addProductInv("Galaxy", "samsung", 100, 100, Category.ELECTRONICS, keywords), "Add productInv with null discount to inventory successed"); //discount null 
            Assert.AreEqual(Guid.Empty, inventory.addProductInv("Galaxy", "samsung",  100, 100, Category.ELECTRONICS, keywords), "Add productInv with null purchase type to inventory successed"); //purchase type null
            Assert.AreEqual(Guid.Empty, inventory.addProductInv("Galaxy", "samsung",  -5, 100, Category.ELECTRONICS, keywords), "Add productInv with negative price to inventory successed"); //negative price
            Assert.AreEqual(Guid.Empty, inventory.addProductInv("Galaxy", "samsung",  100, -5, Category.ELECTRONICS, keywords), "Add productInv with negative quantity to inventory successed"); //negative quantity
            Assert.AreEqual(Guid.Empty, inventory.addProductInv(productName, "samsung", 100, 100, Category.ELECTRONICS, keywords), "Add productInv with already exist name to inventory successed"); //exist name
        }

        [Test()]
        public void addProductInvSuccessTest()
        {
            Assert.AreNotEqual(Guid.Empty, inventory.addProductInv("Galaxy", "samsung",  100, 100, Category.ELECTRONICS, keywords), "Failed to add productInv to inventory");
        }

        [Test()]
        public void deleteProductInventoryTest()
        {
            Assert.NotNull(inventory.getProductByName(productName), "Delete productInv when not supposed to"); 
            Assert.True(inventory.deleteProductInventory(productName), "Fail to delete product inv");
            Assert.Null(inventory.getProductByName(productName), "Didnt delete productInv when supposed to");
        }

        [Test()]
        public void deleteProductThatNotExistsTest()
        {
            Assert.False(inventory.deleteProductInventory("fail"), "Delete non exist productInv successed");

        }

        [Test()]
        public void modifyProductNameTest()
        {
            Assert.False(inventory.modifyProductName("Galaxy", "Iphone2"), "Modify name for non exist product successed");
            Assert.False(inventory.modifyProductName("", productName), "Modify to empty name for product successed");
            Assert.True(inventory.modifyProductName("Galaxy", productName), "Failed to modify product name");
            Assert.AreEqual("Galaxy", inventory.Products.ElementAt(0).Name, "Failed to modify product name");
        }

        [Test()]
        public void modifyProductPriceTest()
        {
            Assert.True(inventory.modifyProductPrice(productName, 500), "Failed to modify product price");
            Assert.AreEqual(500,inventory.getProductByName(productName).Price, "Failed to modify product price");
        }

        [TestCase()]
        public void modifyProductPriceFailingTest()
        {
            Assert.False(inventory.modifyProductPrice(productName, -5), "Modify price to negative for product successed");
            Assert.False(inventory.modifyProductPrice(productName, 0), "Modify price to 0 for product successed");
            Assert.False(inventory.modifyProductPrice("Galaxy", 20), "Modify price for non exist product successed");

        }

        [Test()]
        public void modifyProductQuantityTest()
        {

            Assert.True(inventory.modifyProductQuantity(productName, productID, 30), "Failed to modify product quantity");
            Assert.AreEqual(30, inventory.getProductByName(productName).getProducByID(productID).Quantity, "Failed to modify product quantity");
        }

        [Test()]
        public void modifyProductQuantityFailingTest()
        {
            Assert.False(inventory.modifyProductQuantity(productName, productID, -5), "Modify quantity to negative for product successed");
            Assert.False(inventory.modifyProductQuantity(productName, productID, 0), "Modify quantity to 0 for product successed");
            Assert.False(inventory.modifyProductQuantity("Galaxy", productID, 20), "Modify quantity for non exist product successed");
        }

        [Test()]
        public void addProductTest()
        {
            Assert.AreNotEqual(Guid.Empty, inventory.addProduct(productName, 50), "Failed to add new group of products to productInv");
            Assert.AreEqual(2, inventory.getProductByName(productName).ProductList.Count, "Didnt add new product group when suppused to");
            Assert.AreEqual(Guid.Empty, inventory.addProduct("Galaxy", 50), "Add group of products for non exist to productInv successed");
            Assert.AreEqual(Guid.Empty, inventory.addProduct(productName, -1), "Add group of products with negative quantity to productInv successed");
            Assert.AreEqual(Guid.Empty, inventory.addProduct(productName, 0), "Add group of products with quantity 0 to productInv successed");
        }

        [Test()]
        public void deleteProductTest()
        {
            Assert.True(inventory.deleteProduct(productName, productID), "Fail to delete group of products from productInv");
            Assert.Null(inventory.getProductByName(productName).getProducByID(productID), "Didnt delete group of products from productInv when supposed to");
        }

        [Test()]
        public void deleteNonExistingProduct()
        {
            Assert.False(inventory.deleteProduct(productName, Guid.NewGuid()), "Delete non existing id group of products seccessed");
            Assert.False(inventory.deleteProduct("Galaxy", productID), "Delete non existing productInv seccessed");
        }

        //[Test()]
        //public void modifyProductDiscountTypeTest()
        //{
        //    Discount newDis = new VisibleDiscount(20, new DiscountPolicy());

            //Assert.False(inventory.modifyProductDiscountType(productName, productID, new VisibleDiscount(-5, new DiscountPolicy())), "Modify discount to negative percent successed");
            //Assert.False(inventory.modifyProductDiscountType("Samsung", productID, newDis), "Modify discount for nonexist productInv name successed");
            //Assert.False(inventory.modifyProductDiscountType(productName, Guid.NewGuid(), newDis), "Modify discount for nonexist group of products id successed");

        //    Assert.True(inventory.modifyProductDiscountType(productName, productID, newDis), "Fail to modify discount of product group");
        //    Assert.AreEqual(newDis, inventory.getProductByName(productName).getProducByID(productID).Discount, "Discount type didnt modified when its supposed to");


        //}

        //[Test()]
        //public void modifyProductPurchaseTypeTest()
        //{
        //    PurchaseType newPurchase = new ImmediatePurchase();

            //Assert.False(inventory.modifyProductPurchaseType("Samsung", productID, newPurchase), "Modify purchase type for nonexist productInv name successed");
            //Assert.False(inventory.modifyProductPurchaseType(productName, Guid.NewGuid(), newPurchase), "Modify purchase type for nonexist group of products id successed");

        //    Assert.True(inventory.modifyProductPurchaseType(productName, productID, newPurchase), "Fail to modify purchase type of product group");
        //    Assert.AreEqual(newPurchase, inventory.getProductByName(productName).getProducByID(productID).PurchaseType, "Purchase type didnt modified when its supposed to");
        //}

    }
}