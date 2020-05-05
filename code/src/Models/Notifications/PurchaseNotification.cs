using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.Notifications
{
    class PurchaseNotification : Notification
    {
        StorePurchaseModel _storePurchaseModel;
        string _storeName;

        public PurchaseNotification(StorePurchaseModel storePurchaseModel, string storeName)
        {
            this._storePurchaseModel = storePurchaseModel;
            _storeName = storeName;
        }

        public override string getMessage()
        {
            return _time + ": " + _storePurchaseModel.Username + " bought products in the " + _storeName + " store";
        }
    }
}
