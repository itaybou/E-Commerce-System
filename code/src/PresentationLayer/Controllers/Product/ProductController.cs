using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Products;
using ECommerceSystem.Utilities;
using ECommerceSystem.Utilities;
using System.Security.Claims;

namespace PresentationLayer.Controllers.Products
{
    public class ProductController : Controller
    {
        private IService _service;

        public ProductController(IService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [Route("Product/ViewProduct/{id}")]
        public IActionResult ViewProduct(Guid prodId)
        {
            ViewData["prodId"] = prodId;
            //TEST
            var model = new ProductModel(prodId, "Test Product", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", 25, 30.5, 20.5);
            ViewData["Name"] = model.Name;
            //TEST
            return View("Index", model);
        }

        [AllowAnonymous]
        [Route("ProductListing")]
        public IActionResult ProductListing(string searchInput, string category, string searchType, double from, double to, int prodRating, int storeRating, int page = 0)
        {
            SearchResultModel search;
            from = from > to ? to : from;
            //var prodcuts = GetHashCode products from domain;
            switch(searchType)
            {
                case "Name":
                    search = _service.searchProductsByName(searchInput, category == null ? "" : category, new Range<double>(from, to),
                        new Range<double>(storeRating, storeRating + 1), new Range<double>(prodRating, prodRating + 1));
                    break;
                case "Category":
                    search = _service.searchProductsByCategory(searchInput, new Range<double>(from, to),
                        new Range<double>(storeRating, storeRating + 1), new Range<double>(prodRating, prodRating + 1));
                    break;
                case "Keywords":
                    var keywords = searchInput.Split(" ").ToList();
                    search = _service.searchProductsByKeyword(keywords, category == null ? "" : category, new Range<double>(from, to),
                        new Range<double>(storeRating, storeRating + 1), new Range<double>(prodRating, prodRating + 1));
                    break;
                default:
                    search = _service.getAllProducts(category == null? "" : category, new Range<double>(from, to),
                        new Range<double>(storeRating, storeRating + 1), new Range<double>(prodRating, prodRating + 1));
                    break;
            }
            var model = new ProductListingModel(search, category, searchInput, searchType, from, to, prodRating, storeRating, page);
            return View("ProductListing", model);
        }

        [AllowAnonymous]
        [Route("StoreProductListing")]
        public IActionResult StoreProductListing(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var products = _service.getStoreInfo(storeName).Item2;
            var search = new SearchResultModel(products, new List<string>());
            // get from domain product by store
            var model = new ProductListingModel(search, "", "", "", 0, 0, 0, 0, 0);
            return View("ProductListing", model);
        }

        public IActionResult AddProductToCart(Guid prodID, string storeName)
        {
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var valid = _service.addProductToCart(userId, prodID, storeName, 1);
            return View();
        }

    }
}