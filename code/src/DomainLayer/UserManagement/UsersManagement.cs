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
            return exists ? "User already exists" : error;
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

        public bool addProductToCart(Guid productId, string storeName, int quantity)
        {
            var shop = StoreManagement.Instance.getStoreByName(storeName);
            var product = shop.Inventory.getProductById(productId);
            if (quantity <= 0)
                return false;
            UserShoppingCart userCart = getUserCart(_activeUser);
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

        public ShoppingCartModel ShoppingCartDetails()
        {
            var cart = _activeUser._cart;
            return ModelFactory.CreateShoppingCart(cart);
        }

        public UserShoppingCart userShoppingCart() => _activeUser._cart;

        public bool changeProductQuantity(Guid productId, int quantity)
        {
            var productToChange = userShoppingCart().ToList().Find(prod => prod.Id.Equals(productId));
            if (quantity < 0 || productToChange == null)
                return false;
            if (productToChange.Quantity >= quantity)
            {
                var storeCart = userShoppingCart().StoreCarts.Find(cart => cart.Products.ContainsKey(productToChange));
                if (quantity.Equals(0))
                    storeCart.RemoveFromCart(productToChange);
                else storeCart.ChangeProductQuantity(productToChange, quantity);
                return true;
            }

            return false;
        }

        public bool removeProdcutFromCart(Guid productId)
        {
            var productToRemove = userShoppingCart().ToList().Find(prod => prod.Id.Equals(productId));
            if (productToRemove == null)
                return false;
            userShoppingCart().StoreCarts.Find(cart => cart.Products.ContainsKey(productToRemove)).RemoveFromCart(productToRemove);
            return true;
        }

        public UserShoppingCart getActiveUserShoppingCart()
        {
            return getUserCart(getLoggedInUser());
        }

        public void logUserPurchase(double totalPrice, IDictionary<Product, int> allProducts,
                string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            if (getLoggedInUser() != null && getLoggedInUser().isSubscribed())
            {
                var productsPurchased = allProducts.Select(prod => 
                    new Product(prod.Key.Name, prod.Key.Description, prod.Key.Discount, prod.Key.PurchaseType, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
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

        //@pre - logged in user is system admin
        public List<UserPurchaseModel> userPurchaseHistory(string userName)
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

            return historyUser.getHistoryPurchase().Select(h => ModelFactory.CreateUserPurchase(h)).ToList();
        }
    }
}