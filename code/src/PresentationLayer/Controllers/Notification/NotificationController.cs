﻿using ECommerceSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PresentationLayer.Controllers.Notification
{
    public class NotificationController : Controller
    {
        private const char AssignRequest = (char)0x06;
        private const char NewVisitRequest = (char)0x07;

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
                case (char)(NewVisitRequest + UserTypes.Guests):
                    {
                        var count = HttpContext.Session.GetInt32("guests");
                        HttpContext.Session.SetInt32("guests", (int)count++);
                    }
                    break;
                case (char)(NewVisitRequest + UserTypes.Subscribed):
                    {
                        var count = HttpContext.Session.GetInt32("subscribed");
                        HttpContext.Session.SetInt32("subscribed", (int)count++);
                    }
                    break;
                case (char)(NewVisitRequest + UserTypes.StoreManagers):
                    {
                        var count = HttpContext.Session.GetInt32("managers");
                        HttpContext.Session.SetInt32("managers", (int)count++);
                    }
                    break;
                case (char)(NewVisitRequest + UserTypes.StoreOwners):
                    {
                        var count = HttpContext.Session.GetInt32("owners");
                        HttpContext.Session.SetInt32("owners", (int)count++);
                    }
                    break;
                case (char)(NewVisitRequest + UserTypes.Admins):
                    {
                        var count = HttpContext.Session.GetInt32("admins");
                        HttpContext.Session.SetInt32("admins", (int)count++);
                    }
                    break;
                default:
                    break;
            }
            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult Notification(string notification)
        {
            char requestCode;
            if (notification.Length == 1 && Char.TryParse(notification, out requestCode))
                if (requestCode >= NewVisitRequest && HttpContext.Session.GetString("statistics").Equals("off"))
                    return Ok();
                else return RedirectToAction("RequestNotification", "Notification", new { request = requestCode });
            var notifications = SessionNotifications;
            notifications.TryAdd(Guid.NewGuid(), (notification, DateTime.Now));
            SessionNotifications = notifications;
            HttpContext.Session.SetString("NotificationCount", SessionNotifications.Count.ToString());
            return Json(new { success = true, notification = notification });
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