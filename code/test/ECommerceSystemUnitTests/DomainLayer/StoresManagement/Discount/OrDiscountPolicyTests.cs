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
    public class OrDiscountPolicyTests : AbstractCompositeDiscountTest
    {

        OrDiscountPolicy _emptyOrPolicy;
        OrDiscountPolicy _singleChildOrP4Policy;
        OrDiscountPolicy _twoChildrenOrPolicyP3;
        OrDiscountPolicy _twoChildrenOrPolicyP1P3;
        OrDiscountPolicy _twoChildrenNotSatisfyOrPolicyP2P5;


        Guid _emptyOrPolicyID;
        Guid _singleChildOrPolicyID;
        Guid _twoChildrenOrPolicyP3ID;
        Guid _twoChildrenOrPolicyP1P3ID;
        Guid _twoChildrenNotSatisfyOrPolicyP2P5ID;


        [SetUp]
        public new void setUp()
        {

            _emptyOrPolicyID = Guid.NewGuid();
            _singleChildOrPolicyID = Guid.NewGuid();
            _twoChildrenOrPolicyP3ID = Guid.NewGuid();

            _emptyOrPolicy = new OrDiscountPolicy(_emptyOrPolicyID);
            _singleChildOrP4Policy = new OrDiscountPolicy(_singleChildOrPolicyID, new List<DiscountPolicy>() { _visibleDiscountProduct4 });
            _twoChildrenOrPolicyP3 = new OrDiscountPolicy(_twoChildrenOrPolicyP3ID, new List<DiscountPolicy>() { _notSatisfyConditionalProduct2Discount, _visibleDiscountProduct3 });
            _twoChildrenOrPolicyP1P3 = new OrDiscountPolicy(_twoChildrenOrPolicyP1P3ID, new List<DiscountPolicy>() { _satisfyConditionalProduct1Discount, _visibleDiscountProduct3 });
            _twoChildrenNotSatisfyOrPolicyP2P5 = new OrDiscountPolicy(_twoChildrenNotSatisfyOrPolicyP2P5ID, new List<DiscountPolicy>() { _notSatisfyConditionalProduct2Discount, _notSatisfyConditionalProduct5Discount });

        }


        [Test()]
        public void calculateTotalPriceEmptyChildrenOrTest()
        {
            _emptyOrPolicy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }

        [Test()]
        public void calculateTotalPriceSingleChildOrTest()
        {
            _singleChildOrP4Policy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }

        [Test()]
        public void calculateTotalPriceTwoChildrenNotSatisfyOrTest()
        {
            _twoChildrenNotSatisfyOrPolicyP2P5.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoChildrenOneSatisfyOrTest()
        {
            _twoChildrenOrPolicyP3.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoChildrenTwoSatisfyOrTest()
        {
            _twoChildrenOrPolicyP1P3.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1 * 0.9, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoLevelOrTest()
        {
            OrDiscountPolicy _twoLevelSatisfyOrDiscount = new OrDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _singleChildOrP4Policy, _twoChildrenNotSatisfyOrPolicyP2P5 });


            _twoLevelSatisfyOrDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }

        [Test()]
        public void calculateTotalPriceThreeLevelOrTest()
        {
            OrDiscountPolicy _twoLevelNotOrDiscount1 = new OrDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _twoChildrenOrPolicyP1P3 });
            OrDiscountPolicy _twoLevelNotOrDiscount2 = new OrDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _singleChildOrP4Policy, _twoChildrenNotSatisfyOrPolicyP2P5 });
            OrDiscountPolicy _threeLevelOrDiscount = new OrDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _twoLevelNotOrDiscount1, _twoLevelNotOrDiscount2 });

            _threeLevelOrDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1 * 0.9, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }
    }
}