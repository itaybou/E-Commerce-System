using ECommerceSystem.CommunicationLayer.notifications;
using ECommerceSystem.CommunicationLayer.sessions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ECommerceSystem.CommunicationLayer
{
    public class Communication : ICommunication
    {
        private static readonly Lazy<Communication> lazy = new Lazy<Communication>(() => new Communication());
        public static Communication Instance => lazy.Value;

        public delegate Task NotificationHandler(INotification notification);
        static public event NotificationHandler NotificationSubscribers;

        private readonly WebsocketManager WebSockets;
        private readonly NotificationCenter NotificationsCenter;

        private Communication()
        {
            NotificationsCenter = new NotificationCenter(SessionController.Instance);
            WebSockets = new WebsocketManager(NotificationsCenter);
        }

        public void SendNotification(INotification notification)
        {
            NotificationSubscribers?.Invoke(notification);
        }

        public void Subscribe()
        {
            NotificationSubscribers += NotificationsCenter.Notify;
        }

        public async Task HandleHttpRequest(HttpContext httpContext, Func<Task> next)
        {
            if (httpContext.WebSockets.IsWebSocketRequest)
                await WebSockets.HandleConnection(httpContext);
            else await next();
        }
    }
}
