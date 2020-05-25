using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies.Tests
{
    [TestFixture()]
    public class OrPurchasePolicyTests
    {
        List<string> _bannedLocationsIran;
        List<string> _bannedLocationsIraq;
        LocationPolicy _banIranPolicy;
        LocationPolicy _banIraqPolicy;
        MinPricePerStorePolicy _minPricePerStorePolicy;
        OrPurchasePolicy _singleChildOrPolicy;
        OrPurchasePolicy _twoChildrenOrPolicy;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            _bannedLocationsIran = new List<string>() { "iran" };
            _bannedLocationsIraq = new List<string>() { "iraq" };

            _banIranPolicy = new LocationPolicy(_bannedLocationsIran, Guid.NewGuid());
            _banIraqPolicy = new LocationPolicy(_bannedLocationsIraq, Guid.NewGuid());
            _minPricePerStorePolicy = new MinPricePerStorePolicy(200, Guid.NewGuid());

            Guid _singleChildAndPolicyID = Guid.NewGuid();
            Guid _twoChildrenAndPolicyID = Guid.NewGuid();
            _singleChildOrPolicy = new OrPurchasePolicy(new List<PurchasePolicy>() { _banIranPolicy }, _singleChildAndPolicyID);
            _twoChildrenOrPolicy = new OrPurchasePolicy(new List<PurchasePolicy>() { _banIraqPolicy, _minPricePerStorePolicy }, _twoChildrenAndPolicyID);
        }

        [Test()]
        public void canBuyEmptyChildrenTest()
        {
            OrPurchasePolicy empty = new OrPurchasePolicy(Guid.NewGuid());
            Assert.IsTrue(empty.canBuy(null, 100, null));
        }

        [Test()]
        public void canBuySingleChildTest()
        {
            Assert.IsFalse(_singleChildOrPolicy.canBuy(null, 10, "iran"));
            Assert.IsTrue(_singleChildOrPolicy.canBuy(null, 10, ""));
        }

        [Test()]
        public void canBuyTwoChildrenTest()
        {
            Assert.IsFalse(_twoChildrenOrPolicy.canBuy(null, 10, "iraq"));
            Assert.IsTrue(_twoChildrenOrPolicy.canBuy(null, 300, "iraq"));
            Assert.IsTrue(_twoChildrenOrPolicy.canBuy(null, 10, ""));
            Assert.IsTrue(_twoChildrenOrPolicy.canBuy(null, 300, ""));
        }

        [Test()]
        public void canBuyTwoLevelTest()
        {
            OrPurchasePolicy twoLevelPolicy = new OrPurchasePolicy(new List<PurchasePolicy>() { _singleChildOrPolicy, _twoChildrenOrPolicy }, Guid.NewGuid());

            Assert.IsTrue(twoLevelPolicy.canBuy(null, 300, "iran"));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 10, ""));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 300, "iraq"));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 10, "iraq"));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 10, "iran"));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 300, "iraq iran"));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 300, ""));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 10, "iraq iran"));
        }
    }
}