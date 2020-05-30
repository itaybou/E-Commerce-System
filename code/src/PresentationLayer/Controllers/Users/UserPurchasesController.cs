using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Controllers.Users
{
    public class UserPurchasesController : Controller
    {
        private IService _service;

        public UserPurchasesController(IService service)
        {
            _service = service;
        }

        [Route("Users/PurchaseHistory")]
        public IActionResult Index()
        {
            var session = new Guid(HttpContext.Session.Id);
            var purchases = _service.userPurchaseHistory(session, User.Identity.Name);
            return View("../Users/PurchaseHistory", purchases);
        }
    }
}