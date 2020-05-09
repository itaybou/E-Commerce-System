using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.Notifications
{
    class RemoveManagerNotification : Notification
    {
        string _revmovedManager;
        string _revmover;
        string _storeName;

        public RemoveManagerNotification(string revmovedManager, string revmover, string storeName) : base()
        {
            _revmovedManager = revmovedManager;
            _revmover = revmover;
            _storeName = storeName;
        }

        public override string getMessage()
        {
            return _time.ToString() + ": you have been removed by " + _revmover + "from being manager of the store " + _storeName;
        }
    }
}
