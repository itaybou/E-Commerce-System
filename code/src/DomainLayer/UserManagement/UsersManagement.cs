using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement.security;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public sealed class UsersManagement : IDataManager<User>
    {
        private Dictionary<User, UserShoppingCart> _users;

        private static readonly Lazy<UsersManagement> lazy = new Lazy<UsersManagement>(() => new UsersManagement());

        public static UsersManagement Instance => lazy.Value;

        public Dictionary<User, UserShoppingCart> Users { get => _users; set => _users = value; }

        private UsersManagement()
        {
            _users = new Dictionary<User, UserShoppingCart>();
            _users.Add(new User(new SystemAdmin("admin", Encryption.encrypt("4dMinnn"), "admin", "admin", "admin@gmail.com")), new UserShoppingCart());
        }


        public string register(string uname, string pswd, string fname, string lname, string email)
        {
            string error = null;
            var exists = getAll().Exists(user => user.isSubscribed() && user.Name().Equals(uname));
            if (!exists && Validation.isValidPassword(pswd, out error) && Validation.IsValidEmail(email, out error))
            {
                var encrypted_pswd = Encryption.encrypt(pswd);
                insert(new User(new Subscribed(uname, encrypted_pswd, fname, lname, email)));
                return null;
            }
            return exists ? "User already exists" : error;
        }

        public bool login(string uname, string pswd)
        {
            var encrypted_pswd = Encryption.encrypt(pswd);
            var user = _users.Keys.ToList().Find(u => u.Name().Equals(uname) && u._state.Password().Equals(encrypted_pswd));
            if (user != null)
            {
                //_activeUser = user;
                //_activeUser._cart = getUserCart(user);
            }
            return user != null;
        }

        public User getUserByGUID(Guid userID)
        {
            return _users.Keys.ToList().Find(u => u.Guid.Equals(userID));
        }

        public bool logout(Guid userID) //TODO
        {
            //if (_activeUser != null && !_activeUser._state.isSubscribed())
            //    return false;
            //_activeUser = new User(new Guest());
            //_activeUser._cart = new UserShoppingCart(); ;
            //return true;
            return true;
        }

        public UserShoppingCart getUserCart(User user)
        {
            return _users.ContainsKey(user) ? _users[user] : user._cart;
        }

        public void resetActiveUserShoppingCart(Guid userID)
        {
            User user = getUserByGUID(userID);
            user._cart = new UserShoppingCart();
            _users[user] = user._cart;
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
            return ModelFactory.CreateShoppingCart(cart);
        }

        public UserShoppingCart userShoppingCart(Guid userID) => getUserByGUID(userID)._cart;

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

        public UserShoppingCart getActiveUserShoppingCart(Guid userID)
        {
            return getUserCart(getUserByGUID(userID));
        }

        public void logUserPurchase(Guid userID, double totalPrice, IDictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            if (getUserByGUID(userID) != null && getUserByGUID(userID).isSubscribed())
            {
                var productsPurchased = allProducts.Select(prod => 
                    new Product(prod.Key.Name, prod.Key.Description, prod.Key.Discount, prod.Key.PurchaseType, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
                getUserByGUID(userID)._state.logPurchase(new UserPurchase(totalPrice, productsPurchased,
                    firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address));
            }
        }

        public List<User> getAll()
        {
            return _users.Keys.ToList();
        }

        public void insert(User user)
        {
            _users.Add(user, new UserShoppingCart());
        }

        public bool remove(User user)
        {
            return _users.Remove(user);
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

        public bool isLoggedIn(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}