using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.notifications
{
    public class DisappvoveAssignOwnerNotification : INotitficationType
    {
        private string assignee;
        private string disapprover;
        private string storeName;

        public DisappvoveAssignOwnerNotification(string assignee, string disapprover, string storeName)
        {
            this.assignee = assignee;
            this.disapprover = disapprover;
            this.storeName = storeName;
        }

        public string getMessage()
        {
            return disapprover + "disapproved " + assignee + "to be owner of the store " + storeName;
        }
    }
}
