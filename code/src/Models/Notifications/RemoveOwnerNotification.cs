namespace ECommerceSystem.Models
{
    internal class RemoveOwnerNotification : INotitficationType
    {
        private string _revmovedOwner;
        private string _revmover;
        private string _storeName;

        public RemoveOwnerNotification(string revmovedOwner, string revmover, string storeName)
        {
            _revmovedOwner = revmovedOwner;
            _revmover = revmover;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return "You have been removed by " + _revmover + "from being owner of the store " + _storeName;
        }
    }
}