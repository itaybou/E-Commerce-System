using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class ExceptionController : Controller
    {
        public IActionResult AuthException()
        {
            foreach (var cookie in Request.Cookies.Keys)
                Response.Cookies.Delete(cookie);
            var message = new ActionMessageModel("Oops! Your session has expired. please login again.", Url.Action("Login", "Auth"));
            return View("_ErrorMessage", message);
        }

        public IActionResult DatabaseException()
        {
            var message = new ActionMessageModel("Oops! Something happend. please try again later.", Url.Action("Index", "Home"));
            return View("_ErrorMessage", message);
        }

        public IActionResult LogicException()
        {
            var message = new ActionMessageModel("Oops! Something happend. please check your input or try again later.", Url.Action("Index", "Home"));
            return View("_ErrorMessage", message);
        }

        public IActionResult ExternalSystemException()
        {
            var message = new ActionMessageModel("Oops! connection lost with external services. please try again.", Url.Action("Index", "Home"));
            return View("_ErrorMessage", message);
        }

        public IActionResult ManagementException()
        {
            var message = new ActionMessageModel("You are not authorized to perform this action", Url.Action("UserStoreList", "Store"));
            return View("_ErrorMessage", message);
        }

        public IActionResult UnauthorizedException()
        {
            var message = new ActionMessageModel("You are not authorized to perform this action", Url.Action("Index", "Home"));
            return View("_ErrorMessage", message);
        }
    }
}