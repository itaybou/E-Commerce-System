using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;

namespace ECommerceSystem.Models.DiscountPolicyModels
{
    public class ConditionalCompositeProductDiscModel : DiscountPolicyModel
    {
        public CompositeDiscountPolicyModel productTreeModel { get; set; }
        public float Percentage { get; set; }
        public DateTime ExpDate { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }

        public ConditionalCompositeProductDiscModel(Guid ID, CompositeDiscountPolicyModel productTreeModel, DateTime expDate, float percentage, Guid productID, string productName) : base(ID)
        {
            this.productTreeModel = productTreeModel;
            Percentage = percentage;
            ExpDate = expDate;
            ProductID = productID;
            ProductName = productName;
        }

        public override string GetString()
        {
            return "";
            //return "Composite conditional Discount for product: " + ProductName + ", id: " + ProductID + "\n" + "Buy " + RequiredQuantity + " and get " + Percentage + "% discount, Expires: " + ExpDate.ToString("dd/MM/yyyy");
        }

        public override DiscountPolicy ModelToOrigin()
        {
            DiscountPolicy newCond = this.productTreeModel.ModelToOrigin();
            return ConditionalCompositeProductDicountPolicy.Create(this.Percentage, this.ExpDate, Guid.NewGuid(), this.ProductID, (AndDiscountPolicy)newCond);
        }

        
    }
}

