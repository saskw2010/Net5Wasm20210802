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
    public partial class OrdersComponent : ComponentBase
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
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.Order> grid0;

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

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Order> _getOrdersResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Order> getOrdersResult
        {
            get
            {
                return _getOrdersResult;
            }
            set
            {
                if (!object.Equals(_getOrdersResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersResult", NewValue = value, OldValue = _getOrdersResult };
                    _getOrdersResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getOrdersCount;
        protected int getOrdersCount
        {
            get
            {
                return _getOrdersCount;
            }
            set
            {
                if (!object.Equals(_getOrdersCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersCount", NewValue = value, OldValue = _getOrdersCount };
                    _getOrdersCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddOrder>("Add Order", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportOrdersToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Address", Select = "Id,CreatedOn,ModifiedOn,UserId,Address.Country" }, $"Orders");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportOrdersToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Address", Select = "Id,CreatedOn,ModifiedOn,UserId,Address.Country" }, $"Orders");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetOrdersResult = await Net5Wasmconn.GetOrders(filter:$@"(contains(Id,""{search}"") or contains(UserId,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Address", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getOrdersResult = net5WasmconnGetOrdersResult.Value.AsODataEnumerable();

                getOrdersCount = net5WasmconnGetOrdersResult.Count;
            }
            catch (System.Exception net5WasmconnGetOrdersException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Orders" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.Order args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOrder>("Edit Order", new Dictionary<string, object>() { {"Id", args.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteOrderResult = await Net5Wasmconn.DeleteOrder(id:$"{data.Id}");
                    if (net5WasmconnDeleteOrderResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteOrderException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Order" });
            }
        }
    }
}
