using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class ProductsQuantityPerStorePolicy : PurchasePolicy
    {
        int _min;
        int _max;

        public ProductsQuantityPerStorePolicy(int min, int max)
        {
            this._min = min;
            this._max = max;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            int totalQuantity = products.Aggregate(0, (acc, cur) => acc + cur.Value);
            if(totalQuantity <= _max && totalQuantity >= _min)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
