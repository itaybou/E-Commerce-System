using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;

namespace ECommerceSystem.Models.DiscountPolicyModels
{

    public class CompositeDiscountPolicyModel : DiscountPolicyModel
    {
        List<DiscountPolicyModel> _children;
        CompositeType _type;

        public CompositeDiscountPolicyModel(Guid ID, List<DiscountPolicyModel> children, CompositeType type) : base(ID)
        {
            _children = children;
            _type = type;
        }
    }
}
