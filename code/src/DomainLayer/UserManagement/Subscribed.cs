using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using System;
﻿using ECommerceSystem.DataAccessLayer;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using System.ComponentModel;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Subscribed : IUserState, ISupportInitialize
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserDetails Details { get; set; }
        public List<UserPurchase> PurchaseHistory { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<Guid, INotificationRequest> UserRequests { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, List<Guid>> Assignees { get; set; }  //store name --> list of the owners\managers that this user assign

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, Permissions> Permissions { get; set; }

        public Subscribed(string uname, string pswd, string fname, string lname, string email)
        {
            Username = uname;
            Password = pswd;
            Details = new UserDetails(fname, lname, email);
            PurchaseHistory = new List<UserPurchase>();
            Permissions = new Dictionary<string, Permissions>();
            Assignees = new Dictionary<string, List<Guid>>();
            UserRequests = new Dictionary<Guid, INotificationRequest>();
        }

        public bool isSubscribed()
        {
            return true;
        }

        public void addPermission(Permissions permissions, string storeName)
        {
            Permissions.Add(storeName, permissions);
        }

        public void removePermissions(string storeName)
        {
            Permissions.Remove(storeName);
        }

        public Permissions getPermission(string storeName)
        {
            if (Permissions.ContainsKey(storeName))
            {
                return Permissions[storeName];
            }
            else return null;
        }


        public void logPurchase(UserPurchase purchase)
        {
            PurchaseHistory.Add(purchase);
        }

        public virtual bool isSystemAdmin()
        {
            return false;
        }

        public class UserDetails
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

            public UserDetails(string fname, string lname, string email)
            {
                FirstName = fname;
                LastName = lname;
                Email = email;
            }
        }

        public void addAssignee(string storeName, Guid assigneeID)
        {
            if (!Assignees.ContainsKey(storeName))
            {
                List<Guid> assgneedList = new List<Guid>() { assigneeID };
                Assignees.Add(storeName, assgneedList);
            }
            else
            {
                Assignees[storeName].Add(assigneeID);
            }
        }

        public void BeginInit()
        {
            return;
        }

        public void EndInit()
        {
            foreach(var perm in Permissions)
            {
                if (perm.Value.Store == null)
                    perm.Value.Store = DataAccess.Instance.Stores.GetByIdOrNull(perm.Key, s => s.Name);
            }
        }
        public bool removeAssignee(string storeName, Guid assigneeID)
        {
            if (!Assignees.ContainsKey(storeName) || Assignees[storeName] == null || !Assignees[storeName].Contains(assigneeID))
            {
                return false;
            }
            else
            {
                Assignees[storeName].Remove(assigneeID);
                if(Assignees[storeName].Count == 0)
                {
                    Assignees.Remove(storeName);
                }
                return true;
            }
        }

        public List<Guid> getAssigneesOfStore(string storeName)
        {
            if (!Assignees.ContainsKey(storeName))
            {
                return null; 
            }
            else
            {
                return Assignees[storeName];
            }
        }

        public void removeAllAssigneeOfStore(string storeName)
        {
            Assignees.Remove(storeName);
        }

        public void addUserRequest(INotificationRequest request)
        {
            UserRequests.Add(request.RequestID, request);
        }

        public void removeUserRequest(Guid agreementID)
        {
            UserRequests.Remove(agreementID);
        }

        public IEnumerable<INotificationRequest> GetUserRequests()
        {
            return new List<INotificationRequest>(this.UserRequests.Values); 
        }
    }
}