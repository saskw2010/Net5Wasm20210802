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
    public partial class CategoriesComponent : ComponentBase
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
        protected RadzenDataGrid<Net5Wasm.Models.Net5Wasmconn.Category> grid0;

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

        IEnumerable<Net5Wasm.Models.Net5Wasmconn.Category> _getCategoriesResult;
        protected IEnumerable<Net5Wasm.Models.Net5Wasmconn.Category> getCategoriesResult
        {
            get
            {
                return _getCategoriesResult;
            }
            set
            {
                if (!object.Equals(_getCategoriesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriesResult", NewValue = value, OldValue = _getCategoriesResult };
                    _getCategoriesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getCategoriesCount;
        protected int getCategoriesCount
        {
            get
            {
                return _getCategoriesCount;
            }
            set
            {
                if (!object.Equals(_getCategoriesCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getCategoriesCount", NewValue = value, OldValue = _getCategoriesCount };
                    _getCategoriesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddCategory>("Add Category", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await Net5Wasmconn.ExportCategoriesToCSV(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Name,ImageSource" }, $"Categories");

            }

            if (args == null || args.Value == "xlsx")
            {
                await Net5Wasmconn.ExportCategoriesToExcel(new Query() { Filter = $@"{grid0.Query.Filter}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn,Name,ImageSource" }, $"Categories");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var net5WasmconnGetCategoriesResult = await Net5Wasmconn.GetCategories(filter:$@"(contains(Name,""{search}"") or contains(ImageSource,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getCategoriesResult = net5WasmconnGetCategoriesResult.Value.AsODataEnumerable();

                getCategoriesCount = net5WasmconnGetCategoriesResult.Count;
            }
            catch (System.Exception net5WasmconnGetCategoriesException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load Categories" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(Net5Wasm.Models.Net5Wasmconn.Category args)
        {
            var dialogResult = await DialogService.OpenAsync<EditCategory>("Edit Category", new Dictionary<string, object>() { {"Id", args.Id} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var net5WasmconnDeleteCategoryResult = await Net5Wasmconn.DeleteCategory(id:data.Id);
                    if (net5WasmconnDeleteCategoryResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception net5WasmconnDeleteCategoryException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Category" });
            }
        }
    }
}
