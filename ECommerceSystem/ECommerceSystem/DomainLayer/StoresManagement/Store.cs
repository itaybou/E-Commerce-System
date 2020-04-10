using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class Store
    {
        private string _name;
        private DiscountPolicy _discountPolicy;
        private PurchasePolicy _purchasePolicy;

        private Dictionary<string, Permissions> _premmisions;

        private Inventory _inventory;


        public string Name { get => _name; set => _name = value; }

        public bool checkProductExistence(Product p)
        {
            foreach (ProductInventory pInv in _inventory.ProductInventory)
            {
                var exist = pInv.Products.Exists(product => product.Id == p.Id && product.Quantity - p.Quantity >= 0);
                if (exist)
                    return true;
            }
            return false;
        }

        public void DecreaseProductQuantity(Product p)
        {
            foreach (ProductInventory pInv in _inventory.ProductInventory)
            {
                var product = pInv.Products.Find(pr => pr.Id == p.Id && pr.Quantity - p.Quantity >0);
                product.Quantity = product.Quantity - p.Quantity;

            }

        }

        public void IncreaseProductQuantity(Product p)
        {
            foreach (ProductInventory pInv in _inventory.ProductInventory)
            {
                var product = pInv.Products.Find(pr => pr.Id == p.Id);
                product.Quantity = product.Quantity + p.Quantity;

            }

        }
    }
}
