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
    public class AndDiscountPolicyTests : AbstractCompositeDiscountTest
    {

        AndDiscountPolicy _emptyAndPolicy;
        AndDiscountPolicy _singleChildAndPolicy;
        AndDiscountPolicy _notSatisfyTwoChildrenAndPolicy;
        AndDiscountPolicy _satisfyTwoChildrenAndPolicy;

        Guid _emptyAndPolicyID;
        Guid _singleChildAndPolicyID;
        Guid _notSatisfyTwoChildrenAndPolicyID;
        Guid _satisfyTwoChildrenAndPolicyID;


        [SetUp]
        public new void setUp()
        {

            _emptyAndPolicyID = Guid.NewGuid();
            _singleChildAndPolicyID = Guid.NewGuid();
            _notSatisfyTwoChildrenAndPolicyID = Guid.NewGuid();

            _emptyAndPolicy = new AndDiscountPolicy(_emptyAndPolicyID);
            _singleChildAndPolicy = new AndDiscountPolicy(_singleChildAndPolicyID, new List<DiscountPolicy>() { _visibleDiscountProduct4 });
            _notSatisfyTwoChildrenAndPolicy = new AndDiscountPolicy(_notSatisfyTwoChildrenAndPolicyID, new List<DiscountPolicy>() { _notSatisfyConditionalProduct2Discount, _visibleDiscountProduct3 });
            _satisfyTwoChildrenAndPolicy = new AndDiscountPolicy(_satisfyTwoChildrenAndPolicyID, new List<DiscountPolicy>() { _satisfyConditionalProduct1Discount, _visibleDiscountProduct3 });

        }


        [Test()]
        public void calculateTotalPriceEmptyChildrenAndTest()
        {
            _emptyAndPolicy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceSingleChildAndTest()
        {
            _singleChildAndPolicy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoChildrenAndTest()
        {
            _notSatisfyTwoChildrenAndPolicy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);


            _satisfyTwoChildrenAndPolicy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1 * 0.9, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoLevelAndTest()
        {
            AndDiscountPolicy _twoLevelSatisfyAndDiscount = new AndDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _singleChildAndPolicy, _satisfyTwoChildrenAndPolicy });
            AndDiscountPolicy _twoLevelNotSatisfyAndDiscount = new AndDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _singleChildAndPolicy, _notSatisfyTwoChildrenAndPolicy });

            _twoLevelNotSatisfyAndDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);


            _twoLevelSatisfyAndDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1 * 0.9, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
        }

    }
}