using ECommerceSystem.Utilities;
using NUnit.Framework;
using System;

namespace ECommerceSystem.DomainLayer.Utilities.Tests
{
    [TestFixture()]
    public class RangeTests
    {
        private Range<double> _range;

        [OneTimeSetUp]
        public void setUp()
        {
            _range = null;
        }

        [TearDown]
        public void tearDown()
        {
            _range = null;
        }

        [Test()]
        public void RangeTest()
        {
            _range = new Range<double>(2.0, 5.0);
            Assert.AreEqual(_range.min, 2.0);
            Assert.AreEqual(_range.max, 5.0);
        }

        [Test()]
        public void BadRangeTest()
        {
            Assert.Throws<ArgumentException>(() => _range = new Range<double>(5.0, 2.0));
            Assert.Throws<ArgumentException>(() => _range = new Range<double>(1.0, 0.0));
            Assert.Throws<ArgumentException>(() => _range = new Range<double>(1.0, 0.99));
            Assert.Throws<ArgumentException>(() => _range = new Range<double>(0.09, 0.08));
        }

        [Test()]
        public void inRangeTest()
        {
            _range = new Range<double>(2.0, 5.0);
            Assert.True(_range.inRange(4.0));
            Assert.True(_range.inRange(4.999));
            Assert.True(_range.inRange(2.1));
            Assert.True(_range.inRange(2.0));
            Assert.True(_range.inRange(5.0));

        }

        [Test()]
        public void NotInRangeTest()
        {
            _range = new Range<double>(2.0, 5.0);
            Assert.False(_range.inRange(1.999));
            Assert.False(_range.inRange(0.0));
            Assert.False(_range.inRange(5.000001));
        }
    }
}