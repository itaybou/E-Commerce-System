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
        private IUserState _state;
        private UserShoppingCart _cart;
        private string name;

        public string Name { get => name; set => name = value; }

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
    }
}
