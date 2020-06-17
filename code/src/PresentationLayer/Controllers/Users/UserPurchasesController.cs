using ECommerceSystem.Exceptions;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PresentationLayer.Controllers.Users
{
    public class UserPurchasesController : BaseController
    {
        public UserPurchasesController(IService service) : base(service) { }

        [Route("Users/PurchaseHistory")]
        public IActionResult Index()
        {
            var session = new Guid(HttpContext.Session.Id);
            try
            {
                var purchases = _service.userPurchaseHistory(session, User.Identity.Name);
                return View("../Users/PurchaseHistory", purchases);
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
    }
}