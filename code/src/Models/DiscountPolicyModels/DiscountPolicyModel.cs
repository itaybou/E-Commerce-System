using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public abstract class DiscountPolicyModel
    {
        Guid _ID;

        protected DiscountPolicyModel(Guid ID)
        {
            _ID = ID;
        }
    }
}
