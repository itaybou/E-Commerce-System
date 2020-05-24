using ECommerceSystem.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.CommunicationLayer.sessions
{
    internal class SessionController : ISessionController
    {
        public static IDictionary<Guid, Guid> LoggedSessions;
        public static IDictionary<Guid, Guid> GuestSessions;

        private static readonly Lazy<SessionController> lazy = new Lazy<SessionController>(() => new SessionController());
        public static SessionController Instance => lazy.Value;

        private SessionController()
        {
            LoggedSessions = new ConcurrentDictionary<Guid, Guid>();
            GuestSessions = new ConcurrentDictionary<Guid, Guid>();
        }

        public Guid ResolveSession(Guid sessionID)
        {
            if (Get(LoggedSessions, sessionID) != Guid.Empty)
                return LoggedSessions[sessionID];
            if (Get(GuestSessions, sessionID) != Guid.Empty)
                return GuestSessions[sessionID];

            GuestSessions.Add(sessionID, sessionID);
            return GuestSessions[sessionID];
        }

        public void LoginSession(Guid sessionID, Guid userID)
        {
            GuestSessions.Remove(sessionID);
            LoggedSessions[sessionID] = userID;
        }

        public Guid LogoutSession(Guid sessionID)
        {
            if (Get(LoggedSessions, sessionID) == Guid.Empty)
                throw new AuthenticationException("session expired!");
            var userID = LoggedSessions[sessionID];
            LoggedSessions.Remove(sessionID);
            return userID;
        }

        internal bool ReaffirmSession(Guid guid)
        {
            return LoggedSessions.ContainsKey(guid);
        }

        public Guid GetLoggesUserIDBySession(Guid sessionID)
        {
            if (Get(LoggedSessions, sessionID) == Guid.Empty)
                return Guid.Empty;
            else return LoggedSessions[sessionID];
        }

        public Guid SessionIDByUserID(Guid userID)
        {
            var result = LoggedSessions.FirstOrDefault(s => s.Value.Equals(userID));
            return result.Key;
        }

        private static Guid Get(IDictionary<Guid, Guid> dict, Guid sessionID)
        {
            Guid userID;
            return dict.TryGetValue(sessionID, out userID) ? userID : Guid.Empty;
        }
    }
}