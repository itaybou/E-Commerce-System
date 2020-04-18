using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.ServiceLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;

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

        public long addProduct(string storeName, string productInvName, string discountType, int discountPercentage, string purchaseType, int quantity)
        {
            Discount discount = null;
            PurchaseType purchase = null;
            if (discountType.Equals("visible"))
            {
                discount = new VisibleDiscount(discountPercentage, new DiscountPolicy());
            }
            else
            {
                return -1;
            }
            if (purchaseType.Equals("immediate"))
            {
                purchase = new ImmediatePurchase();
            }
            else
            {
                return -1;
            }

            return _storeService.addProduct(storeName, productInvName, discount, purchase, quantity);

        }

        public long addProductInv(string storeName, string description, string productName, string discountType, int discountPercentage, string purchaseType, double price, int quantity, Category category, List <string> keys)
        {
            Discount discount = null;
            PurchaseType purchase = null;
            if (discountType.Equals("visible"))
            {
                discount = new VisibleDiscount(discountPercentage, new DiscountPolicy());
            }
            else
            {
                return -1;
            }
            if (purchaseType.Equals("immediate"))
            {
                purchase = new ImmediatePurchase();
            }
            else
            {
                return -1;
            }

            return _storeService.addProductInv(storeName, description, productName, discount, purchase, price, quantity, category, keywords);
        }

        public bool assignManager(string newManageruserName, string storeName)
        {
            return _storeService.assignManager(newManageruserName, storeName);
        }

        public bool assignOwner(string newOwneruserName, string storeName)
        {
            return _storeService.assignOwner(newOwneruserName, storeName);
        }

        public bool deleteProduct(string storeName, string productInvName, long productID)
        {
            return _storeService.deleteProduct(storeName, productInvName, productID);
        }

        public bool deleteProductInv(string storeName, string productName)
        {
            return _storeService.deleteProductInv(storeName, productName);
        }

        public bool editPermissions(string storeName, string managerUserName, List<permissionType> permissions)
        {
            return _storeService.editPermissions(storeName, managerUserName, permissions);
        }

        public bool modifyProductDiscountType(string storeName, string productInvName, long productID, string newDiscount, int discountPercentage)
        {
            Discount discount = null;
            if (newDiscount.Equals("visible"))
            {
                discount = new VisibleDiscount(discountPercentage, new DiscountPolicy());
            }
            else
            {
                return false;
            }

            return _storeService.modifyProductDiscountType(storeName, productInvName, productID, discount);
        }

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            return _storeService.modifyProductName(storeName, newProductName, oldProductName);
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            return _storeService.modifyProductPrice(storeName, productInvName, newPrice);
        }

        public bool modifyProductPurchaseType(string storeName, string productInvName, long productID, string purchaseType)
        {
            PurchaseType newPurchase = null;
            if (purchaseType.Equals("immediate"))
            {
                newPurchase = new ImmediatePurchase();
            }
            else
            {
                return false;
            }

            return _storeService.modifyProductPurchaseType(storeName, productInvName, productID, newPurchase);
        }

        public bool modifyProductQuantity(string storeName, string productInvName, long productID, int newQuantity)
        {
            return _storeService.modifyProductQuantity(storeName, productInvName, productID, newQuantity);

        }

        public bool openStore(string name, string discountPolicy, string purchasePolicy)
        {
            DiscountPolicy discount = new DiscountPolicy();
            PurchasePolicy purchase = new PurchasePolicy();

            return _storeService.openStore(name, discount, purchase);
        }

        public List<Tuple<string, List<Tuple<long, int>>, double>> purchaseHistory(string storeName)
        {
            List<StorePurchase> history = _storeService.purchaseHistory(storeName);

            if(history == null)
            {
                return null;
            }
            
            List<Tuple<string, List<Tuple<long, int>>, double>> purchases = new List<Tuple<string, List<Tuple<long, int>>, double>>();
            foreach(StorePurchase s in history)
            {
                List<Tuple<long, int>> products = new List<Tuple<long, int>>();
                foreach(Product p in s.ProductsPurchased)
                {
                    products.Add(Tuple.Create(p.Id, p.Quantity));
                }
                purchases.Add(Tuple.Create(s.User.Name(), products, s.TotalPrice));
            }
            return purchases;
        }

        public bool register(string uname, string pswd, string fname, string lname, string email)
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

        public void cancelSearchFilters()
        {
            var filters = CategoryMethods.GetValues(typeof(Filters)).Select(name => name.ToLower()).ToList().Select(c => c.ToUpper()).ToList();
            filters.ForEach(filter =>
            {
                var f = (Filters)Enum.Parse(typeof(Filters), filter);
                _systemService.cancelFilter(f);
            });
        }

        public Dictionary<string, Dictionary<long, int>> getUserCartDetails()
        {
            var dict = new Dictionary<string, Dictionary<long, int>>();
            var cart = _userServices.ShoppingCartDetails();
            cart.StoreCarts.ForEach(storeCart =>
            {
                var storeDict = new Dictionary<long, int>();
                storeCart.Products.ToList().ForEach(p =>
                {
                    if (storeDict.ContainsKey(p.Key.Id))
                    {
                        storeDict[p.Key.Id] += p.Value;
                    }
                    else storeDict.Add(p.Key.Id, p.Value);
                });
                if (dict.ContainsKey(storeCart.store.Name))
                {
                    dict[storeCart.store.Name] = dict[storeCart.store.Name].Concat(storeDict).ToDictionary(pair => pair.Key, pair => pair.Value);
                }
                else dict.Add(storeCart.store.Name, storeDict);
            });

            return dict;
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

        public bool removeManager(string managerUserName, string storeName)
        {
            return _storeService.removeManager(managerUserName, storeName);
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


        public Dictionary<string, Dictionary<long, int>> AddTocart(long prodID, int quantity) //2.6
        {
            var info = _storeService.getAllStoresInfo();
            var prod = info.ToList().Select(pair => Tuple.Create(pair.Key, pair.Value.Find(p => p.Id.Equals(prodID)))).ToList();
            prod.ForEach(pair => _userServices.addProductToCart(pair.Item2, pair.Item1, quantity));
            return getUserCartDetails();
        }

        public Dictionary<string, Dictionary<long, int>> ViewUserCart() // 2.7
        {
            return getUserCartDetails(); //uses User service function
        }


        public bool RemoveFromCart(long prodID) // 2.7.1
        {
            var info = _storeService.getAllStoresInfo();
            var prod = info.ToList().Select(pair => pair.Value.Find(p => p.Id.Equals(prodID))).First();
            return _userServices.RemoveFromCart(prod);

        }

        public bool ChangeProductCartQuantity(long prodID, int quantity)    // 2.7.2
        {
            var info = _storeService.getAllStoresInfo();
            var prod = info.ToList().Select(pair => pair.Value.Find(p => p.Id.Equals(prodID))).First();
            return _userServices.ChangeProductQunatity(prod, quantity);
        }

        public bool PurchaseProducts(Dictionary<long, int> products, string firstName, string lastName, string id, string creditCardNumber, string creditExpiration, string CVV, string address) // 2.8
        {
            var idNum = Int32.Parse(id);
            var cvvNum = Int32.Parse(CVV);
            List<Product> missingProducts;
            if (products == null)
            {
                missingProducts = _systemService.purchaseUserShoppingCart(firstName, lastName, idNum, creditCardNumber, DateTime.Parse(creditExpiration), cvvNum, address);
                return missingProducts == null ? true : false;
            }
            var storesProducts = _storeService.getAllStoresInfo().ToList().Select(item => Tuple.Create(item.Key, item.Value)).ToList()
                .Select(pair => Tuple.Create(pair.Item1, pair.Item2.FindAll(p => products.ContainsKey(p.Id)))).ToList();

            var purchaseProducts = new List<Tuple<Store, (Product, int)>>();
            foreach (Tuple<Store, List<Product>> s in storesProducts)
            {
                foreach(Product p in s.Item2)
                {
                    purchaseProducts.Add(Tuple.Create(s.Item1, (p, products[p.Id])));
                }
            }
            if(purchaseProducts.Count == 1)
            {
                return _systemService.purchaseProduct(purchaseProducts.First(), firstName, lastName, idNum, creditCardNumber, DateTime.Parse(creditExpiration), cvvNum, address);
            }
            missingProducts = _systemService.purchaseProducts(purchaseProducts, firstName, lastName, idNum, creditCardNumber, DateTime.Parse(creditExpiration), cvvNum, address);
            return missingProducts == null ? true : false;
        }

        public bool logout()    // 3.1
        {
            return _userServices.logout();
        }

        public List<long> PurchaseHistory() // 3.7
        {
            var history = _userServices.loggedUserPurchaseHistory();
            return history.Select(h => h.ProductsPurchased.Select(p => p.Id)).SelectMany(p => p).ToList();
        }
    }
}