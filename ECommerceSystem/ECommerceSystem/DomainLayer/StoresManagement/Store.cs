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
            this._premmisions.Add(ownerUserName, new Permissions(null, true)); 
            this.Name = name;
        }

        public string Name { get => _name; set => _name = value; }

        //@pre - logged in user have permission to add product
        public bool addProduct(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.name].canAddProduct()))
            {
                return false;
            }

            return _inventory.add(productName, discount, purchaseType, price, quantity);
        }

        //@pre - logged in user have permission to delete product
        public bool deleteProduct(string productName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.name].canDeleteProduct()))
            {
                return false;
            }

            return _inventory.delete(productName);
        }

        private bool isLoggedInUserCanMoidfy()
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.name].canModifyProduct()))
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
    }
}
