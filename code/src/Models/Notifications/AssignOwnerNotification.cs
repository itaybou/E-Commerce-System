using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.Notifications
{
    class AssignOwnerNotification : Notification
    {
        string _assigned;
        string _assignee;
        string _storeName;

        public AssignOwnerNotification(string assigned, string assignee, string storeName) : base()
        {
            _assigned = assigned;
            _assignee = assignee;
            _storeName = storeName;
        }

        public override string getMessage()
        {
            return _time.ToString() + ": " + _assignee + " assigned you as a owner of the store " + _storeName;
        }
    }
}
