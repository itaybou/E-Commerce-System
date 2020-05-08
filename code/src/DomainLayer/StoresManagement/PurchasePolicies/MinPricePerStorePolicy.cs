using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class MinPricePerStorePolicy : PurchasePolicy
    {
        double _minPrice;
        Guid _ID;


        public MinPricePerStorePolicy(double min, Guid ID)
        {
            this._minPrice = min;
            this._ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            return _minPrice <= totalPrice;
        }

        public Guid getID()
        {
            return _ID;
        }
    }
}
