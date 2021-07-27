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
    public partial class AddOrderComponent : ComponentBase
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

        Net5Wasm.Models.Net5Wasmconn.Order _order;
        protected Net5Wasm.Models.Net5Wasmconn.Order order
        {
            get
            {
                return _order;
            }
            set
            {
                if (!object.Equals(_order, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "order", NewValue = value, OldValue = _order };
                    _order = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Address> _getAddressesForDeliveryAddressIdResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Address> getAddressesForDeliveryAddressIdResult
        {
            get
            {
                return _getAddressesForDeliveryAddressIdResult;
            }
            set
            {
                if (!object.Equals(_getAddressesForDeliveryAddressIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAddressesForDeliveryAddressIdResult", NewValue = value, OldValue = _getAddressesForDeliveryAddressIdResult };
                    _getAddressesForDeliveryAddressIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getAddressesForDeliveryAddressIdCount;
        protected int getAddressesForDeliveryAddressIdCount
        {
            get
            {
                return _getAddressesForDeliveryAddressIdCount;
            }
            set
            {
                if (!object.Equals(_getAddressesForDeliveryAddressIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAddressesForDeliveryAddressIdCount", NewValue = value, OldValue = _getAddressesForDeliveryAddressIdCount };
                    _getAddressesForDeliveryAddressIdCount = value;
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
            order = new Net5Wasm.Models.Net5Wasmconn.Order(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.Order args)
        {
            try
            {
                var net5WasmconnCreateOrderResult = await Net5Wasmconn.CreateOrder(order:order);
                DialogService.Close(order);
            }
            catch (System.Exception net5WasmconnCreateOrderException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Order!" });
            }
        }

        protected async System.Threading.Tasks.Task DeliveryAddressIdLoadData(LoadDataArgs args)
        {
            var net5WasmconnGetAddressesResult = await Net5Wasmconn.GetAddresses(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getAddressesForDeliveryAddressIdResult = net5WasmconnGetAddressesResult.Value.AsODataEnumerable();

            getAddressesForDeliveryAddressIdCount = net5WasmconnGetAddressesResult.Count;
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
