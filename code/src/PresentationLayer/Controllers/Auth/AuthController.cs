using ECommerceSystem.Exceptions;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers.Auth
{
    public class AuthController : BaseController
    {
        public AuthController(IService service) : base(service) { }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Unauthorized")]
        public IActionResult Unauthorized()
        {
            return Redirect("~/Exception/UnauthorizedException");
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
                try
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    var session = new Guid(HttpContext.Session.Id);
                    var (valid, guid) = _service.login(session, model.Username, model.Password);
                    if (valid)
                    {
                        await AuthenticateAsync(guid, model.Username);
                        var awaitingRequests = _service.GetAwaitingRequests(session);
                        HttpContext.Session.SetString("RequestCount", awaitingRequests.Count().ToString());
                        if (awaitingRequests.Count() != 0)
                            HttpContext.Session.SetInt32("RequestLogin", 1);
                        var message = new ActionMessageModel("Logged in successfully.\nWelcome back!", Url.Action("Index", "Home"));
                        return View("_ActionMessage", message);
                    }
                    else
                    {
                        ModelState.AddModelError("InvalidRegistration", "invalid credentials");
                        return View(model);
                    }
                }
                catch (AuthenticationException)
                {
                    return Redirect("~/Exception/AuthException");
                }
                catch (DatabaseException)
                {
                    return Redirect("~/Exception/DatabaseException");
                }
                catch (LogicException)
                {
                    return Redirect("~/Exception/LogicException");
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
            if (ModelState.IsValid)
            {
                try
                {
                    var (valid, error) = _service.register(model.Username, model.Password, model.FirstName, model.LastName, model.Email);
                    if (valid)
                    {
                        var message = new ActionMessageModel("Registration was successfull.", Url.Action("Index", "Home"));
                        return View("_ActionMessage", message);
                    }
                    ModelState.AddModelError("InvalidRegistration", "invalid credentials.");
                    ModelState.AddModelError("ValidationError", error);
                    return View(model);
                }
                catch (AuthenticationException)
                {
                    return Redirect("~/Exception/AuthException");
                }
                catch (DatabaseException)
                {
                    return Redirect("~/Exception/DatabaseException");
                }
                catch (LogicException)
                {
                    return Redirect("~/Exception/LogicException");
                }

            }
            return View(model);
        }

        [Authorize(Roles = "Admin, Subscribed")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    var sessionId = new Guid(HttpContext.Session.Id);
                    _service.logout(sessionId);
                    HttpContext.Session.Clear();
                    foreach (var cookie in Request.Cookies.Keys)
                        Response.Cookies.Delete(cookie);
                    var message = new ActionMessageModel("Logged out successfully.\nSee you later!", Url.Action("Index", "Home"));
                    return View("_ActionMessage", message);
                }
                catch (AuthenticationException)
                {
                    return Redirect("~/Exception/AuthException");
                } 
                catch(DatabaseException)
                {
                    return Redirect("~/Exception/DatabaseException");
                }
                catch (LogicException)
                {
                    return Redirect("~/Exception/LogicException");
                }
            }
            return Redirect("~/");
        }
    }
}