using System;
using System.Collections.Generic;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    public class Notification : INotification
    {
        protected DateTime _time;
        public (Guid, string) SenderMessage { get; private set; }
        public IDictionary<ICollection<Guid>, ICollection<string>> Messages { get; private set; }

        public Notification(Guid sender, string senderMessage, IDictionary<ICollection<Guid>, ICollection<string>> messages)
        {
            SenderMessage = (sender, senderMessage);
            this._time = DateTime.Now;
            Messages = messages;
        }

        public Notification(Guid sender, string senderMessage)
        {
            SenderMessage = (sender, senderMessage);
            this._time = DateTime.Now;
            Messages = new Dictionary<ICollection<Guid>, ICollection<string>>();
        }

        public Notification()
        {
            SenderMessage = (Guid.Empty, null);
            this._time = DateTime.Now;
            Messages = new Dictionary<ICollection<Guid>, ICollection<string>>();
        }

        public void AddGroupMessage(ICollection<Guid> group, string message)
        {
            var formattedMessage = FormattedMessage(message);
            if (Messages.ContainsKey(group))
                Messages[group].Add(formattedMessage);
            else Messages.Add(group, new List<string>() { formattedMessage });
        }

        public void AddPrivateMessage(Guid userID, string message)
        {
            Messages.Add(new List<Guid>() { userID }, new List<string>() { FormattedMessage(message) });
        }

        public void AddGroupRequest(ICollection<Guid> group, char request)
        {
            var requestCode = request.ToString();
            if (Messages.ContainsKey(group))
                Messages[group].Add(requestCode);
            else Messages.Add(group, new List<string>() { requestCode });
        }

        public void AddPrivateRequest(Guid userID, char request)
        {
            Messages.Add(new List<Guid>() { userID }, new List<string>() { request.ToString() });
        }

        private string FormattedMessage(string message)
        {
            return $"Sent at {_time.ToString()}: {message}";
        }
    }
}