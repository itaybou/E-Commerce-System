using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    interface INotification
    {
        Guid Sender { get; }
        IDictionary<ICollection<Guid>, string> Messages { get; } // { user list => message }
    }
}
