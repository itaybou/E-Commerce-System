using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class User
    {
        public Guid Guid { get; set; }
        public IUserState State { get; set; }
        public UserShoppingCart Cart { get; set; }
        public string Name { get => State.Username; }
        public string Password { get => State.Password; }

        public User()
        {
            State = new Guest();
            Guid = Guid.NewGuid();
            Cart = new UserShoppingCart(Guid);
        }

        public User(IUserState state)
        {
            State = state;
            Guid = Guid.NewGuid();
            Cart = new UserShoppingCart(Guid);
        }

        public User(IUserState state, Guid guid)
        {
            State = state;
            Guid = guid;
            Cart = new UserShoppingCart(Guid);
        }

        public bool isSubscribed()
        {
            return this.State.isSubscribed();
        }

        public void addPermission(Permissions permissions, string storeName)
        {
            State.addPermission(permissions, storeName);
        }

        public void removePermissions(string storeName)
        {
            State.removePermissions(storeName);
        }

        public bool isSystemAdmin()
        {
            return State.isSystemAdmin();
        }

        //Assume _state is subsbcribed
        public List<UserPurchase> getHistoryPurchase()
        {
            return ((Subscribed)State).PurchaseHistory;
        }

        public Permissions getPermission(string storeName)
        {
            return State.getPermission(storeName);
        }
    }
}