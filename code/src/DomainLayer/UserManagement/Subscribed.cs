﻿using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Subscribed : IUserState
    {
        public string _uname;
        public string _pswd;
        private List<UserPurchase> _purchaseHistory;
        public UserDetails _details { get; set; }
        public List<UserPurchase> PurchaseHistory { get => _purchaseHistory; }

        private Dictionary<string, Permissions> _permisions;  //store name --> permission
        private Dictionary<string, List<Guid>> _assignees;  //store name --> list of the owners\managers that this user assign


        public Subscribed(string uname, string pswd, string fname, string lname, string email)
        {
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
            _permisions.Add(storeName, permissions);
        }

        public void removePermissions(string storeName)
        {
            _permisions.Remove(storeName);
        }

        public Permissions getPermission(string storeName)
        {
            if (_permisions.ContainsKey(storeName))
            {
                return _permisions[storeName];
            }
            else return null;
        }


        public string Name()
        {
            return _uname;
        }

        public void logPurchase(UserPurchase purchase)
        {
            _purchaseHistory.Add(purchase);
        }

        public string Password()
        {
            return _pswd;
        }

        public virtual bool isSystemAdmin()
        {
            return false;
        }

        public class UserDetails
        {
            private string _fname;
            private string _lname;
            private string _email;

            public UserDetails(string fname, string lname, string email)
            {
                _fname = fname;
                _lname = lname;
                _email = email;
            }

            public string Fname { get => _fname; set => _fname = value; }
            public string Lname { get => _lname; set => _lname = value; }
            public string Email { get => _email; set => _email = value; }
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