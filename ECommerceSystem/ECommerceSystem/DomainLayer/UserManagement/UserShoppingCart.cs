using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class UserShoppingCart : IEnumerable<Product>
    {
        public List<StoreShoppingCart> _storeCarts { get; set; }

        public UserShoppingCart()
        {
            _storeCarts = new List<StoreShoppingCart>();
        }

        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var storeCart in _storeCarts)
            {
                foreach (var product in storeCart.Products.Keys.ToList())
                {
                    yield return product;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
