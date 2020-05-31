namespace ECommerceSystem.Models
{
    internal class PurchaseNotification : INotitficationType
    {
        private string _username;
        private string _storeName;

        public PurchaseNotification(string username, string storeName)
        {
            _username = username;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return _username + " bought products in the " + _storeName + " store";
        }
    }
}