﻿using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.DomainLayer.UserManagement;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.ServiceLayer
{
    public class UserServices
    {
        private UsersManagement _management;

        public UserServices()
        {
            _management = UsersManagement.Instance;
        }

        public bool isUserSubscribed(string username)
        {
            return _management.Users.Keys.ToList().Any(u => u.Name().Equals(username));
        }

        public bool isUserLogged(string username)
        {
            return _management.getLoggedInUser().isSubscribed() && _management.getLoggedInUser().Name().Equals(username);
        }

        public void removeAllUsers()
        {
            _management.Users.Clear();
            _management._activeUser = new User(new Guest());
        }

        [Trace("Info")]
        /// <summary>
        /// Check whether the user is already in subscribed state and register user to system.
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="pswd">password</param>
        /// <param name="fname">first name</param>
        /// <param name="lname">last name</param>
        /// <param name="email">email address</param>
        /// <returns>true if registered successfully</returns>
        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            //return !_management.getLoggedInUser().isSubscribed() && _management.register(uname, pswd, fname, lname, email).Equals(null);
            return _management.register(uname, pswd, fname, lname, email) == null;
        }

        [Trace("Info")]
        /// <summary>
        /// Change current active user to a user matching input details if exists.
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="pswd">password</param>
        /// <returns>true if logged in successfully</returns>
        public bool login(string uname, string pswd)
        {
            return _management.login(uname, pswd);
        }

        [Trace("Info")]
        /// <summary>
        /// Change current active user to guest state.
        /// </summary>
        /// <returns>true if successful</returns>
        public bool logout()
        {
            return _management.logout();
        }

        [Trace("Info")]
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

        [Trace("Info")]
        /// <summary>
        /// Retrieves user shopping cart details.
        /// </summary>
        /// <returns>the users shopping cart</returns>
        public UserShoppingCart ShoppingCartDetails()
        {
            return _management.ShoppingCartDetails();
        }

        [Trace("Info")]
        /// <summary>
        /// Remove input product from user cart.
        /// </summary>
        /// <param name="p">product to remove</param>
        /// <returns>true if product was removed</returns>
        public bool RemoveFromCart(Product p)
        {
            return _management.removeProdcutFromCart(p);
        }

        [Trace("Info")]
        /// <summary>
        /// Changes specific product quantity in user cart.
        /// </summary>
        /// <param name="p">product to change quantity for</param>
        /// <param name="quantity">the new quantity</param>
        /// <returns>true if change was successful</returns>
        public bool ChangeProductQunatity(Product p, int quantity)
        {
            return _management.changeProductQuantity(p, quantity);
        }

        [Trace("Info")]
        /// <param userName>user to watch his history</param>
        /// <returns>List of the purchase history of userName</returns>
        /// @pre - The logged in user is system admin
        public List<UserPurchase> userPurchaseHistory(string userName)
        {
            return _management.userPurchaseHistory(userName);
        }
    }
}