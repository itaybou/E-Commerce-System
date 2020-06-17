using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PresentationLayer.Controllers
{
    public class BaseController : Controller
    {
        protected IService _service;

        public BaseController(IService service)
        {
            _service = service;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("alive") == null)
            {
                HttpContext.Session.SetString("alive", "alive");
            }
            base.OnActionExecuting(context);
        }
    }
}
