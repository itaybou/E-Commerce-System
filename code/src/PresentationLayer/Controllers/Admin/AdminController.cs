﻿using ECommerceSystem.Exceptions;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PresentationLayer.Controllers.Admin
{
    public class AdminController : Controller
    {
        private IService _service;

        public AdminController(IService service)
        {
            _service = service;
        }

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
    }
}