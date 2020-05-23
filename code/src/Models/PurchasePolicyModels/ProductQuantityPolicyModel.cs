using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class ProductQuantityPolicyModel : PurchasePolicyModel
    {
        protected int _minQuantity;
        protected int _maxQuantity;

        public ProductQuantityPolicyModel(Guid ID, int minQuantity, int maxQuantity) : base(ID)
        {
            _minQuantity = minQuantity;
            _maxQuantity = maxQuantity;
        }
    }
}
