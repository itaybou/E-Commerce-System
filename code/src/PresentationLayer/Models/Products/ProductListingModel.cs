using ECommerceSystem.DomainLayer.Utilities;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models.Products
{
    public class ProductListingModel
    {
        public readonly int MAX_PRODUCTS_PER_PAGE = 12;
        public SearchResultModel VisibleProducts {get; set;}
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public string SearchInput { get; set; }
        public string SearchType { get; set; }



        public double PriceRangeFrom { get; set; }
        public double PriceRangeTo { get; set; }
        public int ProductRatingFilter { get; set; }
        public int StoreRatingFilter { get; set; }

        //public ProductListingModel(SearchResultModel visibleProducts)
        //{
        //    VisibleProducts = visibleProducts;
        //}


        public ProductListingModel(SearchResultModel products, string searchInput, string searchType, double from, double to, int prodRating, int storeRating, int page)
        {
            SearchInput = searchInput;
            SearchType = searchType;
            CurrentPage = page;
            PriceRangeFrom = from;
            PriceRangeTo = to;
            ProductRatingFilter = prodRating;
            StoreRatingFilter = storeRating;

            var dummyProducts = new List<ProductModel>()
            {
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
                                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
                                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
                                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
                                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
                                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
                                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                { new ProductModel(new Guid(), "Product9", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum posuere lorem ipsum dolor. Diam sollicitudin tempor id eu nisl nunc mi. Accumsan sit amet nulla facilisi morbi tempus iaculis urna.", 5, 30.5, 25.5) },
            };

            var keywords = new List<string>()
            {
                {"SomeWords" }, {"SomeOtherWords" }, {"ThirdWord" }
            };
            if (products.ProductResults != null)
            {
                VisibleProducts = products;
            }
            else VisibleProducts = new SearchResultModel(dummyProducts, keywords);
            Pages = (int)Math.Ceiling(VisibleProducts.ProductResults.Count / (double)MAX_PRODUCTS_PER_PAGE);
        }
    }
}
