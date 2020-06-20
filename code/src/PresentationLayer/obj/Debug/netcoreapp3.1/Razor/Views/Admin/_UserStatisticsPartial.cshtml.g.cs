#pragma checksum "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "283874ce83002ad05f2d3a46ac0fce8cbeda5aa7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin__UserStatisticsPartial), @"mvc.1.0.view", @"/Views/Admin/_UserStatisticsPartial.cshtml")]
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
#line 1 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"283874ce83002ad05f2d3a46ac0fce8cbeda5aa7", @"/Views/Admin/_UserStatisticsPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin__UserStatisticsPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ECommerceSystem.Models.UserStatistics>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""row p-3"">
    <!-- CHART -->
    <div class=""box box-primary"">
        <div class=""box-header with-border"">
            <h3 class=""box-title custom-heading"">User Types Site Entries Statistics For Period:</h3>
        </div>
        <div class=""box-body"">
            <div class=""chart"">
                <div class=""row justify-content-between"">
                    <div class=""col"" id=""graphId"" name=""graphId"" style=""width: 800px; height: 400px; margin:auto;""></div>
                    <div class=""col"" id=""graphId2"" name=""graphId2"" style=""width: 800px; height: 400px; margin:auto;""></div>
                </div>
");
#nullable restore
#line 17 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                 if (Model != null)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    <div class=""m-5"">
                        <h3>Per Day in Period Statistics:</h3>
                        <table class=""table table-striped"">
                            <thead>
                                <tr>
                                    <th scope=""col"">Date</th>
                                    <th scope=""col"">Guests</th>
                                    <th scope=""col"">Subscribed</th>
                                    <th scope=""col"">Store Managers</th>
                                    <th scope=""col"">Store Owners</th>
                                    <th scope=""col"">Admins</th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 33 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                 foreach (var stats in Model)
                                {
                                    if (stats.Date <= DateTime.Now.Date)
                                    {
                                        if (stats.Date.Equals(DateTime.Now.Date))
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr>\r\n                                                <td>\r\n                                                    <b>");
#nullable restore
#line 41 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                                  Write(stats.Date.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - TODAY</b>\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 44 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(Context.Session.GetInt32("guests"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 47 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(Context.Session.GetInt32("subscribed"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 50 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(Context.Session.GetInt32("managers"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 53 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(Context.Session.GetInt32("owners"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 56 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(Context.Session.GetInt32("admins"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                            </tr>\r\n");
#nullable restore
#line 59 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                        }
                                        else
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 64 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(stats.Date.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 67 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(stats.Statistics[UserTypes.Guests]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 70 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(stats.Statistics[UserTypes.Subscribed]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 73 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(stats.Statistics[UserTypes.StoreManagers]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 76 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(stats.Statistics[UserTypes.StoreOwners]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 79 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                               Write(stats.Statistics[UserTypes.Admins]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                            </tr>\r\n");
#nullable restore
#line 82 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                                        }
                                    }
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n");
#nullable restore
#line 89 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                } else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button style=\"width: 150px\" type=\"button\" class=\"btn btn-outline-primary m-1\"");
            BeginWriteAttribute("onclick", " onclick=\"", 5022, "\"", 5086, 3);
            WriteAttributeValue("", 5032, "location.href=\'", 5032, 15, true);
#nullable restore
#line 91 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
WriteAttributeValue("", 5047, Url.Action("UserStatistics", "Admin"), 5047, 38, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5085, "\'", 5085, 1, true);
            EndWriteAttribute();
            WriteLiteral("></button>\r\n");
#nullable restore
#line 92 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Admin\_UserStatisticsPartial.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div><!-- /.box-body -->\r\n    </div><!-- /.box -->\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ECommerceSystem.Models.UserStatistics>> Html { get; private set; }
    }
}
#pragma warning restore 1591
