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
using ECommerceSystem.Exceptions;
using Microsoft.AspNetCore.Http;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Models.DiscountPolicyModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace PresentationLayer.Controllers.StoreOwner
{
    [Authorize(Roles = "Admin, Subscribed")]
    public class OwnerController : BaseController
    {
        public OwnerController(IService service) : base(service) { }

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
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
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
                return View("../Store/StoreProducts", (products, id));
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
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("RemoveOwner")]
        public IActionResult RemoveOwner(string username, string store)
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                if (!_service.removeOwner(session, username, store))
                {
                    return RedirectToAction("StoreOwners", "Owner", new { error = true });
                }
                return RedirectToAction("StoreOwners", "Owner", new { storeName = store });
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
        [Route("RemoveProduct")]
        public IActionResult RemoveProduct(string store, string productName, string id)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(store, User.Identity.Name);
                if (!permissions[PermissionType.DeleteProductInv])
                    return Redirect("~/Exception/ManagementException");
                var session = new Guid(HttpContext.Session.Id);
                var prodID = new Guid(id);
                _service.deleteProduct(session, store, productName, prodID);
                ViewData["StoreName"] = store;
                var products = _service.getStoreInfo(store);
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

        [Route("ModifyProductGroup")]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult ModifyProductGroup(string id, string store)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(store, User.Identity.Name);
                if (!permissions[PermissionType.ModifyProduct])
                    return Redirect("~/Exception/ManagementException");
                ViewData["StoreName"] = store;
                var p = _service.getProductInventory(new Guid(id)).Item1;
                string keywords = "";
                p.Keywords.ForEach(word => keywords += word + " ");
                var model = new ProductInventoryModel(p.ID, p.Name, p.Price, p.Description, p.Category, p.Rating, p.RaterCount, new HashSet<string>(p.Keywords), p.ImageURL, store);
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
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }

        }

        [HttpPost]
        [Route("ModifyProductGroup")]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult ModifyProductGroup(ProductInventoryModel model)
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var permissions = _service.getUsernamePermissionTypes(model.StoreName, User.Identity.Name);
                if (!permissions[PermissionType.ModifyProduct])
                    return Redirect("~/Exception/ManagementException");
                var storeName = Request.Form["storeName"].ToString();
                var oldName = Request.Form["oldName"].ToString();
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
            try
            {
                var permissions = _service.getUsernamePermissionTypes(store, User.Identity.Name);
                if (!permissions[PermissionType.DeleteProductInv])
                    return Redirect("~/Exception/ManagementException");
                var session = new Guid(HttpContext.Session.Id);
                _service.deleteProductInv(session, store, productName);
                ViewData["StoreName"] = store;
                var products = _service.getStoreInfo(store);
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
            try
            {
                var permissions = _service.getUsernamePermissionTypes(store, User.Identity.Name);
                if (!permissions[PermissionType.AddProductInv])
                    return Redirect("~/Exception/ManagementException");
                ViewData["StoreName"] = store;
                return View("../Store/AddProduct");
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
                    {
                        if (quantityLimit && (model.MinQuantity != null && model.MaxQuantity != null))
                        {
                            if (_service.addProductInv(session, storeName, model.Description, model.Name, model.Price,
                                model.Quantity, category, keywords, (int)model.MinQuantity, (int)model.MaxQuantity, model.ImageURL) != Guid.Empty)
                            {
                                var products = _service.getStoreInfo(storeName);
                                return View("../Store/StoreInventory", products);
                            }
                        }
                        else
                        {
                            if (_service.addProductInv(session, storeName, model.Description, model.Name, model.Price,
                               model.Quantity, category, keywords, -1, -1, model.ImageURL) != Guid.Empty)
                            {
                                var products = _service.getStoreInfo(storeName);
                                return View("../Store/StoreInventory", products);
                            }
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
            try
            {
                var permissions = _service.getUsernamePermissionTypes(store, User.Identity.Name);
                if (!permissions[PermissionType.ModifyProduct])
                    return Redirect("~/Exception/ManagementException");
                ViewData["StoreName"] = store;
                ViewData["ProductID"] = id;
                ViewData["ProductName"] = prodName;
                return View("../Store/ModifyProduct");
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
            try
            {
                _service.modifyProductQuantity(session, storeName, productName, id, model.Quantity);
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
        [Route("AddConcreteProduct")]
        public IActionResult AddConcreteProduct(string invId)
        {
            var productInvID = new Guid(invId);
            var model = new AddConcreteProductModel();
            try
            {
                var (productInv, storeName) = _service.getProductInventory(productInvID);
                model.Name = productInv.Name;
                model.ExpDateVis = DateTime.Now;
                model.ExpDateCond = DateTime.Now;
                model.ExpDateCondProd = DateTime.Now;
                ViewData["StoreName"] = storeName;
                ViewData["ProductName"] = model.Name;
                return View("../Store/AddConcreteProduct", model);
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
            try
            {
                if(model.Quantity <= 0)
                    ModelState.AddModelError("ProductAddErrorQuantity", "Cannot add zero quantity to products");
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
                            valid = _service.addVisibleDiscount(session, storeName, productID, model.PercentageVis, model.ExpDateVis) != Guid.Empty;
                            if (!valid)
                                ModelState.AddModelError("ProductAddError", "Product added but discount addition failed, try again later.");
                            else
                            {
                                var products = _service.getStoreInfo(storeName);
                                return View("../Store/StoreInventory", products);
                            }
                            break;
                        case "conditional":
                            valid = _service.addCondiotionalProcuctDiscount(session, storeName, productID, model.PercentageCond, model.ExpDateCond, (int)model.RequiredQuantity) != Guid.Empty;
                            if (!valid)
                                ModelState.AddModelError("ProductAddError", "Product added but discount addition failed, try again later.");
                            else
                            {
                                var products = _service.getStoreInfo(storeName);
                                return View("../Store/StoreInventory", products);
                            }
                            break;
                        case "conditional_product":
                                return RedirectToAction("AddConditionalCompositeProductsDiscount", new { storeName, id = productID, percentage = model.PercentageCondProd.ToString(), expDate = model.ExpDateCondProd.ToString() });
                        default:
                        {
                            var products = _service.getStoreInfo(storeName);
                            return View("../Store/StoreInventory", products);
                        }
                }
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
        public IActionResult StoreOwners(string storeName, bool reassignError = false, bool error = false)
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var owners = _service.getStoreOwners(session, storeName);
                if(error)
                    ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already owner.");
                if(reassignError)
                    ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
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
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }

        }

        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult StoreManagers(string storeName)
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var managers = _service.getStoreManagers(session, storeName);
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

        [HttpGet]
        [Authorize(Roles = "Admin, Subscribed")]
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

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult AssignOwner(string storeName)
        {
            var session = new Guid(HttpContext.Session.Id);
            var assignUsername = Request.Form["assignUsername"].ToString();
            if (!String.IsNullOrEmpty(assignUsername) && User.Identity.Name != assignUsername)
            {
                try
                {
                    if (_service.createOwnerAssignAgreement(session, assignUsername, storeName) == Guid.Empty)
                    {
                        return RedirectToAction("StoreOwners", "Owner", new { storeName = storeName, error = true });
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
            else return RedirectToAction("StoreOwners", "Owner", new { storeName = storeName, reassignError = true });
            var message = new ActionMessageModel("Your request has been sent for approval.", Url.Action("StoreOwners", "Owner", new { storeName = storeName }));
            return View("_ActionMessage", message);
        }

        public IActionResult ApproveOwner(string id, string store)
        {
            var session = new Guid(HttpContext.Session.Id);
            var requestID = new Guid(id);
            try
            {
                if (_service.approveAssignOwnerRequest(session, requestID, store))
                {
                    var requestCount = Int32.Parse(HttpContext.Session.GetString("RequestCount"));
                    HttpContext.Session.SetString("RequestCount", (requestCount - 1).ToString());
                }
                else
                {
                    var message = new ActionMessageModel("Failed to approve owner. Please try again later.", Url.Action("UserRequests", "User"));
                    var requestCount = Int32.Parse(HttpContext.Session.GetString("RequestCount"));
                    HttpContext.Session.SetString("RequestCount", (requestCount - 1).ToString());
                    return View("_ErrorMessage", message);
                }
                return RedirectToAction("UserRequests", "User");
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

        public IActionResult DenyOwner(string id, string store)
        {
            var session = new Guid(HttpContext.Session.Id);
            var requestID = new Guid(id);
            try
            {
                if (_service.disApproveAssignOwnerRequest(session, requestID, store))
                {
                    var requestCount = Int32.Parse(HttpContext.Session.GetString("RequestCount"));
                    HttpContext.Session.SetString("RequestCount", (requestCount - 1).ToString());
                }
                return RedirectToAction("UserRequests", "User");
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
        [HttpPost]
        public IActionResult AssignManager(string storeName, bool novice)
        {
            var session = new Guid(HttpContext.Session.Id);
            List<PermissionType> givenPermissions = new List<PermissionType>();
            var assignUsername = Request.Form["assignUsername"].ToString();
            var assignUsername2 = Request.Form["assignUsername2"].ToString();
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
                if (!novice && !String.IsNullOrEmpty(assignUsername) && User.Identity.Name != assignUsername)
                {
                        if (!_service.assignManager(session, assignUsername, storeName) || !_service.editPermissions(session, storeName, assignUsername, givenPermissions))
                        {
                            ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already manager.");
                        }
                } else if(!String.IsNullOrEmpty(assignUsername2) && User.Identity.Name != assignUsername2)
                {
                    if (!_service.assignManager(session, assignUsername2, storeName))
                    {
                        ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already manager.");
                    }
                }
                else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
                var managers = _service.getStoreManagers(session, storeName);
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
                var managers = _service.getStoreManagers(session, storeName);
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
                        ModelState.AddModelError("ErrorAssignSelection", "Error encountred durring assigning process: Check that selected is not already manager or that you assigned this manager.");
                    }
                }
                else ModelState.AddModelError("InvalidAssignSelection", "Invalid assign selection: Selection can't be empty and you can't select current active user.");
                var managers = _service.getStoreManagers(session, storeName);
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

        [Route("StorePurchaseHistory")]
        [Authorize(Roles = "Admin, Subscribed")]
        public IActionResult StorePurchaseHistory(string store)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(store, User.Identity.Name);
                if (!permissions[PermissionType.WatchPurchaseHistory])
                    return Redirect("~/Exception/ManagementException");
                var session = new Guid(HttpContext.Session.Id);
                var purchases = _service.purchaseHistory(session, store);
                return View("../Store/StorePurchaseHistory", (purchases, store));
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
        public IActionResult StorePurchasePolicies(string storeName, bool error = false)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManagePurchasePolicy])
                    return Redirect("~/Exception/ManagementException");
                if(error)
                    ModelState.AddModelError("PolicysError", "Unable to complete operation at the moment, please try again later.");
                ViewData["StoreName"] = storeName;
                var session = new Guid(HttpContext.Session.Id);
                    var policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
                    return View("../Owner/purchase/StorePurchasePolicies", (storeName, policies));
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
        [Route("AddPurchasePolicy")]
        public IActionResult AddPurchasePolicy(string storeName)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManagePurchasePolicy])
                    return Redirect("~/Exception/ManagementException");
                ViewData["StoreName"] = storeName;
                return View("../Owner/purchase/AddPurchasePolicy");
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
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddPurchasePolicy")]
        public IActionResult AddPurchasePolicy(AddPurchasePolicyModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            var state = Request.Form["formState"].ToString();
            ViewData["StoreName"] = storeName;
            try
            {
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

            ModelState.AddModelError("PurchasePolicyAddError", "Unable to add purchase policy at the moment, please try again later.");

            ViewData["StoreName"] = storeName;
            return View("../Owner/purchase/AddPurchasePolicy", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddCompositePolicy")]
        public IActionResult AddCompositePolicy(string storeName)
        {
            List<PurchasePolicyModel> policies;
            var session = new Guid(HttpContext.Session.Id);
            var selectlist = new List<SelectListItem>();
            try
            {
                 policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
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
        [Route("RemovePurchasePolicy")]
        public IActionResult RemovePurchasePolicy(string storeName, string policyID)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManagePurchasePolicy])
                    return Redirect("~/Exception/ManagementException");
                var session = new Guid(HttpContext.Session.Id);
                var id = new Guid(policyID);
                if (!_service.removePurchasePolicy(session, storeName, id))
                {
                    return RedirectToAction("StorePurchasePolicies", "Owner", new { storeName = storeName, error = true });
                }
                return RedirectToAction("StorePurchasePolicies", "Owner", new { storeName = storeName });
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
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddCompositePolicy")]
        public IActionResult AddCompositePolicy(List<SelectListItem> model)
        {
            List<PurchasePolicyModel> policies;
            List<SelectListItem> selectlist;
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
            try
            {
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
                selectlist = new List<SelectListItem>();
                policies = _service.getAllPurchasePolicyByStoreName(session, storeName);
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
        public IActionResult StoreDiscounts(string storeName, bool error = false)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManageDiscounts])
                    return Redirect("~/Exception/ManagementException");
                if(error)
                    ModelState.AddModelError("DiscountsError", "Unable to complete operation at the moment, please try again later.");
                ViewData["StoreName"] = storeName;
                var session = new Guid(HttpContext.Session.Id);
                var discounts = _service.getAllStoreLevelDiscounts(session, storeName);
                return View("../Owner/StoreDiscounts", (storeName, discounts));
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
        [Route("AddStoreDiscount")]
        public IActionResult AddStoreDiscount(string storeName)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManageDiscounts])
                    return Redirect("~/Exception/ManagementException");
                ViewData["StoreName"] = storeName;
                return View("../Owner/discounts/AddStoreDiscount");
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
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddStoreDiscount")]
        public IActionResult AddStoreDiscount(AddStoreDiscountModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            var storeName = Request.Form["storeName"].ToString();
            ViewData["StoreName"] = storeName;
            try
            {
                if (ModelState.IsValid)
                {
                    if (_service.addConditionalStoreDiscount(session, storeName, model.Percentage, model.ExpDate, (int)model.MinPrice) != Guid.Empty)
                    {
                        var discounts = _service.getAllStoreLevelDiscounts(session, storeName);
                        return View("../Owner/StoreDiscounts", (storeName, discounts));
                    }
                    ModelState.AddModelError("AddStoreDiscountError", "Error occured while trying to add store discount. check that you paramters are valid.");
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

            return View("../Owner/discounts/AddStoreDiscount", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("RemoveDiscount")]
        public IActionResult RemoveDiscount(string storeName, string discountID, string type, string productID = null)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManageDiscounts])
                    return Redirect("~/Exception/ManagementException");
                var session = new Guid(HttpContext.Session.Id);
                var id = new Guid(discountID);
                switch(type)
                {
                    case "simple":
                        if(!_service.removeProductDiscount(session, storeName, id, new Guid(productID)))
                            return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName, error = true });
                        break;
                    case "composite":
                        if (!_service.removeCompositeDiscount(session, storeName, id))
                            return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName, error = true });
                        break;
                }
                return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName });
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
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("RemoveStoreDiscount")]
        public IActionResult RemoveStoreDiscount(string storeName, string discountID)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManageDiscounts])
                    return Redirect("~/Exception/ManagementException");
                var session = new Guid(HttpContext.Session.Id);
                var id = new Guid(discountID);
                if(!_service.removeStoreLevelDiscount(session, storeName, id))
                {
                    return RedirectToAction("StoreDiscounts", "Owner", new { storeName = storeName, error = true });
                }
                return RedirectToAction("StoreDiscounts", "Owner", new { storeName = storeName });
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
        [Route("AddProductDiscount")]
        public IActionResult AddProductDiscount(string storeName, string id)
        {
            try {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManageDiscounts])
                    return Redirect("~/Exception/ManagementException");
                ViewData["StoreName"] = storeName;
                ViewData["ProductID"] = id;
                return View("../Owner/discounts/AddProductDiscount");
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
                try
                {
                    switch (state)
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
                        case "conditional_product":
                            {
                                if (model.ExpDate < DateTime.Now)
                                {
                                    ModelState.AddModelError("ExpirationError", "You chose a date prior to today.");
                                }
                                if (model.Percentage < 0 || model.Percentage > 100)
                                {
                                    ModelState.AddModelError("PercentageError", "Percentage can only be in range 0-100");
                                }
                                if (ModelState.IsValid)
                                    return RedirectToAction("AddConditionalCompositeProductsDiscount", new { storeName = storeName, id = productID, percentage = model.Percentage.ToString(), expDate = model.ExpDate.ToString() });
                            }
                            break;
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
            return View("../Owner/discounts/AddProductDiscount", model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        [Route("CompositeDiscounts")]
        public IActionResult CompositeDiscounts(string storeName, bool error = false)
        {
            try
            {
                var permissions = _service.getUsernamePermissionTypes(storeName, User.Identity.Name);
                if (!permissions[PermissionType.ManageDiscounts])
                    return Redirect("~/Exception/ManagementException");
                if (error)
                    ModelState.AddModelError("DiscountsError", "Unable to complete operation at the moment, please try again later.");
                ViewData["StoreName"] = storeName;
                var session = new Guid(HttpContext.Session.Id);
                var discounts = _service.getAllDiscountsForCompose(session, storeName);
                return View("../Owner/discounts/StoreDiscountPolicies", (storeName, discounts));
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
        [Route("AddCompositeDiscount")]
        public IActionResult AddCompositeDiscount(string storeName, string id)
        {
            List<DiscountPolicyModel> discounts;
            var session = new Guid(HttpContext.Session.Id);
            var selectlist = new List<SelectListItem>();
            try
            {
                discounts = _service.getAllDiscountsForCompose(session, storeName);
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
            List<SelectListItem> selectlist;
            List<DiscountPolicyModel> discounts;
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
            try
            {
                if (ids.Count == 0)
                    ModelState.AddModelError("AddDiscountNoArgs", "No Discounts selected for composition.");
                if (ModelState.IsValid)
                {
                    if (ModelState.IsValid)
                    {
                        switch (op)
                        {
                            case "Or":
                                if (_service.addOrDiscountPolicy(session, storeName, ids) != Guid.Empty)
                                {
                                    return RedirectToAction("CompositeDiscounts", "Owner", new { storeName = storeName });
                                }
                                break;
                            case "And":
                                if (_service.addAndDiscountPolicy(session, storeName, ids) != Guid.Empty)
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
                }
                selectlist = new List<SelectListItem>();
                discounts = _service.getAllDiscountsForCompose(session, storeName);
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

        private ConditionalCompositeProductDiscModel ProductsConditionTree
        {
            get
            {
                var context = HttpContext;
                var tree = context.Session.GetString("ProductsConditionTree");
                if (String.IsNullOrEmpty(tree))
                {
                    return new ConditionalCompositeProductDiscModel();
                }
                else
                {
                    return JsonConvert.DeserializeObject<ConditionalCompositeProductDiscModel> (tree, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    });
                }
            }

            set
            {
                if (value == null)
                {
                    HttpContext.Session.SetString("ProductsConditionTree", "");
                }
                else
                {
                    var serialized = JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects,
                        TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                    });
                    HttpContext.Session.SetString("ProductsConditionTree", serialized);
                }
            }
        }


        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddConditionalCompositeProductsDiscount")]
        public IActionResult AddConditionalCompositeProductsDiscount(string storeName, string id, string percentage, string expDate, bool submit = false)
        {
            var session = new Guid(HttpContext.Session.Id);
            var selectlist = new List<SelectListItem>();
            var products = new List<ProductModel>();
            var expiration = DateTime.Now;
            var perc = 0.0f;
            var prodID = Guid.Empty;
             var name = "";
            var condTree = ProductsConditionTree;
            try
            {
                if (submit)
                {
                    var temp = condTree.productTreeModel;
                    condTree.productTreeModel = new CompositeDiscountPolicyModel(Guid.NewGuid(), new List<DiscountPolicyModel>() { temp }, CompositeType.And);
                    if (_service.addConditionalCompositeProcuctDiscount(session, condTree.StoreName, condTree.ProductID, condTree.Percentage, condTree.ExpDate, condTree.productTreeModel) == Guid.Empty)
                        ModelState.AddModelError("AddCompositeDiscountError", "Error occured while trying to add store discount. check that you paramters are valid. maybe discount is already applied.");
                    else
                    {
                        var prods = _service.getStoreInfo(condTree.StoreName);
                        return View("../Store/StoreInventory", prods);
                    }
                }
                expiration = submit ? condTree.ExpDate : DateTime.Parse(expDate);
                perc = submit ? condTree.Percentage : float.Parse(percentage);
                prodID = submit? condTree.ProductID : Guid.Parse(id);
                var inventory = _service.getStoreInfo(submit? condTree.StoreName : storeName).Item2;
                foreach(var prodInv in inventory)
                {
                    var prods = _service.getStoreProductGroup(session, prodInv.ID, submit ? condTree.StoreName : storeName).Item2;
                    foreach (var prod in prods)
                    {
                        if (prod.Id != prodID)
                            products.Add(prod);
                        else name = prod.Name;
                    }
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
            foreach (var prod in products)
            {
                selectlist.Add(new SelectListItem
                {
                    Text = prod.GetString(),
                    Value = prod.Id.ToString()
                });
            }
            ViewData["StoreName"] = submit ? condTree.StoreName : storeName;
            ViewData["ProductID"] = submit ? condTree.ProductID.ToString() : id;
            var tree = new ConditionalCompositeProductDiscModel(Guid.NewGuid(), expiration, prodID, name, perc, storeName);
            
            ProductsConditionTree = tree;
            return View("../Owner/discounts/AddCompositeProductDiscount", (selectlist, tree, false));
        }


        private IEnumerable<CompositeDiscountPolicyModel> GetAllFromTree(CompositeDiscountPolicyModel tree)
        {
            return new[] { tree }
             .Concat(tree.Children.SelectMany(child => {
                 if (child is CompositeDiscountPolicyModel)
                     return GetAllFromTree((CompositeDiscountPolicyModel)child);
                 else return new List<CompositeDiscountPolicyModel>();
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Subscribed")]
        [Route("AddConditionalCompositeProductsDiscount")]
        public IActionResult AddConditionalCompositeProductsDiscount()
        {
            var session = new Guid(HttpContext.Session.Id);
            var selectlist = new List<SelectListItem>();
            var products = new List<ProductModel>();
            var tree = ProductsConditionTree;
            var error = false;
            bool addedCompose = false;
            int compose_count = 0;
            CompositeDiscountPolicyModel current = tree.productTreeModel;
            IEnumerable<CompositeDiscountPolicyModel> compositeDiscounts;
            try
            {
                var choice = Request.Form["edit"].ToString();
                Guid.TryParse(choice, out Guid compose);
                if (!String.IsNullOrEmpty(Request.Form["selectd-1"]))
                {
                    int.TryParse(Request.Form["composites"].ToString(), out compose_count);
                    compose_count = compose_count == 0 ? 1 : compose_count;
                }
                var op = Request.Form["operator"].ToString();
                Enum.TryParse(op, out CompositeType operation);
                if (tree.productTreeModel == null)
                    tree.productTreeModel = new CompositeDiscountPolicyModel(Guid.NewGuid(), new List<DiscountPolicyModel>(), operation);
                var ids = new List<Guid>();
                compositeDiscounts = GetAllFromTree(tree.productTreeModel).ToList();
                if (tree.Init)
                    current = tree.productTreeModel;
                if (!tree.Init && compose != Guid.Empty)
                {
                    current = compositeDiscounts.FirstOrDefault(d => d.ID == compose);
                    current.Type = operation;
                }
                else if (!tree.Init) ModelState.AddModelError("SelectListError", "You did not choose a composite to set or no composites left. Choose composites or create the discount");

                for (int i = 0; i < Request.Form.Count; i++)
                {
                    if (Request.Form.ContainsKey("selectd" + i))
                    {
                        ids.Add(new Guid(Request.Form["selectd" + i]));
                    }
                }
                if (ids.Count == 0)
                    error = true;
                else if (ids.Count == 1 && !tree.Init)
                    ModelState.AddModelError("SelectListError", "More than one product needed for composition");


                var inventory = _service.getStoreInfo(tree.StoreName).Item2;
                foreach (var prodInv in inventory)
                {
                    var prods = _service.getStoreProductGroup(session, prodInv.ID, tree.StoreName).Item2;
                    foreach (var prod in prods)
                    {
                        if (prod.Id != tree.ProductID)
                            products.Add(prod);

                        if (ModelState.IsValid)
                        {
                            if (ids.Any(g => g.Equals(prod.Id)))
                            {
                                current.Children.Add(new ConditionalProductDiscountModel(Guid.NewGuid(), 1, tree.ExpDate, tree.Percentage, prod.Id, prod.Name, prod.GetString()));
                            }
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    if (compose_count > 0)
                        for (int i = 0; i < compose_count; ++i)
                        {
                            current.Children.Add(new CompositeDiscountPolicyModel(Guid.NewGuid(), new List<DiscountPolicyModel>(), operation));
                            addedCompose = true;
                        }
                    tree.Init = false;
                    ProductsConditionTree = tree;
                }
                foreach (var prod in products)
                {
                    selectlist.Add(new SelectListItem
                    {
                        Text = prod.GetString(),
                        Value = prod.Id.ToString()
                    });
                }
                var model = (selectlist, tree, (compositeDiscounts.Where(d => d.Children.Count == 0).Count() != 0) || addedCompose);
                if (error)
                    ModelState.AddModelError("SelectListError", "No Products chosen!");
                return View("../Owner/discounts/AddCompositeProductDiscount", model);
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
            catch(Exception)
            {
                return Redirect("~/Exception/DatabaseException");
            }
        }
    }
    #endregion
}