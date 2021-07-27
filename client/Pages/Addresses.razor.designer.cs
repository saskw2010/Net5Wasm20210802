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
    public partial class AddressesComponent : ComponentBase
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
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.Address> grid0;

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

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Address> _getAddressesResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Address> getAddressesResult
        {
            get
            {
                return _getAddressesResult;
            }
            set
            {
                if (!object.Equals(_getAddressesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAddressesResult", NewValue = value, OldValue = _getAddressesResult };
                    _getAddressesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getAddressesCount;
        protected int getAddressesCount
        {
            get
            {
                return _getAddressesCount;
            }
            set
            {
                if (!object.Equals(_getAddressesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getAddressesCount", NewValue = value, OldValue = _getAddressesCount };
                    _getAddressesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddAddress>("Add Address", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportAddressesToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Country,State,City,Description,PostalCode,PhoneNumber,UserId" }, $"Addresses");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportAddressesToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Country,State,City,Description,PostalCode,PhoneNumber,UserId" }, $"Addresses");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetAddressesResult = await Net5Wasmconn.GetAddresses(filter:$@"(contains(Country,""{search}"") or contains(State,""{search}"") or contains(City,""{search}"") or contains(Description,""{search}"") or contains(PostalCode,""{search}"") or contains(PhoneNumber,""{search}"") or contains(UserId,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getAddressesResult = net5WasmconnGetAddressesResult.Value.AsODataEnumerable();

                getAddressesCount = net5WasmconnGetAddressesResult.Count;
            }
            catch (System.Exception net5WasmconnGetAddressesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Addresses" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.Address args)
        {
            var dialogResult = await DialogService.OpenAsync<EditAddress>("Edit Address", new Dictionary<string, object>() { {"Id", args.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteAddressResult = await Net5Wasmconn.DeleteAddress(id:data.Id);
                    if (net5WasmconnDeleteAddressResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteAddressException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Address" });
            }
        }
    }
}
