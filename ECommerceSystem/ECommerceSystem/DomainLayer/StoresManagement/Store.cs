using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class Store
    {
        private string _name;
        private DiscountPolicy _discountPolicy;
        private PurchasePolicy _purchasePolicy;
        private Dictionary<string, Permissions> _premmisions;
        private Inventory _inventory;
        private bool _isOpen;


        public Store(DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy, string ownerUserName, string name)
        {
            this._discountPolicy = discountPolicy;
            this._purchasePolicy = purchasePolicy;
            this._premmisions = new Dictionary<string, Permissions>();
            this._inventory = new Inventory();
            this._premmisions.Add(ownerUserName, Permissions.CreateOwner(null)); 
            this.Name = name;
            this._isOpen = true;
        }

        public string Name { get => _name; set => _name = value; }

        public bool isOpen()
        {
            return _isOpen;

        }
        //*********Add, Delete, Modify Products*********


        //@pre - logged in user have permission to add product
        public bool addProductInv(string activeUserName, string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            if (!(_premmisions[activeUserName].canAddProduct()))
            {
                return false;
            }

            return _inventory.addProductInv(productName, discount, purchaseType, price, quantity);
        }

        //@pre - logged in user have permission to modify product
        public bool addProduct(string loggedInUserName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }

            else return _inventory.addProduct(productInvName, discount, purchaseType, quantity);
        }


        //@pre - logged in user have permission to delete product
        public bool deleteProductInventory(string loggedInUserName, string productInvName)
        {

            if (!(_premmisions[loggedInUserName].canDeleteProduct()))
            {
                return false;
            }

            return _inventory.deleteProductInventory(productInvName);
        }

        //@pre - logged in user have permission to modify product
        public bool deleteProduct(string loggedInUserName, string productInvName, int productID)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }
            else return _inventory.deleteProduct(productInvName, productID);
        }


        //*********Modify Products*********


        private bool isLoggedInUserCanMoidfy(string loggedInUserName)
        {
            if (!(_premmisions[loggedInUserName].canModifyProduct()))
            {
                return false;
            }
            else
                return true;
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }
            else return _inventory.modifyProductPrice(productName, newPrice);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductDiscountType(string loggedInUserName, string productInvName, int productID, Discount newDiscount)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }
            else return _inventory.modifyProductDiscountType(productInvName, productID, newDiscount);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductPurchaseType(string loggedInUserName, string productInvName, int productID, PurchaseType purchaseType)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }
            else return _inventory.modifyProductPurchaseType(productInvName, productID, purchaseType);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductQuantity(string loggedInUserName, string productName, int productID, int newQuantity)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }
            else return _inventory.modifyProductQuantity(productName, productID, newQuantity);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName)
        {
            if (!isLoggedInUserCanMoidfy(loggedInUserName))
            {
                return false;
            }
            else return _inventory.modifyProductName(newProductName, oldProductName);
        }




        //*********Assign*********

        public bool assignOwner(User loggedInUser, string newOwneruserName)
        {

            if (!_premmisions[loggedInUser.Name].isOwner()) //Check that the assign is owner
            {
                return false;
            }

            if(_premmisions.ContainsKey(newOwneruserName) && _premmisions[newOwneruserName].isOwner() ) // The user of userName is already owner of this store
            {
                return false;
            }

            if (_premmisions.ContainsKey(newOwneruserName))
            {
                _premmisions[newOwneruserName].makeOwner();
            }
            else
            {
                Permissions per = Permissions.CreateOwner(loggedInUser);
                if (per == null) return false;
                _premmisions.Add(newOwneruserName, per);
            }
            return true;
        }

        public bool assignManager(User loggedInUser, string newManageruserName)
        {

            if (!_premmisions[loggedInUser.Name].isOwner()) //Check that the assign is owner
            {
                return false;
            }

            if (_premmisions.ContainsKey(newManageruserName)) // The user of userName is already owner/manager of this store
            {
                return false;
            }

            Permissions per = Permissions.CreateManager(loggedInUser);
            if (per == null) return false; // loggedInUser isn`t subscribed
            _premmisions.Add(newManageruserName, per);
            
            return true;
        }

        public bool removeManager(User loggedInUser, string managerUserName)
        {

            if (!_premmisions.ContainsKey(managerUserName) || _premmisions[managerUserName].isOwner()) // The user of userName isn`t manager of this store or he is owner of this store
            {
                return false;
            }

            if (!_premmisions[managerUserName].AssignedBy.Name.Equals(loggedInUser.Name)) //check that the logged in the user who assigned userName
            {
                return false;
            }

            _premmisions.Remove(managerUserName);
            return true;
        }


        // @Pre - loggedInUserName is the user who assign managerUserName
        //        managerUserName is manager in this store
        public bool editPermissions(string managerUserName, List<permissionType> permissions, string loggedInUserName)
        {
            if(!_premmisions.ContainsKey(managerUserName) || _premmisions[managerUserName].isOwner()) //The managerUserName isn`t manager
            {
                return false;
            }

            if (!_premmisions[managerUserName].AssignedBy.Name.Equals(loggedInUserName)) // The loggedInUserName isn`t the owner who assign managerUserName
            {
                return false;
            }

            _premmisions[managerUserName].edit(permissions);
            return true;
        }
    }
}
