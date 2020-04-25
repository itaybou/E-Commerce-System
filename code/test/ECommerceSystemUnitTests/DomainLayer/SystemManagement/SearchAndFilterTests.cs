using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.Utilities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.SystemManagement.Tests
{
    [TestFixture()]
    public class SearchAndFilterTests
    {
        private Mock<SearchAndFilter> _mock;
        private List<ProductInventory> _products;
        private Store store;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            //_prodIDCounter = 0;
            //_prodInvID = 0;
            var discount = new VisibleDiscount(15.0f, new DiscountPolicy());
            var purchasePolicy = new ImmediatePurchase();
            _products = new List<ProductInventory>()
            {
                { ProductInventory.Create("Dell XPS 15inch", "Laptop", discount, purchasePolicy, 1600.0, 30, Category.ELECTRONICS, new List<string>() { "Laptop", "Computer", "PC"})},
                { ProductInventory.Create("Sony DSLR Camera", "Best camera", discount, purchasePolicy, 500.0, 20, Category.CAMERASPHOTOS, new List<string>() { "Camera", "DSLR", "Sony"})},
                { ProductInventory.Create("Google Pixel 4", "New google phone", discount, purchasePolicy, 700.0, 10, Category.CELLPHONES, new List<string>() { "Google", "Pixel", "Phone"})},
                { ProductInventory.Create("Google Pixel 4", "New google phone", discount, purchasePolicy, 700.0, 10, Category.ELECTRONICS, new List<string>() { "Google", "Pixel", "Phone"})},
                { ProductInventory.Create("Western Digital 4TB HardDrive", "4TB Harddrive", discount, purchasePolicy, 100.0, 100, Category.ELECTRONICS, new List<string>() { "WD", "Harddrive"})},
                { ProductInventory.Create("Dyson V11", "Vaccum cleaner", discount, purchasePolicy, 300.0, 5, Category.HOMEGARDEN, new List<string>() { "Dyson", "Vaccum"})},
                { ProductInventory.Create("Logitech MX Master 3", "Computer mice", discount, purchasePolicy, 110.0, 300, Category.ELECTRONICS, new List<string>() { "Logitech", "Mice"})},
                { ProductInventory.Create("Linkin Park - Meteora", "Nu-Metal music album", discount, purchasePolicy, 20.0, 2, Category.MUSIC, new List<string>() { "Music", "LP", "Nu-Metal"})},
                { ProductInventory.Create("Windows 10 Home", "Microsoft Operatin system", discount, purchasePolicy, 25.0, 5000, Category.SOFTWARE, new List<string>() { "OS", "Microsoft", "Windows"})},
                { ProductInventory.Create("iPhone 11 XL", "Apple new smartphone", discount, purchasePolicy, 800.0, 30, Category.CELLPHONES, new List<string>() { "Apple", "Smartphone", "Phone"})},
            };
            for (var i = 0; i < _products.Count; ++i)
            {
                _products[i].rateProduct(i);        // Give products rating to test filter by rating
            }
            _mock = new Mock<SearchAndFilter>();
            _mock.Setup(s => s.getProductInventories(null)).Returns(_products);
            store = new Store(null, null, "owner", "store1");
            store.Inventory.Products = _products;
            StoreManagement.Instance.Stores.Add(store);

        }

        [TearDown]
        public void tearDown()
        {
            StoreManagement.Instance.Stores.Clear();
        }

        [Test()]
        public void searchProductsByCategoryTest()
        {
            var cellphoneProducts = _mock.Object.searchProductsByCategory("CELLPHONES",null);
            Assert.AreEqual(cellphoneProducts.Item1.Count, 2);   // Test search by CELLPHONE returned 2 products
            Assert.AreEqual(cellphoneProducts.Item1.First().Name, "Google Pixel 4");  // Test first product returned is expected cellphone
            Assert.AreEqual(cellphoneProducts.Item1.ElementAt(1).Name, "iPhone 11 XL");   // Test second product returned is expected cellphone
            Assert.AreEqual(_mock.Object.searchProductsByCategory("ELECTRONICS",null).Item1.Count, 4);   // Test search by ELECTRONICS returned 2 products
            Assert.AreEqual(_mock.Object.searchProductsByCategory("SOFTWARE",null).Item1.Count, 1);   // Test search by SOFTWARE returned 1 product
        }

        [Test()]
        public void searchProductsByNameTest()
        {
            Assert.AreEqual(_mock.Object.searchProductsByName("Windows 10 Home",null).Item1.Count, 1);   // Test search by "Windows" returned one product
            Assert.AreEqual(_mock.Object.searchProductsByName("Windows 10 Home",null).Item1.First().Name, "Windows 10 Home");  // Test first product returned is as expected
            Assert.AreEqual(_mock.Object.searchProductsByName("Google Pixel 4",null).Item1.Count, 2);   // Test returned 2 prodcuts matching the name
        }

        [Test()]
        public void searchProductsByKeywordTest()
        {
            Assert.AreEqual(_mock.Object.searchProductsByKeyword(new List<string> { { "Phone" }, { "Laptop" } },null).Item1.Count, 4);   // Test search by keywords matching expected
            Assert.AreEqual(_mock.Object.searchProductsByKeyword(new List<string> { { "Laptop" }, { "Computer" } },null).Item1.Count, 1);   // Test not returning duplicate result for same product
            Assert.AreEqual(_mock.Object.searchProductsByKeyword(new List<string> { { "Phone" }, { "Apple" } },null).Item1.Count, 3);   // Test not returning duplicate result for same product
        }

        [Test()]
        public void filterProductsTest()
        {
            var prodRatingRange = new Range<double>(4.0, 5.0);
            Assert.AreEqual(_mock.Object.filterProducts(_products,null,prodRatingRange,null).Count, 6);   // Test returned filtered products as expected
            var catFilter = "ELECTRONICS";
            Assert.AreEqual(_mock.Object.filterProducts(_products,null, prodRatingRange, catFilter).Count, 2);
            var priceRangeFilter = new Range<double>(105.0, 1500.0);
            Assert.AreEqual(_mock.Object.filterProducts(_products, priceRangeFilter, prodRatingRange, catFilter).Count, 1);
            var filtered = _mock.Object.filterProducts(_products, priceRangeFilter, prodRatingRange, null);
            Assert.AreEqual(filtered.Count, 3);
            Assert.AreEqual(filtered.First().Name, "Dyson V11");
            Assert.AreEqual(filtered.ElementAt(1).Name, "Logitech MX Master 3");
            Assert.AreEqual(filtered.ElementAt(2).Name, "iPhone 11 XL");
            Assert.AreEqual(_mock.Object.filterProducts(_products, null, prodRatingRange, null).Count, 6);   // Test returned filtered products as expected
            Assert.AreEqual(_mock.Object.filterProducts(_products, null, null, null).Count, 10);   // Test returned filtered products as expected
            Assert.AreEqual(_mock.Object.filterProducts(_mock.Object.searchProductsByCategory("ELECTRONICS",null).Item1,null,null,null).Count, 4);   // Test returned filtered products as expected
            prodRatingRange = new Range<double>(0.0, 4.0);
            Assert.AreEqual(_mock.Object.filterProducts(_mock.Object.searchProductsByCategory("ELECTRONICS",null).Item1,null, prodRatingRange,null).Count, 3);   // Test returned filtered products as expected

            prodRatingRange = new Range<double>(7.0, 200.0);
            Assert.IsEmpty(_mock.Object.filterProducts(_products,null, prodRatingRange,null));   // Test returned filtered products as expected
            catFilter = "TOYS";
            Assert.IsEmpty(_mock.Object.filterProducts(_products,null,null,catFilter));   // Test returned filtered products as expected
        }


    }
}