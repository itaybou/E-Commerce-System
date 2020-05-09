using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    class PurchaseNotification : INotitficationType
    {
        string _username;
        string _storeName;

        public PurchaseNotification(string username, string storeName)
        {
            _username = username;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return _username + " bought products in the " + _storeName + " store";
        }
    }
}
