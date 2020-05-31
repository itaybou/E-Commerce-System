using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Models.Products
{
    public class ProductListingModel
    {
        public readonly int MAX_PRODUCTS_PER_PAGE = 12;
        public SearchResultModel VisibleProducts { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public string SearchInput { get; set; }
        public string SearchType { get; set; }
        public string Category { get; set; }
        public IEnumerable<string> Categories { get; set; }

        public double PriceRangeFrom { get; set; }
        public double PriceRangeTo { get; set; }
        public int ProductRatingFilter { get; set; }
        public int StoreRatingFilter { get; set; }

        public ProductListingModel(SearchResultModel products, string category, string searchInput, string searchType, double from, double to, int prodRating, int storeRating, int page)
        {
            SearchInput = searchInput;
            SearchType = searchType;
            CurrentPage = page;
            PriceRangeFrom = from;
            PriceRangeTo = to;
            ProductRatingFilter = prodRating;
            StoreRatingFilter = storeRating;
            Category = category;
            Categories = EnumMethods.GetValues(typeof(Category));
            if (products.ProductResults != null)
            {
                VisibleProducts = products;
            }
            Pages = (int)Math.Ceiling(VisibleProducts.ProductResults.Count / (double)MAX_PRODUCTS_PER_PAGE);
        }
    }
}