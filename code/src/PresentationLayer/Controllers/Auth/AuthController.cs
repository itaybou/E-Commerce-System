using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //ModelState.AddModelError("", "Test Error");
            return View(model);
        }


        [Route("Register")]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            return View(model);
        }
    }
}