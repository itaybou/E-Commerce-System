using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.Notifications
{
    class RemoveOwnerNotification : Notification
    {
        string _revmovedOwner;
        string _revmover;
        string _storeName;

        public RemoveOwnerNotification(string revmovedOwner, string revmover, string storeName) : base()
        {
            _revmovedOwner = revmovedOwner;
            _revmover = revmover;
            _storeName = storeName;
        }

        public override string getMessage()
        {
            return _time.ToString() + ": you have been removed by " + _revmover + "from being owner of the store " + _storeName;
        }
    }
}
