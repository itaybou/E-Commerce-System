using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public class ConditionalProductDiscountModel : DiscountPolicyModel
    {
        public int RequiredQuantity { get; set; }
        public float Percentage { get; set; }
        public DateTime ExpDate { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }

        public ConditionalProductDiscountModel(Guid ID, int requiredQuantity, DateTime expDate, float percentage, Guid productID, string productName) : base(ID)
        {
            RequiredQuantity = requiredQuantity;
            Percentage = percentage;
            ExpDate = expDate;
            ProductID = productID;
            ProductName = productName;
        }

        public override string GetString()
        {
            return "Conditional Discount for product: " + ProductName + ", id: " + ProductID + "\n" + "Buy " + RequiredQuantity + " and get "+ Percentage + "% discount, Expires: " + ExpDate.ToString("dd/MM/yyyy");
        }
    }
}
