#pragma checksum "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Users\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "40d2340d6ca23d5da9398b18ae97e7c1d6b9fb23"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Dashboard), @"mvc.1.0.view", @"/Views/Users/Dashboard.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"40d2340d6ca23d5da9398b18ae97e7c1d6b9fb23", @"/Views/Users/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ECommerceSystem.Models.UserModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Users\Dashboard.cshtml"
  
    ViewData["Title"] = "User Dashboard";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <div class=""container"">
        <div class=""login-page form"">
            <h1><span class=""text-success""><i class=""fas fa-users-cog""></i></span> Dashboard</h1>
            <div class=""row"">
                <div class=""col-xs-12 col-sm-6 col-md-6"">
                    <div class=""well well-sm"">
                        <div class=""row"">
                            <div class=""col-sm-12"">
                                <h2>
                                    <b>Username: </b>");
#nullable restore
#line 15 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Users\Dashboard.cshtml"
                                                Write(Model.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </h2>
                                <small>
                                    Additional details:
                                </small>
                                <p>
                                    <i class=""fas fa-envelope""></i> ");
#nullable restore
#line 21 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Users\Dashboard.cshtml"
                                                               Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    <br />\r\n                                    <i class=\"fas fa-user\"></i> <b>Firstname: </b> ");
#nullable restore
#line 23 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Users\Dashboard.cshtml"
                                                                              Write(Model.Fname);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    <br />\r\n                                    <i class=\"fas fa-user\"></i> <b>Lastname: </b> ");
#nullable restore
#line 25 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Users\Dashboard.cshtml"
                                                                             Write(Model.Lname);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </p>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ECommerceSystem.Models.UserModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
