#pragma checksum "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1bdeac5ec55c30b963573bd135017913d1782024"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Store_UserStoreList), @"mvc.1.0.view", @"/Views/Store/UserStoreList.cshtml")]
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
#line 1 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1bdeac5ec55c30b963573bd135017913d1782024", @"/Views/Store/UserStoreList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30be6e8d357e7570361a44c6f59cbfc77c09d358", @"/Views/_ViewImports.cshtml")]
    public class Views_Store_UserStoreList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<StoreModel, List<ProductModel>>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("hidden"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "StoreProductListing", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "StoreProductsView", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Owner", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
  
    ViewData["Title"] = "My Stores";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container py-2\">\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-12 mx-auto\">\r\n            <div class=\"row\">\r\n                <div class=\"col\">\r\n                    <h1>");
#nullable restore
#line 12 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                   Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
                </div>
                <div class=""col justify-content-center align-self-center text-right"">
                    <button type=""submit"" style=""width: 200px"" class=""btn btn-lg btn-success"">
                        <i class=""fas fa-plus""></i><i class=""fas fa-store""></i> Open Store
                    </button>
                </div>
            </div>
            <!-- List group-->
            <ul class=""list-group shadow"">
                <!-- list group item-->
");
#nullable restore
#line 23 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                 foreach (var store in Model.Keys)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <li class=""list-group-item"">
                    <!-- Custom content-->
                    <div class=""row"">
                        <div class=""col media align-items-lg-center flex-column flex-lg-row p-3"">
                            <div class=""media-body order-2 order-lg-1"">
                                <h3 class=""mt-0 font-weight-bold mb-2"">");
#nullable restore
#line 30 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                                  Write(store.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                                <ul class=\"list-inline small stars\">\r\n");
#nullable restore
#line 32 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                     for (var i = 0; i < Math.Round(store.Rating); i++)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <li class=\"list-inline-item m-0\"><span class=\"fa fa-star checked\"></span></li>\r\n");
#nullable restore
#line 35 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                     for (var i = 1; i <= 5 - Math.Round(store.Rating); i++)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <li class=\"list-inline-item m-0\"><span class=\"fa fa-star\" style=\"color: grey !important\"></span></li>\r\n");
#nullable restore
#line 39 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </ul>\r\n                                <span><b>Rated by: </b>");
#nullable restore
#line 41 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                  Write(store.RaterCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" customers</span>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"col justify-content-center align-self-center text-right\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bdeac5ec55c30b963573bd135017913d178202410609", async() => {
                WriteLiteral(@"
                                <button type=""submit"" style=""width: 200px"" class=""btn btn-outline-primary m-1 ml-lg-5 order-1 order-lg-2"">
                                    View Products <i class=""fas fa-store""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 45 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                                                                                   WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bdeac5ec55c30b963573bd135017913d178202414036", async() => {
                WriteLiteral(@"
                                <button type=""submit"" style=""width: 200px"" class=""btn btn-outline-primary m-1 ml-lg-5 order-1 order-lg-2"">
                                    Manage Inventory <i class=""fas fa-warehouse""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 50 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                                                                               WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col\">\r\n                        <div class=\"row justify-content-lg-start align-self-right text-right float-lg-right\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bdeac5ec55c30b963573bd135017913d178202417692", async() => {
                WriteLiteral(@"
                                <button type=""submit"" style=""width: 130px; background-color: orangered"" class=""btn m-1 text-light"" >
                                    Close Store <i class=""fas fa-store-slash""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 60 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                                                                                   WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bdeac5ec55c30b963573bd135017913d178202421117", async() => {
                WriteLiteral(@"
                                <button type=""submit"" style=""width: 250px"" class=""btn btn-outline-primary m-1"">
                                    Modify Purchase Policies <i class=""fas fa-dollar-sign""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 65 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                                                                                   WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1bdeac5ec55c30b963573bd135017913d178202424534", async() => {
                WriteLiteral(@"
                                <button type=""submit"" style=""width: 250px"" class=""btn btn-outline-primary m-1"">
                                    Modify Discount Policies <i class=""fas fa-percentage""></i>
                                </button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 70 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                                                                                                                   WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </li>\r\n                    <!-- End -->\r\n");
#nullable restore
#line 79 "C:\My Files\School\הנדסת תוכנה\שנה ג\סמסטר ב\סדנא ליישום פרוייקט תוכנה\Workspace\Version 1\E-Commerce-System\code\src\PresentationLayer\Views\Store\UserStoreList.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </ul> <!-- End -->\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<StoreModel, List<ProductModel>>> Html { get; private set; }
    }
}
#pragma warning restore 1591