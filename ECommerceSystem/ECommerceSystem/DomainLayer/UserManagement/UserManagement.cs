using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement.security;
using ECommerceSystem.ServiceLayer;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    sealed class UserManagement : IDataManager<User>
    {
        private List<User> _users;
        public User _activeUser { get; set; }

        private static readonly Lazy<UserManagement> lazy = new Lazy<UserManagement> (() => new UserManagement());

        public static UserManagement Instance => lazy.Value;

        private UserManagement()
        {
            _activeUser = new User();
        }

        public User getLoggedInUser() => _activeUser;

        public string register(string uname, string pswd, string fname, string lname, string email)
        {
            string error = null;
            var exists = getAll().Exists(user => user.isSubscribed() && ((Subscribed) user._state)._uname.Equals(uname));
            if (!exists && Validation.isValidPassword(pswd, out error) && Validation.IsValidEmail(email))
            {
                var encrypted_pswd = Encryption.encrypt(pswd);
                insert(new User(new Subscribed(uname, encrypted_pswd, fname, lname, email)));
                return null;
            }
            return error;
        }

        public bool login(string uname, string pswd)
        {
            var encrypted_pswd = Encryption.encrypt(pswd);
            var user = _users.Find(u => ((Subscribed) u._state)._uname.Equals(uname) && ((Subscribed)u._state)._pswd.Equals(encrypted_pswd));
            if (user != null)
                _activeUser = user;
            return user != null;
        }

        public List<User> getAll()
        {
            return _users;
        }

        public void insert(User user)
        {
            _users.Add(user);
        }

        public bool remove(User user)
        {
            return _users.Remove(user);
        }
    }
}
