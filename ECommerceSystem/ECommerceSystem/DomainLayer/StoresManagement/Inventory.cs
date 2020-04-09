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
            if(_products.Select(p => p.Name).Contains(productName)) // check if the name already exist
            {
                return false;
            }

            ProductInventory productInventory = ProductInventory.Create(productName, discount, purchaseType, price, quantity);
            return true;
        }
    }
}
