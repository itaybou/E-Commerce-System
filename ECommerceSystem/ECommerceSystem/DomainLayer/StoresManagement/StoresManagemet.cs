using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class StoresManagement
    {
        private List<Store> _stores;
        private UserManagement.UserManagement _userManagement;

        public StoresManagement(UserManagement.UserManagement userManagement)
        {
            this._userManagement = new UserManagement.UserManagement(); //TODO sync
            this._stores = new List<Store>();
        }

        public bool isNameExist(string name)
        {
            return _stores.Select(store => store.Name).Contains(name);
        }
        
        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy)
        {
            User loggedInUser = _userManagement.getLoggedInUser(); //sync
            if (!loggedInUser.isSubscribed()) // sync
            {
                return false;
            }

            if (!isNameExist(name))
            {
                return false;
            }

            Store newStore = new Store(discountPolicy, purchasePolicy, loggedInUser.name, name); //sync - make user.name property
            _userManagement.addOwnStore(newStore, loggedInUser);
            return true;
        }


    }
}
