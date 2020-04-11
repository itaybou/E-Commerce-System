using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;


namespace ECommerceSystem.DomainLayer.UserManagement
{
    class User
    {
        public IUserState _state { get; set; }
        public UserShoppingCart _cart { get; set; }

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

        public string Name()
        {
            return _state.Name();
        }

        //Assume _state is subsbcribed
        public void addOwnStore(Store store)
        {
            ((Subscribed)_state).addOwnStore(store); 
        }

        //Assume _state is subsbcribed
        public void addManagerStore(Store store)
        {
            ((Subscribed)_state).addManagerStore(store);
        }

        //Assume _state is subsbcribed
        public void removeManagerStore(Store store)
        {
            ((Subscribed)_state).removeManagerStore(store);
        }

        internal void logPurchase(UserPurchase userPurchase)
        {
            throw new NotImplementedException();
        }
    }
}
