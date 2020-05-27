using ECommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Controllers.Users
{
    public class UserPurchasesController : Controller
    {
        [Route("Users/PurchaseHistory")]
        public IActionResult Index()
        {
            var purchases = new List<UserPurchaseModel>();
            return View("../Users/PurchaseHistory", purchases);
        }
    }
}