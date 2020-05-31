namespace ECommerceSystem.Models
{
    internal class RemoveManagerNotification : INotitficationType
    {
        private string _revmovedManager;
        private string _revmover;
        private string _storeName;

        public RemoveManagerNotification(string revmovedManager, string revmover, string storeName)
        {
            _revmovedManager = revmovedManager;
            _revmover = revmover;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return "You have been removed by " + _revmover + " from being manager of the store " + _storeName;
        }
    }
}