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
    public class ConditionalStoreDiscountTests : DiscountTest
    {

        ConditionalStoreDiscount _satisfyDiscount;
        ConditionalStoreDiscount _notSatisfyDiscount;

        [SetUp]
        public new void setUp()
        {
            double totalCartPrice = _totalPrice1 + _totalPrice2 + _totalPrice3 + _totalPrice4 + _totalPrice5;
            _satisfyDiscount = new ConditionalStoreDiscount(totalCartPrice - 1, DateTime.Today.AddDays(10), 20, Guid.NewGuid());
            _notSatisfyDiscount = new ConditionalStoreDiscount(totalCartPrice + 1 , DateTime.Today.AddDays(10), 20, Guid.NewGuid());
        }



        [Test()]
        public void calculateTotalPriceTest()
        {
            _notSatisfyDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);

            _satisfyDiscount.calculateTotalPrice(_products);
            Assert.AreEqual((_basePrice1 * _quantity1) * 0.8, _products[_productID1].totalPrice);
            Assert.AreEqual((_basePrice2 * _quantity2) * 0.8, _products[_productID2].totalPrice);

        }

        [Test()]
        public void isSatisfiedTest()
        {
            Assert.True(_satisfyDiscount.isSatisfied(_products));
            Assert.False(_notSatisfyDiscount.isSatisfied(_products));
        }
    }
}