using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.CommunicationLayer.notifications;
using Microsoft.AspNetCore.Http;
using static ECommerceSystem.CommunicationLayer.Communication;

namespace ECommerceSystem.CommunicationLayer
{
    public interface ICommunication
    {
        Task HandleHttpRequest(HttpContext httpContext, Func<Task> next);
        void Subscribe();
        void SendNotification(INotification notification);
    }
}
