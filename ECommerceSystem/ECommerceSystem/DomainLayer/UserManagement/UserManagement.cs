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
        private List<User> _users;
        private Dictionary<User, UserShoppingCart> _carts;

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

        private UserShoppingCart getUserCart(User user)
        {
            return _carts.ContainsKey(user) ? _carts[user] : new UserShoppingCart();
        }

        public Dictionary<Store,List<Product>> ShoppingCartDetails()
        {
            UserShoppingCart cart = getUserCart(_activeUser);
            Dictionary<Store, List<Product>> cartDetails = new Dictionary<Store, List<Product>>();
            foreach(StoreShoppingCart storeCart in cart._storeCarts)
            {
                cartDetails.Add(storeCart.store, storeCart.products);
            }

            return cartDetails;
        }

        public void addProductToCart(Product p, Store s)
        {
            UserShoppingCart cart = getUserCart(_activeUser);
            var exist = cart._storeCarts.Exists(storecart => storecart.store.Name == s.Name );
            if (!exist)
            {
                if(s.checkProductExistence(p))
                {
                    s.IncreaseProductQuantity(p);
                    List<Product> product = new List<Product>();
                    product.Add(p);
                    cart._storeCarts.Add(new StoreShoppingCart(s, product));
                }

            }
            else
            {
                var storeCart = cart._storeCarts.Find(scart => scart.store.Name == s.Name);
                if(s.checkProductExistence(p))
                {
                    storeCart.products.Add(p);
                    s.IncreaseProductQuantity(p);
                }
                    
            }
                
        }

        public void removeProductFromCart(Product p, Store s)
        {
            UserShoppingCart cart = getUserCart(_activeUser);
            var exist = cart._storeCarts.Exists(storecart => storecart.store.Name == s.Name);
            if (exist)
            {
                var storeCart = cart._storeCarts.Find(scart => scart.store.Name == s.Name);
                var product = storeCart.products.Find(pr => pr.Id == p.Id);
                if (product.Quantity == p.Quantity)
                    storeCart.products.Remove(product);
                else if (product.Quantity > p.Quantity)
                {
                    product.Quantity = product.Quantity - p.Quantity;
                    storeCart.store.DecreaseProductQuantity(p);
                }
                else
                    Console.WriteLine("Cant remove Quantity");

            }
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
