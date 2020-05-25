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
    public class CompositeDiscountPolicyTests : AbstractCompositeDiscountTest
    {
        XORDiscountPolicy _emptyXOrPolicy;
        XORDiscountPolicy _singleChildXOrP4Policy;
        OrDiscountPolicy _twoChildrenOrPolicyP1P3;
        AndDiscountPolicy _twoChildrenAndPolicyP2P5;
        OrDiscountPolicy _allDiscounts;


        Guid _emptyXOrPolicyID;
        Guid _singleChildXOrP4PolicyID;
        Guid _twoChildrenOrPolicyP1P3ID;
        Guid _twoChildrenAndPolicyP2P5ID;
        Guid _allDiscountsID;

        [SetUp]
        public new void setUp()
        {

            _emptyXOrPolicyID = Guid.NewGuid();
            _singleChildXOrP4PolicyID = Guid.NewGuid();
            _twoChildrenOrPolicyP1P3ID = Guid.NewGuid();
            _twoChildrenAndPolicyP2P5ID = Guid.NewGuid();
            _allDiscountsID = Guid.NewGuid();

            _emptyXOrPolicy = new XORDiscountPolicy(_emptyXOrPolicyID);
            _singleChildXOrP4Policy = new XORDiscountPolicy(_singleChildXOrP4PolicyID, new List<DiscountPolicy>() { _visibleDiscountProduct4 });
            _twoChildrenOrPolicyP1P3 = new OrDiscountPolicy(_twoChildrenOrPolicyP1P3ID, new List<DiscountPolicy>() { _satisfyConditionalProduct1Discount, _visibleDiscountProduct3 });
            _twoChildrenAndPolicyP2P5 = new AndDiscountPolicy(_twoChildrenAndPolicyP2P5ID, new List<DiscountPolicy>() { _notSatisfyConditionalProduct2Discount, _notSatisfyConditionalProduct5Discount});
            _allDiscounts = new OrDiscountPolicy(_allDiscountsID, new List<DiscountPolicy>() { _emptyXOrPolicy, _singleChildXOrP4Policy, _twoChildrenOrPolicyP1P3, _twoChildrenAndPolicyP2P5 });
        }

        [Test()]
        public void RemoveTest()
        {
            Assert.NotNull(_allDiscounts.getByID(_visibleDiscount4ID));
            _allDiscounts.Remove(_visibleDiscount4ID);
            Assert.Null(_allDiscounts.getByID(_visibleDiscount4ID));

            _allDiscounts.Remove(_twoChildrenOrPolicyP1P3ID);
            Assert.Null(_allDiscounts.getByID(_twoChildrenOrPolicyP1P3ID));
            Assert.Null(_allDiscounts.getByID(_conditionalProductDiscount1ID));
            Assert.Null(_allDiscounts.getByID(_visibleDiscount3ID));

            _allDiscounts.Remove(Guid.NewGuid());
            Assert.NotNull(_allDiscounts.getByID(_conditionalProductDiscount2ID));
            Assert.NotNull(_allDiscounts.getByID(_conditionalProductDiscount5ID));
            Assert.NotNull(_allDiscounts.getByID(_twoChildrenAndPolicyP2P5ID));
            Assert.NotNull(_allDiscounts.getByID(_emptyXOrPolicyID));
            Assert.NotNull(_allDiscounts.getByID(_singleChildXOrP4PolicyID));


        }

    }
}