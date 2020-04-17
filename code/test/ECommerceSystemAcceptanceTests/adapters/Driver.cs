using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class Driver
    {
        public static IBridgeAdapter getAcceptanceBridge()
        {
            var proxyBrige = new ProxyBridge();
            proxyBrige.RealBridge = new RealBridge();
            return proxyBrige;
        }
    }
}
