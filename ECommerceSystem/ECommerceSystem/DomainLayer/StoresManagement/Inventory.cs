using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class Inventory
    {
        private List<ProductInventory> _products;
        private long _productIDCounter;
        public Inventory()
        {
            _products = new List<ProductInventory>();
            _productIDCounter = 0;
        }
        
        //Return null if there isn`t product with name
        private ProductInventory getProductByName(string name)
        {
            foreach (ProductInventory p in _products)
            {
                if (p.Name.Equals(name))
                {
                    return p;
                }
            }
            return null;
        }

        public bool add(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            if(getProductByName(productName) != null) // check if the name already exist
            {
                return false;
            }

            ProductInventory productInventory = ProductInventory.Create(productName, discount, purchaseType, price, quantity, ++_productIDCounter);
            return true;
        }

        public bool delete(string productName)
        {
            ProductInventory product = getProductByName(productName);
            if (product == null) // check if the name already exist
            {
                return false;
            }
            else
            {
                _products.Remove(product);
                return true;
            }
        }

        public bool modifyProductName(string newProductName, string oldProductName)
        {
            foreach(ProductInventory p in _products)
            {
                if (p.Name.Equals(oldProductName))
                {
                    p.Name = newProductName;
                    return true;
                }
            }
            return false;
        }

        public bool modifyProductPrice(string productName, int newPrice)
        {
            foreach (ProductInventory p in _products)
            {
                if (p.Name.Equals(productName))
                {
                    p.Price = newPrice;
                    return true;
                }
            }
            return false;
        }



    }
}
