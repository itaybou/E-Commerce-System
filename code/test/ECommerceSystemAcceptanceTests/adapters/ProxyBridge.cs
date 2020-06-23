using ECommerceSystem.Models;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystemAcceptanceTests.adapters
{


    internal class ProxyBridge : IBridgeAdapter
    {
        private IBridgeAdapter _real;

        internal IBridgeAdapter RealBridge { get => _real; set => _real = value; }

        public Guid addProduct(string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            if (_real != null)
            {
                return _real.addProduct(storeName, productInvName, quantity, minQuantity, maxQuantity);
            }
            else return Guid.NewGuid();
        }

        public Guid addProductInv(string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl)
        {
            if (_real != null)
            {
                return _real.addProductInv( storeName, description, productInvName, price, quantity, category, keywords, minQuantity, maxQuantity, imageUrl);
            }
            else return Guid.NewGuid();
        }

        public bool assignManager(string newManageruserName, string storeName)
        {
            if (_real != null)
            {
                return _real.assignManager( newManageruserName, storeName);
            }
            else return false;
        }

        public Guid createOwnerAssignAgreement(string newOwneruserName, string storeName) 
        {
            if (_real != null)
            {
                return _real.createOwnerAssignAgreement(newOwneruserName, storeName);
            }
            else return Guid.NewGuid();
        }

        public bool approveAssignOwnerRequest(Guid agreementID, string storeName)
        {
            if (_real != null)
            {
                return _real.approveAssignOwnerRequest(agreementID, storeName);
            }
            else return false;
        }

        public bool disApproveAssignOwnerRequest(Guid agreementID, string storeName)
        {
            if (_real != null)
            {
                return _real.disApproveAssignOwnerRequest(agreementID, storeName);
            }
            else return false;
        }

        public bool deleteProduct(string storeName, string productInvName, Guid productID)
        {
            if (_real != null)
            {
                return _real.deleteProduct( storeName, productInvName, productID);
            }
            else return true;
        }

        public bool deleteProductInv( string storeName, string productInvName)
        {
            if (_real != null)
            {
                return _real.deleteProductInv(storeName, productInvName);
            }
            else return true;
        }

        public bool editPermissions(string storeName, string managerUserName, List<PermissionType> permissions)
        {
            if (_real != null)
            {
                return _real.editPermissions( storeName, managerUserName, permissions);
            }
            else return false;
        }

        

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            if (_real != null)
            {
                return _real.modifyProductName(storeName, newProductName, oldProductName);
            }
            else return true;
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            if (_real != null)
            {
                return _real.modifyProductPrice(storeName, productInvName, newPrice);
            }
            else return true;
        }

        public bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity)
        {
            if (_real != null)
            {
                return _real.modifyProductQuantity(storeName, productInvName, productID, newQuantity);
            }
            else return true;
        }

        public bool openStore( string name)
        {
            if (_real != null)
            {
                return _real.openStore(name);
            }
            else return true;
        }

        public IEnumerable<StorePurchaseModel> storePurchaseHistory(string storeName)
        {
            if (_real != null)
            {
                return _real.storePurchaseHistory(storeName);
            }
            else return null;
        }

        public bool register(string uname, string pswd, string fname, string lname, string email) // 2.2
        {
            if (_real != null)
            {
                return _real.register( uname, pswd, fname, lname, email);
            }
            else return true;
        }

        public bool removeManager( string managerUserName, string storeName)
        {
            if (_real != null)
            {
                return _real.removeManager(managerUserName, storeName);
            }
            else return false;
        }

        public bool login(string uname, string pswd) // 2.3
        {
            if (_real != null)
            {
                return _real.login(uname, pswd);
            }
            else return true;
        }

        public Dictionary<StoreModel, List<ProductInventoryModel>> ViewProdcutStoreInfo() // 2.4
        {
            if (_real != null)
            {
                return _real.ViewProdcutStoreInfo();
            }
            else return new Dictionary<StoreModel, List<ProductInventoryModel>>();
        }

        public SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            if( _real !=null )
            {
                return _real.getAllProducts(category, priceFilter, storeRatingFilter, productRatingFilter);
            }
            return new SearchResultModel(new List <ProductInventoryModel>(), new List<string>());
        }
        public SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            if (_real != null)
            {
                return _real.searchProductsByCategory(category, priceFilter, storeRatingFilter, productRatingFilter);
            }
            return new SearchResultModel(new List<ProductInventoryModel>(), new List<string>());
            
        }
        public SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            if (_real != null)
            {
                return _real.searchProductsByName(prodName, category, priceFilter, storeRatingFilter, productRatingFilter);
            }
            return new SearchResultModel(new List<ProductInventoryModel>(), new List<string>());
            
        }
        public SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _real.searchProductsByKeyword(keywords, category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        public bool AddTocart(Guid prodID, string storeName, int quantity) //2.6
        {
            if (_real != null)
            {
                return _real.AddTocart(prodID, storeName, quantity);
            }
            else return false;
        }

        public ShoppingCartModel ViewUserCart() //2.7
        {
            if (_real != null)
            {
                return _real.ViewUserCart();
            }
            else return new ShoppingCartModel();
        }

        public bool RemoveFromCart(Guid prodID)
        {
            if (_real != null)
            {
                return _real.RemoveFromCart(prodID);
            }
            else return false;
        }

        public bool ChangeProductCartQuantity(Guid prodID, int quantity)
        {
            if (_real != null)
            {
                return _real.ChangeProductCartQuantity(prodID, quantity);
            }
            else return false;
        }

        public bool logout() //3.1
        {
            if (_real != null)
            {
                return _real.logout();
            }
            else return true;
        }

        public Task<ICollection<ProductModel>> purchaseUserShoppingCart(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address) // 2.8
        {
            
            if (_real != null)
            {
                    return _real.purchaseUserShoppingCart(firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
            }
            else return null;
        }

        public List<Guid> UserPurchaseHistory(string uname)
        {
            if (_real != null)
            {
                return _real.UserPurchaseHistory(uname);
            }
            else return new List<Guid>();
        }


        public void initSessions()
        {
            if (_real != null)
            {
                 _real.initSessions();
            }
            
        }

        public bool removeOwner(string ownerToRemoveUserName, string storeName)
        {
            if (_real != null)
            {
                return _real.removeOwner( ownerToRemoveUserName, storeName);
            }
            return false;
        }

        public Guid addDayOffPolicy(string storeName, List<DayOfWeek> daysOff)
        {
            if (_real != null)
            {
                return _real.addDayOffPolicy( storeName, daysOff);
            }
            return Guid.Empty;
        }

        public Guid addLocationPolicy(string storeName, List<string> banLocations)
        {
            if (_real != null)
            {
                return _real.addLocationPolicy( storeName, banLocations);
            }
            return Guid.Empty;
        }

        public Guid addMinPriceStorePolicy(string storeName, double minPrice)
        {
            if (_real != null)
            {
                return _real.addMinPriceStorePolicy( storeName,  minPrice);
            }
            return Guid.Empty;
        }

        public Guid addAndPurchasePolicy(string storeName, Guid ID1, Guid ID2)
        {
            if (_real != null)
            {
                return _real.addAndPurchasePolicy( storeName,  ID1,  ID2);
            }
            return Guid.Empty;
        }

        public Guid addOrPurchasePolicy(string storeName, Guid ID1, Guid ID2)
        {
            if (_real != null)
            {
                return _real.addOrPurchasePolicy( storeName,  ID1,  ID2);
            }
            return Guid.Empty;
        }

        public Guid addXorPurchasePolicy(string storeName, Guid ID1, Guid ID2)
        {
            if (_real != null)
            {
                return _real.addXorPurchasePolicy( storeName,  ID1,  ID2);
            }
            return Guid.Empty;
        }

        public bool removePurchasePolicy(string storeName, Guid policyID)
        {
            if (_real != null)
            {
                return _real.removePurchasePolicy( storeName,  policyID);
            }
            return false;
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName(string storeName)
        {
            if (_real != null)
            {
                return _real.getAllPurchasePolicyByStoreName( storeName);
            }
            return new List<PurchasePolicyModel>();
        }
    }
}