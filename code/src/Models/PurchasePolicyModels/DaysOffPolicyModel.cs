using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class DaysOffPolicyModel : PurchasePolicyModel
    {
        protected List<DayOfWeek> daysOff;

        public DaysOffPolicyModel(Guid ID, List<DayOfWeek> daysOff) : base(ID)
        {
            this.daysOff = daysOff;
        }
    }
}
