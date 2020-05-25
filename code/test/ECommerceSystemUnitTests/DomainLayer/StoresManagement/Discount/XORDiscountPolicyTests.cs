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
    public class XORDiscountPolicyTests : AbstractCompositeDiscountTest
    {
        XORDiscountPolicy _emptyXOrPolicy;
        XORDiscountPolicy _singleChildXOrP4Policy;
        XORDiscountPolicy _twoChildrenXOrPolicyP3;
        XORDiscountPolicy _twoChildrenXOrPolicyP1P3;
        XORDiscountPolicy _twoChildrenNotSatisfyXOrPolicyP2P5;


        Guid _emptyXOrPolicyID;
        Guid _singleChildXOrPolicyID;
        Guid _twoChildrenXOrPolicyP3ID;
        Guid _twoChildrenXOrPolicyP1P3ID;
        Guid _twoChildrenNotSatisfyXOrPolicyP2P5ID;


        [SetUp]
        public new void setUp()
        {

            _emptyXOrPolicyID = Guid.NewGuid();
            _singleChildXOrPolicyID = Guid.NewGuid();
            _twoChildrenXOrPolicyP3ID = Guid.NewGuid();

            _emptyXOrPolicy = new XORDiscountPolicy(_emptyXOrPolicyID);
            _singleChildXOrP4Policy = new XORDiscountPolicy(_singleChildXOrPolicyID, new List<DiscountPolicy>() { _visibleDiscountProduct4 });
            _twoChildrenXOrPolicyP3 = new XORDiscountPolicy(_twoChildrenXOrPolicyP3ID, new List<DiscountPolicy>() { _notSatisfyConditionalProduct2Discount, _visibleDiscountProduct3 });
            _twoChildrenXOrPolicyP1P3 = new XORDiscountPolicy(_twoChildrenXOrPolicyP1P3ID, new List<DiscountPolicy>() { _satisfyConditionalProduct1Discount, _visibleDiscountProduct3 });
            _twoChildrenNotSatisfyXOrPolicyP2P5 = new XORDiscountPolicy(_twoChildrenNotSatisfyXOrPolicyP2P5ID, new List<DiscountPolicy>() { _notSatisfyConditionalProduct2Discount, _notSatisfyConditionalProduct5Discount });

        }


        [Test()]
        public void calculateTotalPriceEmptyChildrenXOrTest()
        {
            _emptyXOrPolicy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }

        [Test()]
        public void calculateTotalPriceSingleChildXOrTest()
        {
            _singleChildXOrP4Policy.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }

        [Test()]
        public void calculateTotalPriceTwoChildrenNotSatisfyXOrTest()
        {
            _twoChildrenNotSatisfyXOrPolicyP2P5.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoChildrenOneSatisfyXOrTest()
        {
            _twoChildrenXOrPolicyP3.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoChildrenTwoSatisfyXOrTest()
        {
            _twoChildrenXOrPolicyP1P3.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3 * 0.7, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);
        }

        [Test()]
        public void calculateTotalPriceTwoLevelOneSatisfyXOrTest()
        {
            XORDiscountPolicy _twoLevelXOrDiscount = new XORDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _singleChildXOrP4Policy, _twoChildrenNotSatisfyXOrPolicyP2P5 });


            _twoLevelXOrDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }

        [Test()]
        public void calculateTotalPriceTwoLevelTwoSatisfyXOrTest()
        {
            XORDiscountPolicy _twoLevelXOrDiscount = new XORDiscountPolicy(Guid.NewGuid(), new List<DiscountPolicy>() { _singleChildXOrP4Policy, _twoChildrenXOrPolicyP3 });

            _twoLevelXOrDiscount.calculateTotalPrice(_products);
            Assert.AreEqual(_totalPrice1, _products[_productID1].totalPrice);
            Assert.AreEqual(_totalPrice2, _products[_productID2].totalPrice);
            Assert.AreEqual(_totalPrice3, _products[_productID3].totalPrice);
            Assert.AreEqual(_totalPrice4 * 0.6, _products[_productID4].totalPrice);
            Assert.AreEqual(_totalPrice5, _products[_productID5].totalPrice);

        }
    }
}