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
    public class VisibleDiscount : ProductDiscount
    {
        public VisibleDiscount(float percentage, DateTime expDate, Guid ID, Guid productID) : base(percentage, expDate, ID, productID)
        {
        }

        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if (!products.ContainsKey(ProductID)) return; // the product of this discount isn`t exist in the cart

            double newTotalPrice = (((100 - this.Percentage) / 100) * products[ProductID].basePrice) * products[ProductID].quantity;
            double basePrice = products[ProductID].basePrice;
            int quantity = products[ProductID].quantity;
            products[ProductID] = (basePrice, quantity, newTotalPrice);
        }

        public override DiscountPolicyModel CreateModel()
        {
            var product = DataAccess.Instance.Products.GetByIdOrNull(ProductID, p => p.Id);
            return new VisibleDiscountModel(this.ID, this.ExpirationDate, this.Percentage, product.Id, product.Name);
        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return DateTime.Compare(this.ExpirationDate, DateTime.Today) >= 0;
        }
    }
}