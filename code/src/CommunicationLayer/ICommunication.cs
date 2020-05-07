using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ECommerceSystem.CommunicationLayer
{
    interface ICommunication
    {
        Task HandleHttpRequest(HttpContext httpContext, Func<Task> next);
    }
}
