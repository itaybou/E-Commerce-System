using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

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
            var session = new Guid(HttpContext.Session.Id);
            var model = _service.userDetails(session);
            return View("../Users/Dashboard", model);
        }
    }
}