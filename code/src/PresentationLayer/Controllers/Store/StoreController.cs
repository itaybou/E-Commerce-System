using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Exceptions;

namespace PresentationLayer.Controllers.Store
{
    public class StoreController : BaseController
    {
        public StoreController(IService service) : base(service) { }

        public IActionResult Index()
        {
            var storeInfo = _service.getAllStoresInfo();
            return View("StoreList", storeInfo);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult UserStoreList()
        {
            try
            {
                var permissions = _service.getUserPermissions(new Guid(HttpContext.Session.Id));
                var stores = new Dictionary<StoreModel, List<ProductInventoryModel>>();
                permissions.Keys.ToList().ForEach(storeName =>
                {
                    var store = _service.getStoreInfo(storeName);
                    stores.Add(store.Item1, store.Item2);
                });
                return View("UserStoreList", (stores, permissions));
            } catch(AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
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
            try
            {
                if (ModelState.IsValid)
                {
                    var session = new Guid(HttpContext.Session.Id);
                    var valid = _service.openStore(session, model.StoreName);
                    if (valid)
                    {
                        return RedirectToAction("UserStoreList", "Store");
                    }
                    ModelState.AddModelError("OpenStoreError", "Unable to open store, try again later.");
                    return View("../Store/OpenStore", model);
                }
                return View("../Store/OpenStore", model);
            }
            catch (AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
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
        [Route("RateStore")]
        public IActionResult RateStore(string storeName, string rating)
        {
            try
            {
                var rating_int = Int32.Parse(rating);
                _service.rateStore(storeName, rating_int);
                var storeInfo = _service.getAllStoresInfo();
                return View("StoreList", storeInfo);
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