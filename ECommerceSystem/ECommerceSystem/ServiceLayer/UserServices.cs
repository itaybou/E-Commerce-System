﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.ServiceLayer
{
    class UserServices
    {
        private UserManagement _management;

        public UserServices()
        {
            _management = UserManagement.Instance;
        }

        /// <summary>
        /// Check whether the user is already in subscribed state and register user to system
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="pswd">password</param>
        /// <param name="fname">first name</param>
        /// <param name="lname">last name</param>
        /// <param name="email">email address</param>
        /// <returns></returns>
        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            return !_management.getLoggedInUser().isSubscribed() && _management.register(uname, pswd, fname, lname, email).Equals(null);
        }

        /// <summary>
        /// Change current active user to a user matching input details if exists.
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="pswd">password</param>
        /// <returns></returns>
        public bool login(string uname, string pswd)
        {
            return _management.login(uname, pswd);
        }

        /// <summary>
        /// Add selected product times quantity from store to user shopping cart.
        /// </summary>
        /// <param name="p">product to add</param>
        /// <param name="s">store to add product from</param>
        /// <param name="quantity">the quantity to add</param>
        /// <returns>true if addition was successful</returns>
        public bool addProductToCart(Product p, Store s, int quantity)
        {
            return _management.addProductToCart(p, s, quantity);
        }
    }
}
