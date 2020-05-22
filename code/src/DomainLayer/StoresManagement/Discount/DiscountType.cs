using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public abstract class DiscountType : DiscountPolicy
    {
        protected float _percentage;
        protected DateTime _expDate;
        protected Guid _ID;

        protected DiscountType(float percentage, DateTime expDate, Guid ID)
        {
            _percentage = percentage;
            _expDate = expDate;
            _ID = ID;
        }

        public float Percentage { get => _percentage; set => _percentage = value; }

        public abstract void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);

        public Guid getID()
        {
            return _ID;
        }

        public abstract bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);
    }
}