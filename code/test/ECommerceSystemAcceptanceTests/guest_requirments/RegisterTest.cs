using System;
using System.Collections.Generic;
using System.Text;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.2
    [TestFixture()]
    class RegisterTest
    {
        private IBridgeAdapter _bridge;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            _bridge = Driver.getAcceptanceBridge();
        }

        [TestCase()]
        public void 

    }
}
