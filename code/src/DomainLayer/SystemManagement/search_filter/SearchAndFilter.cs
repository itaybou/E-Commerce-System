using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement.search_filter;
using ECommerceSystem.DomainLayer.SystemManagement.spell_checker;
using ECommerceSystem.DomainLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public class SearchAndFilter
    {
        private StoreManagement _storeManagement;
        private ISpellChecker _spellChecker;

        private List<ProductInventory> _visibleProducts;

        private Range<double> _priceRangeFilter;
        private Range<double> _productRatingFilter;
        private Range<double> _storeRatingFilter;
        private Category? _categoryFilter;

        public Range<double> StoreRatingFilter { get => _storeRatingFilter; set => _storeRatingFilter = value; }
        public StoreManagement StoreManagement { get => _storeManagement; set => _storeManagement = value; }
        public Range<double> PriceRangeFilter { get => _priceRangeFilter; set => _priceRangeFilter = value; }
        public Range<double> ProductRatingFilter { get => _productRatingFilter; set => _productRatingFilter = value; }
        public Category? CategoryFilter { get => _categoryFilter; set => _categoryFilter = value; }

        public SearchAndFilter()
        {
            _storeManagement = StoreManagement.Instance;
            _visibleProducts = getAllProdcuts();
            _spellChecker = new SpellChecker();
        }

        public virtual List<ProductInventory> getAllProdcuts()
        {
            return _storeRatingFilter != null ?
                filterProducts(_storeManagement.getAllStoreInventoryWithRating(_storeRatingFilter)) : filterProducts(_storeManagement.getAllStoresProdcutInventories());
        }

        public SearchResult searchProductsByCategory(Category category)
        {
            _visibleProducts = getAllProdcuts().FindAll(p => p.Category.Equals(category));
            return new SearchResult(_visibleProducts, null);
        }

        public SearchResult searchProductsByName(string prodName)
        {
            _visibleProducts = getAllProdcuts().FindAll(p => p.Name.Equals(prodName));
            return new SearchResult(_visibleProducts, _spellChecker.Correct(prodName));
        }

        public SearchResult searchProductsByKeyword(List<string> keywords)
        {
            var corrected = keywords.Select(word => _spellChecker.Correct(word)).SelectMany(correct => correct).ToList();
            _visibleProducts = getAllProdcuts().FindAll(p => p.Keywords.Intersect(keywords).Any());
            return new SearchResult(_visibleProducts, corrected);
        }

        public List<ProductInventory> filterProducts(List<ProductInventory> products)
        {
            Func<ProductInventory, bool> priceRangeFilter = p => (_priceRangeFilter != null && _priceRangeFilter.inRange(p.Price)) || _priceRangeFilter == null;
            Func<ProductInventory, bool> productRangeFilter = p => (_productRatingFilter != null && _productRatingFilter.inRange(p.Rating)) || _productRatingFilter == null;
            Func<ProductInventory, bool> categoryFilter = p => (_categoryFilter != null && p.Category.Equals(CategoryFilter)) || _categoryFilter == null;
            Func<bool> noFilters = () => (_categoryFilter == null && _productRatingFilter == null && _priceRangeFilter == null);
            return products.Where(p => noFilters() || (priceRangeFilter(p) && productRangeFilter(p) && categoryFilter(p))).ToList();
        }

        public List<ProductInventory> applyPriceRangeFilter(double from, double to)
        {
            _priceRangeFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(getAllProdcuts());
            return _visibleProducts;
        }

        public List<ProductInventory> applyStoreRatingFilter(double from, double to)
        {
            _storeRatingFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(getAllProdcuts());
            return _visibleProducts;
        }

        public List<ProductInventory> applyProductRatingFilter(double from, double to)
        {
            _productRatingFilter = new Range<double>(from, to);
            _visibleProducts = filterProducts(getAllProdcuts());
            return _visibleProducts;
        }

        public List<ProductInventory> applyCategoryFilter(Category category)
        {
            CategoryFilter = category;
            _visibleProducts = filterProducts(getAllProdcuts());
            return _visibleProducts;
        }

        public List<ProductInventory> cancelFilter(Filters filter)
        {
            switch (filter)
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
                    CategoryFilter = null;
                    break;
            }
            _visibleProducts = filterProducts(getAllProdcuts());
            return _visibleProducts;
        }
    }
}