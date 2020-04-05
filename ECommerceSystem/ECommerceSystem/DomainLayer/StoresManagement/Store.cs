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
        private DiscountPolicy _discountPolicy;
        private PurchasePolicy _purchasePolicy;

        private Dictionary<Subscribed, Permissions> managers;
        private Dictionary<Subscribed, Permissions> owners;

        private Inventory _inventory;
    }
}
