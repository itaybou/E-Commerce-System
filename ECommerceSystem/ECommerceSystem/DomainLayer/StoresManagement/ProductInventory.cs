using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class ProductInventory
    {
        private string _name;
        private List<Product> _productInventory;

        public string Name { get => _name; set => _name = value; }

        private ProductInventory(string name)
        {
            this._name = name;
            _productInventory = new List<Product>();
        }

        public static ProductInventory Create(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            ProductInventory productInventory = new ProductInventory(productName);
            Product newProduct = new Product(discount, purchaseType, price, quantity);
            productInventory._productInventory.Add(newProduct);
            return productInventory;
        }
    }
}
