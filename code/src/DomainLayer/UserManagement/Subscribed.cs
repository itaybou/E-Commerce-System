using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Subscribed : IUserState
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserDetails Details { get; set; }
        public List<UserPurchase> PurchaseHistory { get; set; }
        public Dictionary<string, Permissions> Permissions { get; set; }

        public Subscribed(string uname, string pswd, string fname, string lname, string email)
        {
            Username = uname;
            Password = pswd;
            Details = new UserDetails(fname, lname, email);
            PurchaseHistory = new List<UserPurchase>();
            Permissions = new Dictionary<string, Permissions>();
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

        public Permissions getPermission(string storeName)
        {
            if (Permissions.ContainsKey(storeName))
            {
                return Permissions[storeName];
            }
            else return null;
        }

    }
}