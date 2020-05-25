using ECommerceSystem.CommunicationLayer.sessions;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.ServiceLayer
{
    public class UserServices
    {
        private UsersManagement _management;
        private ISessionController _sessions;

        public UserServices()
        {
            _management = UsersManagement.Instance;
            _sessions = SessionController.Instance;
        }

        //public bool isUserSubscribed(string username)
        //{
        //    return _management.Users.Keys.ToList().Any(u => u.Name().Equals(username));
        //}

        //public bool isUserLogged(string username)
        //{
        //    return _management.getLoggedInUser().isSubscribed() && _management.getLoggedInUser().Name().Equals(username);
        //}

        //public void removeAllUsers()
        //{
        //    _management.Users = _management.Users.Where(u => u.Key.Name().Equals("admin")).ToDictionary(pair => pair.Key, pair => pair.Value);
        //    _management._activeUser = new User(new Guest());
        //}

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
        public (bool, string) register(string uname, string pswd, string fname, string lname, string email)
        {
            //return !_management.getLoggedInUser().isSubscribed() && _management.register(uname, pswd, fname, lname, email).Equals(null);
            var response = _management.register(uname, pswd, fname, lname, email);
            return (response == null, response);
        }

        [Trace("Info")]
        /// <summary>
        /// Change current active user to a user matching input details if exists.
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="pswd">password</param>
        /// <returns>true if logged in successfully</returns>
        public (bool, Guid) login(Guid sessionID, string uname, string pswd)
        {
            var (logged, userID) = _management.login(uname, pswd);
            if (logged)
            {
                _sessions.LoginSession(sessionID, userID);
            }
            return (logged, userID);
        }

        [Trace("Info")]
        /// <summary>
        /// Change current active user to guest state.
        /// </summary>
        /// <returns>true if successful</returns>
        public bool logout(Guid sessionID)
        {
            var userID = _sessions.LogoutSession(sessionID);
            return _management.logout(userID);
        }

        [Trace("Info")]
        /// <summary>
        /// Add selected product times quantity from store to user shopping cart.
        /// </summary>
        /// <param name="p">product to add</param>
        /// <param name="s">store to add product from</param>
        /// <param name="quantity">the quantity to add</param>
        /// <returns>true if addition was successful</returns>
        public bool addProductToCart(Guid sessionID, Guid productId, string storeName, int quantity)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.addProductToCart(userID, productId, storeName, quantity);
        }

        [Trace("Info")]
        /// <summary>
        /// Retrieves user shopping cart details.
        /// </summary>
        /// <returns>the users shopping cart</returns>
        public ShoppingCartModel ShoppingCartDetails(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.ShoppingCartDetails(userID);
        }

        [Trace("Info")]
        /// <summary>
        /// Remove input product from user cart.
        /// </summary>
        /// <param name="p">product to remove</param>
        /// <returns>true if product was removed</returns>
        public bool RemoveFromCart(Guid sessionID, Guid productId)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.removeProdcutFromCart(userID, productId);
        }

        [Trace("Info")]
        /// <summary>
        /// Changes specific product quantity in user cart.
        /// </summary>
        /// <param name="p">product to change quantity for</param>
        /// <param name="quantity">the new quantity</param>
        /// <returns>true if change was successful</returns>
        public bool ChangeProductQunatity(Guid sessionID, Guid productId, int quantity)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.changeProductQuantity(userID, productId, quantity);
        }

        [Trace("Info")]
        /// <param userName>user to watch his history</param>
        /// <returns>List of the purchase history of userName</returns>
        /// @pre - The logged in user is system admin
        public ICollection<UserPurchaseModel> userPurchaseHistory(Guid sessionID, string userName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.userPurchaseHistory(userID, userName);
        }

        [Trace("Info")]
        /// <param userName>user to watch his history</param>
        /// <returns>List of the purchase history of userName</returns>
        /// @pre - The logged in user is system admin
        public UserModel userDetails(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.userDetails(userID);
        }

        [Trace("Info")]
        public bool isUserAdmin(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.isUserAdmin(userID);
        }

        [Trace("Info")]
        internal IEnumerable<UserModel> allUsers(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.allUsers(userID);
        }

        [Trace("Info")]
        public List<AssignOwnerRequestModel> getAllAssignOwnerRequestOfUser(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _management.getAllAssignOwnerRequestOfUser(userID);
        }

        internal IEnumerable<UserModel> searchUsers(string username)
        {
            return _management.searchUsers(username);
        }
    }
}