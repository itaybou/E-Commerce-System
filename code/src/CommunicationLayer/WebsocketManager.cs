using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using ECommerceSystem.CommunicationLayer.notifications;
using ECommerceSystem.CommunicationLayer.sessions;
using Microsoft.AspNetCore.Http;

namespace ECommerceSystem.CommunicationLayer
{
    class WebsocketManager
    {
        private readonly NotificationCenter NotificationsCenter;
        private readonly SessionController SessionManager;
        private static ConcurrentDictionary<Guid, WebSocket> _sessionSockets;

        public static ConcurrentDictionary<Guid, WebSocket> SessionSockets { get => _sessionSockets; }

        public WebsocketManager(NotificationCenter notificationsCenter)
        {
            _sessionSockets = new ConcurrentDictionary<Guid, WebSocket>();
            NotificationsCenter = notificationsCenter;
            SessionManager = SessionController.Instance;
        }

        public async Task HandleConnection(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals("/wss"))
            {
                var session = GetSessionID(httpContext);
                if (SessionSockets.ContainsKey(session))
                {
                    await CloseConnectionAsync(httpContext, SessionSockets[GetSessionID(httpContext)], 
                        new WebSocketReceiveResult(0, WebSocketMessageType.Text, true));
                } else if(SessionManager.GetLoggesUserIDBySession(session) != Guid.Empty)
                {
                    var (sessionID, webSocket) = await ConnectAsync(httpContext);
                    await NotificationsCenter.NotifyPastNotifications(webSocket, sessionID);
                    var status = await ReceiveAsync(httpContext, webSocket);
                    await CloseConnectionAsync(httpContext, webSocket, status);
                } else httpContext.Response.StatusCode = 204; // No Content
            }
            else httpContext.Response.StatusCode = 400; // Bad Request
        }

        public async Task<(Guid, WebSocket)> ConnectAsync(HttpContext httpContext)
        {
            var sessionID = GetSessionID(httpContext);
            WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
            SessionSockets.TryAdd(sessionID, webSocket);
            return (sessionID, webSocket);
        }

        public async Task<WebSocketReceiveResult> ReceiveAsync(HttpContext httpContext, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult status = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!status.CloseStatus.HasValue)
                status = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            return status;
        }

        public async Task CloseConnectionAsync(HttpContext httpContext, WebSocket webSocket, WebSocketReceiveResult status)
        {
            await webSocket.CloseAsync(status.CloseStatus.Value, status.CloseStatusDescription, CancellationToken.None);
            SessionSockets.TryRemove(GetSessionID(httpContext), out var removedSocket);
        }

        public static Guid GetSessionID(HttpContext httpContext) => new Guid(httpContext.Session.Id);
    }
}
