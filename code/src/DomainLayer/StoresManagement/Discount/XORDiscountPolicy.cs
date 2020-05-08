using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    class XORDiscountPolicy : CompositeDicountPolicy
    {
        public XORDiscountPolicy(DiscountPolicy left, DiscountPolicy right, Guid ID) : base(left, right, ID)
        {
        }

        private Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> clone(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> cloneProducts = new Dictionary<Guid, (double basePrice, int quantity, double totalPrice)>();
            foreach(var prod in products)
            {
                cloneProducts.Add(prod.Key, (prod.Value.basePrice, prod.Value.quantity, prod.Value.totalPrice));
            }
            return cloneProducts;
        }

        //sum all the total prices of the products
        private double sumPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            double sum = 0;
            foreach(var prod in products)
            {
                sum += prod.Value.totalPrice;
            }
            return sum;
        }
        
        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> cloneProductsLeft = this.clone(products);
            Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> cloneProductsRight = this.clone(products);

            _left.calculateTotalPrice(cloneProductsLeft);
            _right.calculateTotalPrice(cloneProductsRight);

            double totalPriceLeft = this.sumPrice(cloneProductsLeft);
            double totalPriceRight = this.sumPrice(cloneProductsRight);

            if(totalPriceLeft < totalPriceRight)
            {
                _left.calculateTotalPrice(products);
            }
            else
            {
                _right.calculateTotalPrice(products);
            }

        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return _left.isSatisfied(products) || _right.isSatisfied(products); // OR - beacuse if 1/2 satisfied its good, but also if 2/2 satisfied we choose the best of them
        }
    }
}
