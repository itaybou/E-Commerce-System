using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.notifications
{
    public class OwnerAssignRequest : INotificationRequest
    {
        [BsonId]
        public Guid RequestID { get; set; }
        public string AssignerUserName { get; set; }
        public string AssigneeUserName { get; set; }
        public string StoreName { get; set; }
        public char RequestCode { get; set; }
        public DateTime Sent { get; set; }
        public bool NotifyPast { get; set; }

        public OwnerAssignRequest(string assignerUsername, string assigneeUserName, string storeName, Guid agreementID, char requestCode)
        {
            AssignerUserName = assignerUsername;
            AssigneeUserName = assigneeUserName;
            StoreName = storeName;
            RequestID = agreementID;
            RequestCode = requestCode;
            Sent = DateTime.Now;
            NotifyPast = true;
        }

        public string getMessage()
        {
            return "Please approve/disapprove " + AssigneeUserName + " to be a owner of the store - " + StoreName + " in your requests page";
        }

        public char GetRequest()
        {
            return RequestCode;
        }

        public string GetRequestString()
        {
            return "User " + AssigneeUserName + " has been suggested by " + AssignerUserName + " for ownership for a store you are currently owner in " + StoreName;
        }

        public ICollection<string> ExtraParams()
        {
            return new List<string>() { StoreName };
        }
    }
}
