using System;
using System.Collections.Generic;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    public interface INotification
    {
        (Guid, string) SenderMessage { get; }

        bool NotifyPast { get; set; }

        IDictionary<ICollection<Guid>, ICollection<string>> Messages { get; } // { user list => message }

        void AddGroupMessage(ICollection<Guid> group, string message);

        void AddPrivateMessage(Guid userID, string message);

        void AddGroupRequest(ICollection<Guid> group, char request);

        void AddPrivateRequest(Guid userID, char request);
    }
}