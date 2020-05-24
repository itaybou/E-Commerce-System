namespace ECommerceSystemAcceptanceTests.adapters
{
    internal class Driver
    {
        public static IBridgeAdapter getAcceptanceBridge()
        {
            var proxyBrige = new ProxyBridge();
            proxyBrige.RealBridge = new RealBridge();
            return proxyBrige;
        }
    }
}