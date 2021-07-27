using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Net5Wasm.Models.Net5Wasmconn;
using Microsoft.AspNetCore.Identity;
using Net5Wasm.Models;
using Microsoft.JSInterop;
using Net5Wasm.Pages;

namespace Net5Wasm.Layouts
{
    public partial class MainLayoutComponent : LayoutComponentBase
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected Net5WasmconnService Net5Wasmconn { get; set; }

        protected RadzenBody body0;
        protected RadzenSidebar sidebar0;

        string _Culture;
        protected string Culture
        {
            get
            {
                return _Culture;
            }
            set
            {
                if(!object.Equals(_Culture, value))
                {
                    _Culture = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        private void Authenticated()
        {
             StateHasChanged();
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
             if (Security != null)
             {
                  Security.Authenticated += Authenticated;

                  await Security.InitializeAsync(AuthenticationStateProvider);
             }
             await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            Culture = "";

            Culture = await JSRuntime.InvokeAsync<string>("Radzen.getCulture");
        }

        protected async System.Threading.Tasks.Task SidebarToggle0Click(dynamic args)
        {
            await InvokeAsync(() => { sidebar0.Toggle(); });

            await InvokeAsync(() => { body0.Toggle(); });
        }

        protected async System.Threading.Tasks.Task Dropdown0Change(dynamic args)
        {
            var redirect = new Uri(UriHelper.Uri).GetComponents(UriComponents.PathAndQuery | UriComponents.Fragment, UriFormat.UriEscaped);

            var query = $"?culture={Uri.EscapeDataString((string)args)}&redirectUri={redirect}";

            UriHelper.NavigateTo("Culture/SetCulture" + query, forceLoad: true);
        }

        protected async System.Threading.Tasks.Task Profilemenu0Click(dynamic args)
        {
            if (args.Value == "Logout")
            {
                await Security.Logout();
            }
        }
    }
}
