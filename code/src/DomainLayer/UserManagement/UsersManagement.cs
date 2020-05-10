using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.CommunicationLayer.notifications;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement.security;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public sealed class UsersManagement
    {
        private IDictionary<User, UserShoppingCart> _users;

        private static readonly Lazy<UsersManagement> lazy = new Lazy<UsersManagement>(() => new UsersManagement());

        public static UsersManagement Instance => lazy.Value;

        public IDictionary<User, UserShoppingCart> UserCarts { get => _users; set => _users = value; }

        private UsersManagement()
        {
            _users = new Dictionary<User, UserShoppingCart>();
            _users.Add(new User(new SystemAdmin("admin", Encryption.encrypt("4dMinnn"), "admin", "admin", "admin@gmail.com")), new UserShoppingCart());
        }

        public User getUserByGUID(Guid userID)
        {
            User user = _users.Keys.ToList().Find(u => u.Guid.Equals(userID));
            if(user == null)
            {
                user = new User(new Guest(), userID);
                UserCarts.Add(user, null);
            }
            return user;
        }

        public string register(string uname, string pswd, string fname, string lname, string email)
        {
            string error = null;
            var exists = getAll().Exists(user => user.isSubscribed() && user.Name().Equals(uname));
            if (!exists && Validation.isValidPassword(pswd, out error) && Validation.IsValidEmail(email, out error))
            {
                var encrypted_pswd = Encryption.encrypt(pswd);
                UserCarts.Add(new User(new Subscribed(uname, encrypted_pswd, fname, lname, email)), new UserShoppingCart());
                return null;
            }
            return exists ? "User already exists" : error;
        }

        public (bool, Guid) login(string uname, string pswd)
        {
            var encrypted_pswd = Encryption.encrypt(pswd);
            var user = _users.Keys.ToList().Find(u => u.Name().Equals(uname) && u._state.Password().Equals(encrypted_pswd));
            if (user != null)
            {
                user._cart = UserCarts[user];
            }
            return user != null ? (user != null, user.Guid) : (user != null, Guid.Empty);
        }

        public bool logout(Guid userID)
        {
            User user = getUserByGUID(userID);
            if (user == null || !user.isSubscribed())
                return false;
            UserCarts[user] = user._cart;
            return true;
        }

        public UserShoppingCart getUserCart(User user)
        {
            return user._cart;
        }

        public void resetUserShoppingCart(Guid userID)
        {
            var user = getUserByGUID(userID);
            user._cart = new UserShoppingCart();
            UserCarts[user] = user._cart;
        }

        public bool addProductToCart(Guid userID, Guid productId, string storeName, int quantity)
        {
            var shop = StoreManagement.Instance.getStoreByName(storeName);
            var product = shop.Inventory.getProductById(productId);
            if (quantity <= 0)
                return false;
            User user = getUserByGUID(userID);
            UserShoppingCart userCart = getUserCart(user);
            var storeCart = userCart.StoreCarts.Find(cart => cart.store.Name.Equals(shop.Name));
            if (product.Quantity >= quantity)
            {
                if (storeCart == null)
                {
                    storeCart = new StoreShoppingCart(shop);
                    userCart.StoreCarts.Add(storeCart);
                }

                storeCart.AddToCart(product, quantity);
                return true;
            }
            return false;
        }

        public ShoppingCartModel ShoppingCartDetails(Guid userID)
        {
            User user = getUserByGUID(userID);
            var cart = user._cart;
            //REMOVE
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 2000;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += (sender, e) => OnTimedEvent(sender, e, userID);

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;
            //REMOVE
            return ModelFactory.CreateShoppingCart(cart);
        }
        private static Timer aTimer;
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e, Guid userID)
        {
            var comm = Communication.Instance;
            comm.SendPrivateNotification(userID, "hello world");
        }

        public UserShoppingCart userShoppingCart(Guid userID) => _users.Keys.ToList().Find(u => u.Guid.Equals(userID))._cart;

        public bool changeProductQuantity(Guid userID, Guid productId, int quantity)
        {
            var productToChange = userShoppingCart(userID).ToList().Find(prod => prod.Id.Equals(productId));
            if (quantity < 0 || productToChange == null)
                return false;
            if (productToChange.Quantity >= quantity)
            {
                var storeCart = userShoppingCart(userID).StoreCarts.Find(cart => cart.Products.ContainsKey(productToChange));
                if (quantity.Equals(0))
                    storeCart.RemoveFromCart(productToChange);
                else storeCart.ChangeProductQuantity(productToChange, quantity);
                return true;
            }

            return false;
        }

        public bool removeProdcutFromCart(Guid userID, Guid productId)
        {
            var productToRemove = userShoppingCart(userID).ToList().Find(prod => prod.Id.Equals(productId));
            if (productToRemove == null)
                return false;
            userShoppingCart(userID).StoreCarts.Find(cart => cart.Products.ContainsKey(productToRemove)).RemoveFromCart(productToRemove);
            return true;
        }

        internal bool isUserAdmin(Guid userID)
        {
            var user = getUserByGUID(userID);
            return user.isSystemAdmin();
        }

        internal IEnumerable<UserModel> allUsers(Guid userID)
        {
            var user = getUserByGUID(userID);
            if (!user.isSystemAdmin())
                return new List<UserModel>();
            else return UserCarts.Keys.Where(u => u.isSubscribed()).Select(u => ModelFactory.CreateUser(u));
        }

        internal IEnumerable<UserModel> searchUsers(string username)
        {
            var users = UserCarts.Keys.Where(user => user.isSubscribed() && user.Name().ToLower().StartsWith(username.ToLower()));
            return users.OrderBy(user => user.Name().Length).Select(user => ModelFactory.CreateUser(user));
        }

        //public UserShoppingCart getActiveUserShoppingCart()
        //{
        //    return getUserCart(getLoggedInUser());
        //}

        public void logUserPurchase(Guid userID, double totalPrice, IDictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var user = getUserByGUID(userID);
            if (user != null && user.isSubscribed())
            {
                var productsPurchased = allProducts.Select(prod => 
                    new Product(prod.Key.Name, prod.Key.Description, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
                    user._state.logPurchase(new UserPurchase(totalPrice, productsPurchased,
                    firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address));
            }
        }

        public List<User> getAll()
        {
            return _users.Keys.ToList();
        }

        public void addPermission(User user, Permissions permissions, string storeName)
        {
            user.addPermission(permissions, storeName);
        }

        public void removePermissions(string storeName, User user)
        {
            user.removePermissions(storeName);
        }

        public User getUserByName(string managerUserName)
        {
            return _users.Keys.ToList().Find(u => u.Name().Equals(managerUserName));
        }

        public bool isSubscribed(string newManageruserName)
        {
            return _users.Keys.ToList().Exists(u => u.Name().Equals(newManageruserName));
        }

        //@pre - logged in user is system admin
        public List<UserPurchaseModel> userPurchaseHistory(Guid userID, string userName)
        {
            User historyUser = getUserByName(userName);
            User user = getUserByGUID(userID);

            if (!user.isSystemAdmin())
            {
                return null;
            }
            if (historyUser == null || !historyUser.isSubscribed())
            {
                return null;
            }

            return historyUser.getHistoryPurchase().Select(h => ModelFactory.CreateUserPurchase(h)).ToList();
        }

        public UserModel userDetails(Guid userID)
        {
            var user = getUserByGUID(userID);
            if (!user.isSubscribed())
                return null;
            return ModelFactory.CreateUser(user);
        }
    }
}