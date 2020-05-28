using ECommerceSystem.Models;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Models.Products;
using PresentationLayer.Models.PurchasePolicy;

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
#region Products
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

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("RemoveProduct")]
        public IActionResult RemoveProduct(string store, string productName, string id)
        {
            var session = new Guid(HttpContext.Session.Id);
            var prodID = new Guid(id);
            _service.deleteProduct(session, store, productName, prodID);
            ViewData["StoreName"] = store;
            var products = _service.getStoreInfo(store);
            return View("../Store/StoreInventory", products);
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
        [Route("RemoveProductInv")]
        public IActionResult RemoveProductInv(string store, string productName)
        {
            var session = new Guid(HttpContext.Session.Id);
            _service.deleteProductInv(session, store, productName);
            ViewData["StoreName"] = store;
            var products = _service.getStoreInfo(store);
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
            var quantityLimit = Request.Form["quantity_policy"].ToString() == "true" ? true : false;
            ViewData["StoreName"] = storeName;
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
                    if(model.Keywords != null)
                        model.Keywords.Split(" ").ToList().ForEach(word => keywords.Add(word));
                    if (quantityLimit && (model.MinQuantity != null && model.MaxQuantity != null))
                    {
                        if (_service.addProductInv(session, storeName, model.Description, model.Name, model.Price,
                            model.Quantity, category, keywords, (int)model.MinQuantity, (int)model.MaxQuantity, model.ImageURL) != Guid.Empty)
                        {
                            var products = _service.getStoreInfo(storeName);
                            return View("../Store/StoreInventory", products);
                        }
                    } else
                    {
                        if (_service.addProductInv(session, storeName, model.Description, model.Name, model.Price,
                           model.Quantity, category, keywords, -1, -1, model.ImageURL) != Guid.Empty)
                        {
                            var products = _service.getStoreInfo(storeName);
                            return View("../Store/StoreInventory", products);
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
                    catch (LogicException)
                    {
                        return Redirect("~/Exception/LogicException");
                    }
                }
                if(!String.IsNullOrWhiteSpace(model.ImageURL))
                    ModelState.AddModelError("ImageURLError", "The image URL entered is not a valid image URL!");
            }
            return View("../Store/AddProduct", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("ModifyProduct")]
        public IActionResult ModifyProduct(string store, string id, string prodName)
        {
            ViewData["StoreName"] = store;
            ViewData["ProductID"] = id;
            ViewData["ProductName"] = prodName;
            return View("../Store/ModifyProduct");
        }

        [HttpPost]
        [Route("ModifyProduct")]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult ModifyProduct(AddProductModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var id = new Guid(Request.Form["productID"].ToString());
            var productName = Request.Form["productName"].ToString();
            ViewData["StoreName"] = storeName;
            ViewData["ProductID"] = id;
            ViewData["ProductName"] = productName;
            _service.modifyProductQuantity(session, storeName, productName, id, model.Quantity);
            var products = _service.getStoreInfo(storeName);
            return View("../Store/StoreInventory", products);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddConcreteProduct")]
        public IActionResult AddConcreteProduct(string invId)
        {
            var productInvID = new Guid(invId);
            var model = new AddConcreteProductModel();
            var (productInv, storeName) = _service.getProductInventory(productInvID);
            model.Name = productInv.Name;
            model.ExpDate = DateTime.Now;
            ViewData["StoreName"] = storeName;
            ViewData["ProductName"] = model.Name;
            return View("../Store/AddConcreteProduct", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddConcreteProduct")]
        public IActionResult AddConcreteProduct(AddConcreteProductModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var productName = Request.Form["productName"].ToString();
            var quantityLimit = Request.Form["quantity_policy"].ToString() == "true" ? true : false;
            var state = Request.Form["formState"].ToString();
            ViewData["StoreName"] = storeName;
            ViewData["ProductName"] = productName;
            var valid = false;
            var productID = Guid.Empty;
            if (ModelState.IsValid)
            {
                if (quantityLimit)
                    productID = _service.addProduct(session, storeName, productName, model.Quantity, model.MinQuantity, model.MaxQuantity);
                else productID = _service.addProduct(session, storeName, productName, model.Quantity, -1, -1);
                if (productID == Guid.Empty)
                    ModelState.AddModelError("ProductAddError", "Error occured while trying to add product. check that you paramters are valid.");
                else switch (state)
                    {
                        case "visible":
                            valid = _service.addVisibleDiscount(session, storeName, productID, model.Percentage, model.ExpDate) != Guid.Empty;
                            if (!valid)
                                ModelState.AddModelError("ProductAddError", "Product added but discount addition failed, try again later.");
                            else
                            {
                                var products = _service.getStoreInfo(storeName);
                                return View("../Store/StoreInventory", products);
                            }
                            break;
                        case "conditional":
                            valid = _service.addCondiotionalProcuctDiscount(session, storeName, productID, model.Percentage, model.ExpDate, (int)model.RequiredQuantity) != Guid.Empty;
                            if (!valid)
                                ModelState.AddModelError("ProductAddError", "Product added but discount addition failed, try again later.");
                            else
                            {
                                var products = _service.getStoreInfo(storeName);
                                return View("../Store/StoreInventory", products);
                            }
                            break;
                    }
            }
            return View("../Store/AddConcreteProduct", model);
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
        #endregion
#region Mangers/Owners
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
                if (_service.createOwnerAssignAgreement(session, assignUsername, storeName) == Guid.Empty)
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
            var message = new ActionMessageModel("Your request has been sent for approval.", Url.Action("StoreOwners", "Owner"));
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
        #endregion
#region PurchasePolicy
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("StorePurchasePolicies")]
        public IActionResult StorePurchasePolicies(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var session = new Guid(HttpContext.Session.Id);
            var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
            return View("../Owner/purchase/StorePurchasePolicies", (storeName, policies));
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddPurchasePolicy")]
        public IActionResult AddPurchasePolicy(string storeName)
        {
            ViewData["StoreName"] = storeName;
            return View("../Owner/purchase/AddPurchasePolicy");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddPurchasePolicy")]
        public IActionResult AddPurchasePolicy(AddPurchasePolicyModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var state = Request.Form["formState"].ToString();
            ViewData["StoreName"] = storeName;
            switch (state)
            {
                case "days":
                    var days = new List<DayOfWeek>();
                    for (var i = 1; i <= 7; i++)
                    {
                        if (Request.Form.ContainsKey("day" + i))
                        {
                            var day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), Request.Form["day" + i].ToString());
                            if (!days.Contains(day))
                                days.Add(day);
                        }
                    }
                    if (_service.addDayOffPolicy(session, storeName, days) != Guid.Empty)
                    {
                        var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
                        return View("../Owner/purchase/StorePurchasePolicies", (storeName, policies));
                    }
                    break;
                case "location":
                    var locations = new List<string>();
                    model.BannedLocations.Split(",").ToList().ForEach(loc => locations.Add(loc.Trim()));
                    if (_service.addLocationPolicy(session, storeName, locations) != Guid.Empty)
                    {
                        var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
                        return View("../Owner/purchase/StorePurchasePolicies", (storeName, policies));
                    }
                    break;
                case "price":
                    if (_service.addMinPriceStorePolicy(session, storeName, model.MinPrice) != Guid.Empty)
                    {
                        var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
                        return View("../Owner/purchase/StorePurchasePolicies", (storeName, policies));
                    }
                    break;
            }
            ModelState.AddModelError("PurchasePolicyAddError", "Unable to add purchase policy at the moment, please try again later.");

            ViewData["StoreName"] = storeName;
            return View("../Owner/purchase/AddPurchasePolicy", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddCompositePolicy")]
        public IActionResult AddCompositePolicy(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            var selectlist = new List<SelectListItem>();
            var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
            foreach (var policy in policies)
            {
                selectlist.Add(new SelectListItem
                {
                    Text = policy.GetString(),
                    Value = policy.ID.ToString(),
                });
            }
            ViewData["StoreName"] = storeName;
            return View("../Owner/purchase/AddCompositePolicy", selectlist);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddCompositePolicy")]
        public IActionResult AddCompositePolicy(List<SelectListItem> model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var op = Request.Form["operator"].ToString();
            ViewData["StoreName"] = storeName;
            var ids = new List<Guid>();
            for (int i = 0; i < Request.Form.Count; i++)
            {
                if (Request.Form.ContainsKey("selectd" + i))
                {
                    ids.Add(new Guid(Request.Form["selectd" + i]));
                }
            }
            if (ids.Count != 2)
                ModelState.AddModelError("AddCompositeCountError", "Can only choose two policies to compose. please choose 2 policies.");
            else
            {
                var id1 = ids.ElementAt(0);
                var id2 = ids.ElementAt(1);
                if (ModelState.IsValid)
                {
                    switch (op)
                    {
                        case "Or":
                            if (_service.addOrPurchasePolicy(session, storeName, id1, id2) != Guid.Empty)
                            {
                                return RedirectToAction("StorePurchasePolicies", "Owner", new { storeName = storeName });
                            }
                            break;
                        case "And":
                            if (_service.addAndPurchasePolicy(session, storeName, id1, id2) != Guid.Empty)
                            {
                                return RedirectToAction("StorePurchasePolicies", "Owner", new { storeName = storeName });
                            }
                            break;
                        case "Xor":
                            if (_service.addXorPurchasePolicy(session, storeName, id1, id2) != Guid.Empty)
                            {
                                return RedirectToAction("StorePurchasePolicies", "Owner", new { storeName = storeName });
                            }
                            break;
                    }
                    ModelState.AddModelError("AddCompositeDiscountError", "Error occured while trying to add store discount. check that you paramters are valid.");
                }
            }
            var selectlist = new List<SelectListItem>();
            var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
            foreach (var policy in policies)
            {
                selectlist.Add(new SelectListItem
                {
                    Text = policy.GetString(),
                    Value = policy.ID.ToString(),
                });
            }
            return View("../Owner/purchase/AddCompositePolicy", selectlist);
        }
    #endregion

    #region Discounts
    [Authorize(Roles = "Admin, Subscribed")]
        [Route("StoreDiscounts")]
        public IActionResult StoreDiscounts(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var session = new Guid(HttpContext.Session.Id);
            var discounts = _service.getAllStoreLevelDiscounts(session, storeName);
            return View("../Owner/StoreDiscounts", (storeName, discounts));
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddStoreDiscount")]
        public IActionResult AddStoreDiscount(string storeName)
        {
            ViewData["StoreName"] = storeName;
            return View("../Owner/discounts/AddStoreDiscount");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddStoreDiscount")]
        public IActionResult AddStoreDiscount(AddStoreDiscountModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            ViewData["StoreName"] = storeName;
            if (ModelState.IsValid)
            {
                if (_service.addConditionalStoreDiscount(session, storeName, model.Percentage, model.ExpDate, (int)model.MinPrice) != Guid.Empty)
                {
                    var discounts = _service.getAllStoreLevelDiscounts(session, storeName);
                    return View("../Owner/StoreDiscounts", (storeName, discounts));
                }
                ModelState.AddModelError("AddStoreDiscountError", "Error occured while trying to add store discount. check that you paramters are valid.");
            }
            return View("../Owner/discounts/AddStoreDiscount", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddProductDiscount")]
        public IActionResult AddProductDiscount(string storeName, string id)
        {
            ViewData["StoreName"] = storeName;
            ViewData["ProductID"] = id;
            return View("../Owner/discounts/AddProductDiscount");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddProductDiscount")]
        public IActionResult AddProductDiscount(AddProductDiscountModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var productID = new Guid(Request.Form["ProductID"].ToString());
            var state = Request.Form["formState"].ToString();
            ViewData["StoreName"] = storeName;
            ViewData["ProductID"] = productID;
            if (ModelState.IsValid)
            {   
                switch(state)
                {
                    case "visible":
                        if (_service.addVisibleDiscount(session, storeName, productID, model.Percentage, model.ExpDate) != Guid.Empty)
                        {
                            var products = _service.getStoreInfo(storeName);
                            return View("../Store/StoreInventory", products);
                        }
                        ModelState.AddModelError("AddProductDiscountError", "Error occured while trying to add store discount. check that you paramters are valid.");
                        break;
                    case "conditional":
                        if (_service.addCondiotionalProcuctDiscount(session, storeName, productID, model.Percentage, model.ExpDate, (int)model.RequiredQuantity) != Guid.Empty)
                        {
                            var products = _service.getStoreInfo(storeName);
                            return View("../Store/StoreInventory", products);
                        }
                        ModelState.AddModelError("AddProductDiscountError", "Error occured while trying to add store discount. check that you paramters are valid.");
                        break;
                }
            }
            return View("../Owner/discounts/AddProductDiscount", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("CompositeDiscounts")]
        public IActionResult CompositeDiscounts(string storeName)
        {
            ViewData["StoreName"] = storeName;
            var session = new Guid(HttpContext.Session.Id);
            var discounts = _service.getAllDiscountsForCompose(session, storeName);
            return View("../Owner/discounts/StoreDiscountPolicies", (storeName, discounts));
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddCompositeDiscount")]
        public IActionResult AddCompositeDiscount(string storeName, string id)
        {
            var session = new Guid(HttpContext.Session.Id);
            var selectlist = new List<SelectListItem>();
            var discounts = _service.getAllDiscountsForCompose(session, storeName);
            foreach(var discount in discounts)
            {
                selectlist.Add(new SelectListItem
                {
                    Text = discount.GetString(),
                    Value = discount.ID.ToString(),
                });
            }
            ViewData["StoreName"] = storeName;
            ViewData["ProductID"] = id;
            return View("../Owner/discounts/AddCompositeDiscount", selectlist);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddCompositeDiscount")]
        public IActionResult AddCompositeDiscount(List<SelectListItem> model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var op = Request.Form["operator"].ToString();
            ViewData["StoreName"] = storeName;
            var ids = new List<Guid>();
            for(int i = 0; i < Request.Form.Count; i++)
            {
                if (Request.Form.ContainsKey("selectd" + i))
                {
                    ids.Add(new Guid(Request.Form["selectd" + i]));
                }
            }
            if (ModelState.IsValid)
            {
                switch (op)
                {
                    case "Or":
                        if(_service.addOrDiscountPolicy(session, storeName, ids) != Guid.Empty)
                        {
                            return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName });
                        }
                        break;
                    case "And":
                        if(_service.addAndDiscountPolicy(session, storeName, ids) != Guid.Empty)
                        {
                            return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName });
                        }
                        break;
                    case "Xor":
                        if (_service.addXorDiscountPolicy(session, storeName, ids) != Guid.Empty)
                        {
                            return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName });
                        }
                        break;
                }
                ModelState.AddModelError("AddCompositeDiscountError", "Error occured while trying to add store discount. check that you paramters are valid.");

            }
            var selectlist = new List<SelectListItem>();
            var discounts = _service.getAllDiscountsForCompose(session, storeName);
            foreach (var discount in discounts)
            {
                selectlist.Add(new SelectListItem
                {
                    Text = discount.GetString(),
                    Value = discount.ID.ToString(),
                });
            }
            return View("../Owner/discounts/AddCompositeDiscount", selectlist);
        }
    }
    #endregion
}