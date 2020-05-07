using ECommerceSystem.CommunicationLayer.sessions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerceSystem.CommunicationLayer.notifications
{
    class NotificationCenter
    {
        readonly ISessionController SessionManager;
        readonly static Dictionary<Guid, ConcurrentQueue<string>> NotificationQueues = new Dictionary<Guid, ConcurrentQueue<string>>();

        public NotificationCenter(ISessionController sessionManager)
        {
            SessionManager = sessionManager;
        }

        private async Task SendNotificationAsync(WebSocket webSocket, string notification)
        {
            var bytes = Encoding.UTF8.GetBytes(notification);
            var buffer = new ArraySegment<byte>(bytes, 0, bytes.Length);
            await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task Notify(INotification notification)
        {
        }

        async Task NotifyPastNotifications(WebSocket webSocket, Guid sessionID)
        {

        }
    }
}
