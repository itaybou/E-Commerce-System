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
    public class CompositePurchasePolicyTests
    {

        List<string> _bannedLocationsIran;
        List<string> _bannedLocationsIraq;
        List<string> _bannedLocationsTurkey;
        List<string> _bannedLocationsEgypt;
        List<string> _bannedLocationsLebanon;
        LocationPolicy _banIranPolicy;
        LocationPolicy _banIraqPolicy;
        LocationPolicy _banTurekeyPolicy;
        LocationPolicy _banEgyptPolicy;
        LocationPolicy _banLebanonPolicy;
        MinPricePerStorePolicy _minPricePerStorePolicy;
        XORPurchasePolicy _xorPolicy;
        OrPurchasePolicy _orPolicy;
        AndPurchasePolicy _andPolicy;
        AndPurchasePolicy _policy;

        Guid _banIranPolicyID;
        Guid _orPolicyID;
        Guid _banEgyptPolicyID;
        Guid _banTurkeyPolicyID;


        [SetUp]
        public void setUpFixture()
        {
            _bannedLocationsIran = new List<string>() { "iran" };
            _bannedLocationsIraq = new List<string>() { "iraq" };
            _bannedLocationsTurkey = new List<string>() { "turkey" };
            _bannedLocationsEgypt = new List<string>() { "egypt" };
            _bannedLocationsLebanon = new List<string>() { "lebanon" };

            _banIranPolicyID = Guid.NewGuid();
            _banTurkeyPolicyID = Guid.NewGuid();
            _banEgyptPolicyID = Guid.NewGuid();

            _banIranPolicy = new LocationPolicy(_bannedLocationsIran, _banIranPolicyID);
            _banIraqPolicy = new LocationPolicy(_bannedLocationsIraq, Guid.NewGuid());
            _banTurekeyPolicy = new LocationPolicy(_bannedLocationsTurkey, _banTurkeyPolicyID);
            _banEgyptPolicy = new LocationPolicy(_bannedLocationsEgypt, _banEgyptPolicyID);
            _banLebanonPolicy = new LocationPolicy(_bannedLocationsLebanon, Guid.NewGuid());
            _minPricePerStorePolicy = new MinPricePerStorePolicy(200, Guid.NewGuid());


            _xorPolicy = new XORPurchasePolicy(new List<PurchasePolicy>() { _banIranPolicy, _banIraqPolicy }, Guid.NewGuid());
            _orPolicyID = Guid.NewGuid();
            _orPolicy = new OrPurchasePolicy(new List<PurchasePolicy>() { _banTurekeyPolicy, _banEgyptPolicy }, _orPolicyID);
            _andPolicy = new AndPurchasePolicy(new List<PurchasePolicy>() { _banLebanonPolicy, _minPricePerStorePolicy }, Guid.NewGuid());
            _policy = new AndPurchasePolicy(new List<PurchasePolicy>() { _xorPolicy, _orPolicy, _andPolicy }, Guid.NewGuid()); ;
        }


        [Test()]
        public void canBuyTest()
        {

            Assert.IsTrue(_policy.canBuy(null, 300, "iran"));
            Assert.IsFalse(_policy.canBuy(null, 300, ""));
        }


        [Test()]
        public void RemoveLeafTest()
        {
            Assert.IsNotNull(_policy.getByID(_banIranPolicyID));
            _policy.Remove(_banIranPolicyID);
            Assert.IsNull(_policy.getByID(_banIranPolicyID));
        }

        [Test()]
        public void RemoveCompositeTest()
        {
            Assert.IsNotNull(_policy.getByID(_orPolicyID));
            _policy.Remove(_orPolicyID);
            Assert.IsNull(_policy.getByID(_orPolicyID));
            Assert.IsNull(_policy.getByID(_banEgyptPolicyID));
            Assert.IsNull(_policy.getByID(_banTurkeyPolicyID));
        }


        [Test()]
        public void getByIDTest()
        {
            Assert.IsNotNull(_policy.getByID(_orPolicyID));
            Assert.IsNotNull(_policy.getByID(_banEgyptPolicyID));
            Assert.IsNull(_policy.getByID(Guid.NewGuid()));

        }

    }
}