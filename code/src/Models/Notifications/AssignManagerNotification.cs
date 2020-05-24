namespace ECommerceSystem.Models
{
    internal class AssignManagerNotification : INotitficationType
    {
        private string _assigned;
        private string _assignee;
        private string _storeName;

        public AssignManagerNotification(string assigned, string assignee, string storeName)
        {
            _assigned = assigned;
            _assignee = assignee;
            _storeName = storeName;
        }

        public string getMessage()
        {
            return _assignee + " assigned you as a manager of the store " + _storeName;
        }
    }
}