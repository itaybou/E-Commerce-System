using ECommerceSystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        void SendPrivateNotificationRequest(Guid userID, INotificationRequest request);

        void SendGroupNotificationRequest(List<Guid> userIds, INotificationRequest request);

        void NotifyConnection(List<Guid> userIds, UserTypes typeConnected);
    }
}