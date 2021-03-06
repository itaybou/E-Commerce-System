#pragma checksum "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "991c5965b7f57752f5cce14c91a2013434582128"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Store_StorePurchaseHistory), @"mvc.1.0.view", @"/Views/Store/StorePurchaseHistory.cshtml")]
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
#line 1 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
using ECommerceSystem.Models.DiscountPolicyModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
using ECommerceSystem.Models.PurchasePolicyModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"991c5965b7f57752f5cce14c91a2013434582128", @"/Views/Store/StorePurchaseHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Store_StorePurchaseHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<(IEnumerable<StorePurchaseModel>, string)>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-img-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/unavailable.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Unavailable Product image"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("50"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("50"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
  
    ViewData["Title"] = "Store Purchase History";
    var store = Model.Item2;
    var purchases = Model.Item1;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid w-75\">\r\n    <h1 style=\"font-family: Roboto\">");
#nullable restore
#line 12 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                               Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 12 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                    Write(store);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>
    <div class=""row"">
        <div class=""col-12"">
            <div class=""table-responsive"">
                <table class=""table"">
                    <thead>
                        <tr>
                            <th scope=""col"">Purchase Date</th>
                            <th scope=""col"" class=""text-left"">Purchasing Username</th>
                            <th scope=""col"">Prodcuts Purchased</th>
                            <th scope=""col"" class=""text-center"">Total Price</th>
                        </tr>
                    </thead>
                    <tbody>
");
#nullable restore
#line 26 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                         foreach (var purchase in purchases)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td class=\"text-xl-left\">\r\n                                    <h6><b>");
#nullable restore
#line 30 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                      Write(purchase.PurchaseDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h6>\r\n                                </td>\r\n                                <td>\r\n                                    <b>");
#nullable restore
#line 33 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                  Write(purchase.Username);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b>
                                </td>
                                <td class=""w-50"">
                                    <table class=""table table-striped"">
                                        <thead>
                                            <tr>
                                                <th scope=""col""></th>
                                                <th scope=""col"" style=""text-align: center"">Product Name</th>
                                                <th scope=""col"" class=""text-center"">Quantity</th>
                                                <th scope=""col"" class=""text-center"">Price</th>
                                                <th scope=""col"" class=""text-center"">Discount</th>
                                            </tr>
                                        </thead>
");
#nullable restore
#line 46 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                         foreach (var product in purchase.ProductsPurchased)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr class=\"text-center\">\r\n                                                <td style=\"width: 12%\">\r\n");
#nullable restore
#line 50 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                     if (!String.IsNullOrWhiteSpace(product.ImageURL))
                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        <img class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 2759, "\"", 2782, 1);
#nullable restore
#line 52 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
WriteAttributeValue("", 2765, product.ImageURL, 2765, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Product image\" width=\"50\" height=\"50\"> <!--user image-->\r\n");
#nullable restore
#line 53 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                    }
                                                    else
                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "991c5965b7f57752f5cce14c91a201343458212811396", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
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
            WriteLiteral(" <!--default image-->\r\n");
#nullable restore
#line 57 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                </td>\r\n                                                <td style=\"text-align: center\">\r\n                                                    ");
#nullable restore
#line 60 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                               Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 63 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                               Write(product.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td>\r\n                                                    ");
#nullable restore
#line 66 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                               Write(product.BasePrice);

#line default
#line hidden
#nullable disable
            WriteLiteral(" $\r\n                                                </td>\r\n                                                <td>\r\n");
#nullable restore
#line 69 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                     if (product.Discount != null)
                                                    {
                                                        var discount = product.Discount;
                                                        if (discount is VisibleDiscountModel visible)
                                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            <span>");
#nullable restore
#line 74 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                             Write(visible.Percentage);

#line default
#line hidden
#nullable disable
            WriteLiteral("%</span>\r\n");
#nullable restore
#line 75 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                        }
                                                        else if (discount is ConditionalProductDiscountModel cond)
                                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            <span>");
#nullable restore
#line 78 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                             Write(cond.Percentage);

#line default
#line hidden
#nullable disable
            WriteLiteral("% </span>\r\n                                                            <br />\r\n                                                            <span>Above ");
#nullable restore
#line 80 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                                   Write(cond.RequiredQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Units</span>\r\n");
#nullable restore
#line 81 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                        }
                                                    }
                                                    else
                                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        <span>None</span>\r\n");
#nullable restore
#line 86 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                </td>\r\n                                            </tr>\r\n");
#nullable restore
#line 89 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    </table>\r\n                                </td>\r\n                                <td class=\"text-center alert-warning\">\r\n                                    <h5><strong>");
#nullable restore
#line 93 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                                           Write(purchase.TotalPrice.ToString("0.000"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" $</strong></h5>\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 96 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StorePurchaseHistory.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </tbody>\r\n                </table>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<(IEnumerable<StorePurchaseModel>, string)> Html { get; private set; }
    }
}
#pragma warning restore 1591
