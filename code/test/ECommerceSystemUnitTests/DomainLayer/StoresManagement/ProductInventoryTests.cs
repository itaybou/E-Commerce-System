using ECommerceSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement.Tests
{
    [TestFixture()]
    public class ProductInventoryTests
    {
        private string productName = "Iphone", description = "description";
        private PurchaseType purchaseType = new ImmediatePurchase();
        private double price = 100;
        private int quantity = 5;
        private Category category = Category.CELLPHONES;
        private List<string> keywords = new List<string>();
        private Guid productID;
        private Guid productInvID = Guid.NewGuid();
        private ProductInventory productInv;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            productInv = ProductInventory.Create(productName, description, price, quantity, category, keywords);
            productID = productInv.ProductList.First().Id;
        }

        [TearDown]
        public void tearDown()
        {
            productInv.ProductList.Clear();
        }

        [Test()]
        public void CreateTest()
        {
            Assert.NotNull(ProductInventory.Create(productName, description, price, quantity, category, keywords));
            Assert.Null(ProductInventory.Create(productName, description, -5, quantity, category, keywords)); // price < 0 null
            Assert.Null(ProductInventory.Create(productName, description, price, -5, category, keywords)); // quantity > 0 null
        }

        [Test()]
        public void modifyProductQuantityTest()
        {
            Assert.AreEqual(5, productInv.getProducByID(productID).Quantity, "modify product to negative quantity successed");
            Assert.AreEqual(5, productInv.getProducByID(productID).Quantity, "modify non exist id product");
            Assert.True(productInv.modifyProductQuantity(productID, 3));
            Assert.AreEqual(3, productInv.getProducByID(productID).Quantity);
        }

        [Test()]
        public void modifyProductQuantityFailingTest()
        {
            Assert.False(productInv.modifyProductQuantity(productID, -2), "modify product to negative quantity successed"); // negative quantity
            Assert.False(productInv.modifyProductQuantity(Guid.NewGuid(), 3), "modify non exist id product"); // Id procuct isn`t exist
        }

        [Test()]
        public void deleteProductTest()
        {
            Assert.False(productInv.deleteProduct(Guid.NewGuid()), "delete non exist id product"); // non exist id product
            Guid guid = productInv.addProduct(10, 50);
            Assert.True(productInv.deleteProduct(guid), "Fail to delete exist product");
            Assert.Null(productInv.getProducByID(guid), "Fail to delete exist product");
        }

        [Test()]
        public void deleteProductThatDoesntExistTest()
        {
            Assert.False(productInv.deleteProduct(Guid.NewGuid()), "delete non exist id product"); // non exist id product
        }

        [Test()]
        public void addProductTest()
        {
            Assert.AreEqual(Guid.Empty, productInv.addProduct(-1, 50), "Add new product with negative quantity successed");
            Assert.AreEqual(Guid.Empty, productInv.addProduct(10, -2), "Add new product with negative price successed");

            Guid guid = productInv.addProduct(10, 50);
            Assert.AreNotEqual(Guid.Empty, guid);
            Assert.NotNull(productInv.getProducByID(guid), "Fail to add a product");
        }

        //[Test()]
        //public void modifyProductDiscountTypeTest()
        //{
        //    Discount newDiscount = new VisibleDiscount(10, new DiscountPolicy());

        //    Assert.False(productInv.modifyProductDiscountType(productIDCounter, null), "Modify discount to null discount successed");
        //    Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to null discount successed");// check that the discount didnt changed
        //    Assert.False(productInv.modifyProductDiscountType(Guid.NewGuid(), newDiscount), "Modify discount to non exist id product successed");
        //    Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to non exist id product successed"); // check that the discount didnt changed
        //    Assert.True(productInv.modifyProductDiscountType(productIDCounter, newDiscount), "Fail to modify discount type");
        //    Assert.AreEqual(newDiscount, productInv.getProducByID(productIDCounter).Discount, "Fail to modify discount type");
        //}

        //[Test()]
        //public void modifyProductPurchaseTypeTest()
        //{
        //    PurchaseType newPurchase = new ImmediatePurchase();
        //    Assert.False(productInv.modifyProductPurchaseType(productIDCounter, null), "Modify discount to null discount successed");
        //    Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to null discount successed");// check that the purchase type didnt changed
        //    Assert.False(productInv.modifyProductPurchaseType(Guid.NewGuid(), newPurchase), "Modify discount to null discount successed");
        //    Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to null discount successed");// check that the purchase type didnt changed
        //    Assert.True(productInv.modifyProductPurchaseType(productIDCounter, newPurchase), "Fail to modify discount type");
        //    Assert.AreEqual(newPurchase, productInv.getProducByID(productIDCounter).PurchaseType, "Fail to modify discount type");
        //}

        //[Test()]
        //public void rateProductTest()
        //{
        //    productInv.rateProduct(5.0);
        //    Assert.AreEqual(5.0, productInv.Rating);
        //    productInv.rateProduct(2.0);
        //    Assert.AreEqual(3.5, productInv.Rating);
        //}
    }
}