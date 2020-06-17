using ECommerceSystem.Exceptions;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace PresentationLayer.Controllers.Admin
{
    public class AdminController : BaseController
    {
        public AdminController(IService service) : base(service) { }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            try
            {
                var userList = _service.allUsers(new Guid(HttpContext.Session.Id));
                return View("Users", userList);
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

        [Authorize(Roles = "Admin")]
        public IActionResult Stores()
        {
            try
            {
                var storeInfo = _service.getAllStoresInfo().Keys.ToList();
                return View("Stores", storeInfo);
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

        [Authorize(Roles = "Admin")]
        public IActionResult UserPurchaseHistory(string username)
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                var purchases = _service.userPurchaseHistory(session, username);
                return View("../Users/PurchaseHistory", purchases);
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

        [Authorize(Roles = "Admin")]
        public IActionResult StorePurchaseHistory(string store)
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
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
    }
}