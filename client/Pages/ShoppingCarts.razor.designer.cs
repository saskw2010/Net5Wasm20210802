using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Net5Wasm.Models.Net5Wasmconn;
using Microsoft.AspNetCore.Identity;
using Net5Wasm.Models;
using Net5Wasm.Client.Pages;

namespace Net5Wasm.Pages
{
    public partial class ShoppingCartsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

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
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected Net5WasmconnService Net5Wasmconn { get; set; }
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.ShoppingCart> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.ShoppingCart> _getShoppingCartsResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.ShoppingCart> getShoppingCartsResult
        {
            get
            {
                return _getShoppingCartsResult;
            }
            set
            {
                if (!object.Equals(_getShoppingCartsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getShoppingCartsResult", NewValue = value, OldValue = _getShoppingCartsResult };
                    _getShoppingCartsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getShoppingCartsCount;
        protected int getShoppingCartsCount
        {
            get
            {
                return _getShoppingCartsCount;
            }
            set
            {
                if (!object.Equals(_getShoppingCartsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getShoppingCartsCount", NewValue = value, OldValue = _getShoppingCartsCount };
                    _getShoppingCartsCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddShoppingCart>("Add Shopping Cart", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportShoppingCartsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,CreatedOn,ModifiedOn,UserId" }, $"Shopping Carts");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportShoppingCartsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,CreatedOn,ModifiedOn,UserId" }, $"Shopping Carts");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetShoppingCartsResult = await Net5Wasmconn.GetShoppingCarts(filter:$@"(contains(UserId,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getShoppingCartsResult = net5WasmconnGetShoppingCartsResult.Value.AsODataEnumerable();

                getShoppingCartsCount = net5WasmconnGetShoppingCartsResult.Count;
            }
            catch (System.Exception net5WasmconnGetShoppingCartsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load ShoppingCarts" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.ShoppingCart args)
        {
            var dialogResult = await DialogService.OpenAsync<EditShoppingCart>("Edit Shopping Cart", new Dictionary<string, object>() { {"Id", args.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteShoppingCartResult = await Net5Wasmconn.DeleteShoppingCart(id:data.Id);
                    if (net5WasmconnDeleteShoppingCartResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteShoppingCartException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete ShoppingCart" });
            }
        }
    }
}
