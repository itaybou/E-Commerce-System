using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.notifications
{
    public class OwnerAssignRequest : INotitficationType
    {
        private string _assigneeUserName;
        private string _storeName;
        private Guid _agreementID;

        public OwnerAssignRequest(string assigneeUserName, string storeName, Guid agreementID)
        {
            _assigneeUserName = assigneeUserName;
            _storeName = storeName;
            _agreementID = agreementID;
        }

        public string getMessage()
        {
            return "Please approve " + _assigneeUserName + " to be a owner of the store - " + _storeName + " in your requests page";
        }
    }
}
