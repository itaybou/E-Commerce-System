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
    public class AndPurchasePolicyTests
    {

        List<string> _bannedLocationsIran;
        List<string> _bannedLocationsIraq;
        LocationPolicy _banIranPolicy;
        LocationPolicy _banIraqPolicy;
        MinPricePerStorePolicy _minPricePerStorePolicy;
        AndPurchasePolicy _singleChildAndPolicy;
        AndPurchasePolicy _twoChildrenAndPolicy;

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
            _singleChildAndPolicy = new AndPurchasePolicy(new List<PurchasePolicy>() { _banIranPolicy }, _singleChildAndPolicyID);
            _twoChildrenAndPolicy = new AndPurchasePolicy(new List<PurchasePolicy>() { _banIraqPolicy, _minPricePerStorePolicy }, _twoChildrenAndPolicyID);
        }

        [Test()]
        public void canBuyEmptyChildrenTest()
        {
            AndPurchasePolicy empty = new AndPurchasePolicy(Guid.NewGuid());
            Assert.IsTrue(empty.canBuy(null, 100, null));
        }

        [Test()]
        public void canBuySingleChildTest()
        {

            Assert.IsFalse(_singleChildAndPolicy.canBuy(null, 10, "iran"));
            Assert.IsTrue(_singleChildAndPolicy.canBuy(null, 10, "iraq"));

        }

        [Test()]
        public void canBuyTwoChildrenTest()
        {

            Assert.IsFalse(_twoChildrenAndPolicy.canBuy(null, 300, "iraq"));
            Assert.IsFalse(_twoChildrenAndPolicy.canBuy(null, 10, ""));
            Assert.IsFalse(_twoChildrenAndPolicy.canBuy(null, 10, "iraq"));

            Assert.IsTrue(_twoChildrenAndPolicy.canBuy(null, 300, ""));
        }

        [Test()]
        public void canBuyTwoLevelTest()
        {
            AndPurchasePolicy twoLevelPolicy = new AndPurchasePolicy(new List<PurchasePolicy>() { _singleChildAndPolicy, _twoChildrenAndPolicy }, Guid.NewGuid());

            Assert.IsFalse(twoLevelPolicy.canBuy(null, 300, "iran"));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 10, ""));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 300, "iraq"));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 10, "iraq"));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 10, "iran"));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 300, "iraq iran"));
            Assert.IsFalse(twoLevelPolicy.canBuy(null, 10, "iraq iran"));
            Assert.IsTrue(twoLevelPolicy.canBuy(null, 300, ""));

        }

    }
}