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
    public partial class AddWishlistsProductComponent : ComponentBase
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
            wishlistsproduct = new Net5Wasm.Models.Net5Wasmconn.WishlistsProduct(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.WishlistsProduct args)
        {
            try
            {
                var net5WasmconnCreateWishlistsProductResult = await Net5Wasmconn.CreateWishlistsProduct(wishlistsProduct:wishlistsproduct);
                DialogService.Close(wishlistsproduct);
            }
            catch (System.Exception net5WasmconnCreateWishlistsProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new WishlistsProduct!" });
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

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
