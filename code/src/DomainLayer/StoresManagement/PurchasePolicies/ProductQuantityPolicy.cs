using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class ProductQuantityPolicy : PurchasePolicy
    {
        int _min;
        int _max;
        Guid productID;

        public ProductQuantityPolicy(int min, int max)
        {
            this._min = min;
            this._max = max;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            int quantity = products[productID];
            return quantity >= _min && quantity <= _max;
        }
    }
}
