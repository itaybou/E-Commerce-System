#pragma checksum "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edb33a7c8383a9e2e5cedc4ca58eb386936bcce4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Owner_purchase_AddCompositePolicy), @"mvc.1.0.view", @"/Views/Owner/purchase/AddCompositePolicy.cshtml")]
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
#line 1 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\_ViewImports.cshtml"
using PresentationLayer.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
using System.Text.RegularExpressions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
using ECommerceSystem.Utilities.extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
using ECommerceSystem.Utilities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edb33a7c8383a9e2e5cedc4ca58eb386936bcce4", @"/Views/Owner/purchase/AddCompositePolicy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Owner_purchase_AddCompositePolicy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<SelectListItem>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-item"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Owner", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddCompositePolicy", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 6 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
  
    ViewData["Title"] = "Add Composite Purchase Policy";
    var storeName = ViewData["StoreName"];
    var operators = Enum.GetValues(typeof(CompositeType)).Cast<CompositeType>();
    var opTypes = operators.ToDictionary(k => k, v => v.GetStringValue());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container-fluid"">
    <div class=""login-page"">
        <a type=""button"" class=""bttn btn-ripple login-btn px-lg-4 m-1"" style=""width: 15%"" href=""javascript:void(0);"" id=""backLink"">
            <i class=""fas fa-arrow-circle-left""></i>
            Go Back
        </a>
        <h2 style=""font-family: Roboto"">");
#nullable restore
#line 18 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                   Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 18 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                                        Write(storeName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n        <div class=\"form\" method=\"post\">\r\n");
#nullable restore
#line 20 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
             if (!ViewContext.ModelState.IsValid)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"alert alert-danger m-2\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "edb33a7c8383a9e2e5cedc4ca58eb386936bcce48188", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 23 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 25 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"alert-warning p-3 m-2\">\r\n                <h5>Can only compose 2 purchase policies.</h5>\r\n            </div>\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "edb33a7c8383a9e2e5cedc4ca58eb386936bcce410271", async() => {
                WriteLiteral(@"
                <div class=""form-group row justify-content-between"">
                    <div class=""col-md-6"">
                        <button type=""submit"" id=""visible"" name=""visible"" class=""btn btn-warning m-1"" style=""width: 40%""><b>CREATE POLICY</b></button>
                    </div>
                    <div class=""form-group"">
                        <div class=""col-md-12"">
                            <label style=""text-align: left"" for=""password"">Discount Operation</label>
                            <select class=""form-control"" id=""operator"" name=""operator"">
");
#nullable restore
#line 38 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                 foreach (var op in opTypes.Keys)
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "edb33a7c8383a9e2e5cedc4ca58eb386936bcce411520", async() => {
#nullable restore
#line 40 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                                                         Write(opTypes[op]);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                       WriteLiteral(op);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 41 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                            </select>
                        </div>
                    </div>
                </div>
                <div class=""flex"" style=""display:block"">
                    <table class="" table table-striped"">
                        <thead class=""thead-dark"">
                            <tr class=""text-left"">
                                <th>Choose</th>
                                <th>Policy Description</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 55 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                              var counter = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                             foreach (var discount in Model)
                            {
                                if (!String.IsNullOrEmpty(discount.Text))
                                {
                                    var checkboxName = "selectd" + counter;

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    <tr class=\"p-5\">\r\n                                        <td>\r\n                                            <input class=\"big-checkbox\" type=\"checkbox\"");
                BeginWriteAttribute("id", " id=\"", 3204, "\"", 3222, 1);
#nullable restore
#line 63 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
WriteAttributeValue("", 3209, checkboxName, 3209, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("name", " name=\"", 3223, "\"", 3243, 1);
#nullable restore
#line 63 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
WriteAttributeValue("", 3230, checkboxName, 3230, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("value", " value=\"", 3244, "\"", 3267, 1);
#nullable restore
#line 63 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
WriteAttributeValue("", 3252, discount.Value, 3252, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                                        </td>\r\n                                        <td class=\"p-3\">\r\n                                            <label style=\"white-space: pre-line\">");
#nullable restore
#line 66 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                                                            Write(Html.Raw(discount.Text));

#line default
#line hidden
#nullable disable
                WriteLiteral("</label><br>\r\n                                        </td>\r\n                                    </tr>\r\n");
#nullable restore
#line 69 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
                                    counter++;
                                }
                            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                        </tbody>\r\n                    </table>\r\n                </div>\r\n                <input id=\"storeName\" name=\"storeName\" type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 3855, "\"", 3873, 1);
#nullable restore
#line 75 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Owner\purchase\AddCompositePolicy.cshtml"
WriteAttributeValue("", 3863, storeName, 3863, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<SelectListItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591
