﻿using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Utilities.extensions;
using System.Net;
using System.Globalization;
using ECommerceSystem.Exceptions;

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
            try
            {
                var products = _service.getStoreInfo(storeName);
                return View("../Store/StoreInventory", products);
            }
            catch (AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }

        }

        [Route("ProductGroupListing")]
        public IActionResult ProductGroupListing(string store, string id)
        {
            ViewData["StoreName"] = store;
            var prodID = new Guid(id);
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                var products = _service.getStoreProductGroup(session, prodID, store);
                return View("../Store/StoreProducts", products);
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }

        }

        [Route("ModifyProductGroup")]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult ModifyProductGroup(string id, string store)
        {
            ViewData["StoreName"] = store;
            try
            {
                var p = _service.getProductInventory(new Guid(id)).Item1;
                string keywords = "";
                p.Keywords.ForEach(word => keywords += word + " ");
                var model = new ProductInventoryModel(p.ID, p.Name, p.Price, p.Description, p.Category, p.Rating, p.RaterCount, new HashSet<string>(p.Keywords), p.ImageURL);
                return View("../Store/ModifyProductGroup", model);
            }
            catch (AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }

        }

        [HttpPost]
        [Route("ModifyProductGroup")]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult ModifyProductGroup(ProductInventoryModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var oldName = Request.Form["oldName"].ToString();
            try
            {
                _service.modifyProductName(session, storeName, model.Name, oldName);
                _service.modifyProductPrice(session, storeName, model.Name, (int)model.Price);
                var products = _service.getStoreInfo(storeName);
                return View("../Store/StoreInventory", products);
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
                if (String.IsNullOrWhiteSpace(model.ImageURL) || IsImageUrl(model.ImageURL))
                {
                    Category category = (Category)Enum.Parse(typeof(Category), model.Category);
                    var keywords = new List<string>();
                    model.Name.Split(" ").ToList().ForEach(word => keywords.Add(word));
                    if(model.Keywords != null)
                        model.Keywords.Split(" ").ToList().ForEach(word => keywords.Add(word)) ;
                    try
                    {
                        if (_service.addProductInv(session, storeName, model.Description, model.Name, model.Price,
                            model.Quantity, category, keywords, -1, -1, model.ImageURL) != Guid.Empty)
                        {
                            var products = _service.getStoreInfo(storeName);
                            return View("../Store/StoreInventory", products);
                        }
                        ModelState.AddModelError("AddProductError", "Error occured while trying to add product. check that you paramters are valid.");
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
                if(!String.IsNullOrWhiteSpace(model.ImageURL))
                    ModelState.AddModelError("AddProductError", "The image URL entered is not a valid image URL!");
            }
            return View("../Store/AddProduct", model);
        }

        bool IsImageUrl(string URL)
        {
            try
            {
                var req = (HttpWebRequest)HttpWebRequest.Create(URL);
                req.Method = "HEAD";
                using (var resp = req.GetResponse())
                {
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                               .StartsWith("image/");
                }
            } catch(Exception)
            {
                return false;
            }
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult StoreOwners(string storeName)
        {
            try
            {
                var owners = _service.getStoreOwners(storeName);
                return View("../Owner/StoreOwners", owners);
            }
            catch (AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }

        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult StoreManagers(string storeName)
        {
            try
            {
                var managers = _service.getStoreManagers(storeName);
                return View("../Owner/StoreManagers", managers);
            }
            catch (AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpGet]
        public JsonResult AssignSearch(string query, string storeName)
        {
            try
            {
                var userList = _service.searchUsers(query);
                var userPermissions = userList.Select(user => (user, _service.getUsernamePermissionTypes(storeName, user.Username).Values.ToArray()));
                var json = from userPerm in userPermissions select new { id = userPerm.Item1.Id, username = userPerm.Item1.Username, permissions = userPerm.Item2 };
                return Json(json);
            }
            catch (Exception) { return Json(new object()); }

        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpPost]
        public IActionResult AssignOwner(string storeName)
        {

            var session = new Guid(HttpContext.Session.Id);
            var assignUsername = Request.Form["assignUsername"].ToString();
            if (!String.IsNullOrEmpty(assignUsername) && User.Identity.Name != assignUsername)
            {
                try
                {
                    if (_service.createOwnerAssignAgreement(session, assignUsername, storeName) != Guid.Empty)
                    {
                        ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already owner.");
                    }
                }
                catch (AuthenticationException)
                {
                    return Redirect("~/Exception/AuthException");
                }
                catch (DatabaseException)
                {
                    return Redirect("~/Exception/DatabaseException");
                }
            }
            else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
            var message = new ActionMessageModel("Your request has benn sent for approval.", Url.Action("StoreOwners", "Owner"));
            return View("_ActionMessage", message);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpPost]
        public IActionResult AssignManager(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            List<PermissionType> givenPermissions = new List<PermissionType>();
            var assignUsername = Request.Form["assignUsername"].ToString();
            var permissionTypeEnums = Enum.GetValues(typeof(PermissionType)).Cast<PermissionType>();
            var permissionTypes = permissionTypeEnums.ToDictionary(k => k.GetStringValue(), v => v);
            for (var i = 0; i < permissionTypeEnums.Count(); ++i)
            {
                if (Request.Form.ContainsKey("permission_" + i))
                {
                    givenPermissions.Add(permissionTypes[Request.Form["permission_" + i].ToString()]);
                }
            }
            try
            {
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
            catch (AuthenticationException)
            {
                return Redirect("~/Exception/AuthException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [HttpPost]
        public IActionResult RemoveManager(string manager, string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                if (!_service.removeManager(session, manager, storeName))
                {
                    ModelState.AddModelError("InvalidRemoveOperation", $"Error occured while trying to remove manager '{manager}'. try again later.");
                }
                var managers = _service.getStoreManagers(storeName);
                return View("../Owner/StoreManagers", managers);
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
        public IActionResult EditManagerPermissions(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            List<PermissionType> givenPermissions = new List<PermissionType>();
            var editUsername = Request.Form["editUsername"].ToString();
            var permissionTypeEnums = Enum.GetValues(typeof(PermissionType)).Cast<PermissionType>();
            var permissionTypes = permissionTypeEnums.ToDictionary(k => k.GetStringValue(), v => v);
            for (var i = 0; i < permissionTypeEnums.Count(); ++i)
            {
                if (Request.Form.ContainsKey("permission2_" + i))
                {
                    givenPermissions.Add(permissionTypes[Request.Form["permission2_" + i].ToString()]);
                }
            }
            try
            {
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
    }
}