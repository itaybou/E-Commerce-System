using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystemAcceptanceTests.adapters
{
    internal class RealBridge : IBridgeAdapter
    {
        private UserServices _userServices;
        private StoreService _storeService;
        private SystemServices _systemService;

        private Guid _loginSessionID;
        private Guid _guestSessionID;


        public RealBridge()
        {
            _userServices = new UserServices();
            _storeService = new StoreService();
            _systemService = new SystemServices();
            _loginSessionID = Guid.Empty;
            _guestSessionID = Guid.Empty;
        }

        public void initSessions()
        {
            _loginSessionID = Guid.Empty;
            _guestSessionID = Guid.Empty;
        }


        public Guid addProduct( string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;
         
            else
                sessionID = _loginSessionID;

            return _storeService.addProduct(sessionID, storeName, productInvName, quantity, minQuantity, maxQuantity);
        }

        public Guid addProductInv( string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addProductInv(sessionID, storeName, description, productInvName, price, quantity, category, keywords, minQuantity, maxQuantity, imageUrl);
        }

        public bool assignManager( string newManageruserName, string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.assignManager(sessionID, newManageruserName, storeName);
        }

        public Guid createOwnerAssignAgreement(string newOwneruserName, string storeName)
        {

            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.createOwnerAssignAgreement(sessionID, newOwneruserName, storeName);
        }

        public bool approveAssignOwnerRequest(Guid agreementID, string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.approveAssignOwnerRequest(sessionID, agreementID, storeName);
        }

        public bool disApproveAssignOwnerRequest(Guid agreementID, string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.disApproveAssignOwnerRequest(sessionID, agreementID, storeName);
        }

        public bool deleteProduct(string storeName, string productInvName, Guid productID)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.deleteProduct(sessionID, storeName, productInvName, productID);
        }

        public bool deleteProductInv(string storeName, string productInvName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.deleteProductInv(sessionID, storeName, productInvName);
        }

        public bool editPermissions(string storeName, string managerUserName, List<PermissionType> permissions)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.editPermissions(sessionID, storeName, managerUserName, permissions);
        }

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.modifyProductName(sessionID, storeName, newProductName, oldProductName);
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.modifyProductPrice(sessionID, storeName, productInvName, newPrice);
        }


        public bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.modifyProductQuantity(sessionID, storeName, productInvName, productID, newQuantity);
        }

        public bool openStore(string name)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.openStore(sessionID, name);
        }

        public IEnumerable<StorePurchaseModel> storePurchaseHistory(string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.purchaseHistory(sessionID, storeName);
        }

        //public Dictionary<string, Dictionary<Guid, int>> getUserCartDetails()
        //{
        //    var dict = new Dictionary<string, Dictionary<Guid, int>>();
        //    //var cart = _userServices.ShoppingCartDetails();
        //    //cart.StoreCarts.ForEach(storeCart =>
        //    //{
        //    //    var storeDict = new Dictionary<Guid, int>();
        //    //    storeCart.Products.ToList().ForEach(p =>
        //    //    {
        //    //        if (storeDict.ContainsKey(p.Key.Id))
        //    //        {
        //    //            storeDict[p.Key.Id] += p.Value;
        //    //        }
        //    //        else storeDict.Add(p.Key.Id, p.Value);
        //    //    });
        //    //    if (dict.ContainsKey(storeCart.store.Name))
        //    //    {
        //    //        dict[storeCart.store.Name] = dict[storeCart.store.Name].Concat(storeDict).ToDictionary(pair => pair.Key, pair => pair.Value);
        //    //    }
        //    //    else dict.Add(storeCart.store.Name, storeDict);
        //    //});

        //    return dict;
        //}

        // Requirments
        public bool register(string uname, string pswd, string fname, string lname, string email) // 2.2
        {
            return _userServices.register(uname, pswd, fname, lname, email).Item1;
        }

        public bool removeManager( string managerUserName, string storeName)
        {

            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.removeManager(sessionID, managerUserName, storeName);
        }

        public bool login( string uname, string pswd) // 2.3
        {
            _loginSessionID = Guid.NewGuid();
            Guid sessionID= _loginSessionID; 
            _guestSessionID = Guid.Empty;

            return _userServices.login(sessionID, uname, pswd).Item1;
        }

        public Dictionary<StoreModel, List<ProductInventoryModel>> ViewProdcutStoreInfo() // 2.4
        {
            return _storeService.getAllStoresInfo();
            
        }

        public List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to) // 2.5
        {
            //catName = catName != null? catName.ToUpper() : catName;
            //var categories = EnumMethods.GetValues(typeof(Category)).Select(name => name.ToLower()).ToList().Select(c => c.ToUpper());
            //if (catName != null && !categories.Contains(catName))
            //{
            //    return new List<string>();
            //}
            //Category cat = catName != null? (Category)Enum.Parse(typeof(Category), catName) : Category.HEALTH;
            //filters.ForEach(filter =>
            //{
            //    switch (filter)
            //    {
            //        case "price":
            //            _systemService.applyPriceRangeFilter(from, to);
            //            break;
            //        case "product_rating":
            //            _systemService.applyProductRatingFilter(from, to);
            //            break;
            //        case "store_rating":
            //            _systemService.applyStoreRatingFilter(from, to);
            //            break;
            //        case "category":
            //            _systemService.applyCategoryFilter(cat);
            //            break;
            //    }
            //});

            //if (prodName != null)
            //{
            //    return _systemService.searchProductsByName(prodName).Select(p => p.Name).ToList();
            //}
            //else if (catName != null)
            //{
            //    return _systemService.searchProductsByCategory(cat).Select(p => p.Name).ToList();
            //}
            //else if (keywords != null)
            //{
            //    return _systemService.searchProductsByKeyword(keywords).Select(p => p.Name).ToList();
            //}
            //else return _systemService.getAllProducts().Select(p => p.Name).ToList();
            return null;
        }

        public bool AddTocart( Guid productId, string storeName, int quantity) //2.6
        {
            //var info = _storeService.getAllStoresInfo();
            //var prod = info.ToList().Select(pair => Tuple.Create(pair.Key, pair.Value.Find(p => p.Id.Equals(prodID)))).ToList();
            //prod.ForEach(pair => _userServices.addProductToCart(pair.Item2, pair.Item1, quantity));
            //return getUserCartDetails();
            //return null;
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;


            return _userServices.addProductToCart(sessionID, productId, storeName, quantity);
        }

        public ShoppingCartModel ViewUserCart() // 2.7
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _userServices.ShoppingCartDetails(sessionID); //uses User service function
        }

        public bool RemoveFromCart(Guid prodID) // 2.7.1
        {
            //var info = _storeService.getAllStoresInfo();
            //var prod = info.ToList().Select(pair => pair.Value.Find(p => p.Id.Equals(prodID))).First();
            //return _userServices.RemoveFromCart(prodId);
            return true;
        }

        public bool ChangeProductCartQuantity(Guid prodID, int quantity)    // 2.7.2
        {
            //var info = _storeService.getAllStoresInfo();
            //var prod = info.ToList().Select(pair => pair.Value.Find(p => p.Id.Equals(prodID))).First();
            //return _userServices.ChangeProductQunatity(prod, quantity);
            return true;
        }

        public bool PurchaseProducts(Dictionary<Guid, int> products, string firstName, string lastName, string id, string creditCardNumber, string creditExpiration, string CVV, string address) // 2.8
        {
            //var idNum = Int32.Parse(id);
            //var cvvNum = Int32.Parse(CVV);
            //List<Product> missingProducts;
            //if (products == null)
            //{
            //    missingProducts = _systemService.purchaseUserShoppingCart(firstName, lastName, idNum, creditCardNumber, DateTime.Parse(creditExpiration), cvvNum, address);
            //    return missingProducts == null ? true : false;
            //}
            //var storesProducts = _storeService.getAllStoresInfo().ToList().Select(item => Tuple.Create(item.Key, item.Value)).ToList()
            //    .Select(pair => Tuple.Create(pair.Item1, pair.Item2.FindAll(p => products.ContainsKey(p.Id)))).ToList();

            //var purchaseProducts = new List<Tuple<Store, (Product, int)>>();
            //foreach (Tuple<Store, List<Product>> s in storesProducts)
            //{
            //    foreach(Product p in s.Item2)
            //    {
            //        purchaseProducts.Add(Tuple.Create(s.Item1, (p, products[p.Id])));
            //    }
            //}
            //if(purchaseProducts.Count == 1)
            //{
            //    return _systemService.purchaseProduct(purchaseProducts.First(), firstName, lastName, idNum, creditCardNumber, DateTime.Parse(creditExpiration), cvvNum, address);
            //}
            //missingProducts = _systemService.purchaseProducts(purchaseProducts, firstName, lastName, idNum, creditCardNumber, DateTime.Parse(creditExpiration), cvvNum, address);
            //return missingProducts == null ? true : false;
            return true;
        }

        public bool logout()    // 3.1
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))//guest uset
            {
                sessionID = _guestSessionID;
                _guestSessionID = Guid.Empty;
            }
            else//login user
            {
                sessionID = _loginSessionID;
                _loginSessionID = Guid.Empty;
            }
                
            
            return _userServices.logout(sessionID);
        }

        public List<Guid> UserPurchaseHistory(string uname) // 3.7
        {
            //var history = _userServices.userPurchaseHistory(uname);
            //return history.Select(h => h.ProductsPurchased.Select(p => p.Id)).SelectMany(p => p).ToList();
            return new List<Guid>();
        }

        
    }
}