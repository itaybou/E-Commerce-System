using System;
using System.Collections.Generic;

namespace ECommerceSystem.Models
{
    public interface INotificationRequest : INotitficationType
    {
        Guid RequestID { get; set; }
        char RequestCode { get; set; }
        DateTime Sent { get; set; }
        bool NotifyPast { get; set; }

        char GetRequest();

        string GetRequestString();

        ICollection<string> ExtraParams();
    }
}