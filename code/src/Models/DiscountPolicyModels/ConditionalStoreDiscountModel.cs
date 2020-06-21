using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public class ConditionalStoreDiscountModel : DiscountPolicyModel
    {
        public double RequiredPrice { get; set; }
        public float Percentage { get; set; }
        public DateTime ExpDate { get; set; }

        public ConditionalStoreDiscountModel(Guid ID, double requiredPrice, DateTime expDate, float percentage) : base(ID)
        {
            RequiredPrice = requiredPrice;
            Percentage = percentage;
            ExpDate = expDate;
        }

        public override string GetString()
        {
            return "Store discount: Buy above " + RequiredPrice + " and get " + Percentage + "% discount, expires: " + ExpDate.ToString("dd/MM/yyyy");
        }

        public override DiscountPolicy ModelToOrigin()
        {
            return new ConditionalStoreDiscount(this.RequiredPrice, this.ExpDate, this.Percentage, Guid.NewGuid());
        }

        public override string GetSelectionString()
        {
            return "";
        }
    }
}
