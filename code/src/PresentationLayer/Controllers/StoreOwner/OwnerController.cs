using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers.StoreOwner
{
    public class OwnerController : Controller
    {

        private IService _service;

        public OwnerController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Subscribed")]
        [Route("StoreProducts")]
        public IActionResult StoreProductsView(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var products = _service.getStoreInfo(storeName);

            return View("../Store/StoreInventory", products);
        }

        [Authorize(Roles = "Subscribed")]
        [Route("AddProduct")]
        public IActionResult AddProduct(string store)
        {
            ViewData["StoreName"] = store;
            return View("../Store/AddProduct", new AddProductModel(Guid.Empty, null, null, null, 0, 0, 0));
        }

        [HttpPost]
        [Authorize(Roles = "Subscribed")]
        [Route("AddProduct")]
        public IActionResult AddProduct(AddProductModel model)
        {
            var store = ViewData["StoreName"];
            return View("../Store/AddProduct", new AddProductModel(Guid.Empty, null, null, null, 0, 0, 0));
        }
    }
}