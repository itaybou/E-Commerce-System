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
            var (sender, senderNotif) = notification.SenderMessage;
            if(sender != Guid.Empty) 
                notification.AddPrivateMessage(sender, senderNotif);
            var notificationGroups = notification.Messages;
            foreach(var group in notificationGroups.Keys)
            {
                var notif = notificationGroups[group];
                foreach(var userID in group)
                {
                    var sessionID = SessionManager.SessionIDByUserID(userID);
                    if(!sessionID.Equals(Guid.Empty))
                    {
                        if(WebsocketManager.SessionSockets.ContainsKey(sessionID))
                        {
                            var socket = WebsocketManager.SessionSockets[sessionID];
                            await SendNotificationAsync(socket, notif);
                        }
                    } 
                    else
                    {
                        if(!NotificationQueues.ContainsKey(userID))
                            NotificationQueues.Add(userID, new ConcurrentQueue<string>());
                        NotificationQueues[userID].Enqueue(notif);
                    }
                }
            }
        }

        internal async Task NotifyPastNotifications(WebSocket webSocket, Guid sessionID)
        {
            var userID = SessionManager.GetLoggesUserIDBySession(sessionID);
            if(userID != Guid.Empty)
            {
                if(NotificationQueues.ContainsKey(userID))
                {
                    var notificationQueue = NotificationQueues[userID];
                    while(!notificationQueue.IsEmpty)
                    {
                        notificationQueue.TryDequeue(out var notification);
                        await SendNotificationAsync(webSocket, notification);
                    }
                }
            }
        }

    }
}
