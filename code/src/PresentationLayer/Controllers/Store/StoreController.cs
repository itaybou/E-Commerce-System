using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers.Store
{

    public class StoreController : Controller
    {

        private IService _service;

        public StoreController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var storeInfo = _service.getAllStoresInfo();
            return View("StoreList", storeInfo);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult UserStoreList()
        {
            var permissions = _service.getUserPermissions(new Guid(HttpContext.Session.Id));
            var stores = new Dictionary<StoreModel, List<ProductModel>>();
            permissions.Keys.ToList().ForEach(storeName =>
            {
                var store = _service.getStoreInfo(storeName);
                stores.Add(store.Item1, store.Item2);
            });
            return View("UserStoreList", (stores, permissions));
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("OpenStore")]
        public IActionResult OpenStore()
        {
            return View("../Store/OpenStore", new OpenStoreModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("OpenStore")]
        public IActionResult OpenStore(OpenStoreModel model)
        {
            if(ModelState.IsValid)
            {
                var session = new Guid(HttpContext.Session.Id);
                var valid = _service.openStore(session, model.StoreName);
                if(valid)
                {
                    return RedirectToAction("UserStoreList", "Store");
                }
                ModelState.AddModelError("OpenStoreError", "Unable to open store, try again later.");
                return View("../Store/OpenStore", model);
            }
            return View("../Store/OpenStore", model);
        }
    }
}