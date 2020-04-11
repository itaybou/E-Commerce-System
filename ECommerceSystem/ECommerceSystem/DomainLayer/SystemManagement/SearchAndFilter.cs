using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.Utilities;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    class SearchAndFilter
    {
        private StoreManagement _storeManagement;

        private List<ProductInventory> _visibleProducts;

        private Range<double> _priceRangeFilter;
        private Range<double> _productRatingFilter;
        private Range<double> _storeRatingFilter;
        private Category? _categoryFilter;

        public Range<double> StoreRatingFilter { get => _storeRatingFilter;  }

        public SearchAndFilter(StoreManagement storeManagement)
        {
            _storeManagement = storeManagement;
            _visibleProducts = getAllProdcuts();
        }

        public List<ProductInventory> getAllProdcuts()
        {
            return _storeRatingFilter != null ?
                filterProducts(_storeManagement.getAllStoreInventoryWithRating(_storeRatingFilter)) : filterProducts(_storeManagement.getAllStoresProdcutInventories());
        }

        public List<ProductInventory> searchProductsByCategory(Category category)
        {
            _visibleProducts = filterProducts(getAllProdcuts().FindAll(p => p.Category.Equals(category)));
            return _visibleProducts;
        }

        public List<ProductInventory> searchProductsByName(string prodName)
        {
            _visibleProducts = filterProducts(getAllProdcuts().FindAll(p => p.Name.Equals(prodName)));
            return _visibleProducts;
        }

        public List<ProductInventory> searchProductsByKeyword(List<string> keywords)
        {
            _visibleProducts = filterProducts(getAllProdcuts().FindAll(p => p.Keywords.Intersect(keywords).Any()));
            return _visibleProducts;
        }

        public List<ProductInventory> filterProducts(List<ProductInventory> products)
        {
            Func<ProductInventory, bool> priceRangeFilter = p => (_priceRangeFilter != null && _priceRangeFilter.inRange(p.Price));
            Func<ProductInventory, bool> productRangeFilter = p => (_productRatingFilter != null && _productRatingFilter.inRange(p.Rating));
            Func<ProductInventory, bool> categoryFilter = p => (_categoryFilter != null && p.Category.Equals(_categoryFilter));
            Func<bool> noFilters = () => (_categoryFilter == null && _productRatingFilter == null && _priceRangeFilter == null);
            return products.Where(p => noFilters() || priceRangeFilter(p) || productRangeFilter(p) || categoryFilter(p)).ToList();
        }

        private void applyPriceRangeFilter(double from, double to)
        {
            _priceRangeFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(_visibleProducts);
        }

        private void applyStoreRatingFilter(double from, double to)
        {
            _storeRatingFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(_visibleProducts);
        }

        private void applyProductRatingFilter(double from, double to)
        {
            _productRatingFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(_visibleProducts);
        }

        private void applyCategoryFilter(double from, double to)
        {
            _productRatingFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(_visibleProducts);
        }

        private void cancelFilter(Filters filter)
        {
            switch(filter)
            {
                case Filters.PRICE_RANGE:
                    _priceRangeFilter = null;
                    break;
                case Filters.PRODUCT_RATING:
                    _productRatingFilter = null;
                    break;
                case Filters.STORE_RATING:
                    _storeRatingFilter = null;
                    break;
                case Filters.CATEGORY:
                    _categoryFilter = null;
                    break;
            }
            _visibleProducts = filterProducts(_visibleProducts);
        }

        public enum Filters {
            PRICE_RANGE,
            PRODUCT_RATING,
            STORE_RATING,
            CATEGORY
        }
    }
}
