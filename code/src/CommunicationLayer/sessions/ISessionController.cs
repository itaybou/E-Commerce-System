using System;

namespace ECommerceSystem.CommunicationLayer.sessions
{
    public interface ISessionController
    {
        Guid ResolveSession(Guid sessionID);

        void LoginSession(Guid sessionID, Guid userID);

        Guid LogoutSession(Guid sessionID);

        Guid GetLoggesUserIDBySession(Guid sessionID);

        Guid SessionIDByUserID(Guid userID);
    }
}