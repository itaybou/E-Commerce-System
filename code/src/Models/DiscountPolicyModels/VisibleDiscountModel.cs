using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public class VisibleDiscountModel : DiscountPolicyModel
    {
        public float Percentage { get; set; }
        public DateTime ExpDate { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }

        public VisibleDiscountModel(Guid ID, DateTime expDate, float percentage, Guid productID, string productName) : base(ID)
        {
            Percentage = percentage;
            ExpDate = expDate;
            ProductID = productID;
            ProductName = productName;
        }

        public override string GetString()
        {
            return "Visible Discount for product: " + ProductName + ", id: " + ProductID + "\n" + Percentage + "% discount, Expires: " + ExpDate.ToString("dd/MM/yyyy");
        }

        public override DiscountPolicy ModelToOrigin()
        {
            return new VisibleDiscount(this.Percentage, this.ExpDate, Guid.NewGuid(), this.ProductID);
        }
    }
}
