using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    class VisibleDiscountModel : DiscountPolicyModel
    {
        float _percentage;
        Guid _productID;
        DateTime _expDate;

        public VisibleDiscountModel(Guid ID, DateTime expDate, float percentage, Guid productID) : base(ID)
        {
            _percentage = percentage;
            _productID = productID;
            _expDate = expDate;
        }
    }
}
