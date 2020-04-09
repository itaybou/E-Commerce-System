using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class StoreShoppingCart
    {
        private Store _store { get; set; }
        private List<Product> _products { get; set; }

        public Store store { get => _store; set => _store = value; }
        public List<Product> products { get => _products; set => _products = value; }

        public StoreShoppingCart (Store s, List<Product> products)
        {
            _store = s;
            _products = products;
        }

    }
}