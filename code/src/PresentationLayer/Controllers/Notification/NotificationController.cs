using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

namespace PresentationLayer.Controllers.Notification
{
    public class NotificationController : Controller
    {
        private ConcurrentDictionary<Guid, (string, DateTime)> SessionNotifications
        {
            get
            {
                var context = HttpContext;
                var notifications = context.Session.GetString("SessionNotification");
                if (String.IsNullOrEmpty(notifications))
                {
                    return new ConcurrentDictionary<Guid, (string, DateTime)>();
                } else
                {
                    return JsonConvert.DeserializeObject<ConcurrentDictionary<Guid, (string, DateTime)>>(notifications);
                }
            }

            set
            {
                if(value == null)
                {
                    HttpContext.Session.SetString("SessionNotification", "");
                } else
                {
                    var serialized = JsonConvert.SerializeObject(value);
                    HttpContext.Session.SetString("SessionNotification", serialized);
                }
            }
        }

        public IActionResult Index()
        {
            var model = SessionNotifications;
            return View("../Notifications/Notifications", model);
        }

        public IActionResult RemoveAllNotifications(string id)
        {
            SessionNotifications = null;
            HttpContext.Session.SetString("NotificationCount", SessionNotifications.Count.ToString());
            return RedirectToAction("Index");
        }

        public IActionResult RemoveNotification(string id)
        {
            var notifications = SessionNotifications;
            notifications.TryRemove(new Guid(id), out var val);
            SessionNotifications = notifications;
            HttpContext.Session.SetString("NotificationCount", SessionNotifications.Count.ToString());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Notification(string notification)
        {
            var notifications = SessionNotifications;
            notifications.TryAdd(Guid.NewGuid(), (notification, DateTime.Now));
            SessionNotifications = notifications;
            HttpContext.Session.SetString("NotificationCount", SessionNotifications.Count.ToString());
            return Json(new { success = true, notification = notification });
        }
    }
}