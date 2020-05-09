using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.Notifications
{
    class AssignManagerNotification : Notification
    {
        string _assigned;
        string _assignee;
        string _storeName;

        public AssignManagerNotification(string assigned, string assignee, string storeName) : base()
        {
            _assigned = assigned;
            _assignee = assignee;
            _storeName = storeName;
        }

        public override string getMessage()
        {
            return _time.ToString() + ": " + _assignee + " assigned you as a manager of the store " + _storeName;
        }
    }
}
