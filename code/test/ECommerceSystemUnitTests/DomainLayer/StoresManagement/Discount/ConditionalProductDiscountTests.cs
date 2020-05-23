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
    public class ConditionalProductDiscountTests : DiscountTest
    {

        ConditionalProductDiscount _satisfyDiscount;
        ConditionalProductDiscount _notSatisfyDiscount;

        [SetUp]
        public new void setUp()
        {
            _satisfyDiscount = new ConditionalProductDiscount(20, DateTime.Today.AddDays(10), Guid.NewGuid(), _productID1, _quantity1 - 1);
            _notSatisfyDiscount = new ConditionalProductDiscount(20, DateTime.Today.AddDays(10), Guid.NewGuid(), _productID2, _quantity2 + 1);
        }

        [Test()]
        public void calculateTotalPriceTest()
        {
            _notSatisfyDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);

            _satisfyDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1 * 0.8, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
        }

        [Test()]
        public void isSatisfiedTest()
        {
            Assert.True(_satisfyDiscount.isSatisfied(_products));
            Assert.False(_notSatisfyDiscount.isSatisfied(_products));
        }
    }
}