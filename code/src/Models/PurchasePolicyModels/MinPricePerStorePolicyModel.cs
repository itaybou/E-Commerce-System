using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class MinPricePerStorePolicyModel : PurchasePolicyModel
    {
        protected double _requiredMinPrice;

        public MinPricePerStorePolicyModel(Guid ID, double requiredMinPrice) : base(ID)
        {
            _requiredMinPrice = requiredMinPrice;
        }
    }
}
