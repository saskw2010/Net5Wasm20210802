#pragma checksum "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c488835f9f4b18baac318331a354ce99b98f797a"
// <auto-generated/>
#pragma warning disable 1591
namespace Net5Wasm.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Net5Wasm.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\admin\source\repos\Net5Wasm20210729\client\_Imports.razor"
using Net5Wasm.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
using Net5Wasm.Models.Net5Wasmconn;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
using Net5Wasm.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/add-application-role")]
    public partial class AddApplicationRole : Net5Wasm.Pages.AddApplicationRoleComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Radzen.Blazor.RadzenContent>(0);
            __builder.AddAttribute(1, "Container", "main");
            __builder.AddAttribute(2, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenElement(3, "div");
                __builder2.AddAttribute(4, "class", "row");
                __builder2.OpenElement(5, "div");
                __builder2.AddAttribute(6, "class", "col-md-12");
                __builder2.OpenComponent<Radzen.Blazor.RadzenTemplateForm<IdentityRole>>(7);
                __builder2.AddAttribute(8, "Data", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<IdentityRole>(
#nullable restore
#line 19 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                        role

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(9, "Visible", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 19 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                         role != null

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(10, "Submit", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<IdentityRole>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<IdentityRole>(this, 
#nullable restore
#line 19 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                                                 Form0Submit

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(11, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Forms.EditContext>)((context) => (__builder3) => {
                    __builder3.OpenElement(12, "div");
                    __builder3.AddAttribute(13, "style", "margin-bottom: 1rem");
                    __builder3.AddAttribute(14, "class", "row");
                    __builder3.OpenElement(15, "div");
                    __builder3.AddAttribute(16, "class", "col-md-3");
                    __builder3.OpenComponent<Radzen.Blazor.RadzenLabel>(17);
                    __builder3.AddAttribute(18, "Component", "Name");
                    __builder3.AddAttribute(19, "style", "width: 100%");
                    __builder3.AddAttribute(20, "Text", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 23 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                         L["label1.Text"]

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.CloseComponent();
                    __builder3.CloseElement();
                    __builder3.AddMarkupContent(21, "\r\n              ");
                    __builder3.OpenElement(22, "div");
                    __builder3.AddAttribute(23, "class", "col-md-9");
                    __builder3.OpenComponent<Radzen.Blazor.RadzenTextBox>(24);
                    __builder3.AddAttribute(25, "style", "display: block; width: 100%");
                    __builder3.AddAttribute(26, "Name", "Name");
                    __builder3.AddAttribute(27, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 27 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                                  role.Name

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(28, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => role.Name = __value, role.Name))));
                    __builder3.AddAttribute(29, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<System.String>>>(() => role.Name));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(30, "\r\n                ");
                    __builder3.OpenComponent<Radzen.Blazor.RadzenRequiredValidator>(31);
                    __builder3.AddAttribute(32, "Component", "Name");
                    __builder3.AddAttribute(33, "style", "position: absolute");
                    __builder3.AddAttribute(34, "Text", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 29 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                                            L["NameRequiredValidator.Text"]

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.CloseComponent();
                    __builder3.CloseElement();
                    __builder3.CloseElement();
                    __builder3.AddMarkupContent(35, "\r\n            ");
                    __builder3.OpenElement(36, "div");
                    __builder3.AddAttribute(37, "class", "row");
                    __builder3.OpenElement(38, "div");
                    __builder3.AddAttribute(39, "class", "col offset-sm-3");
                    __builder3.OpenComponent<Radzen.Blazor.RadzenButton>(40);
                    __builder3.AddAttribute(41, "ButtonStyle", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Radzen.ButtonStyle>(
#nullable restore
#line 35 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                           ButtonStyle.Primary

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(42, "ButtonType", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Radzen.ButtonType>(
#nullable restore
#line 35 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                            ButtonType.Submit

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(43, "Icon", "save");
                    __builder3.AddAttribute(44, "Text", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 35 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                                                                  L["button1.Text"]

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.CloseComponent();
                    __builder3.AddMarkupContent(45, "\r\n                ");
                    __builder3.OpenComponent<Radzen.Blazor.RadzenButton>(46);
                    __builder3.AddAttribute(47, "ButtonStyle", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Radzen.ButtonStyle>(
#nullable restore
#line 37 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                           ButtonStyle.Light

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(48, "Text", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 37 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                     L["button2.Text"]

#line default
#line hidden
#nullable disable
                    ));
                    __builder3.AddAttribute(49, "Click", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Microsoft.AspNetCore.Components.Web.MouseEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 37 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\AddApplicationRole.razor"
                                                                                                Button2Click

#line default
#line hidden
#nullable disable
                    )));
                    __builder3.CloseComponent();
                    __builder3.CloseElement();
                    __builder3.CloseElement();
                }
                ));
                __builder2.CloseComponent();
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.Extensions.Localization.IStringLocalizer<AddApplicationRole> L { get; set; }
    }
}
#pragma warning restore 1591
