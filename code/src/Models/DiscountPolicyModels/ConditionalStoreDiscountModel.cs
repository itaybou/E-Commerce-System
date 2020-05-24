using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    class ConditionalStoreDiscountModel : DiscountPolicyModel
    {
        double _requiredPrice;
        float _percentage;
        DateTime _expDate;

        public ConditionalStoreDiscountModel(Guid ID, double requiredPrice, DateTime expDate, float percentage) : base(ID)
        {
            _requiredPrice = requiredPrice;
            _percentage = percentage;
            _expDate = expDate;
        }
    }
}
