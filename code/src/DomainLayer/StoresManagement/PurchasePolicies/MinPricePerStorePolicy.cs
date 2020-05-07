using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class MinPricePerStorePolicy : PurchasePolicy
    {
        int _minPrice;

        public MinPricePerStorePolicy(int min, int max)
        {
            this._minPrice = min;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            return _minPrice <= totalPrice;
        }
    }
}
