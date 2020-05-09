using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    public class Notification : INotification
    {
        public (Guid, string) SenderMessage { get; private set; }
        public IDictionary<ICollection<Guid>, string> Messages { get; private set; }

        public Notification(Guid sender, string senderMessage, IDictionary<ICollection<Guid>, string> messages)
        {
            SenderMessage = (sender, senderMessage);
            Messages = messages;
        }

        public Notification(Guid sender, string senderMessage)
        {
            SenderMessage = (sender, senderMessage);
            Messages = new Dictionary<ICollection<Guid>, string>();
        }

        public Notification()
        {
            SenderMessage = (Guid.Empty, null);
            Messages = new Dictionary<ICollection<Guid>, string>();
        }

        public void AddGroupMessage(ICollection<Guid> group, string message)
        {
            Messages.Add(group, message);
        }

        public void AddPrivateMessage(Guid userID, string message)
        {
            Messages.Add(new List<Guid>() { userID }, message);
        }
    }
}
