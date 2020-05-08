using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class ConditionalProductDiscount : DiscountType
    {
        private int _reauiredQuantity;
        private Guid _productID;

        public ConditionalProductDiscount(Guid productID, int reauiredQuantity, DateTime expDate, float percentage, Guid ID) : base(percentage, expDate, ID)
        {
            _reauiredQuantity = reauiredQuantity;
            _productID = productID;
        }


        //if the customer buy _reauiredQuantity he get discount percentage on all the quantity of the product
        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if(products[_productID].quantity >= _reauiredQuantity)
            {
                double newTotalPrice = (((100 - this.Percentage) / 100) * products[_productID].basePrice) * products[_productID].quantity;
                double basePrice = products[_productID].basePrice;
                int quantity = products[_productID].quantity;
                products[_productID] = (basePrice, quantity, newTotalPrice);
            }
        }

        //check that the quantity of product id > required quantity
        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return products[_productID].quantity >= _reauiredQuantity;
        }
    }
}
