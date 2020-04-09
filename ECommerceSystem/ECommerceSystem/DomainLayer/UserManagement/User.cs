using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class User
    {
        private IUserState _state;
        private UserShoppingCart _cart;
        private string name {get; set;}
    }
}
