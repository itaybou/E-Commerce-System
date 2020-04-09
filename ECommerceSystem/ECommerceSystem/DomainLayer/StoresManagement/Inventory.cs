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

        public bool add(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            if(getProductByName(productName) != null) // check if the name already exist
            {
                return false;
            }

            ProductInventory productInventory = ProductInventory.Create(productName, discount, purchaseType, price, quantity);
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

        //Return null if there isn`t product with name
        private ProductInventory getProductByName(string name)
        {
            foreach(ProductInventory p in _products)
            {
                if (p.Name.Equals(name))
                {
                    return p;
                }
            }
            return null;
        }
    }
}
