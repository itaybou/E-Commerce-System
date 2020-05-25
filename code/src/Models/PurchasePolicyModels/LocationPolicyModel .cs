using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class LocationPolicyModel : PurchasePolicyModel
    {
        protected List<string> bannedLocations;

        public LocationPolicyModel(Guid ID, List<string> bannedLocations) : base(ID)
        {
            this.bannedLocations = bannedLocations;
        }
    }
}
