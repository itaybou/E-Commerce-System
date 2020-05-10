using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    class RemoveOwnerNotification : INotitficationType
    {
        string _revmovedOwner;
        string _revmover;
        string _storeName;

        public RemoveOwnerNotification(string revmovedOwner, string revmover, string storeName)
        {
            _revmovedOwner = revmovedOwner;
            _revmover = revmover;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return "You have been removed by " + _revmover + "from being owner of the store " + _storeName;
        }
    }
}
