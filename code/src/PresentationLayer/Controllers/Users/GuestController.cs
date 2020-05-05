using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Auth;

namespace PresentationLayer.Controllers.Users
{
    public class GuestController : Controller
    {
        private UserServices _services;

        public IActionResult Index()
        {
            return View();
        }
    }
}