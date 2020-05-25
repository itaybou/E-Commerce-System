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
    public class VisibleDiscountTests : DiscountTest
    {

        [Test()]
        public void calculateTotalPriceTest()
        {
            VisibleDiscount visibleDiscount = new VisibleDiscount(20, DateTime.Today.AddDays(10), Guid.NewGuid(), _productID1);
            VisibleDiscount notExistProductvisibleDiscount = new VisibleDiscount(20, DateTime.Today.AddDays(10), Guid.NewGuid(), Guid.NewGuid());

            notExistProductvisibleDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);


            visibleDiscount.calculateTotalPrice(_products);
            Assert.AreEqual((_basePrice1 * _quantity1) * 0.8, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);


        }

    }
}