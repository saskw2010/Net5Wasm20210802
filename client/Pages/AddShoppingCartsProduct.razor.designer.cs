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
    public partial class AddShoppingCartsProductComponent : ComponentBase
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

        Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct _shoppingcartsproduct;
        protected Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct shoppingcartsproduct
        {
            get
            {
                return _shoppingcartsproduct;
            }
            set
            {
                if (!object.Equals(_shoppingcartsproduct, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "shoppingcartsproduct", NewValue = value, OldValue = _shoppingcartsproduct };
                    _shoppingcartsproduct = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.ShoppingCart> _getShoppingCartsForShoppingCartIdResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.ShoppingCart> getShoppingCartsForShoppingCartIdResult
        {
            get
            {
                return _getShoppingCartsForShoppingCartIdResult;
            }
            set
            {
                if (!object.Equals(_getShoppingCartsForShoppingCartIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getShoppingCartsForShoppingCartIdResult", NewValue = value, OldValue = _getShoppingCartsForShoppingCartIdResult };
                    _getShoppingCartsForShoppingCartIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getShoppingCartsForShoppingCartIdCount;
        protected int getShoppingCartsForShoppingCartIdCount
        {
            get
            {
                return _getShoppingCartsForShoppingCartIdCount;
            }
            set
            {
                if (!object.Equals(_getShoppingCartsForShoppingCartIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getShoppingCartsForShoppingCartIdCount", NewValue = value, OldValue = _getShoppingCartsForShoppingCartIdCount };
                    _getShoppingCartsForShoppingCartIdCount = value;
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
            shoppingcartsproduct = new Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.ShoppingCartsProduct args)
        {
            try
            {
                var net5WasmconnCreateShoppingCartsProductResult = await Net5Wasmconn.CreateShoppingCartsProduct(shoppingCartsProduct:shoppingcartsproduct);
                DialogService.Close(shoppingcartsproduct);
            }
            catch (System.Exception net5WasmconnCreateShoppingCartsProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new ShoppingCartsProduct!" });
            }
        }

        protected async System.Threading.Tasks.Task ShoppingCartIdLoadData(LoadDataArgs args)
        {
            var net5WasmconnGetShoppingCartsResult = await Net5Wasmconn.GetShoppingCarts(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getShoppingCartsForShoppingCartIdResult = net5WasmconnGetShoppingCartsResult.Value.AsODataEnumerable();

            getShoppingCartsForShoppingCartIdCount = net5WasmconnGetShoppingCartsResult.Count;
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
