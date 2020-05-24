using ECommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Controllers.Users
{
    public class UserPurchasesController : Controller
    {
        [Route("Users/PurchaseHistory")]
        public IActionResult Index()
        {
            var purchases = new List<UserPurchaseModel>()
            {
                {new UserPurchaseModel(DateTime.Now, 200.5, new List<ProductModel> {
                     { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                }, "My Name", "LastName", 213213, "324234-423423", DateTime.Now, 433, "My Address, NY, 32") },
                {new UserPurchaseModel(DateTime.Now, 200.5, new List<ProductModel> {
                     { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                }, "My Name", "LastName", 213213, "324234-423423", DateTime.Now, 433, "My Address, NY, 32") },
                {new UserPurchaseModel(DateTime.Now, 200.5, new List<ProductModel> {
                     { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                }, "My Name", "LastName", 213213, "324234-423423", DateTime.Now, 433, "My Address, NY, 32") },
                {new UserPurchaseModel(DateTime.Now, 200.5, new List<ProductModel> {
                     { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "ProductStore1", "this is a long description", 5, 30.5, 25.5) },
                }, "My Name", "LastName", 213213, "324234-423423", DateTime.Now, 433, "My Address, NY, 32") },
            };
            return View("../Users/PurchaseHistory", purchases);
        }
    }
}