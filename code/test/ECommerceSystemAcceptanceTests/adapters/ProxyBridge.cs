using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class ProxyBridge : IBridgeAdapter
    {
        private IBridgeAdapter real;

        internal IBridgeAdapter RealBridge { get => real; set => real = value; }
    }
}
