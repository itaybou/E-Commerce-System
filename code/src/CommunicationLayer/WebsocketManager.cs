using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace ECommerceSystem.CommunicationLayer
{
    class WebsocketManager
    {
        private static ConcurrentDictionary<Guid, WebSocket> SessionSockets;
        private readonly NotificationCenter NotificationCenter;

        public WebSocketsManager(NotificationCenter notifications)
        {
            NotificationCenter = notifications;
            SessionSockets = new ConcurrentDictionary<Guid, WebSocket>();
        }

        public async Task HandleConnection(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals("/notifications"))
            {
                await NotificationCenter.HandleConnection(httpContext);
            }
            else httpContext.Response.StatusCode = 400;
        }

        public async Task ConnectAsync(HttpContext httpContext)
        {
            var sessionId = GetSessionID(httpContext);
            WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
            SessionSockets.TryAdd(sessionId, webSocket);
        }

        public static Guid GetSessionID(this HttpContext httpContext) => new Guid(httpContext.Session.SessionID);
    }
}
