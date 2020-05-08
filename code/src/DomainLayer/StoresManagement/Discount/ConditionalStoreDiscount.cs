using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class ConditionalStoreDiscount : DiscountType
    {

        private double _reauiredPrice;

        public ConditionalStoreDiscount(double reauiredPrice, DateTime expDate, float percentage, Guid ID) : base(percentage, expDate, ID)
        {
            _reauiredPrice = reauiredPrice;
        }


        //if the customer have to pay _reauiredPrice, he get discount(by percentage from discountType)
        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products) 
        {
            if (this.isSatisfied(products))
            {
                foreach(var prod in products)
                {
                    double newTotalPrice = (((100 - this.Percentage) / 100) * prod.Value.basePrice) * prod.Value.quantity;
                    double basePrice = prod.Value.basePrice;
                    int quantity = prod.Value.quantity;
                    products[prod.Key] = (basePrice, quantity, newTotalPrice); // ??
                }
            }
        }

        //check that the total price > required price
        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            double totalPrice = 0;
            foreach (var prod in products)
            {
                totalPrice += prod.Value.totalPrice;
            }
            return totalPrice >= _reauiredPrice;
        }
    }
}
