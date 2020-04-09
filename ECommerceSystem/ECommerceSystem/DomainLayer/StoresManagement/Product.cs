using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class Product
    {
        private long _id;
        private int _quantity;
        private Discount _discount;
        private PurchaseType _purchaseType;

        public Product(Discount discount, PurchaseType purchaseType, int quantity, long id)
        {
            this._quantity = quantity;
            this._discount = discount;
            this._purchaseType = purchaseType;
            this._id = id;
        }
    }
}
