#pragma checksum "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0885360c3e0aa00693f00519ed56f9509bc11156"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Store_StoreProducts), @"mvc.1.0.view", @"/Views/Store/StoreProducts.cshtml")]
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
#line 1 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
using ECommerceSystem.Models.PurchasePolicyModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
using ECommerceSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0885360c3e0aa00693f00519ed56f9509bc11156", @"/Views/Store/StoreProducts.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Store_StoreProducts : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<(Tuple<StoreModel, List<ProductModel>>, string)>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 200px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-lg btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Owner", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddConcreteProduct", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width: 170px; margin: 0"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-danger m-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RemoveProduct", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary m-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ModifyProduct", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddProductDiscount", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info m-1"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
  
    ViewData["Title"] = "Store Inventory";
    var store = Model.Item1.Item1;
    var products = Model.Item1.Item2;
    var invId = Model.Item2.ToString();
    ViewData["StoreName"] = store.Name;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\" style=\"width: 85%\">\r\n    <h1>");
#nullable restore
#line 13 "E:\לימודים\שנה ג'\סמסטר ו'\סדנא\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
   Write(ViewData["StoreName"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" - Store Products</h1>
    <a href=""javascript:void(0);"" id=""backLink"">
        <div class=""bttn btn-ripple login-btn px-lg-4 m-1"" style=""width: 8%"">
            <i class=""fas fa-arrow-circle-left""></i>
            Go Back
        </div>
    </a>
    <div class=""alert alert-info"">
        <i class=""fa fa-info-circle pull-right""></i>  <strong>Click to add product discount or remove product.</strong>
    </div>
    <div class=""row"">
        <div class=""col md-12"">

");
#nullable restore
#line 26 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
             if (products.Count != 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h1>\r\n                    <i class=\"fa fa-shopping-cart\"></i>\r\n                    <small> - Manage store product group under: <b>");
#nullable restore
#line 30 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                              Write(products.First().Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></small>\r\n                </h1>\r\n");
#nullable restore
#line 32 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <h1>\r\n                    <i class=\"fa fa-shopping-cart\"></i>\r\n                    <small> - Manage store product group</small>\r\n                </h1>\r\n");
#nullable restore
#line 39 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"col justify-content-end align-self-center text-right\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0885360c3e0aa00693f00519ed56f9509bc1115610589", async() => {
                WriteLiteral("\r\n                <i class=\"fas fa-plus\"></i><i class=\"fas fa-box-open\"></i>Add Prodcut\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-invId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 42 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                             WriteLiteral(invId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["invId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-invId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["invId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>

    <hr>

    <table class=""table"">
        <thead class=""thead-light"">
            <tr class=""text-left"">
                <th>#ID</th>
                <th>Name</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Active Discount</th>
                <th>Purchase Type</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 63 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
             foreach (var product in products)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr class=\"text-left\">\r\n                <td class=\"w-25\">");
#nullable restore
#line 66 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                            Write(product.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 67 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
               Write(product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 68 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                 if (@product.Quantity == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td style=\"color: red\"><b>");
#nullable restore
#line 70 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                         Write(product.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></td>\r\n");
#nullable restore
#line 71 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>");
#nullable restore
#line 74 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                   Write(product.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 75 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>");
#nullable restore
#line 76 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
               Write(product.BasePrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n");
#nullable restore
#line 78 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                     if (product.Discount != null)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span>");
#nullable restore
#line 80 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                         Write(product.Discount.GetString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 81 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span>None</span>\r\n");
#nullable restore
#line 85 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td style=\"width: 12%\">\r\n");
#nullable restore
#line 88 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                     if (product.PurchasePolicy != null)
                    {
                        var prod = (ProductQuantityPolicyModel)product.PurchasePolicy;

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span><b>Minimum Purchase Quantity:</b> ");
#nullable restore
#line 91 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                           Write(prod.MinQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span><br />\r\n                        <span><b>Maximum Purchase Quantity:</b> ");
#nullable restore
#line 92 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                           Write(prod.MaxQuantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 93 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span>None</span>\r\n");
#nullable restore
#line 97 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td class=\"text-right align-content-end justify-content-end p-1\" style=\"width: 2%\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0885360c3e0aa00693f00519ed56f9509bc1115619831", async() => {
                WriteLiteral("\r\n                        Remove <i class=\"fas fa-archive\"></i>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-store", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 100 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                               WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["store"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-store", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["store"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 100 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                                                                   WriteLiteral(product.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["productName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-productName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["productName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 100 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                                                                                                WriteLiteral(product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0885360c3e0aa00693f00519ed56f9509bc1115624551", async() => {
                WriteLiteral("\r\n                        Modify <i class=\"fas fa-edit\"></i>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 103 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                             WriteLiteral(product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 103 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                                                           WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["store"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-store", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["store"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 103 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                                                                                            WriteLiteral(product.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["prodName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-prodName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["prodName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0885360c3e0aa00693f00519ed56f9509bc1115629242", async() => {
                WriteLiteral("\r\n                        Discount <i class=\"fas fa-percentage\"></i>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-storeName", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 106 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                     WriteLiteral(store.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["storeName"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-storeName", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["storeName"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 106 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
                                                                                                                                                                WriteLiteral(product.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 111 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Store\StoreProducts.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<(Tuple<StoreModel, List<ProductModel>>, string)> Html { get; private set; }
    }
}
#pragma warning restore 1591
