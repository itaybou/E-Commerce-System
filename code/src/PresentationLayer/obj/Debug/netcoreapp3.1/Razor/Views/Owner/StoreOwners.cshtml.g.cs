#pragma checksum "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b7bede0eccb853811d250f2f4c9e087a17cfa8d2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Owner_StoreOwners), @"mvc.1.0.view", @"/Views/Owner/StoreOwners.cshtml")]
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
#line 1 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b7bede0eccb853811d250f2f4c9e087a17cfa8d2", @"/Views/Owner/StoreOwners.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Owner_StoreOwners : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<(IEnumerable<(UserModel, PermissionModel)>, string)>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/anonymous.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("42"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("42"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Owner", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AssignOwner", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("login-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
  
    var users = Model.Item1;
    var storeName = Model.Item2;
    ViewData["Title"] = "Store Owners - " + storeName;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        var store = document.getElementById('store').value;
        $('.assign-search').select2({
            placeholder: ""Search users to assign"",
            allowClear: true,
            minimumInputLength: 2,
            maximumInputLength: 18,
            templateResult: formatUserResults,
            templateSelection: formatUserSelection,
            escapeMarkup: function (markup) { return markup; },
            ajax: {
                cache: false,
                //type: ""GET"",
                delay: 250,
                url: '/Owner/AssignSearch',
                dataType: 'json',
                type: 'GET',
                data: function (params) {
                    return {
                        query: params.term, //search term
                        storeName: store,
                        page: params.page
                    };
                },
                processResults: function (data, params) {
                    params.page = params.p");
                WriteLiteral(@"age || 1;
                    var newData = [];
                    $.each(data, function (index, item) {
                        newData.push({
                            id: item.id,
                            username: item.username
                        });
                    });
                    return { results: newData };
                },
            }
        });

        function formatUserResults(user) {
            var markup = ""<table class='table-striped'><tr>"";
            if (user.username !== undefined) {
                markup += ""<div><b>"" + user.username + ""</b></div>"";
            }
            markup += ""</td></tr></table>""
            return markup;
        }

        function formatUserSelection(user) {
            return user.username;
        }

        $('#assign-search').on('select2:select', function (e) {
            var username = e.params.data.username;
            $('#assignUsername').val(username);
        });
    </script>
");
            }
            );
            WriteLiteral("\r\n<div class=\"container-fluid w-75\">\r\n    <div class=\"row-2\">\r\n        <div class=\"col\">\r\n            <h1><i class=\"fas fa-crown\"></i> ");
#nullable restore
#line 72 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                        Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
        </div>
        <div class=""col justify-content-center align-self-center text-right"" style=""margin-bottom: 5px"">
            <button style=""width: 250px"" class=""btn btn-lg btn-success"" data-toggle=""modal"" data-target=""#add-modal"">
                <i class=""fas fa-plus""></i><i class=""fas fa-crown""></i> Assign Owner
            </button>
        </div>
");
#nullable restore
#line 79 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
         if (!ViewContext.ModelState.IsValid)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col alert alert-danger\" style=\"margin-top: 5px; margin-bottom: 5px;\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b7bede0eccb853811d250f2f4c9e087a17cfa8d210660", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 82 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
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
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 84 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
    <div class=""row"">
        <div class=""col-lg-12"">
            <div class=""main-box no-header clearfix"">
                <div class=""main-box-body clearfix"">
                    <div class=""table-responsive"">
                        <table class=""table table-striped user-list"">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th><span>Username</span></th>
                                    <th><span>Firstname</span></th>
                                    <th><span>Lastname</span></th>
                                    <th><span>Email</span></th>
                                    <th><span>Assigned By</span></th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 103 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                 foreach (var userPerm in users)
                                {
                                    var user = userPerm.Item1;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "b7bede0eccb853811d250f2f4c9e087a17cfa8d213955", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 111 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                       Write(user.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>");
#nullable restore
#line 113 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                       Write(user.Fname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 115 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                       Write(user.Lname);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 118 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                       Write(user.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>\r\n                                            ");
#nullable restore
#line 121 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                       Write(userPerm.Item2.AssignedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                        </td>
                                        <!--<td style=""width: 10%;"">
                                            <a href=""#"" class=""table-link"">
                                                <span class=""fa-stack"">
                                                    <i class=""fa fa-square fa-stack-2x""></i>
                                                    <i class=""fa fa-search-plus fa-stack-1x fa-inverse""></i>
                                                </span>
                                            </a>
                                            <a href=""#"" class=""table-link danger"">
                                                <span class=""fa-stack"">
                                                    <i class=""fa fa-square fa-stack-2x text-danger""></i>
                                                    <i class=""fa fa-trash fa-stack-1x fa-inverse""></i>
                                                </span>
                    ");
            WriteLiteral("                        </a>\r\n                                        </td> -->\r\n                                    </tr>\r\n");
#nullable restore
#line 138 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class=""modal fade"" id=""add-modal"" role=""dialog"" aria-hidden=""true"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"" style=""height: 300px"">
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b7bede0eccb853811d250f2f4c9e087a17cfa8d219139", async() => {
                WriteLiteral("\r\n                <div class=\"modal-header\">\r\n                    <h5 class=\"modal-title\">");
#nullable restore
#line 153 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                        Write("Assign owner");

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    <div class=""w-100"">
                        <input id=""assignUsername"" name=""assignUsername"" type=""hidden""");
                BeginWriteAttribute("value", " value=\"", 7163, "\"", 7176, 1);
#nullable restore
#line 160 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
WriteAttributeValue("", 7171, null, 7171, 5, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        <select id=""assign-search"" class=""w-100 assign-search"" style=""display: flex; width: 100%""></select>
                    </div>
                </div>
                <div class=""modal-footer"">
                    <input id=""store"" name=""store"" type=""hidden""");
                BeginWriteAttribute("value", " value=\"", 7467, "\"", 7485, 1);
#nullable restore
#line 165 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
WriteAttributeValue("", 7475, storeName, 7475, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Close</button>\r\n                    <button type=\"submit\" class=\"btn btn-primary\">Assign</button>\r\n                </div>\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 151 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Owner\StoreOwners.cshtml"
                                                                           WriteLiteral(storeName);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_8.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<(IEnumerable<(UserModel, PermissionModel)>, string)> Html { get; private set; }
    }
}
#pragma warning restore 1591
