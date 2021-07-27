
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Radzen;
using Net5Wasm.Models.Net5Wasmconn;

namespace Net5Wasm
{
    public partial class Net5WasmconnService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public Net5WasmconnService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/net5wasmconn/");
        }

        public async System.Threading.Tasks.Task ExportAddressesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/addresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/addresses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportAddressesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/addresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/addresses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetAddresses(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Address>> GetAddresses(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Addresses");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddresses(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Address>>();
        }
        partial void OnCreateAddress(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Address> CreateAddress(Address address = default(Address))
        {
            var uri = new Uri(baseUri, $"Addresses");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(address), Encoding.UTF8, "application/json");

            OnCreateAddress(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Address>();
        }

        public async System.Threading.Tasks.Task ExportCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetCategories(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Category>> GetCategories(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Categories");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategories(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Category>>();
        }
        partial void OnCreateCategory(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Category> CreateCategory(Category category = default(Category))
        {
            var uri = new Uri(baseUri, $"Categories");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            OnCreateCategory(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Category>();
        }

        public async System.Threading.Tasks.Task ExportOrdersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOrdersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetOrders(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Order>> GetOrders(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Orders");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOrders(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Order>>();
        }
        partial void OnCreateOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Order> CreateOrder(Order order = default(Order))
        {
            var uri = new Uri(baseUri, $"Orders");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(order), Encoding.UTF8, "application/json");

            OnCreateOrder(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Order>();
        }

        public async System.Threading.Tasks.Task ExportOrdersProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/ordersproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/ordersproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportOrdersProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/ordersproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/ordersproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetOrdersProducts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<OrdersProduct>> GetOrdersProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"OrdersProducts");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOrdersProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<OrdersProduct>>();
        }
        partial void OnCreateOrdersProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<OrdersProduct> CreateOrdersProduct(OrdersProduct ordersProduct = default(OrdersProduct))
        {
            var uri = new Uri(baseUri, $"OrdersProducts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(ordersProduct), Encoding.UTF8, "application/json");

            OnCreateOrdersProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<OrdersProduct>();
        }

        public async System.Threading.Tasks.Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetProducts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Product>> GetProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Products");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Product>>();
        }
        partial void OnCreateProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Product> CreateProduct(Product product = default(Product))
        {
            var uri = new Uri(baseUri, $"Products");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            OnCreateProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Product>();
        }

        public async System.Threading.Tasks.Task ExportShoppingCartsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/shoppingcarts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/shoppingcarts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportShoppingCartsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/shoppingcarts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/shoppingcarts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetShoppingCarts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<ShoppingCart>> GetShoppingCarts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"ShoppingCarts");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetShoppingCarts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<ShoppingCart>>();
        }
        partial void OnCreateShoppingCart(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ShoppingCart> CreateShoppingCart(ShoppingCart shoppingCart = default(ShoppingCart))
        {
            var uri = new Uri(baseUri, $"ShoppingCarts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(shoppingCart), Encoding.UTF8, "application/json");

            OnCreateShoppingCart(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ShoppingCart>();
        }

        public async System.Threading.Tasks.Task ExportShoppingCartsProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/shoppingcartsproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/shoppingcartsproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportShoppingCartsProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/shoppingcartsproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/shoppingcartsproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetShoppingCartsProducts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<ShoppingCartsProduct>> GetShoppingCartsProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"ShoppingCartsProducts");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetShoppingCartsProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<ShoppingCartsProduct>>();
        }
        partial void OnCreateShoppingCartsProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ShoppingCartsProduct> CreateShoppingCartsProduct(ShoppingCartsProduct shoppingCartsProduct = default(ShoppingCartsProduct))
        {
            var uri = new Uri(baseUri, $"ShoppingCartsProducts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(shoppingCartsProduct), Encoding.UTF8, "application/json");

            OnCreateShoppingCartsProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ShoppingCartsProduct>();
        }

        public async System.Threading.Tasks.Task ExportWishlistsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/wishlists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/wishlists/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportWishlistsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/wishlists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/wishlists/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetWishlists(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<Wishlist>> GetWishlists(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"Wishlists");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetWishlists(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<Wishlist>>();
        }
        partial void OnCreateWishlist(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Wishlist> CreateWishlist(Wishlist wishlist = default(Wishlist))
        {
            var uri = new Uri(baseUri, $"Wishlists");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(wishlist), Encoding.UTF8, "application/json");

            OnCreateWishlist(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Wishlist>();
        }

        public async System.Threading.Tasks.Task ExportWishlistsProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/wishlistsproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/wishlistsproducts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportWishlistsProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/net5wasmconn/wishlistsproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/net5wasmconn/wishlistsproducts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetWishlistsProducts(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ODataServiceResult<WishlistsProduct>> GetWishlistsProducts(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string))
        {
            var uri = new Uri(baseUri, $"WishlistsProducts");
            uri = uri.GetODataUri(filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:null, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetWishlistsProducts(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ODataServiceResult<WishlistsProduct>>();
        }
        partial void OnCreateWishlistsProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<WishlistsProduct> CreateWishlistsProduct(WishlistsProduct wishlistsProduct = default(WishlistsProduct))
        {
            var uri = new Uri(baseUri, $"WishlistsProducts");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(wishlistsProduct), Encoding.UTF8, "application/json");

            OnCreateWishlistsProduct(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<WishlistsProduct>();
        }
        partial void OnDeleteAddress(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteAddress(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Addresses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteAddress(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetAddressById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Address> GetAddressById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Addresses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetAddressById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Address>();
        }
        partial void OnUpdateAddress(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateAddress(int? id = default(int?), Address address = default(Address))
        {
            var uri = new Uri(baseUri, $"Addresses({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", address.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(address), Encoding.UTF8, "application/json");

            OnUpdateAddress(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteCategory(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteCategory(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Categories({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteCategory(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetCategoryById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Category> GetCategoryById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Categories({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetCategoryById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Category>();
        }
        partial void OnUpdateCategory(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateCategory(int? id = default(int?), Category category = default(Category))
        {
            var uri = new Uri(baseUri, $"Categories({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", category.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            OnUpdateCategory(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteOrder(string id = default(string))
        {
            var uri = new Uri(baseUri, $"Orders('{HttpUtility.UrlEncode(id.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOrder(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetOrderById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Order> GetOrderById(string id = default(string))
        {
            var uri = new Uri(baseUri, $"Orders('{HttpUtility.UrlEncode(id.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOrderById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Order>();
        }
        partial void OnUpdateOrder(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateOrder(string id = default(string), Order order = default(Order))
        {
            var uri = new Uri(baseUri, $"Orders('{HttpUtility.UrlEncode(id.Trim())}')");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", order.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(order), Encoding.UTF8, "application/json");

            OnUpdateOrder(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteOrdersProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteOrdersProduct(string orderId = default(string), int? productId = default(int?))
        {
            var uri = new Uri(baseUri, $"OrdersProducts(OrderId='{HttpUtility.UrlEncode(orderId.Trim())}',ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteOrdersProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetOrdersProductByOrderIdAndProductId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<OrdersProduct> GetOrdersProductByOrderIdAndProductId(string orderId = default(string), int? productId = default(int?))
        {
            var uri = new Uri(baseUri, $"OrdersProducts(OrderId='{HttpUtility.UrlEncode(orderId.Trim())}',ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetOrdersProductByOrderIdAndProductId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<OrdersProduct>();
        }
        partial void OnUpdateOrdersProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateOrdersProduct(string orderId = default(string), int? productId = default(int?), OrdersProduct ordersProduct = default(OrdersProduct))
        {
            var uri = new Uri(baseUri, $"OrdersProducts(OrderId='{HttpUtility.UrlEncode(orderId.Trim())}',ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", ordersProduct.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(ordersProduct), Encoding.UTF8, "application/json");

            OnUpdateOrdersProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteProduct(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Products({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetProductById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Product> GetProductById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Products({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetProductById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Product>();
        }
        partial void OnUpdateProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateProduct(int? id = default(int?), Product product = default(Product))
        {
            var uri = new Uri(baseUri, $"Products({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", product.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(product), Encoding.UTF8, "application/json");

            OnUpdateProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteShoppingCart(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteShoppingCart(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"ShoppingCarts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteShoppingCart(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetShoppingCartById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ShoppingCart> GetShoppingCartById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"ShoppingCarts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetShoppingCartById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ShoppingCart>();
        }
        partial void OnUpdateShoppingCart(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateShoppingCart(int? id = default(int?), ShoppingCart shoppingCart = default(ShoppingCart))
        {
            var uri = new Uri(baseUri, $"ShoppingCarts({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", shoppingCart.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(shoppingCart), Encoding.UTF8, "application/json");

            OnUpdateShoppingCart(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteShoppingCartsProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteShoppingCartsProduct(int? shoppingCartId = default(int?), int? productId = default(int?))
        {
            var uri = new Uri(baseUri, $"ShoppingCartsProducts(ShoppingCartId={shoppingCartId},ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteShoppingCartsProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetShoppingCartsProductByShoppingCartIdAndProductId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<ShoppingCartsProduct> GetShoppingCartsProductByShoppingCartIdAndProductId(int? shoppingCartId = default(int?), int? productId = default(int?))
        {
            var uri = new Uri(baseUri, $"ShoppingCartsProducts(ShoppingCartId={shoppingCartId},ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetShoppingCartsProductByShoppingCartIdAndProductId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<ShoppingCartsProduct>();
        }
        partial void OnUpdateShoppingCartsProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateShoppingCartsProduct(int? shoppingCartId = default(int?), int? productId = default(int?), ShoppingCartsProduct shoppingCartsProduct = default(ShoppingCartsProduct))
        {
            var uri = new Uri(baseUri, $"ShoppingCartsProducts(ShoppingCartId={shoppingCartId},ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", shoppingCartsProduct.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(shoppingCartsProduct), Encoding.UTF8, "application/json");

            OnUpdateShoppingCartsProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteWishlist(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteWishlist(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Wishlists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteWishlist(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetWishlistById(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Wishlist> GetWishlistById(int? id = default(int?))
        {
            var uri = new Uri(baseUri, $"Wishlists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetWishlistById(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<Wishlist>();
        }
        partial void OnUpdateWishlist(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateWishlist(int? id = default(int?), Wishlist wishlist = default(Wishlist))
        {
            var uri = new Uri(baseUri, $"Wishlists({id})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", wishlist.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(wishlist), Encoding.UTF8, "application/json");

            OnUpdateWishlist(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteWishlistsProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteWishlistsProduct(int? wishlistId = default(int?), int? productId = default(int?))
        {
            var uri = new Uri(baseUri, $"WishlistsProducts(WishlistId={wishlistId},ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteWishlistsProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetWishlistsProductByWishlistIdAndProductId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<WishlistsProduct> GetWishlistsProductByWishlistIdAndProductId(int? wishlistId = default(int?), int? productId = default(int?))
        {
            var uri = new Uri(baseUri, $"WishlistsProducts(WishlistId={wishlistId},ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetWishlistsProductByWishlistIdAndProductId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await response.ReadAsync<WishlistsProduct>();
        }
        partial void OnUpdateWishlistsProduct(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateWishlistsProduct(int? wishlistId = default(int?), int? productId = default(int?), WishlistsProduct wishlistsProduct = default(WishlistsProduct))
        {
            var uri = new Uri(baseUri, $"WishlistsProducts(WishlistId={wishlistId},ProductId={productId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", wishlistsProduct.ETag);

            httpRequestMessage.Content = new StringContent(ODataJsonSerializer.Serialize(wishlistsProduct), Encoding.UTF8, "application/json");

            OnUpdateWishlistsProduct(httpRequestMessage);
            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
