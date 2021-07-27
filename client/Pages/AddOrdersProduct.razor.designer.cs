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
    public partial class AddOrdersProductComponent : ComponentBase
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

        Net5Wasm.Models.Net5Wasmconn.OrdersProduct _ordersproduct;
        protected Net5Wasm.Models.Net5Wasmconn.OrdersProduct ordersproduct
        {
            get
            {
                return _ordersproduct;
            }
            set
            {
                if (!object.Equals(_ordersproduct, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "ordersproduct", NewValue = value, OldValue = _ordersproduct };
                    _ordersproduct = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Order> _getOrdersForOrderIdResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Order> getOrdersForOrderIdResult
        {
            get
            {
                return _getOrdersForOrderIdResult;
            }
            set
            {
                if (!object.Equals(_getOrdersForOrderIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersForOrderIdResult", NewValue = value, OldValue = _getOrdersForOrderIdResult };
                    _getOrdersForOrderIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getOrdersForOrderIdCount;
        protected int getOrdersForOrderIdCount
        {
            get
            {
                return _getOrdersForOrderIdCount;
            }
            set
            {
                if (!object.Equals(_getOrdersForOrderIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOrdersForOrderIdCount", NewValue = value, OldValue = _getOrdersForOrderIdCount };
                    _getOrdersForOrderIdCount = value;
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
            ordersproduct = new Net5Wasm.Models.Net5Wasmconn.OrdersProduct(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.OrdersProduct args)
        {
            try
            {
                var net5WasmconnCreateOrdersProductResult = await Net5Wasmconn.CreateOrdersProduct(ordersProduct:ordersproduct);
                DialogService.Close(ordersproduct);
            }
            catch (System.Exception net5WasmconnCreateOrdersProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new OrdersProduct!" });
            }
        }

        protected async System.Threading.Tasks.Task OrderIdLoadData(LoadDataArgs args)
        {
            var net5WasmconnGetOrdersResult = await Net5Wasmconn.GetOrders(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getOrdersForOrderIdResult = net5WasmconnGetOrdersResult.Value.AsODataEnumerable();

            getOrdersForOrderIdCount = net5WasmconnGetOrdersResult.Count;
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
