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
            if (!User.Identity.IsAuthenticated)
            {
                if (HttpContext.Session.GetString("alive") == null)
                {
                    HttpContext.Session.SetString("alive", "alive");
                    var session = new Guid(HttpContext.Session.Id);
                    _service.GuestStatistics(session);
                }
            }
            else if (User.IsInRole("Admin") && !IsStatisticsController(context))
            {
                HttpContext.Session.SetString("statistics", "off");
                HttpContext.Session.SetInt32("guests", 0);
                HttpContext.Session.SetInt32("subscribed", 0);
                HttpContext.Session.SetInt32("managers", 0);
                HttpContext.Session.SetInt32("owners", 0);
                HttpContext.Session.SetInt32("admins", 0);
            }
            base.OnActionExecuting(context);
        }

        public bool IsStatisticsController(ActionExecutingContext context)
        {
            return context.RouteData.Values["Action"].Equals("GetStatisticsData") ||
                context.RouteData.Values["Action"].Equals("UserStatistics") ||
                context.RouteData.Values["Action"].Equals("DisplayUserStatistics") ||
                context.RouteData.Values["Action"].Equals("RefreshStatistics") ||
                context.RouteData.Values["Action"].Equals("_UserStatisticsPartial");
        }
    }
}
