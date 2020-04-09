using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class Product
    {
        private double _price;
        private int _quantity;
        private Discount _discount;
        private PurchaseType _purchaseType;

        public Product(Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            this._price = price;
            this._quantity = quantity;
            this._discount = discount;
            this._purchaseType = purchaseType;
        }
    }
}
