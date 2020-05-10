﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PresentationLayer.Models;
using PresentationLayer.Models.Auth;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
            //var redirect = Url.Action("Index", "Home");
            //var message = new ActionMessageModel("Hello World", redirect);
            //return View("_ActionMessage", message);
            return View(new LoginModel());
        }

        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("Username", model.Username);
                var session = new Guid(HttpContext.Session.Id);
                var (valid, guid) = _service.login(session, model.Username, model.Password);
                if(valid)
                {
                    await AuthenticateAsync(guid, model.Username);
                    var message = new ActionMessageModel("Logged in successfully.\nWelcome back!", Url.Action("Index", "Home"));
                    return View("_ActionMessage", message);
                } else
                {
                    ModelState.AddModelError("InvalidRegistration", "invalid credentials");
                    return View(model);
                }
            }
            return View(model);
        }

        private async Task AuthenticateAsync(Guid guid, string username)
        {
            var properties = new AuthenticationProperties()
            {
                IsPersistent = false,
            };
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, guid.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, _service.isUserAdmin(guid) ? "Admin" : "Subscribed"),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal, properties);
        }


        [Route("Register")]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid) 
            {
                var (valid, error) = _service.register(model.Username, model.Password, model.FirstName, model.LastName, model.Email);
                if(valid)
                {
                    var message = new ActionMessageModel("Registration was successfull.", Url.Action("Index", "Home"));
                    return View("_ActionMessage", message);
                }
                ModelState.AddModelError("InvalidRegistration", "invalid credentials.");
                ModelState.AddModelError("ValidationError", error);
                return View(model);
            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                //if (_service.logout(userId)) 
                //{
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var sessionId = new Guid(HttpContext.Session.Id);
                _service.logout(sessionId);
                HttpContext.Session.Clear();
                var message = new ActionMessageModel("Logged out successfully.\nSee you later!", Url.Action("Index", "Home"));
                return View("_ActionMessage", message);
                //}
            }
            return Redirect("~/");
        }
    }
}