using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PresentationLayer.Controllers.Admin
{
    public class AdminController : BaseController
    {
        public AdminController(IService service) : base(service) { }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                if (!_service.isUserAdmin(session))
                    throw new AuthenticationException("Session expired");
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
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                if (!_service.isUserAdmin(session))
                    throw new AuthenticationException("Session expired");
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
                if (!_service.isUserAdmin(session))
                    throw new AuthenticationException("Session expired");
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
                if (!_service.isUserAdmin(session))
                    throw new AuthenticationException("Session expired");
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

        [Authorize(Roles = "Admin")]
        public IActionResult UserStatistics()
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                if (!_service.isUserAdmin(session))
                    throw new AuthenticationException("Session expired");
                var model = new UserStatisticsModel();
                var stats = _service.GetUserStatistics(session, model.From, model.To);
                HttpContext.Session.SetInt32("guests", (int)stats.Statistics[UserTypes.Guests]);
                HttpContext.Session.SetInt32("subscribed", (int)stats.Statistics[UserTypes.Subscribed]);
                HttpContext.Session.SetInt32("managers", (int)stats.Statistics[UserTypes.StoreManagers]);
                HttpContext.Session.SetInt32("owners", (int)stats.Statistics[UserTypes.StoreOwners]);
                HttpContext.Session.SetInt32("admins", (int)stats.Statistics[UserTypes.Admins]);
                HttpContext.Session.SetString("stats_date_from", model.From.ToString());
                HttpContext.Session.SetString("stats_date_to", model.To.ToString());
                return DisplayUserStatistics(stats.AllStatistics);
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
        [Authorize(Roles = "Admin")]
        public IActionResult UserStatistics(UserStatisticsModel model)
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                if (!_service.isUserAdmin(session))
                    throw new AuthenticationException("Session expired");
                var stats = _service.GetUserStatistics(session, model.From, model.To);
                HttpContext.Session.SetInt32("guests", (int)stats.Statistics[UserTypes.Guests]);
                HttpContext.Session.SetInt32("subscribed", (int)stats.Statistics[UserTypes.Subscribed]);
                HttpContext.Session.SetInt32("managers", (int)stats.Statistics[UserTypes.StoreManagers]);
                HttpContext.Session.SetInt32("owners", (int)stats.Statistics[UserTypes.StoreOwners]);
                HttpContext.Session.SetInt32("admins", (int)stats.Statistics[UserTypes.Admins]);
                HttpContext.Session.SetString("stats_date_from", model.From.ToString());    
                HttpContext.Session.SetString("stats_date_to", model.To.ToString());
                return DisplayUserStatistics(stats.AllStatistics);
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
        public IActionResult DisplayUserStatistics(IEnumerable<UserStatistics> stats)
        {
            try
            {
                var dateFrom = DateTime.Parse(HttpContext.Session.GetString("stats_date_from"));
                var dateTo = DateTime.Parse(HttpContext.Session.GetString("stats_date_to"));
                var currentDate = DateTime.Now.Date;
                if (dateFrom <= currentDate && dateTo >= currentDate)
                    HttpContext.Session.SetString("statistics", "on");
                else HttpContext.Session.SetString("statistics", "off");
                var model = new UserStatisticsModel(dateFrom, dateTo, stats);
                var guests = HttpContext.Session.GetInt32("guests");
                return View("../Admin/UserStatistics", model);
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
        public IActionResult RefreshStatistics()
        {
            return PartialView("../Admin/_UserStatisticsPartial");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetStatisticsData()
        {
            try
            {
                var guests = HttpContext.Session.GetInt32("guests");
                var subscribed = HttpContext.Session.GetInt32("subscribed");
                var managers = HttpContext.Session.GetInt32("managers");
                var owners = HttpContext.Session.GetInt32("owners");
                var admins = HttpContext.Session.GetInt32("admins");

                if (guests == 0 && subscribed == 0 && managers == 0 && owners == 0 && admins == 0)
                    return Json(new { empty = true});
                // Setting.  
                var graphData = new[]
                {
                    new { Type = "Guests", Count = guests},
                    new { Type = "Subscribed", Count = subscribed},
                    new { Type = "Store Managers", Count = managers},
                    new { Type = "Store Owners", Count = owners},
                    new { Type = "Admins", Count = admins},
                };

                // Loading drop down lists.  
                return Json(graphData);
            }
            catch (Exception e)
            {
                // Info  
                Console.Write(e);
            }

            // Return info.  
            return Ok();
        }
    }
}