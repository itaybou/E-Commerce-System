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
    public class ConditionalProductDiscount : ProductDiscount
    {
        public int RequiredQuantity { get; set; }

        public ConditionalProductDiscount(float percentage, DateTime expDate, Guid ID, Guid productID, int reauiredQuantity) : base(percentage, expDate, ID, productID)
        {
            this.RequiredQuantity = reauiredQuantity;
        }

        //if the customer buy _reauiredQuantity he get discount percentage on all the quantity of the product
        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if(this.isSatisfied(products))
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
            return new ConditionalProductDiscountModel(this.ID, this.RequiredQuantity, this.ExpirationDate, this.Percentage, product.Id, product.Name);
        }

        //check that the quantity of product id > required quantity
        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return DateTime.Compare(this.ExpirationDate,DateTime.Today)>=0 && products.ContainsKey(ProductID) && products[ProductID].quantity >= RequiredQuantity;
        }
    }
}