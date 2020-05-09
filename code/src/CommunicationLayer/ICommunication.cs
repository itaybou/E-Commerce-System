using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.CommunicationLayer.notifications;
using ECommerceSystem.Models;
using Microsoft.AspNetCore.Http;
using static ECommerceSystem.CommunicationLayer.Communication;

namespace ECommerceSystem.CommunicationLayer
{
    public interface ICommunication
    {
        Task HandleHttpRequest(HttpContext httpContext, Func<Task> next);
        void Subscribe();
        void SendPrivateNotification(Guid userID, string notification);
        void SendPrivateNotification(Guid userID, INotitficationType notification);
        void SendGroupNotification(List<Guid> userIds, string notification);
        void SendGroupNotification(List<Guid> userIds, INotitficationType notification);
    }
}
