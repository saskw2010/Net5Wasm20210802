using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Net5Wasm.Data;

namespace Net5Wasm
{
    public partial class ExportNet5WasmconnController : ExportController
    {
        private readonly Net5WasmconnContext context;

        public ExportNet5WasmconnController(Net5WasmconnContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/Net5Wasmconn/addresses/csv")]
        [HttpGet("/export/Net5Wasmconn/addresses/csv(fileName='{fileName}')")]
        public FileStreamResult ExportAddressesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Addresses, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/addresses/excel")]
        [HttpGet("/export/Net5Wasmconn/addresses/excel(fileName='{fileName}')")]
        public FileStreamResult ExportAddressesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Addresses, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/categories/csv")]
        [HttpGet("/export/Net5Wasmconn/categories/csv(fileName='{fileName}')")]
        public FileStreamResult ExportCategoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Categories, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/categories/excel")]
        [HttpGet("/export/Net5Wasmconn/categories/excel(fileName='{fileName}')")]
        public FileStreamResult ExportCategoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Categories, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/orders/csv")]
        [HttpGet("/export/Net5Wasmconn/orders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Orders, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/orders/excel")]
        [HttpGet("/export/Net5Wasmconn/orders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Orders, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/ordersproducts/csv")]
        [HttpGet("/export/Net5Wasmconn/ordersproducts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.OrdersProducts, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/ordersproducts/excel")]
        [HttpGet("/export/Net5Wasmconn/ordersproducts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOrdersProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.OrdersProducts, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/products/csv")]
        [HttpGet("/export/Net5Wasmconn/products/csv(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Products, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/products/excel")]
        [HttpGet("/export/Net5Wasmconn/products/excel(fileName='{fileName}')")]
        public FileStreamResult ExportProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Products, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/shoppingcarts/csv")]
        [HttpGet("/export/Net5Wasmconn/shoppingcarts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportShoppingCartsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.ShoppingCarts, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/shoppingcarts/excel")]
        [HttpGet("/export/Net5Wasmconn/shoppingcarts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportShoppingCartsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.ShoppingCarts, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/shoppingcartsproducts/csv")]
        [HttpGet("/export/Net5Wasmconn/shoppingcartsproducts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportShoppingCartsProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.ShoppingCartsProducts, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/shoppingcartsproducts/excel")]
        [HttpGet("/export/Net5Wasmconn/shoppingcartsproducts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportShoppingCartsProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.ShoppingCartsProducts, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/wishlists/csv")]
        [HttpGet("/export/Net5Wasmconn/wishlists/csv(fileName='{fileName}')")]
        public FileStreamResult ExportWishlistsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Wishlists, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/wishlists/excel")]
        [HttpGet("/export/Net5Wasmconn/wishlists/excel(fileName='{fileName}')")]
        public FileStreamResult ExportWishlistsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Wishlists, Request.Query), fileName);
        }
        [HttpGet("/export/Net5Wasmconn/wishlistsproducts/csv")]
        [HttpGet("/export/Net5Wasmconn/wishlistsproducts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportWishlistsProductsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.WishlistsProducts, Request.Query), fileName);
        }

        [HttpGet("/export/Net5Wasmconn/wishlistsproducts/excel")]
        [HttpGet("/export/Net5Wasmconn/wishlistsproducts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportWishlistsProductsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.WishlistsProducts, Request.Query), fileName);
        }
    }
}
