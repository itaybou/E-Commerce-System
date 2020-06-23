using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement.security;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.Exceptions;
using ECommerceSystem.DomainLayer.SystemManagement;
using PostSharp.Aspects;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public sealed class UsersManagement
    {
        private IDataAccess _data;
        private ICommunication _communication;

        private static readonly Lazy<UsersManagement> lazy = new Lazy<UsersManagement>(() => new UsersManagement());
        public static UsersManagement Instance => lazy.Value;

        private UsersManagement()
        {
            _data = DataAccess.Instance;
            _communication = Communication.Instance;
        }

        public bool CreateAdmin(string username, string password, string firstname, string lastname, string email)
        {
            try
            {
                _data.Users.Insert(new User(new SystemAdmin(username, Encryption.encrypt(password), firstname, lastname, email)));
            } catch(Exception)
            {
                return false;
            }
            return true;
        }

        public User getUserByGUID(Guid userID, bool authenticate)
        {
            User user = _data.Users.GetByIdOrNull(userID, u => u.Guid);
            if(user == null && authenticate)
                throw new AuthenticationException("Not authenticated!");
            if (user == null)
            {
                user = new User(new Guest(), userID);
                _data.Users.CacheUser(user);
            }
            return user;
        }

        public string register(string uname, string pswd, string fname, string lname, string email)
        {
            string error = "";
            bool exists = true;
            lock (String.Intern(uname))
            {
                exists = _data.Users.QueryAll().Any(user => user.isSubscribed() && user.Name.Equals(uname));
                if (!exists)
                {
                    if (Validation.isValidPassword(pswd, out error) && Validation.IsValidEmail(email, out error))
                    {
                        var encrypted_pswd = Encryption.encrypt(pswd);
                        var user = new User(new Subscribed(uname, encrypted_pswd, fname, lname, email));
                        _data.Users.Insert(user);
                        return null;
                    }
                }
            }
            return exists ? "User already exists" : error;
        }

        public (bool, Guid) login(string uname, string pswd)
        {
            var encrypted_pswd = Encryption.encrypt(pswd);
            var user = _data.Users.GetSubscribedUser(uname, encrypted_pswd);
            return user != null ? (user != null, user.Guid) : (user != null, Guid.Empty);
        }

        public bool logout(Guid userID)
        {
            User user = getUserByGUID(userID, true);
            if (user == null || !user.isSubscribed())
            {
                _data.Users.UncacheUser(user);
                return false;
            }
            return true;
        }

        public bool addProductToCart(Guid userID, Guid productId, string storeName, int quantity)
        {
            var shop = StoreManagement.Instance.getStoreByName(storeName);
            var product = shop.Inventory.getProductById(productId);
            if(product == null)
            {
                return false;
            }

            if (quantity <= 0)
                return false;
            User user = getUserByGUID(userID, false);
            UserShoppingCart userCart = getUserCart(user);
            var storeCart = userCart.StoreCarts.Find(cart => cart.Store.Name.Equals(shop.Name));
            if (product.Quantity >= quantity)
            {
                if (storeCart == null)
                {   
                    storeCart = new StoreShoppingCart(shop);
                    userCart.StoreCarts.Add(storeCart);
                }

                if (!storeCart.AddToCart(product, quantity))
                    return false;
                if(user.isSubscribed())
                    _data.Users.Update(user, userID, u => u.Guid);
                _communication.SendPrivateNotification(userID, "Product successfully added to cart!");
                return true;
            }
            return false;
        }

        public ShoppingCartModel ShoppingCartDetails(Guid userID)
        {
            var user = getUserByGUID(userID, false);
            var cart = getUserCart(user);
            return ModelFactory.CreateShoppingCart(cart);
        }

        public UserShoppingCart getUserCart(User user)
        {
            if (user != null)
                return user.Cart;
            return null;
        }

        public bool changeProductQuantity(Guid userID, Guid productId, int quantity)
        {
            if (quantity.Equals(0))
            {
                this.removeProdcutFromCart(userID, productId);
                return true;
            }
            var user = getUserByGUID(userID, false);
            var cart = getUserCart(user);
            var cartProducts = cart.StoreCarts.Select(s => s.ProductQuantities).SelectMany(s => s).Select(p => p.Value.Item1).ToList();
            var productToChange = cartProducts.Find(prod => prod.Id.Equals(productId));
            if (quantity < 0 || productToChange == null)
                return false;
            if (productToChange.Quantity >= quantity)
            {
                var storeCart = cart.StoreCarts.Find(c => c.ProductQuantities.ContainsKey(productToChange.Id));
                storeCart.ChangeProductQuantity(productToChange, quantity);
                _data.Users.Update(user, userID, u => u.Guid);
                return true;
            }

            return false;
        }

        public bool removeProdcutFromCart(Guid userID, Guid productId)
        {
            var user = getUserByGUID(userID, false);
            var cart = getUserCart(user);
            var cartProducts = cart.StoreCarts.Select(s => s.ProductQuantities).SelectMany(s => s).Select(p => p.Value.Item1).ToList();
            var productToRemove = cartProducts.Find(prod => prod.Id.Equals(productId));
            if (productToRemove == null)
                return false;
            var storeCart = cart.StoreCarts.Find(c => c.ProductQuantities.ContainsKey(productToRemove.Id));
            storeCart.RemoveFromCart(productToRemove);
            if(storeCart.ProductQuantities.Count == 0)
            {
                cart.StoreCarts.Remove(storeCart);
            }

            _data.Users.Update(user, userID, u => u.Guid);
            return true;
        }

        internal bool isUserAdmin(Guid userID)
        {
            var user = getUserByGUID(userID, false);
            return user.isSystemAdmin();
        }

        internal IEnumerable<UserModel> allUsers(Guid userID)
        {
            var user = getUserByGUID(userID, true);
            if (!user.isSystemAdmin())
                return new List<UserModel>();
            else return _data.Users.FetchAll().Select(u => ModelFactory.CreateUser(u));
        }

        public void resetUserShoppingCart(Guid userID)
        {
            var user = getUserByGUID(userID, false);
            user.Cart = new UserShoppingCart(userID);
            _data.Users.Update(user, userID, u => u.Guid);
        }

        internal IEnumerable<UserModel> searchUsers(string username)
        {
            var users = _data.Users.GetSubscribedByUsernameStart(username);
            return users.OrderBy(user => user.Name.Length).Select(user => ModelFactory.CreateUser(user));
        }

        public void logUserPurchase(Guid userID, double totalPrice, IDictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            var user = getUserByGUID(userID, true);
            if (user != null && user.isSubscribed())
            {
                var productsPurchased = allProducts.Select(prod =>
                    new Product(prod.Key.Name, prod.Key.Description, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
                try
                {
                    user.State.logPurchase(new UserPurchase(totalPrice, productsPurchased,
                    firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address));
                    _data.Users.Update(user, userID, u => u.Guid);

                }
                catch(Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new LogicException("Faild : log user purchase");
                }

            }
        }

        public IEnumerable<INotificationRequest> GetAwaitingRequests(Guid userID)
        {
            var user = getUserByGUID(userID, true);
            if(user != null && user.isSubscribed())
            {
                return user.GetUserRequests();
            }
            throw new LogicException("Unsubscribed users does not hold requests.");
        }

        public void addPermission(User user, Permissions permissions, string storeName)
        {
            user.addPermission(permissions, storeName);
        }

        public void removePermissions(string storeName, User user)
        {
            user.removePermissions(storeName);
        }

        public User getUserByName(string username)
        {
            return _data.Users.QueryAll().Where(u => u.Name.Equals(username)).FirstOrDefault();
        }

        public bool isSubscribed(string newManageruserName)
        {
            return _data.Users.QueryAll().Any(u => u.Name.Equals(newManageruserName));
        }

        //@pre - logged in user is system admin
        public List<UserPurchaseModel> userPurchaseHistory(Guid userID, string userName)
        {
            User historyUser = getUserByName(userName);
            User user = getUserByGUID(userID, true);

            if (historyUser == null || !historyUser.isSubscribed())
            {
                return null;
            }

            return historyUser.getHistoryPurchase().Select(p => ModelFactory.CreateUserPurchase(p)).ToList();
        }

        public UserModel userDetails(Guid userID)
        {
            var user = getUserByGUID(userID, true);
            if (!user.isSubscribed())
                return null;
            return ModelFactory.CreateUser(user);
        }

        public void addAssignee(User user, string storeName, Guid assigneeID)
        {
            user.addAssignee(storeName, assigneeID);
        }

        public bool removeAssignee(User user, string storeName, Guid assigneeID)
        {
            return user.removeAssignee(storeName, assigneeID);
        }

        public List<Guid> getAssigneesOfStore(User user, string storeName)
        {
            return user.getAssigneesOfStore(storeName);
        }

        public void removeAllAssigneeOfStore(User user, string storeName)
        {
            user.removeAllAssigneeOfStore(storeName);
        }
    }
}