using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Utilities.extensions;

namespace ECommerceSystem.Models.PurchasePolicyModels
{
    public class CompositePurchasePolicyModel : PurchasePolicyModel
    {
        public List<PurchasePolicyModel> Children;
        public CompositeType Type;

        public CompositePurchasePolicyModel(Guid ID, List<PurchasePolicyModel> children, CompositeType type) : base(ID)
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
                     "<h6>Purchase type: <span style=\"color: black\">{0}</span></b>", Type.GetStringValue()) + "</span></h5></li>");
            for (var i = 0; i < Children.Count; i++)
            {
                var discount = Children.ElementAt(i);
                builder.Append("<li class='list-group-item'>" + discount.GetString() + "</li>");
            }
            builder.Append("</ul>");
            return builder.ToString();
        }
    }
}
