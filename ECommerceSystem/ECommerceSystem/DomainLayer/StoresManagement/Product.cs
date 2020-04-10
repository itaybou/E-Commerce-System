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
        private float _price;
        private int _quantity;
        private Discount _discount;
        private PurchaseType _purchaseType;

        public int Quantity { get => _quantity; set => _quantity = value; }
        public long Id { get => _id; set => _id = value; }
        public float Price { get => _price; set => _price = value; }
    }
}
