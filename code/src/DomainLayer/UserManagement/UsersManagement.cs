using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement.security;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public sealed class UsersManagement : IDataManager<User>
    {
        private Dictionary<User, UserShoppingCart> _users;

        public User _activeUser { get; set; }

        private static readonly Lazy<UsersManagement> lazy = new Lazy<UsersManagement>(() => new UsersManagement());

        public static UsersManagement Instance => lazy.Value;

        public Dictionary<User, UserShoppingCart> Users { get => _users; set => _users = value; }

        private UsersManagement()
        {
            _activeUser = new User();
            _users = new Dictionary<User, UserShoppingCart>();
            _users.Add(new User(new SystemAdmin("admin", Encryption.encrypt("4dMinnn"), "admin", "admin", "admin@gmail.com")), new UserShoppingCart());
        }

        public User getLoggedInUser() => _activeUser;

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
            return exists? "User already exists" : error;
        }

        public bool login(string uname, string pswd)
        {
            var encrypted_pswd = Encryption.encrypt(pswd);
            var user = _users.Keys.ToList().Find(u => u.Name().Equals(uname) && u._state.Password().Equals(encrypted_pswd));
            if (user != null)
            {
                _activeUser = user;
                _activeUser._cart = getUserCart(user);
            }
            return user != null;
        }

        public bool logout()
        {
            if (_activeUser != null && !_activeUser._state.isSubscribed())
                return false;
            _activeUser = new User(new Guest());
            _activeUser._cart = new UserShoppingCart(); ;
            return true;
        }

        public UserShoppingCart getUserCart(User user)
        {
            return _users.ContainsKey(user) ? _users[user] : user._cart;
        }

        public void resetActiveUserShoppingCart()
        {
            getLoggedInUser()._cart = new UserShoppingCart();
            _users[getLoggedInUser()] = getLoggedInUser()._cart;
        }

        public bool addProductToCart(Product p, Store s, int quantity)
        {
            if (quantity <= 0)
                return false;
            UserShoppingCart userCart = getUserCart(_activeUser);
            var storeCart = userCart.StoreCarts.Find(cart => cart.store.Name.Equals(s.Name));
            if (p.Quantity >= quantity)
            {
                if (storeCart == null)
                {
                    storeCart = new StoreShoppingCart(s);
                    userCart.StoreCarts.Add(storeCart);
                }

                storeCart.AddToCart(p, quantity);
                return true;
            }

            return false;
        }

        public UserShoppingCart ShoppingCartDetails() => _activeUser._cart;

        public bool changeProductQuantity(Product p, int quantity)
        {
            if (quantity < 0 || ShoppingCartDetails().All(prod => !prod.Id.Equals(p.Id)))
                return false;
            if (p.Quantity >= quantity)
            {
                var storeCart = ShoppingCartDetails().StoreCarts.Find(cart => cart.Products.ContainsKey(p));
                if (quantity.Equals(0))
                    storeCart.RemoveFromCart(p);
                else storeCart.ChangeProductQuantity(p, quantity);
                return true;
            }

            return false;
        }

        public bool removeProdcutFromCart(Product p)
        {
            if (ShoppingCartDetails().All(prod => !prod.Id.Equals(p.Id)))
                return false;
            ShoppingCartDetails().StoreCarts.Find(cart => cart.Products.ContainsKey(p)).RemoveFromCart(p);
            return true;
        }

        public UserShoppingCart getActiveUserShoppingCart()
        {
            return getUserCart(getLoggedInUser());
        }

        public void logUserPurchase(double totalPrice, Dictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            if (getLoggedInUser() != null && getLoggedInUser().isSubscribed())
            {
                var productsPurchased = allProducts.Select(prod => new Product(prod.Key.Discount, prod.Key.PurchaseType, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id, prod.Key.Name, prod.Key.Description)).ToList();
                getLoggedInUser()._state.logPurchase(new UserPurchase(totalPrice, productsPurchased,
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

        public List<UserPurchase> loggedUserPurchaseHistory()
        {
            return getLoggedInUser().isSubscribed() ? getLoggedInUser().getHistoryPurchase() : new List<UserPurchase>();
        }

        //@pre - logged in user is system admin
        public List<UserPurchase> userPurchaseHistory(string userName)
        {
            User historyUser = getUserByName(userName);
            User loggedInUser = getLoggedInUser();

            if (!loggedInUser.isSystemAdmin())
            {
                return null;
            }
            if (historyUser == null || !historyUser.isSubscribed())
            {
                return null;
            }

            return historyUser.getHistoryPurchase();
        }
    }
}