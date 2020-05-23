using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
     public enum CompositeType
    {
        And,
        Or,
        Xor
    }

    public class CompositePurchasePolicyModel : PurchasePolicyModel
    {
        protected List<PurchasePolicyModel> _children;
        protected CompositeType _type;

        public CompositePurchasePolicyModel(Guid ID, List<PurchasePolicyModel> children, CompositeType type) : base(ID)
        {
            _children = children;
            _type = type;
        }
    }
}
