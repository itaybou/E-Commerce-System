#pragma checksum "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2c7d7ffcac742ba6a88a5df2a9ec6c6a878df53b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Index), @"mvc.1.0.view", @"/Views/Product/Index.cshtml")]
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
#line 1 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2c7d7ffcac742ba6a88a5df2a9ec6c6a878df53b", @"/Views/Product/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ECommerceSystem.Models.ProductInventoryModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("hidden"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RateProduct", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Product", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-img-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/unavailable.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Unavailable Product image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("500"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("350"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddProductToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
  
    ViewData["Title"] = "Product";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
    $(function () {
        $('[data-toggle=""popover""]').popover({
            container: 'body',
            html: true,
            placement: 'bottom',
            sanitize: false,
            content: function () {
                return $(""#PopoverContent"").html();
            }
        });
    });
    </script>
");
            }
            );
            WriteLiteral("\r\n<section style=\"display: none\">\r\n    <div id=\"PopoverContent\" class=\"hidden\">\r\n        <label class=\"alert-warning\">Rating will be rounded in range 0 - 5</label>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c7d7ffcac742ba6a88a5df2a9ec6c6a878df53b8263", async() => {
                WriteLiteral(@"
            <div class=""input-group"">
                <input id=""rating"" name=""rating"" type=""number"" class=""form-control"" placeholder=""0-5""
                       aria-label=""0-5"" aria-describedby=""button-addon1"">
                <div class=""input-group-append"" id=""button-addon1"">
                    <button type=""submit"" class=""btn btn-outline-primary"" data-toggle=""popover"" data-placement=""bottom""
                            data-html=""true"" data-title=""Search"">
                        <i class=""far fa-thumbs-up""></i>
                    </button>
                </div>
            </div>
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
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-prodID", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 27 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                                                                    WriteLiteral(Model.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["prodID"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-prodID", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["prodID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</section>\r\n\r\n    <div class=\"container\">\r\n        <h1>");
#nullable restore
#line 43 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
       Write(ViewData["Name"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
        <a href=""javascript:void(0);"" id=""backLink"">
            <div class=""bttn btn-ripple login-btn px-lg-4 m-1"" style=""width: 12%"">
                <i class=""fas fa-arrow-circle-left""></i>
                Go Back
            </div>
        </a>
        <div class=""product-card"">
            <div class=""container-fliud"">
                <div class=""wrapper row"">
                    <div class=""preview col-md-6"">

                        <div class=""preview-pic tab-content"">
                            <div class=""tab-pane active"" id=""pic-1"">
");
#nullable restore
#line 57 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                 if (!String.IsNullOrWhiteSpace(Model.ImageURL))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <img class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 2245, "\"", 2266, 1);
#nullable restore
#line 59 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
WriteAttributeValue("", 2251, Model.ImageURL, 2251, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Product image\" width=\"500\" height=\"350\"> <!--user image-->\r\n");
#nullable restore
#line 60 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "2c7d7ffcac742ba6a88a5df2a9ec6c6a878df53b14041", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" <!--default image-->\r\n");
#nullable restore
#line 64 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                            </div>
                        </div>
                    </div>
                    <div class=""details col-md-6"">
                        <div class=""row flex justify-content-between p-2"">
                            <div>
                                <h2 class=""product-title"">");
#nullable restore
#line 71 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                                     Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                            </div>\r\n                            <div>\r\n                                <h5><b>Category</b>: ");
#nullable restore
#line 74 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                                Write(Enum.GetName(typeof(Category), Model.Category));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"alert-warning\">\r\n                            <h5><b>Store: </b> ");
#nullable restore
#line 78 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                          Write(Model.StoreName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        </div>\r\n                            <div class=\"row flex justify-content-between p-2\">\r\n                                <div>\r\n                                    <h5><i class=\"fas fa-star checked\"></i><strong> ");
#nullable restore
#line 82 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                                                               Write(Math.Round(Model.Rating, 2));

#line default
#line hidden
#nullable disable
            WriteLiteral(" Rating</strong></h5>\r\n                                </div>\r\n                                <div>\r\n                                    <h5><i class=\"far fa-thumbs-up\"></i><strong> ");
#nullable restore
#line 85 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                                                            Write(Model.RaterCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" Raters</strong></h5>
                                    <section>
                                        <div id=""PopoverContent"" class=""d-none"">
                                            <div class=""input-group"">
                                                <input type=""text"" class=""form-control"" placeholder=""Recipient's username""
                                                       aria-label=""Recipient's username with two button addons"" aria-describedby=""button-addon1"">
                                                <div class=""input-group-append"" id=""button-addon1"">
                                                    <button class=""btn btn-outline-primary"" type=""button"" data-toggle=""popover"" data-placement=""bottom""
                                                            data-html=""true"" data-title=""Search"">
                                                        <i class=""fas fa-search""></i>
                                                    </button>
                           ");
            WriteLiteral(@"                     </div>
                                            </div>
                                        </div>
                                    </section>
                                    <a href=""#"" class=""btn btn-info w-100"" data-html='true' data-toggle='popover' data-title=""Give your rating"">
                                        <i class=""far fa-thumbs-up""></i> Rate
                                    </a>
                                </div>
                            </div>
                            <p class=""product-description"">
                                <h5 class=""text-secondary""><b>Product description:</b></h5>
");
#nullable restore
#line 107 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                 if (String.IsNullOrEmpty(Model.Description))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <h5 class=\"text-muted\">Not provided</h5>\r\n");
#nullable restore
#line 110 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <h5> ");
#nullable restore
#line 113 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                    Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
#nullable restore
#line 114 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </p>\r\n                            <div class=\"alert-info p-1\">\r\n                                <b>Keywords:</b>\r\n");
#nullable restore
#line 118 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                 foreach (var keyword in Model.Keywords)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span>");
#nullable restore
#line 120 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                      Write(keyword + " | ");

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 121 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </div>\r\n                            <h4 class=\"price p-1\">\r\n                                current price:\r\n                                <span>");
#nullable restore
#line 125 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                 Write(Model.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" $</span>\r\n                            </h4>\r\n                            <div class=\"action\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2c7d7ffcac742ba6a88a5df2a9ec6c6a878df53b23057", async() => {
                WriteLiteral(@"
                                    <button type=""submit"" style=""width: 200px"" class=""add-to-cart btn btn-default"">
                                        <i class=""fas fa-cart-plus""></i> add to cart
                                    </button>
                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-prodID", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 128 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Product\Index.cshtml"
                                                                                                              WriteLiteral(Model.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["prodID"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-prodID", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["prodID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n            </div>\r\n        </div>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ECommerceSystem.Models.ProductInventoryModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
