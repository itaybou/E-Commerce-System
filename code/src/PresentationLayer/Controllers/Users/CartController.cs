using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.User;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult AddProductToCart(string prodID)
        {
            var id = new Guid(prodID);
            var session = new Guid(HttpContext.Session.Id);
            var store = _service.getProductInventory(id).Item2;
            var valid = _service.addProductToCart(session, id, store, 1);
            if (valid)
                TempData["Notification"] = "Added product to cart!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Payment(CheckoutModel model)
        {
            //model.Products = Somthing     Get user cart from domain
            var session = new Guid(HttpContext.Session.Id);
            var cart = _service.ShoppingCartDetails(session);
            model.Products = new CartModel(cart).UserCart.Cart.Select(m => m.Value).SelectMany(p => p); // Temp
            if (ModelState.IsValid)
            {
            }
            return View("Checkout", model);
        }
    }
}