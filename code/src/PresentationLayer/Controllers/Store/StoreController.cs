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
            //var stores = new Dictionary<StoreModel, List<ProductModel>>()
            //{
            //    {new StoreModel("Store1", 4, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } },
            //    {new StoreModel("Store1", 4.6, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 4.3, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 2.1, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 5, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 3.7, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 3.3, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }

            //};
            return View("StoreList", storeInfo);
        }

        [Authorize(Roles = "Subscribed")]
        public IActionResult UserStoreList()
        {
            var permissions = _service.getUserPermissions(new Guid(HttpContext.Session.Id));
            Dictionary<StoreModel, List<ProductModel>> stores = new Dictionary<StoreModel, List<ProductModel>>();
            permissions.Keys.ToList().ForEach(storeName =>
            {
                var store = _service.getStoreInfo(storeName);
                stores.Add(store.Item1, store.Item2);
            });
            //var stores = new Dictionary<StoreModel, List<ProductModel>>()
            //{
            //    {new StoreModel("Store1", 4, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } },
            //    {new StoreModel("Store1", 4.6, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 4.3, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 2.1, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 5, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 3.7, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }, {new StoreModel("Store1", 3.3, 85), new List<ProductModel>() {
            //         { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //        { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
            //    } }

            //};
            
            return View("UserStoreList", (stores, permissions));
        }

        [Authorize(Roles = "Subscribed")]
        [Route("OpenStore")]
        public IActionResult OpenStore()
        {
            return View("../Store/OpenStore", new OpenStoreModel());
        }

        [HttpPost]
        [Authorize(Roles = "Subscribed")]
        [Route("OpenStore")]
        public IActionResult OpenStore(OpenStoreModel model)
        {
            if(ModelState.IsValid)
            {
                var session = new Guid(HttpContext.Session.Id);
                var valid = _service.openStore(session, model.StoreName, model.DiscountPolicy, model.PurchasePolicy);
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