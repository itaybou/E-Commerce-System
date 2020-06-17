
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class ConditionalCompositeProductDicountPolicy : ProductDiscount
    {
        public AndDiscountPolicy conditionTree { get; set; }

        private ConditionalCompositeProductDicountPolicy(float percentage, DateTime expDate, Guid ID, Guid productID, AndDiscountPolicy conditionTree) : base(percentage, expDate, ID, productID)
        {
            this.conditionTree = conditionTree;
            
        }

        public static ConditionalCompositeProductDicountPolicy Create(float percentage, DateTime expDate, Guid ID, Guid productID, AndDiscountPolicy conditionTree)
        {
            if (isConditionalProductTree(conditionTree))
            {
                return new ConditionalCompositeProductDicountPolicy(percentage, expDate.Date, ID, productID, conditionTree);
            }
            else
                return null;
        }

        private static bool isConditionalProductTree(CompositeDiscountPolicy tree)
        {
            foreach(DiscountPolicy d in tree.Children)
            {
                if (d is CompositeDiscountPolicy)
                {
                    return isConditionalProductTree((CompositeDiscountPolicy)d);
                }
                else if (!(d is ConditionalProductDiscount))
                    return false;
                else
                    return true;
            }

            return true;
        }

        //if the customer buy _reauiredQuantity he get discount percentage on all the quantity of the product
        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if (this.isSatisfied(products))
            {
                double newTotalPrice = (((100 - this.Percentage) / 100) * products[ProductID].basePrice) * products[ProductID].quantity;
                double basePrice = products[ProductID].basePrice;
                int quantity = products[ProductID].quantity;
                products[ProductID] = (basePrice, quantity, newTotalPrice);
            }
        }

        public override DiscountPolicyModel CreateModel()
        {
            var product = DataAccess.Instance.Products.GetByIdOrNull(ProductID, p => p.Id);
            return new ConditionalCompositeProductDiscModel(ID, (CompositeDiscountPolicyModel)conditionTree.CreateModel(), ExpirationDate, Percentage, ProductID, product.Name);
        }

        //check that the quantity of product id > required quantity
        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return DateTime.Compare(this.ExpirationDate, DateTime.Today) >= 0 && this.conditionTree.isSatisfied(products);
        }
    }
}
