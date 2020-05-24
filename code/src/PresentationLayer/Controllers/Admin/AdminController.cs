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

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            var userList = _service.allUsers(new Guid(HttpContext.Session.Id));
            return View("Users", userList);
        }
    }
}