using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement.security;
using ECommerceSystem.ServiceLayer;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    sealed class UserManagement : IDataManager<User>
    {
        private Dictionary<User, UserShoppingCart> _users;

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
            var user = _users.Keys.ToList().Find(u => ((Subscribed) u._state)._uname.Equals(uname) && ((Subscribed)u._state)._pswd.Equals(encrypted_pswd));
            if (user != null)
            {
                _activeUser = user;
                _activeUser._cart = getUserCart(user);
            }
            return user != null;
        }

        public bool logout()
        {
            if (!_activeUser._state.isSubscribed())
                return false;
            _activeUser._state = new Guest();
            _activeUser._cart = new UserShoppingCart();;
            return true;
        }

        public UserShoppingCart getUserCart(User user)
        {
            return _users.ContainsKey(user) ? _users[user] : new UserShoppingCart();
        }


        public bool addProductToCart(Product p, Store s, int quantity)
        {
            if (quantity <= 0)
                return false;
            UserShoppingCart userCart = getUserCart(_activeUser);
            var storeCart = userCart._storeCarts.Find(cart => cart.store.Name.Equals(s.Name));
            if (p.Quantity >= quantity)
            {
                if (storeCart == null)
                {
                    Dictionary<Product, int> products = new Dictionary<Product, int>();
                    storeCart = new StoreShoppingCart(s, products);
                }

                storeCart.AddToCart(p, quantity);
                return true;
            }

            return false;
        }

        public UserShoppingCart ShoppingCartDetails() => _activeUser._cart;

        public bool changeProductQuantity(Product p, int quantity)
        {
            if (quantity < 0 || ShoppingCartDetails().All(prod => prod != p))
                return false;
            if (p.Quantity >= quantity)
            {
                var storeCart = ShoppingCartDetails()._storeCarts.Find(cart => cart.Products.ContainsKey(p));
                if (quantity.Equals(0))
                    storeCart.RemoveFromCart(p);
                else storeCart.ChangeProductQuantity(p, quantity);
                return true;
            }

            return false;
        }

        public bool removeProdcutFromCart(Product p)
        {
            if (ShoppingCartDetails().All(prod => prod != p))
                return false;
            ShoppingCartDetails()._storeCarts.Find(cart => cart.Products.ContainsKey(p)).RemoveFromCart(p);
            return true;
        }

        public List<User> getAll()
        {
            return _users.Keys.ToList();
        }

        public void insert(User user)
        {
            _users.Add(user, null);
        }

        public bool remove(User user)
        {
            return _users.Remove(user);
        }


    }
}
