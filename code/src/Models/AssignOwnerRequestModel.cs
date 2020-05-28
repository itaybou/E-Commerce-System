using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class AssignOwnerRequestModel
    {
        [BsonId]
        public Guid AgreementID { get; set; }
        public string AssignerUserName { get; set; }
        public string AsigneeUserName { get; set; }
        public string StoreName { get; set; }

        public AssignOwnerRequestModel(Guid agreementID, string assignerUserName, string asigneeUserName, string storeName)
        {
            AgreementID = agreementID;
            AssignerUserName = assignerUserName;
            AsigneeUserName = asigneeUserName;
            StoreName = storeName;
        }
    }
}
