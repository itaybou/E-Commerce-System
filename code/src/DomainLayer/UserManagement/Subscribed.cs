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

        private Dictionary<string, Permissions> _permisions;  //store name --> permission
        private Dictionary<string, List<Guid>> _assignees;  //store name --> list of the owners\managers that this user assign
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, Permissions> Permissions { get; set; }

        public Subscribed(string uname, string pswd, string fname, string lname, string email)
        {
            Username = uname;
            Password = pswd;
            Details = new UserDetails(fname, lname, email);
            PurchaseHistory = new List<UserPurchase>();
            Permissions = new Dictionary<string, Permissions>();
            _uname = uname;
            _pswd = pswd;
            _details = new UserDetails(fname, lname, email);
            _purchaseHistory = new List<UserPurchase>();
            _permisions = new Dictionary<string, Permissions>();
            _assignees = new Dictionary<string, List<Guid>>();
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
            if (_permisions.ContainsKey(storeName))
            {
                return _permisions[storeName];
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
            if (!_assignees.ContainsKey(storeName))
            {
                List<Guid> assgneedList = new List<Guid>() { assigneeID };
                _assignees.Add(storeName, assgneedList);
            }
            else
            {
                _assignees[storeName].Add(assigneeID);
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
            if (!_assignees.ContainsKey(storeName) || _assignees[storeName] == null || !_assignees[storeName].Contains(assigneeID))
            {
                return false;
            }
            else
            {
                _assignees[storeName].Remove(assigneeID);
                if(_assignees[storeName].Count == 0)
                {
                    _assignees.Remove(storeName);
                }
                return true;
            }
        }

        public List<Guid> getAssigneesOfStore(string storeName)
        {
            if (!_assignees.ContainsKey(storeName))
            {
                return null; 
            }
            else
            {
                return _assignees[storeName];
            }
        }

        public void removeAllAssigneeOfStore(string storeName)
        {
            _assignees.Remove(storeName);
        }
    }
}