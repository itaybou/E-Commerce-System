using ECommerceSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PresentationLayer.Controllers.Notification
{
    public class NotificationController : Controller
    {
        private const char AssignRequest = (char)0x07;
        private const char NewVisitRequest = (char)0x01;
        private static object _statsLock = new object();

        private ConcurrentDictionary<Guid, (string, DateTime)> SessionNotifications
        {
            get
            {
                var context = HttpContext;
                var notifications = context.Session.GetString("SessionNotification");
                if (String.IsNullOrEmpty(notifications))
                {
                    return new ConcurrentDictionary<Guid, (string, DateTime)>();
                }
                else
                {
                    return JsonConvert.DeserializeObject<ConcurrentDictionary<Guid, (string, DateTime)>>(notifications);
                }
            }

            set
            {
                if (value == null)
                {
                    HttpContext.Session.SetString("SessionNotification", "");
                }
                else
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

        public IActionResult RequestNotification(char request)
        {
            switch(request)
            {
                case AssignRequest:
                    try
                    {
                        var isLogin = HttpContext.Session.GetInt32("RequestLogin");
                        if(isLogin != 1)
                        {
                            var requestCount = Int32.Parse(HttpContext.Session.GetString("RequestCount"));
                            HttpContext.Session.SetString("RequestCount", (requestCount + 1).ToString());
                        } else HttpContext.Session.SetInt32("RequestLogin", 0);
                    } catch(Exception) { }
                    return Json(new { success = false, notification = "New Assign owner request is pending for your response." });
                case (char)((int)NewVisitRequest + UserTypes.Guests):
                        if ((HttpContext.Session.GetString("statistics") != null && HttpContext.Session.GetString("statistics").Equals("on")))
                        {
                            lock(_statsLock)
                            {
                                var count = HttpContext.Session.GetInt32("guests");
                                HttpContext.Session.SetInt32("guests", (int)(count + 1));
                                return Json(new { success = false, notification = "Guest", stats = true });
                            }
                          
                        }
                    break;
                case (char)((int)NewVisitRequest + UserTypes.Subscribed):
                        if ((HttpContext.Session.GetString("statistics") != null && HttpContext.Session.GetString("statistics").Equals("on")))
                        {
                            lock (_statsLock)
                            {
                                var count = HttpContext.Session.GetInt32("subscribed");
                                HttpContext.Session.SetInt32("subscribed", (int)(count + 1));
                                return Json(new { success = false, notification = "Subscribed", stats = true });
                            }
                        }
                    break;
                case (char)((int)NewVisitRequest + UserTypes.StoreManagers):
                        if ((HttpContext.Session.GetString("statistics") != null && HttpContext.Session.GetString("statistics").Equals("on")))
                        {
                            lock (_statsLock)
                            {
                                var count = HttpContext.Session.GetInt32("managers");
                                HttpContext.Session.SetInt32("managers", (int)(count + 1));
                                return Json(new { success = false, notification = "Store Manager", stats = true });
                            }
                        }
                    break;
                case (char)((int)NewVisitRequest + UserTypes.StoreOwners):
                        if ((HttpContext.Session.GetString("statistics") != null && HttpContext.Session.GetString("statistics").Equals("on")))
                        {
                            lock (_statsLock)
                            {
                                var count = HttpContext.Session.GetInt32("owners");
                                HttpContext.Session.SetInt32("owners", (int)(count + 1));
                                return Json(new { success = false, notification = "Store Owner", stats = true });
                            }
                        }
                    break;
                case (char)((int)NewVisitRequest + UserTypes.Admins):
                        if ((HttpContext.Session.GetString("statistics") != null && HttpContext.Session.GetString("statistics").Equals("on")))
                        {
                            lock (_statsLock)
                            {
                                var count = HttpContext.Session.GetInt32("admins");
                                HttpContext.Session.SetInt32("admins", (int)(count + 1));
                                return Json(new { success = false, notification = "Admin", stats = true });
                            }
                        }
                    break;
                default:
                    return Ok();
            }
            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult Notification(string notification)
        {
            char requestCode;
            if (notification != null)
            {
                if (notification.Length == 1 && Char.TryParse(notification, out requestCode))
                    return RedirectToAction("RequestNotification", "Notification", new { request = requestCode });
                var notifications = SessionNotifications;
                notifications.TryAdd(Guid.NewGuid(), (notification, DateTime.Now));
                SessionNotifications = notifications;
                HttpContext.Session.SetString("NotificationCount", SessionNotifications.Count.ToString());
                return Json(new { success = true, notification = notification });
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ApproveRequest(string id, char code)
        {
            switch(code)
            {
                case AssignRequest:
                    var storeName = Request.Form["extra0"].ToString();
                    return RedirectToAction("ApproveOwner", "Owner", new { id = id, store = storeName });
                default:
                    return new EmptyResult();
            }
        }

        [HttpPost]
        public IActionResult DenyRequest(string id, char code)
        {
            switch (code)
            {
                case AssignRequest:
                    var storeName = Request.Form["extra0"].ToString();
                    return RedirectToAction("DenyOwner", "Owner", new { id = id, store = storeName });
                default:
                    return new EmptyResult();
            }
        }
    }
}