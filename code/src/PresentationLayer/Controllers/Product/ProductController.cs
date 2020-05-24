using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using ECommerceSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IActionResult ViewProduct(string id)
        {
            var prodId = new Guid(id);
            ViewData["prodId"] = prodId;
            var model = _service.getProductInventory(prodId).Item1;
            if (model == null)
                return RedirectToAction("ProductListing");
            ViewData["Name"] = model.Name;
            return View("Index", model);
        }

        [AllowAnonymous]
        [Route("ProductListing")]
        public IActionResult ProductListing(string searchInput, string category, string searchType, double from = 0, double to = Int32.MaxValue, int prodRating = -1, int storeRating = -1, int page = 0)
        {
            SearchResultModel search;
            from = from > to ? to : from;
            //var prodcuts = GetHashCode products from domain;
            switch (searchType)
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
                    search = _service.getAllProducts(category, new Range<double>(from, to),
                        storeRating == -1 ? new Range<double>(0, 5) : new Range<double>(storeRating, storeRating + 1),
                        prodRating == -1 ? new Range<double>(0, 5) : new Range<double>(prodRating, prodRating + 1));
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
    }
}