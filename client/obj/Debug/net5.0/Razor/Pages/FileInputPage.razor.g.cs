#pragma checksum "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "56aa5e104686c8eb57eec9512a22cdbfae224a41"
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
#line 5 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Net5Wasm.Api;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Net5Wasm.Models.Net5Wasmconn;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Net5Wasm.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
           [Authorize]

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.LayoutAttribute(typeof(MainLayout))]
    [Microsoft.AspNetCore.Components.RouteAttribute("/fileinput")]
    public partial class FileInputPage : Net5Wasm.Pages.EditCategoryComponent
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
                __builder2.AddAttribute(6, "class", "col-xl-6");
                __builder2.AddMarkupContent(7, "<h3>FileInput</h3>\r\n                ");
                __builder2.OpenElement(8, "div");
                __builder2.AddAttribute(9, "style", "margin: 2rem 0");
                __builder2.AddContent(10, "Employee: ");
                __builder2.OpenElement(11, "b");
                __builder2.AddContent(12, 
#nullable restore
#line 21 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
                                                           category.Name + " " + category.Id

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(13, "\r\n                ");
                __builder2.OpenComponent<Radzen.Blazor.RadzenFileInput<string>>(14);
                __builder2.AddAttribute(15, "Style", "width:400px");
                __builder2.AddAttribute(16, "Change", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<string>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<string>(this, 
#nullable restore
#line 23 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
                                          args => OnChange(args, "FileInput")

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(17, "Error", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<Radzen.UploadErrorEventArgs>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Radzen.UploadErrorEventArgs>(this, 
#nullable restore
#line 23 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
                                                                                       args => OnError(args, "FileInput")

#line default
#line hidden
#nullable disable
                )));
                __builder2.AddAttribute(18, "Value", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<string>(
#nullable restore
#line 22 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
                                              category.ImageSource

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(19, "ValueChanged", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<string>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<string>(this, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.CreateInferredEventCallback(this, __value => category.ImageSource = __value, category.ImageSource))));
                __builder2.AddAttribute(20, "ValueExpression", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Linq.Expressions.Expression<System.Func<string>>>(() => category.ImageSource));
                __builder2.CloseComponent();
                __builder2.CloseElement();
                __builder2.AddMarkupContent(21, "\r\n            ");
                __builder2.OpenElement(22, "div");
                __builder2.AddAttribute(23, "class", "col-xl-6");
                __builder2.OpenComponent<Net5Wasm.Client.Shared.EventConsole>(24);
                __builder2.AddComponentReferenceCapture(25, (__value) => {
#nullable restore
#line 26 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
                                    console = (Net5Wasm.Client.Shared.EventConsole)__value;

#line default
#line hidden
#nullable disable
                }
                );
                __builder2.CloseComponent();
                __builder2.CloseElement();
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 74 "C:\Users\admin\source\repos\Net5Wasm20210729\client\Pages\FileInputPage.razor"
       
   
    EventConsole console;

   

    void OnChange(string value, string name)
    {
        console.Log($"{name} value changed");
    }

    void OnError(UploadErrorEventArgs args, string name)
    {
        console.Log($"{args.Message}");
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private Microsoft.Extensions.Localization.IStringLocalizer<EditCategory> L { get; set; }
    }
}
#pragma warning restore 1591
