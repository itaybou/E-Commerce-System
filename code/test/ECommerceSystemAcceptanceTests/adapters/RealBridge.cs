using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.ServiceLayer;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class RealBridge : IBridgeAdapter
    {
        private UserServices _userServices;
        private StoreService _storeService;
        private SystemServices _systemService;

        public RealBridge()
        {
            _userServices = new UserServices();
            _storeService = new StoreService();
            _systemService = new SystemServices();
        }

        // Utility methods
        public bool IsUserLogged(string username)
        {
            return _userServices.isUserLogged(username);
        }

        public bool IsUserSubscribed(string username)
        {
            return _userServices.isUserSubscribed(username);
        }

        public void usersCleanUp()
        {
            _userServices.removeAllUsers();
        }

        public void storesCleanUp()
        {
            _storeService.removeAllStores();
        }

        public void openStoreWithProducts(string storeName, string ownerName, List<string> products)
        {
            _storeService.openStore(storeName, new DiscountPolicy(), new PurchasePolicy());
            _storeService.assignOwner(ownerName, storeName);
            var i = 0.2;
            var cat = Category.BABIES;
            var keywords = new List<string>() { { "hello" }, { "world" }, { "pokemon" } };
            products.ForEach(p =>
            {
                _storeService.addProductInv(storeName, p, "desc", new VisibleDiscount(10.0f, new DiscountPolicy()), new ImmediatePurchase(), 10.0 * i, 20, cat, keywords);
                i += 0.2;
                cat = Category.ELECTRONICS;
                keywords = new List<string>() { { "hello" }, { "my" }, { "name" }, { "is" }, { "inigo" }, { "montoya" } };
            });
        }

        // Requirments
        public bool register(string uname, string pswd, string fname, string lname, string email) // 2.2
        {
            return _userServices.register(uname, pswd, fname, lname, email);
        }

        public bool login(string uname, string pswd) // 2.3
        {
            return _userServices.login(uname, pswd);
        }

        public Dictionary<string, List<string>> ViewProdcutStoreInfo() // 2.4
        {
            var info = _storeService.getAllStoresInfo();
            var dict = new Dictionary<string, List<string>>();
            info.ToList().ForEach(pair =>
            {
                dict.Add(pair.Key.Name, pair.Value.Select(prod => prod.Id.ToString()).ToList());
            });
            return dict;
        }

        public List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to) // 2.5
        {
            catName = catName != null? catName.ToUpper() : catName;
            var categories = CategoryMethods.GetValues(typeof(Category)).Select(name => name.ToLower()).ToList().Select(c => c.ToUpper());
            if (catName != null && !categories.Contains(catName))
            {
                return new List<string>();
            }
            Category cat = catName != null? (Category)Enum.Parse(typeof(Category), catName) : Category.HEALTH;
            filters.ForEach(filter =>
            {
                switch (filter)
                {
                    case "price":
                        _systemService.applyPriceRangeFilter(from, to);
                        break;
                    case "product_rating":
                        _systemService.applyProductRatingFilter(from, to);
                        break;
                    case "store_rating":
                        _systemService.applyStoreRatingFilter(from, to);
                        break;
                    case "category":
                        _systemService.applyCategoryFilter(cat);
                        break;
                }
            });

            if (prodName != null)
            {
                return _systemService.searchProductsByName(prodName).Select(p => p.Name).ToList();
            }
            else if (catName != null)
            {
                return _systemService.searchProductsByCategory(cat).Select(p => p.Name).ToList();
            }
            else if (keywords != null)
            {
                return _systemService.searchProductsByKeyword(keywords).Select(p => p.Name).ToList();
            }
            else return _systemService.getAllProducts().Select(p => p.Name).ToList();
        }

        public void cancelSearchFilters()
        {
            var filters = CategoryMethods.GetValues(typeof(Filters)).Select(name => name.ToLower()).ToList().Select(c => c.ToUpper()).ToList();
            filters.ForEach(filter =>
            {
                var f = (Filters)Enum.Parse(typeof(Filters), filter);
                _systemService.cancelFilter(f);
            });
        }

        public Dictionary<long, int> AddTocart(long prodID, int quantity) //2.6
        {
            var info = _storeService.getAllStoresInfo();
            var prod = info.ToList().FindAll(pair => pair.Value.Exists(p => p.Id.Equals(prodID)));
            prod.ForEach(pair =>
            {
                pair.Value.ForEach(p => { _userServices.addProductToCart(p, pair.Key, quantity); });
            });

            var dict = new Dictionary<long,int>();
            prod.ForEach(pair =>
            {
                pair.Value.ForEach(p => { dict.Add(p.Id, p.Quantity); });
            });

            return dict;
        }
    }
}