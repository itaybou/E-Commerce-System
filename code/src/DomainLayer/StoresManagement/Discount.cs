namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public abstract class Discount
    {
        private float _percentage;
        private DiscountPolicy _policy;

        public abstract double CalculateDiscount(double price); // returns discount percentage
    }
}