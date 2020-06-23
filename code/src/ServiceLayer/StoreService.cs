using ECommerceSystem.CommunicationLayer.sessions;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Models.DiscountPolicyModels;

namespace ECommerceSystem.ServiceLayer
{
    public class StoreService
    {
        private StoreManagement _storeManagement;
        private ISessionController _sessions;

        public StoreService()
        {
            _storeManagement = StoreManagement.Instance;
            _sessions = SessionController.Instance;
        }


        [Trace("Info")]
        //Usecase - 2.4
        public Tuple<StoreModel, List<ProductInventoryModel>> getStoreInfo(string storeName)
        {
            return _storeManagement.getStoreProducts(storeName);
        }

        [Trace("Info")]
        public Tuple<StoreModel, List<ProductModel>> getStoreProductGroup(Guid sessionID, Guid productInvID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getStoreProductGroup(productInvID, storeName);
        }

        [Trace("Info")]
        public Dictionary<StoreModel, List<ProductInventoryModel>> getAllStoresInfo()
        {
            return _storeManagement.getAllStoresProducts();
        }

        [Trace("Info")]
        //Usecase - 3.2
        public bool openStore(Guid sessionID, string name)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.openStore(userID, name);
        }

        [Trace("Info")]
        //Usecase - 4.1.1
        public Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl) // -1 if not needed in both
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addProductInv(userID, storeName, description, productInvName, price, quantity, category, keywords, minQuantity, maxQuantity, imageUrl);
        }

        [Trace("Info")]
        //Usecase - 4.1.2
        public bool deleteProductInv(Guid sessionID, string storeName, string productInvName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.deleteProductInventory(userID, storeName, productInvName);
        }

        [Trace("Info")]
        //Usecase - 4.1.3
        public Guid addProduct(Guid sessionID, string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addProduct(userID, storeName, productInvName, quantity, minQuantity, maxQuantity);
        }

        [Trace("Info")]
        public bool deleteProduct(Guid sessionID, string storeName, string productInvName, Guid productID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.deleteProduct(userID, storeName, productInvName, productID);
        }

        [Trace("Info")]
        public bool modifyProductName(Guid sessionID, string storeName, string newProductName, string oldProductName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductName(userID, storeName, newProductName, oldProductName);
        }

        [Trace("Info")]
        public bool modifyProductPrice(Guid sessionID, string storeName, string productInvName, int newPrice)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductPrice(userID, storeName, productInvName, newPrice);
        }

        [Trace("Info")]
        public bool modifyProductQuantity(Guid sessionID, string storeName, string productInvName, Guid productID, int newQuantity)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductQuantity(userID, storeName, productInvName, productID, newQuantity);
        }

        //[Trace("Info")]
        //public bool modifyProductDiscountType(Guid sessionID, string storeName, string productInvName, Guid productID, DiscountType newDiscount) // TODO: fix service types version 1
        //{
        //    var userID = _sessions.ResolveSession(sessionID);
        //    return _storeManagement.modifyProductDiscountType(userID, storeName, productInvName, productID, newDiscount);
        //}

        //[Trace("Info")]
        //public bool (Guid sessionID, string storeName, string productInvName, Guid productID, PurchaseType purchaseType) // TODO: fix service types version 1
        //{
        //    var userID = _sessions.ResolveSession(sessionID);
        //    return _storeManagement.modifyProductPurchaseType(userID, storeName, productInvName, productID, purchaseType);
        //}

        [Trace("Info")]
        //Usecase - 4.3
        public Guid createOwnerAssignAgreement(Guid sessionID, string newOwneruserName, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.createOwnerAssignAgreement(userID, newOwneruserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.3
        public bool approveAssignOwnerRequest(Guid sessionID, Guid agreementID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.approveAssignOwnerRequest(userID, agreementID, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.3
        public bool disApproveAssignOwnerRequest(Guid sessionID, Guid agreementID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.disApproveAssignOwnerRequest(userID, agreementID, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.4
        public bool removeOwner(Guid sessionID, string ownerToRemoveUserName, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removeOwner(userID, ownerToRemoveUserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.5
        public bool assignManager(Guid sessionID, string newManageruserName, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.assignManager(userID, newManageruserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.6
        public bool editPermissions(Guid sessionID, string storeName, string managerUserName, List<PermissionType> permissions)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.editPermissions(userID, storeName, managerUserName, permissions);
        }

        [Trace("Info")]
        //Usecase - 4.7
        public bool removeManager(Guid sessionID, string managerUserName, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removeManager(userID, managerUserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.10
        //Usecase - 6.4.2
        public IEnumerable<StorePurchaseModel> purchaseHistory(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.purchaseHistory(userID, storeName);
        }

        //*********Manage Purchase Policy  --   REQUIREMENT 4.2*********

        //*********ADD*********
        [Trace("Info")]
        public Guid addDayOffPolicy(Guid sessionID, string storeName, List <DayOfWeek> daysOff)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.AddDayOffPolicy(userID, storeName, daysOff);
        }

        [Trace("Info")]
        public Guid addLocationPolicy(Guid sessionID, string storeName, List<string> banLocations)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addLocationPolicy(userID, storeName, banLocations);
        }

        [Trace("Info")]
        public Guid addMinPriceStorePolicy(Guid sessionID, string storeName, double minPrice)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addMinPriceStorePolicy(userID, storeName, minPrice);
        }

        [Trace("Info")]
        public Guid addAndPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addAndPurchasePolicy(userID, storeName, ID1, ID2);
        }

        [Trace("Info")]
        public Guid addOrPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addOrPurchasePolicy(userID, storeName, ID1, ID2);
        }

        [Trace("Info")]
        public Guid addXorPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addXorPurchasePolicy(userID, storeName, ID1, ID2);
        }

        //*********REMOVE*********
        [Trace("Info")]
        public bool removePurchasePolicy(Guid sessionID, string storeName, Guid policyID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removePurchasePolicy(userID, storeName, policyID);
        }

        [Trace("Info")]
        public IDictionary<PermissionType, bool> getUsernamePermissionTypes(string storeName, string username)
        {
            return _storeManagement.getUserPermissionTypes(storeName, username);
        }


        //*********gett all policies for gui*********
        [Trace("Info")]
        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getAllPurchasePolicyByStoreName(userID, storeName);
        } 



        //*********Manage Dicsount Policy  --   REQUIREMENT 4.2*********

        //*********ADD*********

        //if the product already have discount, the new discount override the old
        [Trace("Info")]
        public Guid addVisibleDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addVisibleDiscount(userID, storeName, productID, percentage, expDate);
        }

        [Trace("Info")]
        public Guid addCondiotionalProcuctDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addCondiotionalProcuctDiscount(userID, storeName, productID, percentage, expDate, minQuantityForDiscount);
        }

        [Trace("Info")]
        public Guid addConditionalCompositeProcuctDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate, CompositeDiscountPolicyModel conditionalTree)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addConditionalCompositeProcuctDiscount(userID, storeName, productID, percentage, expDate, conditionalTree);
        }

        [Trace("Info")]
        public Guid addConditionalStoreDiscount(Guid sessionID, string storeName, float percentage, DateTime expDate, int minPriceForDiscount)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addConditionalStoreDiscount(userID, storeName, percentage, expDate, minPriceForDiscount);
        }

        //cant compose store level discount
        //@pre - IDs doesn`t contain store level discount id
        [Trace("Info")]
        public Guid addAndDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addAndDiscountPolicy(userID, storeName, IDs);
        }

        //cant compose store level discount
        //@pre - IDs doesn`t contain store level discount id
        [Trace("Info")]
        public Guid addOrDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addOrDiscountPolicy(userID, storeName, IDs);
        }

        //cant compose store level discount
        //@pre - IDs doesn`t contain store level discount id
        [Trace("Info")]
        public Guid addXorDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addXorDiscountPolicy(userID, storeName, IDs);
        }

        //*********REMOVE*********
        [Trace("Info")]
        public bool removeProductDiscount(Guid sessionID, string storeName, Guid discountID, Guid productID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removeProductDiscount(userID, storeName, discountID, productID);
        }

        [Trace("Info")]
        public bool removeCompositeDiscount(Guid sessionID, string storeName, Guid discountID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removeCompositeDiscount(userID, storeName, discountID);
        }

        [Trace("Info")]
        public bool removeStoreLevelDiscount(Guid sessionID, string storeName, Guid discountID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removeStoreLevelDiscount(userID, storeName, discountID);
        }

        [Trace("Info")]
        public List<DiscountPolicyModel> getAllStoreLevelDiscounts(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getAllStoreLevelDiscounts(userID, storeName);
        }

        //return all store discounts without store level discount
        [Trace("Info")]
        public List<DiscountPolicyModel> getAllDiscountsForCompose(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getAllDiscountsForCompose(userID, storeName);
        }

        [Trace("Info")]
        public IDictionary<string, PermissionModel> getUserPermissions(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getUserPermissions(userID);
        }

        [Trace("Info")]
        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getStoreOwners(userID, storeName);
        }

        [Trace("Info")]
        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreManagers(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getStoreManagers(userID, storeName);
        }

        [Trace("Info")]
        public (ProductInventoryModel, string) getProductInventory(Guid prodID)
        {
            return _storeManagement.getProductInventory(prodID);
        }

        [Trace("Info")]
        public void rateProduct(Guid prodID, int rating)
        {
            _storeManagement.rateProduct(prodID, rating);
        }

        [Trace("Info")]
        public void rateStore(string storeName, int rating)
        {
            _storeManagement.rateStore(storeName, rating);
        }
    }
}