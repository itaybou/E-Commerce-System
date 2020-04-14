using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;


namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class User
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

        public bool isSystemAdmin()
        {
            return _state.isSystemAdmin();
        }

        //Assume _state is subsbcribed
        public List<UserPurchase> getHistoryPurchase()
        {
            return ((Subscribed)_state).PurchaseHistory;
        }
    }
}
