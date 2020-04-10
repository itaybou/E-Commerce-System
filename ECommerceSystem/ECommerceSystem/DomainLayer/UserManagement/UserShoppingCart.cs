using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class UserShoppingCart
    {
        public List<StoreShoppingCart> _storeCarts { get; set; }

        public UserShoppingCart()
        {
            _storeCarts = new List<StoreShoppingCart>();
        }
    }
}
