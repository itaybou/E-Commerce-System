﻿using System;
using System.Collections.Generic;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Guest : IUserState
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool isSubscribed()
        {
            return false;
        }

        public void logPurchase(UserPurchase purchase)
        {
            throw new NotSupportedException();
        }

        public bool isSystemAdmin()
        {
            return false;
        }

        public void addPermission(Permissions permissions, string storeName)
        {
            return; // guest dont have permissions
        }

        public void removePermissions(string storeName)
        {
            return;  // guest dont have permissions
        }

        public Permissions getPermission(string storeName)
        {
            return null;  // guest dont have permissions
        }

        public void addAssignee(string storeName, Guid assigneeID)
        {
            return;
        }

        public bool removeAssignee(string storeName, Guid assigneeID)
        {
            return false;
        }

        public List<Guid> getAssigneesOfStore(string storeName)
        {
            return null;
        }

        public void removeAllAssigneeOfStore(string storeName)
        {
            return;
        }

        public void addUserRequest(INotificationRequest request)
        {
            return;
        }

        public void removeUserRequest(Guid agreementID)
        {
            return;
        }

        public IEnumerable<INotificationRequest> GetUserRequests()
        {
            return null;
        }
    }
}