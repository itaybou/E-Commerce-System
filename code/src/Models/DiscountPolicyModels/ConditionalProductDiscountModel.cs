using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    class ConditionalProductDiscountModel : DiscountPolicyModel
    {
        int _requiredQuantity;
        float _percentage;
        DateTime _expDate;

        public ConditionalProductDiscountModel(Guid ID, int requiredQuantity, DateTime expDate, float percentage) : base(ID)
        {
            _requiredQuantity = requiredQuantity;
            _percentage = percentage;
            _expDate = expDate;
        }
    }
}
