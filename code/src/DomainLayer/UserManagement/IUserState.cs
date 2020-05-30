using System;
using System.Collections.Generic;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public interface IUserState
    {
        string Username { get; set; }

        string Password { get; set; }

        bool isSubscribed();

        void logPurchase(UserPurchase userPurchase);

        bool isSystemAdmin();

        void addPermission(Permissions permissions, string storeName);

        void removePermissions(string storeName);

        Permissions getPermission(string storeName);

        void addAssignee(string storeName, Guid assigneeID);
        bool removeAssignee(string storeName, Guid assigneeID);
        List<Guid> getAssigneesOfStore(string storeName);
        void removeAllAssigneeOfStore(string storeName);
        void addUserRequest(INotificationRequest request);
        void removeUserRequest(Guid agreementID);
        IEnumerable<INotificationRequest> GetUserRequests();
    }
}