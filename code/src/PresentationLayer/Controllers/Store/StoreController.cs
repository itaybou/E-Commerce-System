using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers.Store
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            var stores = new Dictionary<StoreModel, List<ProductModel>>()
            {
                {new StoreModel("Store1", 4, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } },
                {new StoreModel("Store1", 4.6, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 4.3, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 2.1, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 5, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 3.7, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 3.3, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }

            };
            return View("StoreList", stores);
        }

        public IActionResult UserStoreList()
        {
            var stores = new Dictionary<StoreModel, List<ProductModel>>()
            {
                {new StoreModel("Store1", 4, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } },
                {new StoreModel("Store1", 4.6, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 4.3, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 2.1, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 5, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 3.7, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }, {new StoreModel("Store1", 3.3, 85), new List<ProductModel>() {
                     { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                    { new ProductModel(Guid.NewGuid(), "Product1", "this is a long description", 5, 30.5, 25.5) },
                } }

            };
            return View("UserStoreList", stores);
        }
    }
}