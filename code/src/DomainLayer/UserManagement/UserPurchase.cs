﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class UserPurchase
    {
        private DateTime _purchaseDate;
        private List<Product> _productsPurchased;
        private double _totalPrice;
        private PaymentShipmentDetails _paymentShippingMethod;

        public UserPurchase(double totalPrice, List<Product> productsPurchased, 
            string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            _totalPrice = totalPrice;
            _productsPurchased = productsPurchased;
            _paymentShippingMethod = new PaymentShipmentDetails(firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
            _purchaseDate = DateTime.Now;
        }
    }

    public class PaymentShipmentDetails
    {
        private string _firstName;
        private string _lastName;
        private int _id;
        private string _creditCardNumber;
        private DateTime _expirationCreditCard;
        private int _CVV;
        private string _address;

        public PaymentShipmentDetails(string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            _firstName = firstName;
            _lastName = lastName;
            _id = id;
            _creditCardNumber = creditCardNumber;
            _expirationCreditCard = expirationCreditCard;
            _CVV = CVV;
            _address = address;
        }
    }
}
