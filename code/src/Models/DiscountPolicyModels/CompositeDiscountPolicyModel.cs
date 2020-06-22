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
        public List<DiscountPolicyModel> Children;
        public CompositeType Type;

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
                     "<h6>Discount type: <span style=\"color: black\">{0}</span></b>", Type.GetStringValue()) + "</span></h6></li>");
            for(var i = 0; i < Children.Count; i++)
            {
                var discount = Children.ElementAt(i);
                if ((discount is ConditionalProductDiscountModel && ((ConditionalProductDiscountModel)discount).Percentage != 0) ||
                    (discount is VisibleDiscountModel && ((VisibleDiscountModel)discount).Percentage != 0) ||
                    (discount is ConditionalCompositeProductDiscModel && ((ConditionalCompositeProductDiscModel)discount).Percentage != 0))
                builder.Append("<li class='list-group-item'>" + discount.GetString() + "</li>");
            }
            builder.Append("</ul>");
            return builder.ToString();
        }

        public override string GetSelectionString()
        {
            var builder = new StringBuilder();
            builder.Append(
                "<ul class='list-group p-1'>" +
                (Children.Count == 1 ? "" :
                string.Format("<li class='list-group-item list-group-item-primary p-0'><b>" +
                "<h6> Condition type: <span style=\"color: black\">{0}</span></b>", Type.GetStringValue()) + "</span></h6></li>"));
            for (var i = 0; i < Children.Count; i++)
            {
                var discount = Children.ElementAt(i);
                if(discount is CompositeDiscountPolicyModel && ((CompositeDiscountPolicyModel)discount).Children.Count == 0)
                    builder.Append("<li class='list-group-item' style='background-color: #FFD7D1'>" + "<div class=\"row\"><input class=\"checkbox\" type=\"radio\" id=\"edit\" name=\"edit\" value=\"" + discount.ID+ "\"> Choose to set composite product condition</div></li>");
                else builder.Append("<li class='list-group-item'>"+ discount.GetSelectionString() + "</li>");
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
