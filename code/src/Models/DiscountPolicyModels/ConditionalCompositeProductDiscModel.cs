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

        public string StoreName { get; set; }

        public bool Init { get; set; }

        public ConditionalCompositeProductDiscModel(Guid ID, CompositeDiscountPolicyModel productTreeModel, DateTime expDate, float percentage, Guid productID, string productName) : base(ID)
        {
            this.productTreeModel = productTreeModel;
            Percentage = percentage;
            ExpDate = expDate;
            ProductID = productID;
            ProductName = productName;
        }

        public ConditionalCompositeProductDiscModel(Guid ID, DateTime expDate, Guid prodID, string prodName, float percentage, string store) : base(ID)
        {
            Percentage = percentage;
            ExpDate = expDate;
            ProductID = prodID;
            ProductName = prodName;
            StoreName = store;
            productTreeModel = null;
            Init = true;
        }

        public ConditionalCompositeProductDiscModel() : base(Guid.Empty)
        {
        }

        public override string GetString()
        {
            return "<h5>Discount for product: " + ProductName + ", id: " + ProductID + "\n" + Percentage + "% discount, Expires: " + ExpDate.ToString("dd/MM/yyyy") + " <b>Will Available on purchasing the following: </b></h5>" + productTreeModel.GetSelectionString();
        }

        public override DiscountPolicy ModelToOrigin()
        {
            DiscountPolicy newCond = this.productTreeModel.ModelToOrigin();

            return ConditionalCompositeProductDicountPolicy.Create(this.Percentage, this.ExpDate, Guid.NewGuid(), this.ProductID, (AndDiscountPolicy)newCond);
        }

        public override string GetSelectionString()
        {
            return GetString();
        }
    }
}

