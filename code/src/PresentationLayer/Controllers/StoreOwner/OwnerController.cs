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

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("StoreProducts")]
        public IActionResult StoreProductsView(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var products = _service.getStoreInfo(storeName);

            return View("../Store/StoreInventory", products);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddProduct")]
        public IActionResult AddProduct(string store)
        {
            ViewData["StoreName"] = store;
            return View("../Store/AddProduct");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddProduct")]
        public IActionResult AddProduct(AddProductModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            if (ModelState.IsValid)
            {
                Category category = (Category)Enum.Parse(typeof(Category), model.Category);
                var keywords = new List<string>();
                model.Name.Split(" ").ToList().ForEach(word => keywords.Add(word));
                model.Keywords.Split(" ").ToList().ForEach(word => keywords.Add(word));
                if (_service.addProductInv(session, storeName, model.Description, model.Name, model.BasePrice,
                    model.Quantity, category, keywords, model.MinPurchaseQuantity, model.MaxPurchaseQuantity) != Guid.Empty)
                {
                    var products = _service.getStoreInfo(storeName);
                    return View("../Store/StoreInventory", products);
                }
                ModelState.AddModelError("AddProductError", "Error occured while trying to add product. check that you paramters are valid.");
            }
            return View("../Store/AddProduct", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult StoreOwners(string storeName)
        {
            var owners = _service.getStoreOwners(storeName);
            return View("../Owner/StoreOwners", owners);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult StoreManagers(string storeName)
        {
            var managers = _service.getStoreManagers(storeName); 
            return View("../Owner/StoreManagers", managers);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpGet]
        public JsonResult AssignSearch(string query, string storeName)
        {
            var userList = _service.searchUsers(query);
            var userPermissions = userList.Select(user => (user, _service.getUsernamePermissionTypes(storeName, user.Username).Values.ToArray()));
            var json = from userPerm in userPermissions select new { id = userPerm.Item1.Id, username = userPerm.Item1.Username, permissions = userPerm.Item2 };
            return Json(json);
        }

        [Authorize(Roles = "Admin, Subscribed")]
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
            return View("../Owner/StoreOwners", owners);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpPost]
        public IActionResult AssignManager(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            List<PermissionType> givenPermissions = new List<PermissionType>();
            var assignUsername = Request.Form["assignUsername"].ToString();
            var permissionTypes = PermissionType.Descriptions();
            for (var i = 0; i < PermissionType.Descriptions().Count(); ++i)
            {
                if (Request.Form.ContainsKey("permission_" + i)) {
                    givenPermissions.Add(permissionTypes[Request.Form["permission_" + i].ToString()]);
                }
            }
            if (!String.IsNullOrEmpty(assignUsername) && User.Identity.Name != assignUsername)
            {
                if (!_service.assignManager(session, assignUsername, storeName) || !_service.editPermissions(session, storeName, assignUsername, givenPermissions))
                {
                    ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already manager.");
                }
            }
            else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
            var managers = _service.getStoreManagers(storeName);
            return View("../Owner/StoreManagers", managers);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpPost]
        public IActionResult RemoveManager(string manager, string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            if(!_service.removeManager(session, manager, storeName)) {
                ModelState.AddModelError("InvalidRemoveOperation", $"Error occured while trying to remove manager '{manager}'. try again later.");
            }
            var managers = _service.getStoreManagers(storeName);
            return View("../Owner/StoreManagers", managers);
        }

        [HttpPost]
        public IActionResult EditManagerPermissions(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            List<PermissionType> givenPermissions = new List<PermissionType>();
            var editUsername = Request.Form["editUsername"].ToString();
            var permissionTypes = PermissionType.Descriptions();
            for (var i = 0; i < permissionTypes.Count(); ++i)
            {
                if (Request.Form.ContainsKey("permission2_" + i))
                {
                    givenPermissions.Add(permissionTypes[Request.Form["permission2_" + i].ToString()]);
                }
            }
            if (!String.IsNullOrEmpty(editUsername) && User.Identity.Name != editUsername)
            {
                if (!_service.editPermissions(session, storeName, editUsername, givenPermissions))
                {
                    ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already manager.");
                }
            }
            else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
            var managers = _service.getStoreManagers(storeName);
            return View("../Owner/StoreManagers", managers);
        }
    }
}