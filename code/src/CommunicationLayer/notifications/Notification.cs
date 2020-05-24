using System;
using System.Collections.Generic;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    public class Notification : INotification
    {
        protected DateTime _time;
        public (Guid, string) SenderMessage { get; private set; }
        public IDictionary<ICollection<Guid>, string> Messages { get; private set; }

        public Notification(Guid sender, string senderMessage, IDictionary<ICollection<Guid>, string> messages)
        {
            SenderMessage = (sender, senderMessage);
            this._time = DateTime.Now;
            Messages = messages;
        }

        public Notification(Guid sender, string senderMessage)
        {
            SenderMessage = (sender, senderMessage);
            this._time = DateTime.Now;
            Messages = new Dictionary<ICollection<Guid>, string>();
        }

        public Notification()
        {
            SenderMessage = (Guid.Empty, null);
            this._time = DateTime.Now;
            Messages = new Dictionary<ICollection<Guid>, string>();
        }

        public void AddGroupMessage(ICollection<Guid> group, string message)
        {
            Messages.Add(group, $"Sent at {_time.ToString()}: {message}");
        }

        public void AddPrivateMessage(Guid userID, string message)
        {
            Messages.Add(new List<Guid>() { userID }, $"Sent at {_time.ToString()}: {message}");
        }
    }
}