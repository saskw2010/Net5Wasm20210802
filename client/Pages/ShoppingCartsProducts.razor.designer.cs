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
    public partial class ShoppingCartsProductsComponent : ComponentBase
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
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct> grid0;

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

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct> _getShoppingCartsProductsResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct> getShoppingCartsProductsResult
        {
            get
            {
                return _getShoppingCartsProductsResult;
            }
            set
            {
                if (!object.Equals(_getShoppingCartsProductsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getShoppingCartsProductsResult", NewValue = value, OldValue = _getShoppingCartsProductsResult };
                    _getShoppingCartsProductsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getShoppingCartsProductsCount;
        protected int getShoppingCartsProductsCount
        {
            get
            {
                return _getShoppingCartsProductsCount;
            }
            set
            {
                if (!object.Equals(_getShoppingCartsProductsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getShoppingCartsProductsCount", NewValue = value, OldValue = _getShoppingCartsProductsCount };
                    _getShoppingCartsProductsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddShoppingCartsProduct>("Add Shopping Carts Product", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportShoppingCartsProductsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "ShoppingCart,Product", Select = "ShoppingCart.UserId,Product.Name,CreatedOn,ModifiedOn,Quantity" }, $"Shopping Carts Products");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportShoppingCartsProductsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "ShoppingCart,Product", Select = "ShoppingCart.UserId,Product.Name,CreatedOn,ModifiedOn,Quantity" }, $"Shopping Carts Products");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetShoppingCartsProductsResult = await Net5Wasmconn.GetShoppingCartsProducts(filter:$@"{(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"ShoppingCart,Product", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getShoppingCartsProductsResult = net5WasmconnGetShoppingCartsProductsResult.Value.AsODataEnumerable();

                getShoppingCartsProductsCount = net5WasmconnGetShoppingCartsProductsResult.Count;
            }
            catch (System.Exception net5WasmconnGetShoppingCartsProductsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load ShoppingCartsProducts" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct args)
        {
            var dialogResult = await DialogService.OpenAsync<EditShoppingCartsProduct>("Edit Shopping Carts Product", new Dictionary<string, object>() { {"ShoppingCartId", args.ShoppingCartId}, {"ProductId", args.ProductId} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteShoppingCartsProductResult = await Net5Wasmconn.DeleteShoppingCartsProduct(shoppingCartId:data.ShoppingCartId, productId:data.ProductId);
                    if (net5WasmconnDeleteShoppingCartsProductResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteShoppingCartsProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete ShoppingCartsProduct" });
            }
        }
    }
}
