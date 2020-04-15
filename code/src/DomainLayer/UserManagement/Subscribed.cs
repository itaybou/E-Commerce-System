using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Subscribed : IUserState
    {
        public string _uname;
        public string _pswd;
        private List<UserPurchase> _purchaseHistory;

        public UserDetails _details { get; set; }
        public List<UserPurchase> PurchaseHistory { get => _purchaseHistory; }

        private List<Store> _storesOwned;
        private List<Store> _storesManaged;

        public Subscribed(string uname, string pswd, string fname, string lname, string email)
        {
            _uname = uname;
            _pswd = pswd;
            _details = new UserDetails(fname, lname, email);
            _purchaseHistory = new List<UserPurchase>();
        }

        public bool isSubscribed()
        {
            return true;
        }

        public void addOwnStore(Store store)
        {
            _storesOwned.Add(store);
        }

        public void addManagerStore(Store store)
        {
            _storesManaged.Add(store);
        }

        public void removeManagerStore(Store store)
        {
            _storesManaged.Remove(store);
        }

        public string Name()
        {
            return _uname;
        }

        public void logPurchase(UserPurchase purchase)
        {
            _purchaseHistory.Add(purchase);
        }

        public string Password()
        {
            return _pswd;
        }

        public virtual bool isSystemAdmin()
        {
            return false;
        }

        public class UserDetails
        {
            private string _fname { get; set; }
            private string _lname { get; set; }
            private string _email { get; set; }

            public UserDetails(string fname, string lname, string email)
            {
                _fname = fname;
                _lname = lname;
                _email = email;
            }
        }
    }
}