using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    class AssignOwnerNotification : INotitficationType
    {
        string _assigned;
        string _assignee;
        string _storeName;

        public AssignOwnerNotification(string assigned, string assignee, string storeName)
        {
            _assigned = assigned;
            _assignee = assignee;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return _assignee + " assigned you as a owner of the store " + _storeName;
        }
    }
}
