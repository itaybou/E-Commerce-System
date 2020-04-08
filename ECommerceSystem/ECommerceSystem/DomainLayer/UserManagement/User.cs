using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class User
    {
        public IUserState _state { get; set; }
        private UserShoppingCart _cart { get; set; }

        public User()
        {
            _state = new Guest();
            _cart = new UserShoppingCart();
        }

        public User(IUserState state)
        {
            _state = state;
            _cart = new UserShoppingCart();
        }

        public bool isSubscribed()
        {
            return this._state.isSubscribed();
        }
    }
}
