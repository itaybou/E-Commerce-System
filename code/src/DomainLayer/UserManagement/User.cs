using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class User
    {
        public IUserState _state { get; set; }
        public UserShoppingCart _cart { get; set; }
        public Guid Guid { get => _guid; set => _guid = value; }

        Guid _guid;

        public User()
        {
            _state = new Guest();
            _cart = new UserShoppingCart();
            _guid = Guid.NewGuid();
        }

        public User(IUserState state)
        {
            _state = state;
            _cart = new UserShoppingCart();
            _guid = Guid.NewGuid();
        }

        public User(IUserState state, Guid guid)
        {
            _state = state;
            _cart = new UserShoppingCart();
            _guid = guid;
        }

        public bool isSubscribed()
        {
            return this._state.isSubscribed();
        }

        public string Name()
        {
            return _state.Name();
        }

        public void addPermission(Permissions permissions, string storeName)
        {
            _state.addPermission(permissions, storeName);
        }

        public void removePermissions(string storeName)
        {
            _state.removePermissions(storeName);
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

        public Permissions getPermission(string storeName)
        {
            return _state.getPermission(storeName);
        }

        public void addNotification(Notification notification)
        {
            _state.addNotification(notification);
        }
    }
}