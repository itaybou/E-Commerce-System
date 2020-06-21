using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public class ConditionalProductDiscountModel : DiscountPolicyModel
    {
        public int RequiredQuantity { get; set; }
        public float Percentage { get; set; }
        public DateTime ExpDate { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string Additional { get; set; }

        public ConditionalProductDiscountModel(Guid ID, int requiredQuantity, DateTime expDate, float percentage, Guid productID, string productName, string additional = "") : base(ID)
        {
            RequiredQuantity = requiredQuantity;
            Percentage = percentage;
            ExpDate = expDate;
            ProductID = productID;
            ProductName = productName;
            Additional = additional;
        }

        public override string GetString()
        {
            return "Conditional Discount for product: " + ProductName + ", id: " + ProductID + "\n" + "Buy " + RequiredQuantity + " and get "+ Percentage + "% discount, Expires: " + ExpDate.ToString("dd/MM/yyyy");
        }

        public override DiscountPolicy ModelToOrigin()
        {
            return new ConditionalProductDiscount(this.Percentage, this.ExpDate, Guid.NewGuid(), this.ProductID, this.RequiredQuantity);
        }

        public override string GetSelectionString()
        {
            if (String.IsNullOrEmpty(Additional))
                return "ID: " + ProductID + " Name: " + ProductName;
            else return Additional;
        }
    }
}
