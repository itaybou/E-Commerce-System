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

        public bool addProduct(string productName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            User loggedInUser = _userManagement.getLoggedInUser(); //sync
            if (!loggedInUser.isSubscribed()) // sync
            {
                return false;
            }

            if (!(_premmisions[loggedInUser.name].canAddProduct()))
            {
                return false;
            }

            return _inventory.add(productName, discount, purchaseType, price, quantity);

        }
    }
}
