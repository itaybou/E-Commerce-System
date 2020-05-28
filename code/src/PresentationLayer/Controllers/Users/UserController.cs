﻿using ECommerceSystem.Exceptions;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PresentationLayer.Controllers.Users
{
    public class UserController : Controller
    {
        private IService _service;

        public UserController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var model = _service.userDetails(session);
                return View("../Users/Dashboard", model);
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
    }
}