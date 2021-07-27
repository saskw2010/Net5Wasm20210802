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
    public partial class WishlistsProductsComponent : ComponentBase
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
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct> grid0;

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

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct> _getWishlistsProductsResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.WishlistsProduct> getWishlistsProductsResult
        {
            get
            {
                return _getWishlistsProductsResult;
            }
            set
            {
                if (!object.Equals(_getWishlistsProductsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getWishlistsProductsResult", NewValue = value, OldValue = _getWishlistsProductsResult };
                    _getWishlistsProductsResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getWishlistsProductsCount;
        protected int getWishlistsProductsCount
        {
            get
            {
                return _getWishlistsProductsCount;
            }
            set
            {
                if (!object.Equals(_getWishlistsProductsCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getWishlistsProductsCount", NewValue = value, OldValue = _getWishlistsProductsCount };
                    _getWishlistsProductsCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddWishlistsProduct>("Add Wishlists Product", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportWishlistsProductsToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Wishlist,Product", Select = "Wishlist.UserId,Product.Name,CreatedOn,ModifiedOn" }, $"Wishlists Products");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportWishlistsProductsToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Wishlist,Product", Select = "Wishlist.UserId,Product.Name,CreatedOn,ModifiedOn" }, $"Wishlists Products");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetWishlistsProductsResult = await Net5Wasmconn.GetWishlistsProducts(filter:$@"{(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Wishlist,Product", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getWishlistsProductsResult = net5WasmconnGetWishlistsProductsResult.Value.AsODataEnumerable();

                getWishlistsProductsCount = net5WasmconnGetWishlistsProductsResult.Count;
            }
            catch (System.Exception net5WasmconnGetWishlistsProductsException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load WishlistsProducts" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.WishlistsProduct args)
        {
            var dialogResult = await DialogService.OpenAsync<EditWishlistsProduct>("Edit Wishlists Product", new Dictionary<string, object>() { {"WishlistId", args.WishlistId}, {"ProductId", args.ProductId} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteWishlistsProductResult = await Net5Wasmconn.DeleteWishlistsProduct(wishlistId:data.WishlistId, productId:data.ProductId);
                    if (net5WasmconnDeleteWishlistsProductResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteWishlistsProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete WishlistsProduct" });
            }
        }
    }
}
