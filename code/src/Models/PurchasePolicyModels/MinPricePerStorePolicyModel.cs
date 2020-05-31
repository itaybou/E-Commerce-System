using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class MinPricePerStorePolicyModel : PurchasePolicyModel
    {
        public double RequiredMinPrice { get; set; }

        public MinPricePerStorePolicyModel(Guid ID, double requiredMinPrice) : base(ID)
        {
            RequiredMinPrice = requiredMinPrice;
        }

        public override string GetString()
        {
            return "Minimun Purchase Price: " + RequiredMinPrice; 
        }
    }
}
