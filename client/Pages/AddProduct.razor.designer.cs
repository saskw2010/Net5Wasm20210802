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
    public partial class AddProductComponent : ComponentBase
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

        Net5Wasm.Models.Net5Wasmconn.Product _product;
        protected Net5Wasm.Models.Net5Wasmconn.Product product
        {
            get
            {
                return _product;
            }
            set
            {
                if (!object.Equals(_product, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "product", NewValue = value, OldValue = _product };
                    _product = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Category> _getCategoriesForCategoryIdResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Category> getCategoriesForCategoryIdResult
        {
            get
            {
                return _getCategoriesForCategoryIdResult;
            }
            set
            {
                if (!object.Equals(_getCategoriesForCategoryIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriesForCategoryIdResult", NewValue = value, OldValue = _getCategoriesForCategoryIdResult };
                    _getCategoriesForCategoryIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getCategoriesForCategoryIdCount;
        protected int getCategoriesForCategoryIdCount
        {
            get
            {
                return _getCategoriesForCategoryIdCount;
            }
            set
            {
                if (!object.Equals(_getCategoriesForCategoryIdCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriesForCategoryIdCount", NewValue = value, OldValue = _getCategoriesForCategoryIdCount };
                    _getCategoriesForCategoryIdCount = value;
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
            product = new Net5Wasm.Models.Net5Wasmconn.Product(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(Net5Wasm.Models.Net5Wasmconn.Product args)
        {
            try
            {
                var net5WasmconnCreateProductResult = await Net5Wasmconn.CreateProduct(product:product);
                DialogService.Close(product);
            }
            catch (System.Exception net5WasmconnCreateProductException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Product!" });
            }
        }

        protected async System.Threading.Tasks.Task CategoryIdLoadData(LoadDataArgs args)
        {
            var net5WasmconnGetCategoriesResult = await Net5Wasmconn.GetCategories(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:true);
            getCategoriesForCategoryIdResult = net5WasmconnGetCategoriesResult.Value.AsODataEnumerable();

            getCategoriesForCategoryIdCount = net5WasmconnGetCategoriesResult.Count;
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
