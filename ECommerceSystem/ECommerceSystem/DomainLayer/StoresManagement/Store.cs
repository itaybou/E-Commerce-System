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

        public Store(DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy, string ownerUserName, string name)
        {
            this._discountPolicy = discountPolicy;
            this._purchasePolicy = purchasePolicy;
            this._premmisions = new Dictionary<string, Permissions>();
            this._inventory = new Inventory();
            this._premmisions.Add(ownerUserName, Permissions.CreateOwner(null)); 
            this.Name = name;
        }

        public string Name { get => _name; set => _name = value; }

        //*********Add, Delete, Modify Products*********


        //@pre - logged in user have permission to add product
        public bool addProductInv(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.Name].canAddProduct()))
            {
                return false;
            }

            return _inventory.addProductInv(productName, discount, purchaseType, price, quantity);
        }

        //@pre - logged in user have permission to modify product
        public bool addProduct(string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.addProduct(productInvName, discount, purchaseType, quantity);
        }


        //@pre - logged in user have permission to delete product
        public bool deleteProductInventory(string productInvName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.Name].canDeleteProduct()))
            {
                return false;
            }

            return _inventory.deleteProductInventory(productInvName);
        }

        //@pre - logged in user have permission to modify product
        public bool deleteProduct(string productInvName, int productID)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.deleteProduct(productInvName, productID);
        }


        private bool isLoggedInUserCanMoidfy()
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.Name].canModifyProduct()))
            {
                return false;
            }
            return true;
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductPrice(string productName, int newPrice)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.modifyProductPrice(productName, newPrice);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductDiscountType(string productInvName, int productID, Discount newDiscount)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.modifyProductDiscountType(productInvName, productID, newDiscount);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductPurchaseType(string productInvName, int productID, PurchaseType purchaseType)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.modifyProductPurchaseType(productInvName, productID, purchaseType);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductQuantity(string productName, int productID, int newQuantity)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.modifyProductQuantity(productName, productID, newQuantity);
        }

        //@pre - logged in user have permission to modify product
        public bool modifyProductName(string newProductName, string oldProductName)
        {
            if (!isLoggedInUserCanMoidfy())
            {
                return false;
            }
            else return _inventory.modifyProductName(newProductName, oldProductName);
        }


        private User isLoggedInUserSubscribed()
        {
            User loggedInUser = _userManagement.getLoggedInUser(); //sync
            if (!loggedInUser.isSubscribed()) // sync
            {
                return null;
            }
            return loggedInUser;
        }



        //*********Assign*********

        public bool assignOwner(string userName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_premmisions[loggedInUser.name].isOwner()) //Check that the assign is owner
            {
                return false;
            }

            if(_premmisions.ContainsKey(userName) && _premmisions[userName].isOwner() ) // The user of userName is already owner of this store
            {
                return false;
            }

            if (_premmisions.ContainsKey(userName))
            {
                _premmisions[userName].makeOwner();
            }
            else
            {
                Permissions per = Permissions.CreateOwner(loggedInUser);
                if (per == null) return false;
                _premmisions.Add(userName, per);
            }
            return true;
        }

        public bool assignManager(string userName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_premmisions[loggedInUser.Name].isOwner()) //Check that the assign is owner
            {
                return false;
            }

            if (_premmisions.ContainsKey(userName)) // The user of userName is already owner/manager of this store
            {
                return false;
            }

            Permissions per = Permissions.CreateManager(loggedInUser);
            if (per == null) return false;
            _premmisions.Add(userName, per);
            
            return true;
        }

        public bool removeManager(string userName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_premmisions.ContainsKey(userName) || _premmisions[userName].isOwner()) // The user of userName isn`t manager of this store or he is owner of this store
            {
                return false;
            }

            if (!_premmisions[userName].AssignedBy.Name.Equals(loggedInUser.Name)) //check that the logged in the user who assigned userName
            {
                return false;
            }

            _premmisions.Remove(userName);
            return true;
        }
    }
}
