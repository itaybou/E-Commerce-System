using System;
using System.Collections.Generic;
using ECommerceSystem.Models;

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

        public void addAssignee(string storeName, Guid assigneeID)
        {
            State.addAssignee(storeName, assigneeID);
        }

        public bool removeAssignee(string storeName, Guid assigneeID)
        {
            return State.removeAssignee(storeName, assigneeID);
        }

        public List<Guid> getAssigneesOfStore(string storeName)
        {
            return State.getAssigneesOfStore(storeName);
        }

        public void removeAllAssigneeOfStore(string storeName)
        {
            State.removeAllAssigneeOfStore(storeName);
        }

        public void addAssignOwnerRequest(AssignOwnerRequestModel request)
        {
            State.addAssignOwnerRequest(request);
        }

        public void removeAssignOwnerRequest(Guid agreementID)
        {
            State.removeAssignOwnerRequest(agreementID);
        }

        public List<AssignOwnerRequestModel> getAllAssignOwnerRequestOfUser()
        {
            return State.getAllAssignOwnerRequestOfUser();
        }
    }

}