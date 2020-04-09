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
        private double _price;
        private List<Product> _productInventory;

        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set => _price = value; }

        private ProductInventory(string name, double price)
        {
            this._name = name;
            this._price = price;
            this._productInventory = new List<Product>();
        }

        public static ProductInventory Create(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity, long productIDCounter)
        {
            ProductInventory productInventory = new ProductInventory(productName, price);
            Product newProduct = new Product(discount, purchaseType, quantity, productIDCounter);
            productInventory._productInventory.Add(newProduct);
            return productInventory;
        }
    }
}
