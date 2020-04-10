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

        public long Id { get => _id; set => _id = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        internal Discount Discount { get => _discount; set => _discount = value; }
        internal PurchaseType PurchaseType { get => _purchaseType; set => _purchaseType = value; }
    }
}
