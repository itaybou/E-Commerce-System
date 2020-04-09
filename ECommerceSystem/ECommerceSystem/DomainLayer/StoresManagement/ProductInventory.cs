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
        private List<Product> _products;

        public string Name { get => _name; set => _name = value; }
        public double Price { get => _price; set => _price = value; }

        private ProductInventory(string name, double price)
        {
            this._name = name;
            this._price = price;
            this._products = new List<Product>();
        }

        public static ProductInventory Create(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity, long productIDCounter)
        {
            ProductInventory productInventory = new ProductInventory(productName, price);
            Product newProduct = new Product(discount, purchaseType, quantity, productIDCounter);
            productInventory._products.Add(newProduct);
            return productInventory;
        }

        private Product getProducByID(long id)
        {
            foreach(Product p in _products)
            {
                if(p.Id == id)
                {
                    return p;
                }
            }
            return null;
        }

        public bool modifyProductQuantity(int productID, int newQuantity)
        {
            if(newQuantity <= 0)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if(product == null)
            {
                return false;
            }
            product.Quantity = newQuantity;
            return true;
        }

        public bool deleteProduct(int productID)
        {
            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            _products.Remove(product);
            return true;
        }

        public bool addProduct(Discount discount, PurchaseType purchaseType, int quantity, long id)
        {
            if(quantity <= 0)
            {
                return false;
            }
            Product p = new Product(discount, purchaseType, quantity, id);
            return true;
        }

        public bool modifyProductDiscountType(int productID, Discount newDiscount)
        {
            if(newDiscount == null)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            product.Discount = newDiscount;
            return true;
        }

        public bool modifyProductPurchaseType(int productID, PurchaseType purchaseType)
        {
            if (purchaseType == null)
            {
                return false;
            }

            Product product = getProducByID(productID);
            if (product == null)
            {
                return false;
            }
            product.PurchaseType = purchaseType;
            return true;
        }
    }
}
