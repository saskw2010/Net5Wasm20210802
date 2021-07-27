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
    public partial class OrdersProductsComponent : ComponentBase
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
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.OrdersProduct> grid0;

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

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.OrdersProduct> _getOrdersProductsResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.OrdersProduct> getOrdersProductsResult
        {
            get
            {
                return _getOrdersProductsResult;
            }
            set
            {
                if (!object.Equals(_getOrdersProductsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersProductsResult", NewValue = value, OldValue = _getOrdersProductsResult };
                    _getOrdersProductsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getOrdersProductsCount;
        protected int getOrdersProductsCount
        {
            get
            {
                return _getOrdersProductsCount;
            }
            set
            {
                if (!object.Equals(_getOrdersProductsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersProductsCount", NewValue = value, OldValue = _getOrdersProductsCount };
                    _getOrdersProductsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddOrdersProduct>("Add Orders Product", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportOrdersProductsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Order,Product", Select = "Order.UserId,Product.Name,CreatedOn,ModifiedOn,Quantity" }, $"Orders Products");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportOrdersProductsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Order,Product", Select = "Order.UserId,Product.Name,CreatedOn,ModifiedOn,Quantity" }, $"Orders Products");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetOrdersProductsResult = await Net5Wasmconn.GetOrdersProducts(filter:$@"(contains(OrderId,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Order,Product", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getOrdersProductsResult = net5WasmconnGetOrdersProductsResult.Value.AsODataEnumerable();

                getOrdersProductsCount = net5WasmconnGetOrdersProductsResult.Count;
            }
            catch (System.Exception net5WasmconnGetOrdersProductsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load OrdersProducts" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.OrdersProduct args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOrdersProduct>("Edit Orders Product", new Dictionary<string, object>() { {"OrderId", args.OrderId}, {"ProductId", args.ProductId} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteOrdersProductResult = await Net5Wasmconn.DeleteOrdersProduct(orderId:$"{data.OrderId}", productId:data.ProductId);
                    if (net5WasmconnDeleteOrdersProductResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteOrdersProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete OrdersProduct" });
            }
        }
    }
}
