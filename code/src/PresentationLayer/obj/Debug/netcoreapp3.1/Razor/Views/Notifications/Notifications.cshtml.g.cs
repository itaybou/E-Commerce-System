#pragma checksum "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c419a6a3b02eccd29842be5c772cc11ec86dde58"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Notifications_Notifications), @"mvc.1.0.view", @"/Views/Notifications/Notifications.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c419a6a3b02eccd29842be5c772cc11ec86dde58", @"/Views/Notifications/Notifications.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Notifications_Notifications : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IDictionary<Guid, (string, DateTime)>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
  
    ViewData["Title"] = "Recent Notifications";
    var notificationCount = Context.Session.GetString("NotificationCount");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"row justify-content-between\">\r\n        <h2>");
#nullable restore
#line 10 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(":</h2>\r\n        <div class=\"justify-content-center\">\r\n");
#nullable restore
#line 12 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
             if (notificationCount == null || notificationCount == "0")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h4 class=\"text-info\">You have no notifications currently.</h4>\r\n");
#nullable restore
#line 15 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <button class=\"btn btn-danger\"");
            BeginWriteAttribute("onclick", " onclick=\"", 635, "\"", 714, 3);
            WriteAttributeValue("", 645, "location.href=\'", 645, 15, true);
#nullable restore
#line 18 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
WriteAttributeValue("", 660, Url.Action("RemoveAllNotifications", "Notification"), 660, 53, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 713, "\'", 713, 1, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    Clear All\r\n                </button>\r\n");
#nullable restore
#line 21 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n            <table class=\"w-100\">\r\n");
#nullable restore
#line 24 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
                 foreach (var notification in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <tr>
                        <td>
                            <div class=""alert alert-warning"">
                                <div class=""row justify-content-between"">
                                    <h5><b>Notification:</b></h5>
                                    <button class=""btn btn-sm btn-danger""");
            BeginWriteAttribute("onclick", " onclick=\"", 1249, "\"", 1355, 3);
            WriteAttributeValue("", 1259, "location.href=\'", 1259, 15, true);
#nullable restore
#line 31 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
WriteAttributeValue("", 1274, Url.Action("RemoveNotification", "Notification", new { id = notification.Key }), 1274, 80, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1354, "\'", 1354, 1, true);
            EndWriteAttribute();
            WriteLiteral(@">
                                        <i class=""fas fa-times""></i>
                                    </button>
                                </div>
                                <div class=""row"">
                                    <div class=""col-md-12"">
                                        ");
#nullable restore
#line 37 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
                                   Write(notification.Value.Item1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </div>\r\n                                    <div class=\"col-md-12\">\r\n                                        <strong>Recieved at:</strong> ");
#nullable restore
#line 40 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
                                                                 Write(notification.Value.Item2);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 46 "C:\Users\Shir Markovits\Desktop\shir\University\semester f\סדנא\version4\E-Commerce-System\code\src\PresentationLayer\Views\Notifications\Notifications.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </table>\r\n        </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IDictionary<Guid, (string, DateTime)>> Html { get; private set; }
    }
}
#pragma warning restore 1591
