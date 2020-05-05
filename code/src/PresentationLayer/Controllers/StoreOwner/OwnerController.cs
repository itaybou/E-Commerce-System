using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers.StoreOwner
{
    public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult StoreProductsView(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var products = new List<ProductModel>() {
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
            };

            return View("../Store/StoreInventory", products);
        }
    }
}