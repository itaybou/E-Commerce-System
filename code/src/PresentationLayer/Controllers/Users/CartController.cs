using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.User;

namespace PresentationLayer.Controllers.Users
{
    public class CartController : Controller
    {
        private IService _service;

        public CartController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var sessionID = new Guid(HttpContext.Session.Id);
            var cart = _service.ShoppingCartDetails(sessionID);
            return View(new CartModel(cart));
        }

        [HttpPost]
        [Route("Checkout")]
        public IActionResult Checkout()
        {
            var session = new Guid(HttpContext.Session.Id);
            var cart = _service.ShoppingCartDetails(session);
            var cartModel = new CartModel(cart);
            var model = new CheckoutModel(cartModel);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(CheckoutModel model)
        {
            //model.Products = Somthing     Get user cart from domain
            var session = new Guid(HttpContext.Session.Id);
            var cart = _service.ShoppingCartDetails(session);
            model.Products = new CartModel(cart).UserCart.Cart.Select(m => m.Value).SelectMany(p => p); // Temp
            if(ModelState.IsValid)
            {

            }
            return View("Checkout", model);
        }
    }
}