using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Tests
{
    [TestFixture()]
    public class ProductInventoryTests
    {

        string productName = "Iphone", description = "description";
        Discount discount = new VisibleDiscount(10, new DiscountPolicy());
        PurchaseType purchaseType = new ImmediatePurchase();
        double price = 100;
        int quantity = 5;
        Category category = Category.CELLPHONES;
        List<string> keywords = new List<string>();
        Guid productIDCounter = Guid.NewGuid();
        Guid productInvID = Guid.NewGuid();
        ProductInventory productInv;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            keywords.Add("phone");
        }

        [SetUp]
        public void setUp()
        {
            productInv = ProductInventory.Create(productName, description, discount, purchaseType, price, quantity, category, keywords, productIDCounter, productInvID);
        }

        [TearDown]
        public void tearDown()
        {
            productInv.ProductList.Clear();
        }

        [Test()]
        public void CreateTest()
        {
            Assert.NotNull(ProductInventory.Create(productName, description, discount, purchaseType, price, quantity, category, keywords, productIDCounter, productInvID));
            Assert.Null(ProductInventory.Create(productName, description, null, purchaseType, price, quantity, category, keywords, productIDCounter, productInvID)); // discount null
            Assert.Null(ProductInventory.Create(productName, description, discount, null, price, quantity, category, keywords, productIDCounter, productInvID)); // purchase typ null
            Assert.Null(ProductInventory.Create(productName, description, discount, purchaseType, -5, quantity, category, keywords, productIDCounter, productInvID)); // price < 0 null
            Assert.Null(ProductInventory.Create(productName, description, discount, purchaseType, price, -5, category, keywords, productIDCounter, productInvID)); // quantity > 0 null
        }

        [Test()]
        public void modifyProductQuantityTest()
        {
            Assert.False(productInv.modifyProductQuantity(productIDCounter, -2), "modify product to negative quantity successed"); // negative quantity
            Assert.AreEqual(5, productInv.getProducByID(productIDCounter).Quantity, "modify product to negative quantity successed");
            Assert.False(productInv.modifyProductQuantity(Guid.NewGuid(), 3), "modify non exist id product"); // Id procuct isn`t exist
            Assert.AreEqual(5, productInv.getProducByID(productIDCounter).Quantity, "modify non exist id product");
            Assert.True(productInv.modifyProductQuantity(productIDCounter, 3));
            Assert.AreEqual(3, productInv.getProducByID(productIDCounter).Quantity);
        }

        [Test()]
        public void deleteProductTest()
        {
            Assert.False(productInv.deleteProduct(Guid.NewGuid()), "delete non exist id product"); // non exist id product
            Assert.NotNull(productInv.getProducByID(productIDCounter), "non exist id product deleted"); 
            Assert.True(productInv.deleteProduct(productIDCounter), "Fail to delete exist product");
            Assert.Null(productInv.getProducByID(productIDCounter), "Fail to delete exist product");
        }

        [Test()]
        public void addProductTest()
        {
            Assert.False(productInv.addProduct(null, purchaseType, 10, 50, Guid.NewGuid()), "Add new product with null discount successed");
            Assert.False(productInv.addProduct(discount, null, 10, 50, Guid.NewGuid()), "Add new product with null purchaseType successed");
            Assert.False(productInv.addProduct(discount, purchaseType, -1, 50, Guid.NewGuid()), "Add new product with negative quantity successed");
            Assert.False(productInv.addProduct(discount, purchaseType, 10, -2, Guid.NewGuid()), "Add new product with negative price successed");
            Assert.False(productInv.addProduct(discount, purchaseType, 10, 50, productIDCounter), "Add new product with exist id successed");

            Guid guid = Guid.NewGuid();
            Assert.True(productInv.addProduct(discount, purchaseType, 10, 50, guid));
            Assert.NotNull(productInv.getProducByID(guid), "Fail to add a product");
        }

        //[Test()]
        //public void modifyProductDiscountTypeTest()
        //{
        //    Discount newDiscount = new VisibleDiscount(10, new DiscountPolicy());

            Assert.False(productInv.modifyProductDiscountType(productIDCounter, null), "Modify discount to null discount successed");
            Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to null discount successed");// check that the discount didnt changed
            Assert.False(productInv.modifyProductDiscountType(Guid.NewGuid(), newDiscount), "Modify discount to non exist id product successed");
            Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to non exist id product successed"); // check that the discount didnt changed
            Assert.True(productInv.modifyProductDiscountType(productIDCounter, newDiscount), "Fail to modify discount type");
            Assert.AreEqual(newDiscount, productInv.getProducByID(productIDCounter).Discount, "Fail to modify discount type");
        }

        [Test()]
        public void modifyProductPurchaseTypeTest()
        {
            PurchaseType newPurchase = new ImmediatePurchase();
            Assert.False(productInv.modifyProductPurchaseType(productIDCounter, null), "Modify discount to null discount successed");
            Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to null discount successed");// check that the purchase type didnt changed
            Assert.False(productInv.modifyProductPurchaseType(Guid.NewGuid(), newPurchase), "Modify discount to null discount successed");
            Assert.AreEqual(discount, productInv.getProducByID(productIDCounter).Discount, "Modify discount to null discount successed");// check that the purchase type didnt changed
            Assert.True(productInv.modifyProductPurchaseType(productIDCounter, newPurchase), "Fail to modify discount type");
            Assert.AreEqual(newPurchase, productInv.getProducByID(productIDCounter).PurchaseType, "Fail to modify discount type");
        }

        [Test()]
        public void rateProductTest()
        {
            productInv.rateProduct(5.0);
            Assert.AreEqual(5.0, productInv.Rating);
            productInv.rateProduct(2.0);
            Assert.AreEqual(3.5, productInv.Rating);
        }

    }
}