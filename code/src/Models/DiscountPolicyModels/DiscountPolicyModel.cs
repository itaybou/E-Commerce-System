using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public abstract class DiscountPolicyModel
    {
        public Guid ID { get; set; }

        protected DiscountPolicyModel(Guid id)
        {
            ID = id;
        }

        public abstract string GetString();

        public abstract string GetSelectionString();

        public abstract DiscountPolicy ModelToOrigin();

    }
}
