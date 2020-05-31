using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class LocationPolicyModel : PurchasePolicyModel
    {
        public List<string> BannedLocations { get; set; }

        public LocationPolicyModel(Guid ID, List<string> bannedLocations) : base(ID)
        {
            this.BannedLocations = bannedLocations;
        }

        public override string GetString()
        {
            var builder = new StringBuilder();
            builder.Append("Banned purchase locations: ");
            foreach(var location in BannedLocations)
            {
                builder.Append(location + ", ");
            }
            return builder.ToString();
        }
    }
}
