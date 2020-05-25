using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies.Tests
{
    [TestFixture()]
    public class MinPricePerStorePolicyTests
    {

        MinPricePerStorePolicy _posPricePolicy;
        MinPricePerStorePolicy _zeroPricePolicy;


        [OneTimeSetUp]
        public void setUpFixture()
        {
            _posPricePolicy = new MinPricePerStorePolicy(100, Guid.NewGuid());
            _zeroPricePolicy = new MinPricePerStorePolicy(0, Guid.NewGuid());

        }

        [Test()]
        public void canBuyTest()
        {

            Assert.IsTrue(_zeroPricePolicy.canBuy(null, 150, null));
            Assert.IsTrue(_zeroPricePolicy.canBuy(null, 0, null));
            Assert.IsFalse(_zeroPricePolicy.canBuy(null, -50, null));

            Assert.IsTrue(_posPricePolicy.canBuy(null, 150, null));
            Assert.IsTrue(_posPricePolicy.canBuy(null, 100, null));
            Assert.IsFalse(_posPricePolicy.canBuy(null, 90, null));
            Assert.IsFalse(_posPricePolicy.canBuy(null, -50, null));

        }

    }
}