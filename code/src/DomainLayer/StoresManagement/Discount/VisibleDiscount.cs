using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class VisibleDiscount : DiscountType
    {

        Guid _productID;


        public VisibleDiscount(float percentage, DateTime expDate, Guid productID, Guid ID) : base(percentage, expDate, ID)
        {
            _productID = productID;
        }

        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if (!products.ContainsKey(_productID)) return;

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
