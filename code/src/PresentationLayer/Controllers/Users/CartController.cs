using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.User;

namespace PresentationLayer.Controllers.Users
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var cart = new CartModel();
            return View(cart);
        }
    }
}