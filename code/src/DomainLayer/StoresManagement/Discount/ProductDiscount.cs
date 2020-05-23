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
        protected Guid _productID;

        protected ProductDiscount(float percentage, DateTime expDate, Guid ID, Guid productID) : base(percentage, expDate, ID)
        {
            _productID = productID;
        }

        protected Guid ProductID { get => _productID; set => _productID = value; }
    }
}
