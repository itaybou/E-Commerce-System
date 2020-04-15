using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.Utilities;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.SystemManagement.Tests
{
    [TestFixture()]
    public class SearchAndFilterTests
    {
        private Mock<SearchAndFilter> _mock;
        private List<ProductInventory> _products;
        private int _prodIDCounter, _prodInvID;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            _prodIDCounter = 0;
            _prodInvID = 0;
            var discount = new VisibleDiscount(15.0f, new DiscountPolicy());
            var purchasePolicy = new ImmediatePurchase();
            _products = new List<ProductInventory>()
            {
                { ProductInventory.Create("Dell XPS 15inch", "Laptop", discount, purchasePolicy, 1600.0, 30, Category.ELECTRONICS, new List<string>() { "Laptop", "Computer", "PC"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Sony DSLR Camera", "Best camera", discount, purchasePolicy, 500.0, 20, Category.CAMERASPHOTOS, new List<string>() { "Camera", "DSLR", "Sony"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Google Pixel 4", "New google phone", discount, purchasePolicy, 700.0, 10, Category.CELLPHONES, new List<string>() { "Google", "Pixel", "Phone"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Google Pixel 4", "New google phone", discount, purchasePolicy, 700.0, 10, Category.ELECTRONICS, new List<string>() { "Google", "Pixel", "Phone"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Western Digital 4TB HardDrive", "4TB Harddrive", discount, purchasePolicy, 100.0, 100, Category.ELECTRONICS, new List<string>() { "WD", "Harddrive"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Dyson V11", "Vaccum cleaner", discount, purchasePolicy, 300.0, 5, Category.HOMEGARDEN, new List<string>() { "Dyson", "Vaccum"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Logitech MX Master 3", "Computer mice", discount, purchasePolicy, 110.0, 300, Category.ELECTRONICS, new List<string>() { "Logitech", "Mice"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Linkin Park - Meteora", "Nu-Metal music album", discount, purchasePolicy, 20.0, 2, Category.MUSIC, new List<string>() { "Music", "LP", "Nu-Metal"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("Windows 10 Home", "Microsoft Operatin system", discount, purchasePolicy, 25.0, 5000, Category.SOFTWARE, new List<string>() { "OS", "Microsoft", "Windows"}, _prodIDCounter++, _prodInvID++)},
                { ProductInventory.Create("iPhone 11 XL", "Apple new smartphone", discount, purchasePolicy, 800.0, 30, Category.CELLPHONES, new List<string>() { "Apple", "Smartphone", "Phone"}, _prodIDCounter++, _prodInvID++)},
            };
            for (var i = 0; i < _products.Count; ++i)
            {
                _products[i].rateProduct(i);        // Give products rating to test filter by rating
            }
            _mock = new Mock<SearchAndFilter>();
            _mock.Setup(s => s.getAllProdcuts()).Returns(_products);
        }

        [TearDown]
        public void tearDown()
        {
            _mock.Object.PriceRangeFilter = null;
            _mock.Object.ProductRatingFilter = null;
            _mock.Object.StoreRatingFilter = null;
            _mock.Object.CategoryFilter = null;
        }

        [Test()]
        public void searchProductsByCategoryTest()
        {
            var cellphoneProducts = _mock.Object.searchProductsByCategory(Category.CELLPHONES);
            Assert.AreEqual(cellphoneProducts.Count, 2);   // Test search by CELLPHONE returned 2 products
            Assert.AreEqual(cellphoneProducts.First().Name, "Google Pixel 4");  // Test first product returned is expected cellphone
            Assert.AreEqual(cellphoneProducts.ElementAt(1).Name, "iPhone 11 XL");   // Test second product returned is expected cellphone
            Assert.AreEqual(_mock.Object.searchProductsByCategory(Category.ELECTRONICS).Count, 4);   // Test search by ELECTRONICS returned 2 products
            Assert.AreEqual(_mock.Object.searchProductsByCategory(Category.SOFTWARE).Count, 1);   // Test search by SOFTWARE returned 1 product
        }

        [Test()]
        public void searchProductsByNameTest()
        {
            Assert.AreEqual(_mock.Object.searchProductsByName("Windows 10 Home").Count, 1);   // Test search by "Windows" returned one product
            Assert.AreEqual(_mock.Object.searchProductsByName("Windows 10 Home").First().Name, "Windows 10 Home");  // Test first product returned is as expected
            Assert.AreEqual(_mock.Object.searchProductsByName("Google Pixel 4").Count, 2);   // Test returned 2 prodcuts matching the name
        }

        [Test()]
        public void searchProductsByKeywordTest()
        {
            Assert.AreEqual(_mock.Object.searchProductsByKeyword(new List<string> { { "Phone" }, { "Laptop" } }).Count, 4);   // Test search by keywords matching expected
            Assert.AreEqual(_mock.Object.searchProductsByKeyword(new List<string> { { "Laptop" }, { "Computer" } }).Count, 1);   // Test not returning duplicate result for same product
            Assert.AreEqual(_mock.Object.searchProductsByKeyword(new List<string> { { "Phone" }, { "Apple" } }).Count, 3);   // Test not returning duplicate result for same product
        }

        [Test()]
        public void filterProductsTest()
        {
            _mock.Object.ProductRatingFilter = new Range<double>(4.0, 5.0);
            Assert.AreEqual(_mock.Object.filterProducts(_products).Count, 6);   // Test returned filtered products as expected
            _mock.Object.CategoryFilter = Category.ELECTRONICS;
            Assert.AreEqual(_mock.Object.filterProducts(_products).Count, 2);
            _mock.Object.PriceRangeFilter = new Range<double>(105.0, 1500.0);
            Assert.AreEqual(_mock.Object.filterProducts(_products).Count, 1);
            _mock.Object.CategoryFilter = null;
            Assert.AreEqual(_mock.Object.filterProducts(_products).Count, 3);
            Assert.AreEqual(_mock.Object.filterProducts(_products).First().Name, "Dyson V11");
            Assert.AreEqual(_mock.Object.filterProducts(_products).ElementAt(1).Name, "Logitech MX Master 3");
            Assert.AreEqual(_mock.Object.filterProducts(_products).ElementAt(2).Name, "iPhone 11 XL");
            _mock.Object.PriceRangeFilter = null;
            Assert.AreEqual(_mock.Object.filterProducts(_products).Count, 6);   // Test returned filtered products as expected
            _mock.Object.ProductRatingFilter = null;
            Assert.AreEqual(_mock.Object.filterProducts(_products).Count, 10);   // Test returned filtered products as expected

            Assert.AreEqual(_mock.Object.filterProducts(_mock.Object.searchProductsByCategory(Category.ELECTRONICS)).Count, 4);   // Test returned filtered products as expected
            _mock.Object.ProductRatingFilter = new Range<double>(0.0, 4.0);
            Assert.AreEqual(_mock.Object.filterProducts(_mock.Object.searchProductsByCategory(Category.ELECTRONICS)).Count, 3);   // Test returned filtered products as expected

            _mock.Object.ProductRatingFilter = new Range<double>(7.0, 200.0);
            Assert.IsEmpty(_mock.Object.filterProducts(_products));   // Test returned filtered products as expected

            _mock.Object.ProductRatingFilter = null;
            _mock.Object.CategoryFilter = Category.TOYS;
            Assert.IsEmpty(_mock.Object.filterProducts(_products));   // Test returned filtered products as expected
        }

        [Test()]
        public void applyPriceRangeFilterTest()
        {
            Assert.AreEqual(_mock.Object.applyPriceRangeFilter(15.0, 150.0).Count, 4);   // Test returned filtered products as expected
            Assert.NotNull(_mock.Object.PriceRangeFilter);          //Check the filter object is not null
        }

        [Test()]
        public void applyStoreRatingFilterTest()
        {
            Assert.AreEqual(_mock.Object.applyStoreRatingFilter(3.0, 4.0).Count, 10);   // Test returned filtered products as expected
            Assert.NotNull(_mock.Object.StoreRatingFilter);          //Check the filter object is not null
        }

        [Test()]
        public void applyProductRatingFilterTest()
        {
            Assert.AreEqual(_mock.Object.applyProductRatingFilter(4.0, 5.0).Count, 6);   // Test returned filtered products as expected
            Assert.NotNull(_mock.Object.ProductRatingFilter);          //Check the filter object is not null
        }

        [Test()]
        public void applyCategoryFilterTest()
        {
            Assert.AreEqual(_mock.Object.applyCategoryFilter(Category.ELECTRONICS).Count, 4);   // Test returned filtered products as expected
            Assert.NotNull(_mock.Object.CategoryFilter);          //Check the filter object is not null
        }

        [Test()]
        public void cancelFilterTest()
        {
            Assert.AreEqual(_mock.Object.cancelFilter(Filters.CATEGORY).Count, 10);      // Test returned filtered products as expected
            Assert.AreEqual(_mock.Object.cancelFilter(Filters.PRICE_RANGE).Count, 10);   // Test returned filtered products as expected
            _mock.Object.applyPriceRangeFilter(15.0, 150.0);
            _mock.Object.applyCategoryFilter(Category.ELECTRONICS);
            Assert.AreEqual(_mock.Object.cancelFilter(Filters.CATEGORY).Count, 4);      // Test returned filtered products as expected
            Assert.IsNull(_mock.Object.CategoryFilter);
            _mock.Object.applyCategoryFilter(Category.ELECTRONICS);
            Assert.AreEqual(_mock.Object.cancelFilter(Filters.PRICE_RANGE).Count, 4);   // Test returned filtered products as expected
            Assert.IsNull(_mock.Object.ProductRatingFilter);
        }
    }
}