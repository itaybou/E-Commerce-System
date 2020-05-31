using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models.Products
{
    public class ChooseProductModel
    {
        public string Store { get; set; }
        public bool Listing { get; set; }
        public Guid ProductInventoryID { get; set; }
        public IEnumerable<ProductModel> Products { get; set;}
        public string RedirectPath { get; set; }

        public ChooseProductModel()
        {

        }

        public ChooseProductModel(string store, bool listing, Guid invID, IEnumerable<ProductModel> products, string redirect)
        {
            Store = store;
            Listing = listing;
            ProductInventoryID = invID;
            Products = products;
            RedirectPath = redirect;
        }
    }
}
