using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class VisibleDiscount : ProductDiscount
    {
        public VisibleDiscount(float percentage, DateTime expDate, Guid ID, Guid productID) : base(percentage, expDate, ID, productID)
        {
        }

        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if (!products.ContainsKey(_productID)) return; // the product of this discount isn`t exist in the cart

            double newTotalPrice = (((100 - this.Percentage) / 100) * products[_productID].basePrice) * products[_productID].quantity;
            double basePrice = products[_productID].basePrice;
            int quantity = products[_productID].quantity;
            products[_productID] = (basePrice, quantity, newTotalPrice);
        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return true;
        }
    }
}
