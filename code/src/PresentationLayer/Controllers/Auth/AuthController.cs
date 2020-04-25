using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Auth;

namespace PresentationLayer.Controllers.Auth
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }
    }
}