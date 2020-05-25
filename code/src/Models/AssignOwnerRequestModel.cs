using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class AssignOwnerRequestModel
    {
        public Guid AgreementID;
        public string AssignerUserName;
        public string AsigneeUserName;
        public string StoreName;

        public AssignOwnerRequestModel(Guid agreementID, string assignerUserName, string asigneeUserName, string storeName)
        {
            AgreementID = agreementID;
            AssignerUserName = assignerUserName;
            AsigneeUserName = asigneeUserName;
            StoreName = storeName;
        }
    }
}
