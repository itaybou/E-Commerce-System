﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.DiscountPolicyModels;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class ConditionalProductDiscount : ProductDiscount
    {
        private int _requiredQuantity;

        public ConditionalProductDiscount(float percentage, DateTime expDate, Guid ID, Guid productID, int reauiredQuantity) : base(percentage, expDate, ID, productID)
        {
            this._requiredQuantity = reauiredQuantity;
        }

        //if the customer buy _reauiredQuantity he get discount percentage on all the quantity of the product
        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if(this.isSatisfied(products))
            {
                double newTotalPrice = (((100 - this.Percentage) / 100) * products[_productID].basePrice) * products[_productID].quantity;
                double basePrice = products[_productID].basePrice;
                int quantity = products[_productID].quantity;
                products[_productID] = (basePrice, quantity, newTotalPrice);
            }
        }

        public override DiscountPolicyModel CreateModel()
        {
            return new ConditionalProductDiscountModel(this._ID, this._requiredQuantity, this._expDate, this._percentage);
        }

        //check that the quantity of product id > required quantity
        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return products.ContainsKey(_productID) && products[_productID].quantity >= _requiredQuantity;
        }
    }
}