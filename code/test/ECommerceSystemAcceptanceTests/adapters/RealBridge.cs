using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.ServiceLayer;
using ECommerceSystem.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _guestSessionID = Guid.NewGuid();
        }

        public void initSessions()
        {
            _loginSessionID = Guid.Empty;
            _guestSessionID = Guid.NewGuid();
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

        //public List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to) // 2.5
        //{

        //    catName = catName != null ? catName.ToUpper() : catName;
        //    var categories = EnumMethods.GetValues(typeof(Category)).Select(name => name.ToLower()).ToList().Select(c => c.ToUpper());
        //    if (catName != null && !categories.Contains(catName))
        //    {
        //        return new List<string>();
        //    }
        //    Category cat = catName != null ? (Category)Enum.Parse(typeof(Category), catName) : Category.HEALTH;
        //    filters.ForEach(filter =>
        //    {
        //        switch (filter)
        //        {
        //            //case "price":
        //            //    _systemService.applyPriceRangeFilter(from, to);
        //            //    break;
        //            //case "product_rating":
        //            //    _systemService.applyProductRatingFilter(from, to);
        //            //    break;
        //            //case "store_rating":
        //            //    _systemService.applyStoreRatingFilter(from, to);
        //            //    break;
        //            case "category":
        //                _systemService.searchProductsByCategory()
        //                break;
        //            case "keywords":
        //                _systemService.applyCategoryFilter(cat);
        //                break;
        //        }
        //    });

        //    if (prodName != null)
        //    {
        //        return _systemService.searchProductsByName(prodName).Select(p => p.Name).ToList();
        //    }
        //    else if (catName != null)
        //    {
        //        return _systemService.searchProductsByCategory(cat).Select(p => p.Name).ToList();
        //    }
        //    else if (keywords != null)
        //    {
        //        return _systemService.searchProductsByKeyword(keywords).Select(p => p.Name).ToList();
        //    }
        //    else return _systemService.getAllProducts().Select(p => p.Name).ToList();
        //    return null;
        //}

        public SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemService.getAllProducts(category, priceFilter, storeRatingFilter, productRatingFilter);
        }
        public SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemService.searchProductsByCategory(category, priceFilter, storeRatingFilter, productRatingFilter);
        }
        public SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemService.searchProductsByName(prodName, category, priceFilter, storeRatingFilter, productRatingFilter);
        }
        public SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemService.searchProductsByKeyword(keywords, category, priceFilter, storeRatingFilter, productRatingFilter);
        }


        public bool AddTocart( Guid productId, string storeName, int quantity) //2.6
        {
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
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _userServices.RemoveFromCart(sessionID, prodID);
        }

        public bool ChangeProductCartQuantity(Guid prodID, int quantity)    // 2.7.2
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _userServices.ChangeProductQunatity(sessionID ,prodID, quantity);
        }

        public Task<ICollection<ProductModel>> purchaseUserShoppingCart(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address) // 2.8
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _systemService.purchaseUserShoppingCart(sessionID, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);

        }

        public bool logout()    // 3.1
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))//guest user
            {
                sessionID = _guestSessionID;
                _guestSessionID = Guid.Empty;
            }
            else//login user
            {
                sessionID = _loginSessionID;
                _loginSessionID = Guid.Empty;
            }

            _guestSessionID = Guid.NewGuid();
            
            return _userServices.logout(sessionID);
        }

        public List<Guid> UserPurchaseHistory(string uname) // 3.7
        {
            //var history = _userServices.userPurchaseHistory(uname);
            //return history.Select(h => h.ProductsPurchased.Select(p => p.Id)).SelectMany(p => p).ToList();
            return new List<Guid>();
        }

        public bool removeOwner(string ownerToRemoveUserName, string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.removeOwner(sessionID, ownerToRemoveUserName, storeName);
        }

        public Guid addDayOffPolicy(string storeName, List<DayOfWeek> daysOff)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addDayOffPolicy(sessionID, storeName, daysOff);
        }

        public Guid addLocationPolicy( string storeName, List<string> banLocations)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addLocationPolicy(sessionID, storeName, banLocations);
        }

        public Guid addMinPriceStorePolicy( string storeName, double minPrice)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addMinPriceStorePolicy(sessionID, storeName, minPrice);
        }

        public Guid addAndPurchasePolicy( string storeName, Guid ID1, Guid ID2)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addAndPurchasePolicy(sessionID, storeName, ID1,ID2);
        }

        public Guid addOrPurchasePolicy( string storeName, Guid ID1, Guid ID2)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addOrPurchasePolicy(sessionID, storeName, ID1, ID2);
        }

        public Guid addXorPurchasePolicy( string storeName, Guid ID1, Guid ID2)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addXorPurchasePolicy(sessionID, storeName, ID1, ID2);
        }

        public bool removePurchasePolicy( string storeName, Guid policyID)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.removePurchasePolicy(sessionID, storeName, policyID);
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName(string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.getAllPurchasePolicyByStoreName(sessionID, storeName);
        }

        public Guid addVisibleDiscount(string storeName, Guid productID, float percentage, DateTime expDate)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addVisibleDiscount(sessionID, storeName, productID, percentage, expDate);
        }

        public Guid addCondiotionalProcuctDiscount(string storeName, Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addCondiotionalProcuctDiscount(sessionID,  storeName, productID, percentage, expDate, minQuantityForDiscount);
        }

        public Guid addConditionalCompositeProcuctDiscount(string storeName, Guid productID, float percentage, DateTime expDate, CompositeDiscountPolicyModel conditionalTree)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addConditionalCompositeProcuctDiscount(sessionID, storeName, productID, percentage, expDate, conditionalTree);
        }

        public Guid addConditionalStoreDiscount(string storeName, float percentage, DateTime expDate, int minPriceForDiscount)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addConditionalStoreDiscount(sessionID, storeName, percentage, expDate, minPriceForDiscount);
        }

        public Guid addAndDiscountPolicy(string storeName, List<Guid> IDs)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addAndDiscountPolicy(sessionID, storeName, IDs);
        }

        public Guid addOrDiscountPolicy(string storeName, List<Guid> IDs)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addOrDiscountPolicy(sessionID, storeName, IDs);
        }

        public Guid addXorDiscountPolicy(string storeName, List<Guid> IDs)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.addXorDiscountPolicy(sessionID, storeName, IDs);
        }

        public bool removeProductDiscount(string storeName, Guid discountID, Guid productID)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.removeProductDiscount(sessionID, storeName, discountID, productID);
        }

        public bool removeCompositeDiscount(string storeName, Guid discountID)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.removeCompositeDiscount(sessionID, storeName, discountID);
        }

        public bool removeStoreLevelDiscount(string storeName, Guid discountID)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.removeStoreLevelDiscount(sessionID, storeName, discountID);
        }

        public List<DiscountPolicyModel> getAllStoreLevelDiscounts(string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.getAllStoreLevelDiscounts(sessionID, storeName);
        }

        public List<DiscountPolicyModel> getAllDiscountsForCompose(string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.getAllDiscountsForCompose(sessionID, storeName);
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners( string storeName)
        {
            Guid sessionID;
            if (_loginSessionID.Equals(Guid.Empty))
                sessionID = _guestSessionID;

            else
                sessionID = _loginSessionID;

            return _storeService.getStoreOwners(sessionID,  storeName);
        }
    }
}