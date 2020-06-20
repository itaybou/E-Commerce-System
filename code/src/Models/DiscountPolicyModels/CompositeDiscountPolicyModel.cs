using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Utilities.extensions;

namespace ECommerceSystem.Models.DiscountPolicyModels
{

    public class CompositeDiscountPolicyModel : DiscountPolicyModel
    {
        public List<DiscountPolicyModel> Children { get; set; }
        public CompositeType Type { get; set; }

        public CompositeDiscountPolicyModel(Guid ID, List<DiscountPolicyModel> children, CompositeType type) : base(ID)
        {
            Children = children;
            Type = type;
        }

        public override string GetString()
        {
            if (Children.Count < 2)
                return null;
            var builder = new StringBuilder();
            builder.Append(
                "<ul class='list-group p-1'>" +
                     string.Format("<li class='list-group-item list-group-item-primary p-0'><b>" +
                     "<h6>Discount type: <span style=\"color: black\">{0}</span></b>", Type.GetStringValue()) + "</span></h5></li>");
            for(var i = 0; i < Children.Count; i++)
            {
                var discount = Children.ElementAt(i);
                builder.Append("<li class='list-group-item'>" + discount.GetString() + "</li>");
            }
            builder.Append("</ul>");
            return builder.ToString();
        }

        public override DiscountPolicy ModelToOrigin()
        {
            List<DiscountPolicy> newChildren = new List<DiscountPolicy>();
            foreach(DiscountPolicyModel child in this.Children)
            {
                newChildren.Add(child.ModelToOrigin());
            }
            if (this.Type.Equals(CompositeType.Or))
            {
                return new OrDiscountPolicy(Guid.NewGuid(), newChildren);
            }
            else if (this.Type.Equals(CompositeType.And))
            {
                return new AndDiscountPolicy(Guid.NewGuid(), newChildren);
            }
            else if (this.Type.Equals(CompositeType.Xor))
            {
                return new XORDiscountPolicy(Guid.NewGuid(), newChildren);
            }
            else
                return null;

        }
    }
}
