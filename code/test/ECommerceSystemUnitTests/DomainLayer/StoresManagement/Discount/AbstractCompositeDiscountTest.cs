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
    public class AbstractCompositeDiscountTest : DiscountTest
    {
        protected ConditionalProductDiscount _satisfyConditionalProduct1Discount;
        protected ConditionalProductDiscount _notSatisfyConditionalProduct2Discount;
        protected ConditionalProductDiscount _notSatisfyConditionalProduct5Discount;
        protected VisibleDiscount _visibleDiscountProduct3;
        protected VisibleDiscount _visibleDiscountProduct4;

        protected Guid _visibleDiscount3ID;
        protected Guid _visibleDiscount4ID;
        protected Guid _conditionalProductDiscount1ID;
        protected Guid _conditionalProductDiscount2ID;
        protected Guid _conditionalProductDiscount5ID;


        [SetUp]
        public new void setUp()
        {
            _conditionalProductDiscount1ID = Guid.NewGuid();
            _conditionalProductDiscount2ID = Guid.NewGuid();
            _conditionalProductDiscount5ID = Guid.NewGuid();
            _visibleDiscount3ID = Guid.NewGuid();

            _satisfyConditionalProduct1Discount = new ConditionalProductDiscount(10, DateTime.Today.AddDays(10), _conditionalProductDiscount1ID, _productID1, _quantity1 - 1);
            _notSatisfyConditionalProduct2Discount = new ConditionalProductDiscount(20, DateTime.Today.AddDays(10), _conditionalProductDiscount2ID, _productID1, _quantity2 + 1);
            _notSatisfyConditionalProduct5Discount = new ConditionalProductDiscount(50, DateTime.Today.AddDays(10), _conditionalProductDiscount5ID, _productID5, _quantity5 + 1);
            _visibleDiscountProduct3 = new VisibleDiscount(30, DateTime.Today.AddDays(10), _visibleDiscount3ID, _productID3);
            _visibleDiscountProduct4 = new VisibleDiscount(40, DateTime.Today.AddDays(10), _visibleDiscount4ID, _productID4);


        }

    }
}