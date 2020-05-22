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
    public class VisibleDiscountTests
    {
        Guid _productID1;
        Guid _productID2;
        double _basePrice1;
        double _basePrice2;
        int _quantity1;
        int _quantity2;
        double _totalPrice1;
        double _totalPrice2;
        Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products;

        [SetUp]
        public void setUp()
        {
            _productID1 = Guid.NewGuid();
            _productID2 = Guid.NewGuid();
            _basePrice1 = 10;
            _basePrice2 = 20;
            _quantity1 = 10;
            _quantity2 = 20;
            _totalPrice1 = _basePrice1 * _quantity1;
            _totalPrice2 = _basePrice2 * _quantity2;
            products = new Dictionary<Guid, (double basePrice, int quantity, double totalPrice)>();
            products.Add(_productID1, (_basePrice1, _quantity1, _totalPrice1));
            products.Add(_productID2, (_basePrice2, _quantity2, _totalPrice2));
        }

        [Test()]
        public void calculateTotalPriceTest()
        {
            VisibleDiscount visibleDiscount = new VisibleDiscount(20, DateTime.Today.AddDays(10), Guid.NewGuid(), _productID1);
            VisibleDiscount notExistProductvisibleDiscount = new VisibleDiscount(20, DateTime.Today.AddDays(10), Guid.NewGuid(), Guid.NewGuid());

            notExistProductvisibleDiscount.calculateTotalPrice(products);
            Assert.AreEqual(_totalPrice1, products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, products[_productID2].totalPrice);


            visibleDiscount.calculateTotalPrice(products);
            Assert.AreEqual((_basePrice1 * _quantity1) * 0.8, products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, products[_productID2].totalPrice);


        }

    }
}