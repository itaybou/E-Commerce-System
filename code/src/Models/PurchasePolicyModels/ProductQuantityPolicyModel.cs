using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class ProductQuantityPolicyModel : PurchasePolicyModel
    {
        public string ProductName { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }

        public ProductQuantityPolicyModel(Guid ID, string productName, int minQuantity, int maxQuantity) : base(ID)
        {
            MinQuantity = minQuantity;
            MaxQuantity = maxQuantity;
            ProductName = productName;
        }

        public override string GetString()
        {
            return "Product: " + ProductName + " can be purchased only in quantities in range " + MinQuantity + "-" + MaxQuantity;
        }
    }
}
