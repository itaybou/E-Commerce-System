using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    class XORDiscountPolicy : CompositeDiscountPolicy
    {
        public XORDiscountPolicy(Guid ID) : base(ID)
        {
        }

        public XORDiscountPolicy(Guid ID, List<DiscountPolicy> children) : base(ID, children)
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

            if (Children.Count == 0)
            {
                return;
            }

            List<double> totalPricesChildren = new List<double>();
            List<Dictionary<Guid, (double basePrice, int quantity, double totalPrice)>> productsClones = new List<Dictionary<Guid, (double basePrice, int quantity, double totalPrice)>>();

            //find the best children discount:
            foreach (DiscountPolicy d in Children)
            {
                Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> cloned = this.clone(products);
                d.calculateTotalPrice(cloned);
                productsClones.Add(cloned);
                totalPricesChildren.Add(this.sumPrice(cloned));
            }

            //find the index of the best children discount
            double minChildPrice = totalPricesChildren.Min();
            int minChildPriceIndex = totalPricesChildren.IndexOf(minChildPrice);

            //use the best children discount to update the prices in the products data structure
            Children.ElementAt(minChildPriceIndex).calculateTotalPrice(products);
        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {

            foreach(DiscountPolicy d in Children)
            {
                if (d.isSatisfied(products))
                    return true;
            }

            return false; // OR - beacuse if 1/2 satisfied its good, but also if 2/2 satisfied we choose the best of them
        }
    }
}
