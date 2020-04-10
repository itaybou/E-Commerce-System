using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;


namespace ECommerceSystem.ServiceLayer
{
    class StoreService
    {
        private StoresManagement _storeManagement;

        public StoreService()
        {
            _storeManagement = new StoresManagement();
        }

        //Usecase - 3.2
        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy)
        {
            return _storeManagement.openStore(name, discountPolicy, purchasePolicy);
        }

        //Usecase - 4.1.1
        public bool addProductInv(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            return _storeManagement.addProductInv(storeName, productInvName, discount, purchaseType, price, quantity);

        }

        //Usecase - 4.1.2
        public bool deleteProductInv(string storeName, string productInvName)
        {
            return _storeManagement.deleteProductInventory(storeName, productInvName);
        }

        //Usecase - 4.1.3
        public bool addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            return _storeManagement.addProduct(storeName, productInvName, discount, purchaseType, quantity);
        }
        public bool deleteProduct(string storeName, string productInvName, int productID)
        {
            return _storeManagement.deleteProduct(storeName, productInvName, productID);
        }
        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            return _storeManagement.modifyProductName(storeName, newProductName, oldProductName);
        }
        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            return _storeManagement.modifyProductPrice(storeName, productInvName, newPrice);
        }
        public bool modifyProductQuantity(string storeName, string productInvName, int productID, int newQuantity)
        {
            return _storeManagement.modifyProductQuantity(storeName, productInvName, productID, newQuantity);
        }
        public bool modifyProductDiscountType(string storeName, string productInvName, int productID, Discount newDiscount)
        {
            return _storeManagement.modifyProductDiscountType(storeName, productInvName, productID, newDiscount);
        }
        public bool modifyProductPurchaseType(string storeName, string productInvName, int productID, PurchaseType purchaseType)
        {
            return _storeManagement.modifyProductPurchaseType(storeName, productInvName, productID, purchaseType);
        }

        //Usecase - 4.3
        public bool assignOwner(string newOwneruserName, string storeName)
        {
            return _storeManagement.assignOwner(newOwneruserName, storeName);
        }

        //Usecase - 4.5
        public bool assignManager(string newManageruserName, string storeName)
        {
            return _storeManagement.assignManager(newManageruserName, storeName);
        }

        //Usecase - 4.6
        public bool editPermissions(string storeName, string managerUserName, List<permissionType> permissions)
        {
            return _storeManagement.editPermissions(storeName, managerUserName, permissions);
        }

        //Usecase - 4.7
        public bool removeManager(string managerUserName, string storeName)
        {
            return _storeManagement.removeManager(managerUserName, storeName);
        }

        //Usecase - 4.10
        //TOOD

        //Usecase - 6.4
        //TOOD




    }
}
