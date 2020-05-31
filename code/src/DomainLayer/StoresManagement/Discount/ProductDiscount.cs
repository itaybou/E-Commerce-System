using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.DiscountPolicyModels;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public abstract class ProductDiscount : DiscountType
    {
        public Guid ProductID { get; set; }

        protected ProductDiscount(float percentage, DateTime expDate, Guid ID, Guid productID) : base(percentage, expDate, ID)
        {
            ProductID = productID;
        }
    }
}