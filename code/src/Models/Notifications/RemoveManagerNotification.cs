using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    class RemoveManagerNotification : INotitficationType
    {
        string _revmovedManager;
        string _revmover;
        string _storeName;

        public RemoveManagerNotification(string revmovedManager, string revmover, string storeName)
        {
            _revmovedManager = revmovedManager;
            _revmover = revmover;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return "You have been removed by " + _revmover + "from being manager of the store " + _storeName;
        }
    }
}
