﻿using ECommerceSystem.Exceptions;
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
            try
            {
                var model = _service.getProductInventory(prodId).Item1;
                if (model == null)
                    return RedirectToAction("ProductListing");
                ViewData["Name"] = model.Name;
                return View("Index", model);
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }

        }

        [AllowAnonymous]
        [Route("ProductListing")]
        public IActionResult ProductListing(string searchInput, string category, string searchType, double from = 0, double to = Int32.MaxValue, int prodRating = -1, int storeRating = -1, int page = 0)
        {
            SearchResultModel search;
            from = from > to ? to : from;
            //var prodcuts = GetHashCode products from domain;

            try
            {
                switch (searchType)
                {
                    case "Name":
                        search = _service.searchProductsByName(searchInput, category == null ? "" : category, new Range<double>(from, to),
                            new Range<double>(storeRating, 5), new Range<double>(prodRating, 5));
                        break;

                    case "Category":
                        search = _service.searchProductsByCategory(searchInput, new Range<double>(from, to),
                            new Range<double>(storeRating, 5), new Range<double>(prodRating, 5));
                        break;

                    case "Keywords":
                        var keywords = searchInput.Split(" ").ToList();
                        search = _service.searchProductsByKeyword(keywords, category == null ? "" : category, new Range<double>(from, to),
                            new Range<double>(storeRating, 5), new Range<double>(prodRating, 5));
                        break;

                    default:
                        search = _service.getAllProducts(category, new Range<double>(from, to),
                            storeRating == -1 ? new Range<double>(0, 5) : new Range<double>(storeRating, 5),
                            prodRating == -1 ? new Range<double>(0, 5) : new Range<double>(prodRating, 5));
                        break;
                }
                var model = new ProductListingModel(search, category, searchInput, searchType, from, to, prodRating, storeRating, page);
                return View("ProductListing", model);
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }

        }

        [Route("StoreProductListing")]
        public IActionResult StoreProductListing(string storeName)
        {
            try
            {
                ViewData["StoreName"] = storeName;
                var products = _service.getStoreInfo(storeName).Item2;
                var search = new SearchResultModel(products, new List<string>());
                // get from domain product by store
                var model = new ProductListingModel(search, "", "", "", 0, 0, 0, 0, 0);
                return View("ProductListing", model);
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }

        }

        [HttpPost]
        [Route("RateProduct")]
        public IActionResult RateProduct(string prodID, string rating)
        {
            var id = new Guid(prodID);
            try
            {
                var rating_int = Int32.Parse(rating);
                _service.rateProduct(id, rating_int);

                // get from domain product by store
                ViewData["prodId"] = prodID;
                var model = _service.getProductInventory(id).Item1;
                if (model == null)
                    return RedirectToAction("ProductListing");
                ViewData["Name"] = model.Name;
                return View("Index", model);
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }
            catch (Exception)
            { 
                return Redirect("~/Exception/LogicException");
            }

        }

        [Route("ConcreteProducts")]
        public IActionResult ConcreteProducts(string prodID, string store, bool listing, bool error = false)
        {
            var session = new Guid(HttpContext.Session.Id);
            var id = new Guid(prodID);
            try
            {
                var products = _service.getStoreProductGroup(session, id, store);
                var redirect = listing ? Url.Action("ProductListing", "Product") : Url.Action("ViewProduct", "Product", new { id = prodID });
                var model = new ChooseProductModel(store, listing, id, products.Item2, redirect);
                if (error)
                    ModelState.AddModelError("InvalidProductSelection", "Add To Cart failed: check that product chosen and that quantity is valid for that product and that your cart quantity with required quantity does not exceed available quantity.");
                return View("_ChooseProductModal", model);
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }
            catch (Exception)
            {
                return Redirect("~/Exception/LogicException");
            }
        }
        
    }
}