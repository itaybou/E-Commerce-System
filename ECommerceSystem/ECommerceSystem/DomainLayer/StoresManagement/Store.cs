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


        public string Name { get => _name; set => _name = value; }

    }
}
