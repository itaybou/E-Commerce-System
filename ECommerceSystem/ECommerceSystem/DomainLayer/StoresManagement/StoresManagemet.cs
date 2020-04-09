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


        // Return null if the name isn`t exist
        public Store getStoreByName(string name)
        {
            foreach(Store store in _stores)
            {
                if (store.Name.Equals(name))
                {
                    return store;
                }
            }
            return null; 
        }

        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy)
        {
            User loggedInUser = _userManagement.getLoggedInUser(); //sync
            if (!loggedInUser.isSubscribed()) // sync
            {
                return false;
            }

            if (getStoreByName(name) != null) //name already exist
            {
                return false;
            }

            Store newStore = new Store(discountPolicy, purchasePolicy, loggedInUser.name, name); //sync - make user.name property
            _userManagement.addOwnStore(newStore, loggedInUser);
            return true;
        }

        public bool addProduct(string storeName, string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            Store store = getStoreByName(storeName);
            return store.addProduct(productName, discount, purchaseType, price, quantity);
        }
    }
}
