using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount.Tests
{
    [TestFixture()]
    public class DiscountTest
    {
        protected Guid _productID1;
        protected Guid _productID2;
        protected Guid _productID3;
        protected Guid _productID4;
        protected Guid _productID5;
        protected double _basePrice1;
        protected double _basePrice2;
        protected double _basePrice3;
        protected double _basePrice4;
        protected double _basePrice5;
        protected int _quantity1;
        protected int _quantity2;
        protected int _quantity3;
        protected int _quantity4;
        protected int _quantity5;
        protected double _totalPrice1;
        protected double _totalPrice2;
        protected double _totalPrice3;
        protected double _totalPrice4;
        protected double _totalPrice5;
        protected Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> _products;


        [SetUp]
        public void setUp()
        {
            _productID1 = Guid.NewGuid();
            _productID2 = Guid.NewGuid();
            _productID3 = Guid.NewGuid();
            _productID4 = Guid.NewGuid();
            _productID5 = Guid.NewGuid();
            _basePrice1 = 10;
            _basePrice2 = 20;
            _basePrice3 = 30;
            _basePrice4 = 40;
            _basePrice5 = 50;
            _quantity1 = 10;
            _quantity2 = 20;
            _quantity3 = 30;
            _quantity4 = 40;
            _quantity5 = 50;
            _totalPrice1 = _basePrice1 * _quantity1;
            _totalPrice2 = _basePrice2 * _quantity2;
            _totalPrice3 = _basePrice3 * _quantity3;
            _totalPrice4 = _basePrice4 * _quantity4;
            _totalPrice5 = _basePrice5 * _quantity5;
            _products = new Dictionary<Guid, (double basePrice, int quantity, double totalPrice)>();
            _products.Add(_productID1, (_basePrice1, _quantity1, _totalPrice1));
            _products.Add(_productID2, (_basePrice2, _quantity2, _totalPrice2));
            _products.Add(_productID3, (_basePrice3, _quantity3, _totalPrice3));
            _products.Add(_productID4, (_basePrice4, _quantity4, _totalPrice4));
            _products.Add(_productID5, (_basePrice5, _quantity5, _totalPrice5));
        }
    }
}