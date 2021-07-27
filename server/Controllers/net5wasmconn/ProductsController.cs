using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNet.OData.Query;



namespace Net5Wasm.Controllers.Net5Wasmconn
{
  using Models;
  using Data;
  using Models.Net5Wasmconn;

  [ODataRoutePrefix("odata/net5wasmconn/Products")]
  [Route("mvc/odata/net5wasmconn/Products")]
  public partial class ProductsController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public ProductsController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/Products
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.Product> GetProducts()
    {
      var items = this.context.Products.AsQueryable<Models.Net5Wasmconn.Product>();
      this.OnProductsRead(ref items);

      return items;
    }

    partial void OnProductsRead(ref IQueryable<Models.Net5Wasmconn.Product> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Product> GetProduct(int key)
    {
        var items = this.context.Products.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnProductDeleted(Models.Net5Wasmconn.Product item);
    partial void OnAfterProductDeleted(Models.Net5Wasmconn.Product item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteProduct(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Products
                .Where(i => i.Id == key)
                .Include(i => i.ShoppingCartsProducts)
                .Include(i => i.WishlistsProducts)
                .Include(i => i.OrdersProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Product>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnProductDeleted(item);
            this.context.Products.Remove(item);
            this.context.SaveChanges();
            this.OnAfterProductDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnProductUpdated(Models.Net5Wasmconn.Product item);
    partial void OnAfterProductUpdated(Models.Net5Wasmconn.Product item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutProduct(int key, [FromBody]Models.Net5Wasmconn.Product newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Products
                .Where(i => i.Id == key)
                .Include(i => i.ShoppingCartsProducts)
                .Include(i => i.WishlistsProducts)
                .Include(i => i.OrdersProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Product>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnProductUpdated(newItem);
            this.context.Products.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Products.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Category");
            this.OnAfterProductUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchProduct(int key, [FromBody]Delta<Models.Net5Wasmconn.Product> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Products.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Product>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnProductUpdated(item);
            this.context.Products.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Products.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Category");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnProductCreated(Models.Net5Wasmconn.Product item);
    partial void OnAfterProductCreated(Models.Net5Wasmconn.Product item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.Product item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnProductCreated(item);
            this.context.Products.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Products.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Category");

            this.OnAfterProductCreated(item);

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
