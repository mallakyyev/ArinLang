#pragma checksum "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e9e5a6aa4fca9789a6be52e3468a8ee94939266f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Email_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Email/Index.cshtml")]
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
#line 2 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\_ViewImports.cshtml"
using ARINLAB;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\_ViewImports.cshtml"
using System.Globalization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9e5a6aa4fca9789a6be52e3468a8ee94939266f", @"/Areas/Admin/Views/Email/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5944270c8b5d9816cf52d026e4a62ac0184f904f", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Email_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DAL.Models.Dto.EmailsModelDTO.EmailsDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SendEmail", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n    <div class=\"container-fluid\">\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-12\">\r\n                <h1 class=\"page-header\">\r\n                    ");
#nullable restore
#line 11 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
               Write(SharedLocalizer["Send email"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </h1>\r\n            </div>\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n            <div id=\"gridContainer\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9e5a6aa4fca9789a6be52e3468a8ee94939266f5987", async() => {
                WriteLiteral("\r\n                    <div class=\"col-md-12\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9e5a6aa4fca9789a6be52e3468a8ee94939266f6318", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 20 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
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
                WriteLiteral("\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e9e5a6aa4fca9789a6be52e3468a8ee94939266f7992", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 21 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n                        <div class=\"form-row\">\r\n                            <div class=\"col-md-12\">\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e9e5a6aa4fca9789a6be52e3468a8ee94939266f9823", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 25 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n                                <div class=\"form-group\">\r\n                                    <label>");
#nullable restore
#line 28 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                      Write(SharedLocalizer["Email header"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </label>\r\n                                    <div id=\"header\"></div>\r\n                                </div>\r\n\r\n                                <div class=\"form-group\">\r\n                                    <label>");
#nullable restore
#line 33 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                      Write(SharedLocalizer["Subject"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </label>\r\n                                    <div id=\"ind-name\"></div>\r\n\r\n                                </div>\r\n\r\n                                <div class=\"form-group\">\r\n                                    <label>");
#nullable restore
#line 39 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                      Write(SharedLocalizer["Message"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" *</label>\r\n");
                WriteLiteral(@"                                    <div id=""description-editor""></div>
                                </div>
                                <br />


                                <script>
                                    $(function () {
                                         $(""#adminEmail"").dxTextBox({
                                            name: ""AdminEmail"",

                                        }).dxValidator({
                                            validationRules: [{
                                                type: ""required"",
                                                message: '");
#nullable restore
#line 54 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                                     Write(SharedLocalizer["Admin email is required"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'\r\n                                            },\r\n                                                {\r\n                                                    type: \"email\",\r\n                                                    message:\'");
#nullable restore
#line 58 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                                        Write(SharedLocalizer["Admin email is invalid"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'

                                                }]
                                        });

                                         $(""#password"").dxTextBox({
                                            name: ""Password"",
                                        }).dxValidator({
                                            validationRules: [{
                                                type: ""required"",
                                                message: '");
#nullable restore
#line 68 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                                     Write(SharedLocalizer["Enter password"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'
                                            }]
                                        });

                                         $(""#header"").dxTextBox({
                                            name: ""Header"",
                                        }).dxValidator({
                                            validationRules: [{
                                                type: ""required"",
                                                message: '");
#nullable restore
#line 77 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                                     Write(SharedLocalizer["Enter email header"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'
                                            }]
                                        });

                                        $(""#ind-name"").dxTextBox({
                                            name: ""Subject"",

                                        }).dxValidator({
                                            validationRules: [{
                                                type: ""required"",
                                                message: '");
#nullable restore
#line 87 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                                     Write(SharedLocalizer["Enter subject"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'
                                            }]
                                        });


                                        $(""#description-editor"").dxHtmlEditor({
                                            height: 250,
                                            toolbar: {
                                                items: [
                                                    ""undo"", ""redo"", ""separator"",
                                                    {
                                                        formatName: ""header"",
                                                        formatValues: [false, 1, 2, 3, 4, 5]
                                                    },
                                                                                                                ""bold"", ""italic"", ""strike"", ""underline"", ""separator"",
                                                             ""alignLeft"", ""alignCenter"", ""alignRight"", ""alignJustify"", ""separator"",
     ");
                WriteLiteral(@"                                                         ""orderedList"", ""bulletList"", ""separator"",
                                                              ""color"", ""background"", ""separator"",
                                                               ""link"", ""image"", ""separator"",
                                                               ""clear"", ""codeBlock"", ""blockquote"",
                                                    {
                                                        widget: ""dxButton"",
                                                        options: {
                                                            text: ""Show markup"",
                                                            stylingMode: ""text"",
                                                            onClick: function () {
                                                                popupInstance.show();
                                                            }
                                 ");
                WriteLiteral(@"                       }
                                                    }
                                                ]
                                            },
                                            name: ""Message"",

                                        }).dxValidator({
                                            validationRules: [{
                                                type: ""required"",
                                                message: '");
#nullable restore
#line 124 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                                     Write(SharedLocalizer["Enter message"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\'\r\n                                            }]\r\n                                        }).dxHtmlEditor(\"instance\");\r\n                                    });\r\n                                </script>\r\n                                <label>");
#nullable restore
#line 129 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                                  Write(SharedLocalizer["Send to"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@": </label>
                                <div class=""form-group"">
                                    <div id=""sendToOrg""></div>
                                </div>
                                <div class=""form-group"">
                                    <div id=""sendToEnt""></div>
                                </div>
                                <div class=""form-group"">
                                    <div id=""sendToS""></div>
                                </div>
                            </div>

                        </div> <br />

                    </div>
                    <div class=""dx-fieldset"" >
                       
                        <br />

                        <div id=""summary""></div>
                        <br />
                        <div id=""button"" ></div>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n        <script>\r\n           \r\n            $(function () {        \r\n                $(\"#sendToOrg\").dxCheckBox({\r\n                value: true,\r\n                text: \'");
#nullable restore
#line 162 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                  Write(SharedLocalizer["Ordinary Users"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n                name: \'SendedToOrganization\',\r\n                });\r\n                $(\"#sendToEnt\").dxCheckBox({\r\n                value: true,\r\n                text: \'");
#nullable restore
#line 167 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                  Write(SharedLocalizer["Approved Users"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n                name: \'SendedToEntrepreneur\',\r\n                });\r\n                  $(\"#sendToS\").dxCheckBox({\r\n                value: true,\r\n                text: \'");
#nullable restore
#line 172 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                  Write(SharedLocalizer["Subsrcibers"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n                name: \'SendedToSubscribers\',\r\n            });\r\n             $(\"#button\").dxButton({\r\n                text: \'");
#nullable restore
#line 176 "D:\Work\ARINLAB\ARINLAB\ARINLAB\Areas\Admin\Views\Email\Index.cshtml"
                  Write(SharedLocalizer["Send"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\',\r\n                type: \"success\",\r\n                useSubmitBehavior: true,\r\n            });\r\n            $(\"#summary\").dxValidationSummary({});\r\n    });\r\n        </script>\r\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHtmlLocalizer<SharedResource> SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DAL.Models.Dto.EmailsModelDTO.EmailsDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591