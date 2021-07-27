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
    public partial class EditOrdersProductComponent : ComponentBase
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
        public dynamic OrderId { get; set; }

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

        Net5Wasm.Models.Net5Wasmconn.Order _getByOrdersForOrderIdResult;
        protected Net5Wasm.Models.Net5Wasmconn.Order getByOrdersForOrderIdResult
        {
            get
            {
                return _getByOrdersForOrderIdResult;
            }
            set
            {
                if (!object.Equals(_getByOrdersForOrderIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getByOrdersForOrderIdResult", NewValue = value, OldValue = _getByOrdersForOrderIdResult };
                    _getByOrdersForOrderIdResult = value;
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
            hasChanges = false;

            canEdit = true;

            var net5WasmconnGetOrdersProductByOrderIdAndProductIdResult = await Net5Wasmconn.GetOrdersProductByOrderIdAndProductId(orderId:$"{OrderId}", productId:ProductId);
            ordersproduct = net5WasmconnGetOrdersProductByOrderIdAndProductIdResult;

            canEdit = net5WasmconnGetOrdersProductByOrderIdAndProductIdResult != null;

            if (this.ordersproduct.OrderId != null)
            {
                var net5WasmconnGetOrderByIdResult = await Net5Wasmconn.GetOrderById(id:$"{this.ordersproduct.OrderId}");
                getByOrdersForOrderIdResult = net5WasmconnGetOrderByIdResult;
            }

            if (this.ordersproduct.ProductId != null)
            {
                var net5WasmconnGetProductByIdResult = await Net5Wasmconn.GetProductById(id:this.ordersproduct.ProductId);
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

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.OrdersProduct args)
        {
            try
            {
                var net5WasmconnUpdateOrdersProductResult = await Net5Wasmconn.UpdateOrdersProduct(orderId:$"{OrderId}", productId:ProductId, ordersProduct:ordersproduct);
                if (net5WasmconnUpdateOrdersProductResult.StatusCode != System.Net.HttpStatusCode.PreconditionFailed) {
                  DialogService.Close(ordersproduct);
                }

                hasChanges = net5WasmconnUpdateOrdersProductResult.StatusCode == System.Net.HttpStatusCode.PreconditionFailed;
            }
            catch (System.Exception net5WasmconnUpdateOrdersProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update OrdersProduct" });
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

        protected async System.Threading.Tasks.Task Button4Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
