using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.User;

namespace PresentationLayer.Controllers.Users
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            var model = new CartModel(); // Get user cart from domain
            return View(model);
        }

        [HttpPost]
        [Route("Checkout")]
        public IActionResult Checkout(CartModel cartModel)
        {
            var model = new CheckoutModel(cartModel);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(CheckoutModel model)
        {
            //model.Products = Somthing     Get user cart from domain
            model.Products = new CartModel().UserCart.Cart.Select(m => m.Value).SelectMany(p => p); // Temp
            if(ModelState.IsValid)
            {

            }
            return View("Checkout", model);
        }
    }
}