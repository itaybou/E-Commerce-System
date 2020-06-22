#pragma checksum "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bb2de827c720349a7c232e92a51b4a8be62530a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product__ChooseProductModal), @"mvc.1.0.view", @"/Views/Product/_ChooseProductModal.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bb2de827c720349a7c232e92a51b4a8be62530a4", @"/Views/Product/_ChooseProductModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3158d4994470ee60ea44b33758d4b24b68170fe4", @"/Views/_ViewImports.cshtml")]
    public class Views_Product__ChooseProductModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PresentationLayer.Models.Products.ChooseProductModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddProductToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/site.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
  
    ViewData["Title"] = "Choose Product";
    var products = Model.Products;
    var details = products != null && products.Count() > 0 ? products.First() : null;
    var store = Model.Store;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $(document).on('click', ""#products li"", function (e) {
                var index = $(""#products li"").css('background-color', '').index(this);
                $(this).css('background-color', '#D9EDF7');
                $('input[name=""product_select""]').eq(index).prop('checked', true);
                $('.product_check').not(index).hide();
                $('.product_check').eq(index).show();
                $('#is_selected').hide();
            });
        });
    </script>
");
            }
            );
            WriteLiteral(@"
<div class=""container justify-content-center align-items-center p-5 m-5 w-100"">
    <div class=""modal fade"" id=""productModal"" tabindex=""-1"" role=""dialog"">
        <div class=""modal-dialog modal-lg"">
            <div class=""modal-content"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb2de827c720349a7c232e92a51b4a8be62530a47584", async() => {
                WriteLiteral("\r\n                    <div class=\"modal-header alert-success\">\r\n                        <h5 class=\"modal-title\" id=\"exampleModalLabel\"><i class=\"fas fa-shopping-cart\"></i> ");
#nullable restore
#line 31 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                                                                                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h5>\r\n                        <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\"");
                BeginWriteAttribute("onclick", " onclick=\"", 1476, "\"", 1521, 3);
                WriteAttributeValue("", 1486, "location.href=\'", 1486, 15, true);
#nullable restore
#line 32 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
WriteAttributeValue("", 1501, Model.RedirectPath, 1501, 19, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 1520, "\'", 1520, 1, true);
                EndWriteAttribute();
                WriteLiteral(@">
                            <span aria-hidden=""true"">&times;</span>
                        </button>
                    </div>
                    <div class=""modal-body"">
                        <span class=""m-0 p-0""><small>Click on specific product details to select.</small></span>
");
#nullable restore
#line 38 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                         if (!ViewContext.ModelState.IsValid)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <div class=\"alert alert-danger\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb2de827c720349a7c232e92a51b4a8be62530a49817", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 41 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
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
                WriteLiteral("\r\n                            </div>\r\n");
#nullable restore
#line 43 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <div class=\"row alert-warning justify-content-between p-3\">\r\n                            <div>\r\n                                <b>PRODUCT NAME: </b>");
#nullable restore
#line 46 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                                Write(details.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </div>\r\n                            <div>\r\n                                <b>PRODUCT BASE PRICE: </b>");
#nullable restore
#line 49 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                                      Write(details.BasePrice);

#line default
#line hidden
#nullable disable
                WriteLiteral(" $\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 52 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                           var prodDisplayCounter = 0;

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <ul class=\"list-group secondNav\" id=\"products\" style=\"overflow: scroll; max-height: 400px; height: 400px; margin-bottom: 10px; overflow-x: hidden;\">\r\n");
#nullable restore
#line 54 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                             foreach (var product in products.Where(p => p.Quantity > 0))
                            {
                                prodDisplayCounter++;

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                <li class=""list-group-item justify-content-between nav-item"" style=""display: table-cell"">
                                    <div class=""row justify-content-between"">
                                        <div>
                                            <input type=""radio"" class=""big-radio"" style=""display: none"" name=""product_select"" id=""product_select""");
                BeginWriteAttribute("value", " value=\"", 3336, "\"", 3355, 1);
#nullable restore
#line 60 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
WriteAttributeValue("", 3344, product.Id, 3344, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                            <b>Available Quantity:</b> ");
#nullable restore
#line 61 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                                                  Write(product.Quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                            <br />\r\n                                            <b>Discount: </b>\r\n");
#nullable restore
#line 64 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                             if (product.Discount != null)
                                            {
                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                           Write(product.Discount.GetString());

#line default
#line hidden
#nullable disable
#nullable restore
#line 66 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                                                             
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                <span>None</span>\r\n");
#nullable restore
#line 71 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                                            <br />\r\n                                            <b>Purchase Policy: </b>\r\n");
#nullable restore
#line 74 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                             if (product.PurchasePolicy != null)
                                            {
                                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                           Write(product.PurchasePolicy.GetString());

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                                                                   
                                            }
                                            else
                                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                                <span>None</span>\r\n");
#nullable restore
#line 81 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                                            }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                        </div>
                                        <div class=""product_check m-2 text-center"" style=""display: none"">
                                            <small>Selected</small>
                                            <h3><i class=""fas fa-check"" style=""color: #31708F""></i></h3>
                                        </div>
                                    </div>
                                </li>
");
#nullable restore
#line 89 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                        </ul>\r\n");
#nullable restore
#line 91 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                         if (prodDisplayCounter == 0)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <div class=\"p-2\">\r\n                                <h4 class=\"text-danger\">No available products currently.</h4>\r\n                            </div>\r\n");
#nullable restore
#line 96 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral("                    </div>\r\n                    <input id=\"store_name\" name=\"store_name\" type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 5532, "\"", 5546, 1);
#nullable restore
#line 98 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
WriteAttributeValue("", 5540, store, 5540, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <input id=\"listing\" name=\"listing\" type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 5620, "\"", 5642, 1);
#nullable restore
#line 99 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
WriteAttributeValue("", 5628, Model.Listing, 5628, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <input id=\"invID\" name=\"invID\" type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 5712, "\"", 5745, 1);
#nullable restore
#line 100 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
WriteAttributeValue("", 5720, Model.ProductInventoryID, 5720, 25, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <div class=\"modal-footer justify-content-between\">\r\n                        <div class=\"col\">\r\n                            <input type=\"button\" class=\"btn alert-danger\"");
                BeginWriteAttribute("onclick", " onclick=\"", 5939, "\"", 5984, 3);
                WriteAttributeValue("", 5949, "location.href=\'", 5949, 15, true);
#nullable restore
#line 103 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
WriteAttributeValue("", 5964, Model.RedirectPath, 5964, 19, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 5983, "\'", 5983, 1, true);
                EndWriteAttribute();
                WriteLiteral(@" value=""Close"" />
                        </div>
                        <div class=""col"">
                            <div class=""form-group"">
                                <label style=""text-align: left"" for=""password"">Add Quantity<span id=""is_selected"" class=""text-danger"">- None Selected</span></label>
                                <input type=""number"" class=""form-control"" placeholder=""Quantity"" id=""quantity"" name=""quantity"" />
                            </div>
                            <button type=""submit"" class=""btn btn-success btn-block"">
                                <i class=""fas fa-cart-plus""></i> <!--<i class=""fas fa-cart-plus""></i>--> ADD TO CART
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb2de827c720349a7c232e92a51b4a8be62530a424538", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb2de827c720349a7c232e92a51b4a8be62530a425578", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 122 "C:\Users\Itay Bouganim\source\repos\E-Commerce-System\code\src\PresentationLayer\Views\Product\_ChooseProductModal.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bb2de827c720349a7c232e92a51b4a8be62530a427504", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<script>\r\n    $(document).ready(function () {\r\n        $(\"#productModal\").modal({ backdrop: \'static\', keyboard: false });\r\n    });\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PresentationLayer.Models.Products.ChooseProductModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
