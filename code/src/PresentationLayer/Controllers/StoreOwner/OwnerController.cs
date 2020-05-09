using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers.StoreOwner
{
    [Authorize(Roles = "Admin, Subscribed")]
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

        public IActionResult StoreOwners(string storeName)
        {
            var owners = _service.getStoreOwners(storeName);
            return View("../Owner/StoreRoleUsers", (owners, storeName, true));
        }

        public IActionResult StoreManagers(string storeName)
        {
            //var owners = _service.getStoreOwners(storeName); /// TODO: CONTINUE FROM HERE!!!!
            return View("../Owner/StoreRoleUsers", (managers, storeName, false));
        }

        [HttpGet]
        public JsonResult AssignSearch(string query)
        {
            var userList = _service.searchUsers(query);
            var json = from user in userList select new { id = user.Id, username = user.Username };
            return Json(json);
        }

        [HttpPost]
        public IActionResult AssignOwner(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            var assignUsername = Request.Form["assignUsername"].ToString();
            if(!String.IsNullOrEmpty(assignUsername) && User.Identity.Name != assignUsername)
            {
                if (!_service.assignOwner(session, assignUsername, storeName))
                {
                    ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already owner.");
                }
            } else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
            var owners = _service.getStoreOwners(storeName);
            return View("../Owner/StoreRoleUsers", (owners, storeName, true));
        }

        [HttpPost]
        public IActionResult AssignManager(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            var assignUsername = Request.Form["assignUsername"].ToString();
            if (!String.IsNullOrEmpty(assignUsername) && User.Identity.Name != assignUsername)
            {
                if (!_service.assignManager(session, assignUsername, storeName))
                {
                    ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already manager.");
                }
            }
            else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
            //var owners = _service.getStoreManagers(storeName);
            return View("../Owner/StoreRoleUsers", (managers, storeName, false));
        }
    }
}