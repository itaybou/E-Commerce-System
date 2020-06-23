using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement.spell_checker;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using ECommerceSystemץ.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.SystemManagement
{
    public class SearchAndFilter
    {
        private StoreManagement _storeManagement;
        private ISpellChecker _spellChecker;

        public SearchAndFilter()
        {
            _spellChecker = new SpellChecker();
            _storeManagement = StoreManagement.Instance;
        }

        public virtual List<ProductInventory> getProductInventories(Range<double> storeRatingFilter)
        {

            return storeRatingFilter != null ? _storeManagement.getAllStoreInventoryWithRating(storeRatingFilter) : _storeManagement.getAllStoresProdcutInventories();
        }

        public (List<ProductInventory>, List<string>) getAllProducts(Range<double> storeRatingFilter)
        {
            return (getProductInventories(storeRatingFilter), new List<string>());
        }

        public (List<ProductInventory>, List<string>) searchProductsByCategory(string category, Range<double> storeRatingFilter)
        {
            if (!EnumMethods.GetValues(typeof(Category)).Contains(category.ToUpper()))
            {
                return (new List<ProductInventory>(), new List<string>());
            }
            var cat = (Category)Enum.Parse(typeof(Category), category.ToUpper());
            var test = getProductInventories(storeRatingFilter);
            var result = test.FindAll(p => p.Category.ToString().Equals(category));
            return (result, new List<string>());
        }

        public (List<ProductInventory>, List<string>) searchProductsByName(string prodName, Range<double> storeRatingFilter)
        {
            var result = getProductInventories(storeRatingFilter).FindAll(p => p.Name.Equals(prodName));
            return (result, _spellChecker.Correct(prodName));
        }

        public (List<ProductInventory>, List<string>) searchProductsByKeyword(List<string> keywords, Range<double> storeRatingFilter)
        {
            var corrected = keywords.Select(word => _spellChecker.Correct(word)).SelectMany(correct => correct).ToList();
            var result = getProductInventories(storeRatingFilter).FindAll(p => p.Keywords.Intersect(keywords).Any());
            return (result, corrected);
        }

        public List<ProductInventory> filterProducts(List<ProductInventory> products,
            Range<double> priceRangeFilter, Range<double> productRatingFilter, string categoryFilter)
        {
            Category? category;
            try
            {
                category = categoryFilter == null ? null : EnumMethods.GetValues(typeof(Category)).Contains(categoryFilter.ToUpper()) ? (Category?)Enum.Parse(typeof(Category), categoryFilter.ToUpper()) : null;
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : cant parse category");
            }
           
            Func<ProductInventory, bool> priceRangeFilterPred = p => (priceRangeFilter != null && priceRangeFilter.inRange(p.Price)) || priceRangeFilter == null;
            Func<ProductInventory, bool> productRangeFilterPred = p => (productRatingFilter != null && productRatingFilter.inRange(p.Rating)) || productRatingFilter == null;
            Func<ProductInventory, bool> categoryFilterPred = p => (category != null && p.Category.Equals(category)) || category == null;
            Func<bool> noFilters = () => (categoryFilter == null && productRatingFilter == null && priceRangeFilter == null);
            return products.Where(p => noFilters() || (priceRangeFilterPred(p) && productRangeFilterPred(p) && categoryFilterPred(p))).ToList();
        }

        public SearchResultModel getProductSearchResults(string prodName, List<string> keywords, string category,
            Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            if (prodName != null)
            {
                var searchResult = searchProductsByName(prodName, storeRatingFilter);
                return generateSearchResult(filterProducts(searchResult.Item1, priceFilter, productRatingFilter, category), searchResult.Item2);
            }
            if (keywords != null)
            {
                var searchResult = searchProductsByKeyword(keywords, storeRatingFilter);
                return generateSearchResult(filterProducts(searchResult.Item1, priceFilter, productRatingFilter, category), searchResult.Item2);
            }
            if (category != null)
            {
                var searchResult = searchProductsByCategory(category, storeRatingFilter);
                return generateSearchResult(filterProducts(searchResult.Item1, priceFilter, productRatingFilter, category), searchResult.Item2);
            }
            var allProducts = getAllProducts(storeRatingFilter);
            return generateSearchResult(filterProducts(allProducts.Item1, priceFilter, productRatingFilter, category), allProducts.Item2);
        }

        private SearchResultModel generateSearchResult(List<ProductInventory> prods, List<string> suggestions)
        {
            return ModelFactory.CreateSearchResult(prods, suggestions);
        }
    }
}