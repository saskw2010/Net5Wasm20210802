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
    public partial class EditWishlistsProductComponent : ComponentBase
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

        [Parameter]
        public dynamic WishlistId { get; set; }

        [Parameter]
        public dynamic ProductId { get; set; }

        bool _hasChanges;
        protected bool hasChanges
        {
            get
            {
                return _hasChanges;
            }
            set
            {
                if (!object.Equals(_hasChanges, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "hasChanges", NewValue = value, OldValue = _hasChanges };
                    _hasChanges = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if (!object.Equals(_canEdit, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "canEdit", NewValue = value, OldValue = _canEdit };
                    _canEdit = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Net5Wasm.Models.Net5Wasmconn.WishlistsProduct _wishlistsproduct;
        protected Net5Wasm.Models.Net5Wasmconn.WishlistsProduct wishlistsproduct
        {
            get
            {
                return _wishlistsproduct;
            }
            set
            {
                if (!object.Equals(_wishlistsproduct, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "wishlistsproduct", NewValue = value, OldValue = _wishlistsproduct };
                    _wishlistsproduct = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Net5Wasm.Models.Net5Wasmconn.Wishlist _getByWishlistsForWishlistIdResult;
        protected Net5Wasm.Models.Net5Wasmconn.Wishlist getByWishlistsForWishlistIdResult
        {
            get
            {
                return _getByWishlistsForWishlistIdResult;
            }
            set
            {
                if (!object.Equals(_getByWishlistsForWishlistIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByWishlistsForWishlistIdResult", NewValue = value, OldValue = _getByWishlistsForWishlistIdResult };
                    _getByWishlistsForWishlistIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        Net5Wasm.Models.Net5Wasmconn.Product _getByProductsForProductIdResult;
        protected Net5Wasm.Models.Net5Wasmconn.Product getByProductsForProductIdResult
        {
            get
            {
                return _getByProductsForProductIdResult;
            }
            set
            {
                if (!object.Equals(_getByProductsForProductIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByProductsForProductIdResult", NewValue = value, OldValue = _getByProductsForProductIdResult };
                    _getByProductsForProductIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Wishlist> _getWishlistsForWishlistIdResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Wishlist> getWishlistsForWishlistIdResult
        {
            get
            {
                return _getWishlistsForWishlistIdResult;
            }
            set
            {
                if (!object.Equals(_getWishlistsForWishlistIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getWishlistsForWishlistIdResult", NewValue = value, OldValue = _getWishlistsForWishlistIdResult };
                    _getWishlistsForWishlistIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getWishlistsForWishlistIdCount;
        protected int getWishlistsForWishlistIdCount
        {
            get
            {
                return _getWishlistsForWishlistIdCount;
            }
            set
            {
                if (!object.Equals(_getWishlistsForWishlistIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getWishlistsForWishlistIdCount", NewValue = value, OldValue = _getWishlistsForWishlistIdCount };
                    _getWishlistsForWishlistIdCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Product> _getProductsForProductIdResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Product> getProductsForProductIdResult
        {
            get
            {
                return _getProductsForProductIdResult;
            }
            set
            {
                if (!object.Equals(_getProductsForProductIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getProductsForProductIdResult", NewValue = value, OldValue = _getProductsForProductIdResult };
                    _getProductsForProductIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getProductsForProductIdCount;
        protected int getProductsForProductIdCount
        {
            get
            {
                return _getProductsForProductIdCount;
            }
            set
            {
                if (!object.Equals(_getProductsForProductIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getProductsForProductIdCount", NewValue = value, OldValue = _getProductsForProductIdCount };
                    _getProductsForProductIdCount = value;
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
            hasChanges = false;

            canEdit = true;

            var net5WasmconnGetWishlistsProductByWishlistIdAndProductIdResult = await Net5Wasmconn.GetWishlistsProductByWishlistIdAndProductId(wishlistId:WishlistId, productId:ProductId);
            wishlistsproduct = net5WasmconnGetWishlistsProductByWishlistIdAndProductIdResult;

            canEdit = net5WasmconnGetWishlistsProductByWishlistIdAndProductIdResult != null;

            if (this.wishlistsproduct.WishlistId != null)
            {
                var net5WasmconnGetWishlistByIdResult = await Net5Wasmconn.GetWishlistById(id:this.wishlistsproduct.WishlistId);
                getByWishlistsForWishlistIdResult = net5WasmconnGetWishlistByIdResult;
            }

            if (this.wishlistsproduct.ProductId != null)
            {
                var net5WasmconnGetProductByIdResult = await Net5Wasmconn.GetProductById(id:this.wishlistsproduct.ProductId);
                getByProductsForProductIdResult = net5WasmconnGetProductByIdResult;
            }
        }

        protected async System.Threading.Tasks.Task CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            await this.Load();
        }

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.WishlistsProduct args)
        {
            try
            {
                var net5WasmconnUpdateWishlistsProductResult = await Net5Wasmconn.UpdateWishlistsProduct(wishlistId:WishlistId, productId:ProductId, wishlistsProduct:wishlistsproduct);
                if (net5WasmconnUpdateWishlistsProductResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(wishlistsproduct);
                }

                hasChanges = net5WasmconnUpdateWishlistsProductResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception net5WasmconnUpdateWishlistsProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update WishlistsProduct" });
            }
        }

        protected async System.Threading.Tasks.Task WishlistIdLoadData(LoadDataArgs args)
        {
            var net5WasmconnGetWishlistsResult = await Net5Wasmconn.GetWishlists(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getWishlistsForWishlistIdResult = net5WasmconnGetWishlistsResult.Value.AsODataEnumerable();

            getWishlistsForWishlistIdCount = net5WasmconnGetWishlistsResult.Count;
        }

        protected async System.Threading.Tasks.Task ProductIdLoadData(LoadDataArgs args)
        {
            var net5WasmconnGetProductsResult = await Net5Wasmconn.GetProducts(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getProductsForProductIdResult = net5WasmconnGetProductsResult.Value.AsODataEnumerable();

            getProductsForProductIdCount = net5WasmconnGetProductsResult.Count;
        }

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
