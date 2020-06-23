using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Products;
using PresentationLayer.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers.Users
{
    public class CartController : BaseController
    {
        public CartController(IService service) : base(service) { }

        public IActionResult Index(bool error = false, bool empty = false, bool quantityError = false)
        {
            if (quantityError)
                ModelState.AddModelError("CartEmptyError", "Some of the items in your cart are currently out of stock, please remove them before continuing to checkout.");
            if (empty)
                ModelState.AddModelError("CartEmptyError", "Your shopping cart is empty. add products to cart in order to checkout.");
            if (error)
                ModelState.AddModelError("CartError", "Unavailable product quantity.");
            var sessionID = new Guid(HttpContext.Session.Id);
            try
            {
                var cart = _service.ShoppingCartDetails(sessionID);
                return View(new CartModel(cart));
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

        [Route("Checkout")]
        public IActionResult Checkout(bool error = false, bool formError = false, ICollection<ProductModel> unavailable = null)
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var cart = _service.ShoppingCartDetails(session);
                foreach(var store in cart.Cart)
                    foreach(var product in store.Value)
                        if(product.Item1.Quantity <= 0)
                            return RedirectToAction("Index", "Cart", new { quantityError = true });
                var model = new CheckoutModel(cart);
                if(error && !formError)
                {
                    if(unavailable.Count == 0)
                        ModelState.AddModelError("PaymentError", "A Problem occured while trying to process your order payment. Please try again later or check that your payment information and credit details are valid.");
                    else foreach (var product in unavailable)
                        ModelState.AddModelError("Product" + product.Id + "PaymentError", product.Name + " Currently not avaiable. Check later for renewed stock.");
                } else if(error && formError)
                    ModelState.AddModelError("PaymentError", "Invalid payment information fields provided. Please refill purchase form.");
                if (cart.Cart.Count == 0)
                    return RedirectToAction("Index", "Cart", new { empty = true });
                return View(model);
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

        [HttpPost]
        public IActionResult AddProductToCart()
        {
            try
            {
                var store = Request.Form["store_name"].ToString();
                var listing = Request.Form["listing"].ToString() == "value";
                var invID = new Guid(Request.Form["invID"].ToString());
                if (ModelState.IsValid)
                {
                    var session = new Guid(HttpContext.Session.Id);
                    var choice = Request.Form["product_select"].ToString();
                    try
                    {
                        var quantity = Int32.Parse(Request.Form["quantity"].ToString());
                        if (quantity < 1)
                            throw new ArgumentException();
                        var id = new Guid(choice);
                        var valid = _service.addProductToCart(session, id, store, quantity);
                        if (valid)
                        {
                            TempData["Notification"] = "Added product to cart!";
                            return RedirectToAction("Index");
                        }
                        else return RedirectToAction("ConcreteProducts", "Product", new { prodID = invID, store = store, listing = listing, error = true });
                        
                    } catch (Exception)
                    {
                        return RedirectToAction("ConcreteProducts", "Product", new { prodID = invID, store = store, listing = listing, error = true });
                    }
                }
                return RedirectToAction("ConcreteProducts", "Product", new { prodID = invID, store = store, listing = listing, error = true });
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
        }

        [HttpPost]
        public IActionResult ChangeProductQuantity(string id)
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var prodID = new Guid(id);
                var quantity = Int32.Parse(Request.Form["quantity"].ToString());
                if(!_service.ChangeProductQunatity(session, prodID, quantity))
                {
                    return RedirectToAction("Index", "Cart", new { error = true });
                }
                return RedirectToAction("Index", "Cart", new { error = false });
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cart", new { error = true});
            }
        }

        [HttpPost]
        public IActionResult RemoveFromCart(string id)
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var prodID = new Guid(id);
                if (!_service.RemoveFromCart(session, prodID))
                {
                    return RedirectToAction("Index", "Cart", new { error = true });
                }
                return RedirectToAction("Index", "Cart", new { error = false });
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cart", new { error = true });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Payment()
        {
            try
            {
                var session = new Guid(HttpContext.Session.Id);
                var firstname = Request.Form["FirstName"].ToString();
                var lastname = Request.Form["LastName"].ToString();
                var id = Int32.Parse(Request.Form["ID"].ToString());
                var credit = Request.Form["CreditCardNumber"].ToString();
                var expYear = Int32.Parse(Request.Form["CreditCardExpirationYear"].ToString());
                var expMonth = Int32.Parse(Request.Form["CreditCardExpirationMonth"].ToString());
                var cvv = Int32.Parse(Request.Form["CVV"].ToString());
                var address = Request.Form["Address"].ToString();
                var city = Request.Form["City"].ToString();
                var country = Request.Form["Country"].ToString();
                var postcode = Int32.Parse(Request.Form["PostCode"].ToString());
                ICollection<ProductModel> notAvaiableProducts = null;
                if (ModelState.IsValid)
                {
                    var expirationDate = new DateTime(expYear, expMonth, 1);
                    var expandedAddress = address + ", " + city + ", " + country + ", " + postcode;
                    notAvaiableProducts = await _service.purchaseUserShoppingCart(session, firstname, lastname, id, credit, expirationDate, cvv, expandedAddress);
                    if (notAvaiableProducts == null) // purchase succeeded
                    {
                        var model = new CheckoutModel(firstname, lastname, id, expMonth, expYear, cvv, address, city, country, postcode);
                        return View("_PurchaseSuccessModal", model);
                    }
                }
                return RedirectToAction("Checkout", "Cart", new { error = true, formError = false, unavailable = notAvaiableProducts });
            }
            catch(ExternalSystemException)
            {
                return Redirect("~/Exception/ExternalSystemException");
            }
            catch (DatabaseException)
            {
                return Redirect("~/Exception/DatabaseException");
            }
            catch (LogicException)
            {
                return Redirect("~/Exception/LogicException");
            }
            catch (Exception) 
            {
                return RedirectToAction("Checkout", "Cart", new { error = true, formError = true });
            }
        }
    }
}