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
        public async Task<IActionResult> AuthException()
        {
            foreach (var cookie in Request.Cookies.Keys)
                Response.Cookies.Delete(cookie);
            var message = new ActionMessageModel("Oops! Your session has expired. please login again.", Url.Action("Login", "Auth"));
            return View("_ActionMessage", message);
        }
    }
}