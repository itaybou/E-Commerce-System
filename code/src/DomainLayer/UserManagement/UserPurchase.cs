using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class UserPurchase
    {
        public DateTime PurchaseDate { get; set; }
        public List<Product> ProductsPurchased { get; set; }
        public double TotalPrice { get; set; }
        public PaymentShipmentDetails PaymentShippingMethod { get; set; }

        public UserPurchase(double totalPrice, List<Product> productsPurchased,
            string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            TotalPrice = totalPrice;
            ProductsPurchased = productsPurchased;
            PaymentShippingMethod = new PaymentShipmentDetails(firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
            PurchaseDate = DateTime.Now;
        }
    }

    public class PaymentShipmentDetails
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpirationCreditCard { get; set; }
        public int CVV { get; set; }
        public string Address { get; set; }

        public PaymentShipmentDetails(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            Firstname = firstName;
            Lastname = lastName;
            Id = id;
            CreditCardNumber = creditCardNumber;
            ExpirationCreditCard = expirationCreditCard;
            this.CVV = CVV;
            Address = address;
        }
    }
}