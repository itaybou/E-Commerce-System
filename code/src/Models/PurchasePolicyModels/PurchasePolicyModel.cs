using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public abstract class PurchasePolicyModel
    {
        protected Guid ID;

        protected PurchasePolicyModel(Guid iD)
        {
            ID = iD;
        }
    }
}
