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

        private Dictionary<string, Permissions> _managers;
        private Dictionary<string, Permissions> _owners;
        private Inventory _inventory;

        public Store(DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy, string ownerUserName, string name)
        {
            this._discountPolicy = discountPolicy;
            this._purchasePolicy = purchasePolicy;
            this._managers = new Dictionary<string, Permissions>();
            this._owners = new Dictionary<string, Permissions>();
            this._inventory = new Inventory();
            this._owners.Add(ownerUserName, new Permissions()); //TODO - permission
            this.Name = name;
        }

        public string Name { get => _name; set => _name = value; }

    }
}
