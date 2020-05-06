using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PresentationLayer.Models;
using PresentationLayer.Models.Auth;

namespace PresentationLayer.Controllers.Auth
{
    public class AuthController : Controller
    {
        private IService _service;

        public AuthController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        public IActionResult Login()
        {
            var redirect = Url.Action("Index", "Home");
            var message = new ActionMessageModel("Hello World", redirect);
            return View("_ActionMessage", message);
            //return View(new LoginModel());
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
            if(ModelState.IsValid) 
            {
                var valid = _service.register(model.Username, model.Password, model.FirstName, model.LastName, model.Email);
                if(valid)
                {
                    var message = new ActionMessageModel("Registration was successfull.", Url.Action("Index", "Home"));
                    return View("_ActionMessage", message);
                }
                ModelState.AddModelError("InvalidRegistration", "invalid credentials");
                return View(model);
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            return Redirect("~/");
        }
    }
}