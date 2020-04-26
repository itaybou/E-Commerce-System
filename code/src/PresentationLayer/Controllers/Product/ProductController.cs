using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Products;

namespace PresentationLayer.Controllers.Products
{
    public class ProductController : Controller
    {

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
        public IActionResult ProductListing(string searchInput, string searchType, double from, double to, int prodRating, int storeRating, int page = 0)
        {
            var productResults = new ProductListingModel(searchInput, searchType, from, to, prodRating, storeRating, page);
            return View("ProductListing", productResults);
        }

    }
}