namespace ECommerceSystem.Models
{
    internal class AssignOwnerNotification : INotitficationType
    {
        private string _assigned;
        private string _assignee;
        private string _storeName;

        public AssignOwnerNotification(string assigned, string assignee, string storeName)
        {
            _assigned = assigned;
            _assignee = assignee;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return _assignee + " assigned you as a owner of the store " + _storeName;
        }
    }
}