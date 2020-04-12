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
    public sealed class UsersManagement : IDataManager<User>
    {
        private Dictionary<User, UserShoppingCart> _users;

        public User _activeUser { get; set; }

        private static readonly Lazy<UsersManagement> lazy = new Lazy<UsersManagement> (() => new UsersManagement());

        public static UsersManagement Instance => lazy.Value;

        private UsersManagement()
        {
            _activeUser = new User();
        }

        public User getLoggedInUser() => _activeUser;

        public string register(string uname, string pswd, string fname, string lname, string email)
        {
            string error = null;
            var exists = getAll().Exists(user => user.isSubscribed() && user.Name().Equals(uname));
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
            var user = _users.Keys.ToList().Find(u => u.Name().Equals(uname) && ((Subscribed)u._state)._pswd.Equals(encrypted_pswd));
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
            var storeCart = userCart.StoreCarts.Find(cart => cart.store.Name.Equals(s.Name));
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
            if(getLoggedInUser().isSubscribed())
            {
                var productsPurchased = allProducts.Select(prod => new Product(prod.Key.Discount, prod.Key.PurchaseType, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
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
            _users.Add(user, null);
        }

        public bool remove(User user)
        {
            return _users.Remove(user);
        }


        public void addOwnStore(Store store, User user)
        {
            user.addOwnStore(store);
        }

        public void addManagerStore(Store store, User assignedUser)
        {
            assignedUser.addManagerStore(store);
        }

        public void removeManagerStore(Store store, User assignedUser)
        {
            assignedUser.removeManagerStore(store);
        }

        internal User getUserByName(string managerUserName)
        {
            return _users.Keys.ToList().Find(u => u.Name().Equals(managerUserName));
        }

        internal bool isSubscribed(string newManageruserName)
        {
            return _users.Keys.ToList().Exists(u => u.Name().Equals(newManageruserName));
        }
    }
}
