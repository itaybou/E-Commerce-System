using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    public interface INotification
    {
        (Guid, string) SenderMessage { get; }
        IDictionary<ICollection<Guid>, string> Messages { get; } // { user list => message }

        void AddGroupMessage(ICollection<Guid> group, string message);
        void AddPrivateMessage(Guid userID, string message);
    }
}
