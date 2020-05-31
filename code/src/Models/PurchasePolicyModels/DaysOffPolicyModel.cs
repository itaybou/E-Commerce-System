using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class DaysOffPolicyModel : PurchasePolicyModel
    {
        public List<DayOfWeek> DaysOff { get; set; }

        public DaysOffPolicyModel(Guid ID, List<DayOfWeek> daysOff) : base(ID)
        {
            this.DaysOff = daysOff;
        }

        public override string GetString()
        {
            var builder = new StringBuilder();
            builder.Append("Store Days off: ");
            foreach (var day in DaysOff)
            {
                builder.Append(day.ToString() + ", ");
            }
            return builder.ToString();
        }
    }
}
