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

  [ODataRoutePrefix("odata/net5wasmconn/ShoppingCartsProducts")]
  [Route("mvc/odata/net5wasmconn/ShoppingCartsProducts")]
  public partial class ShoppingCartsProductsController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public ShoppingCartsProductsController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/ShoppingCartsProducts
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.ShoppingCartsProduct> GetShoppingCartsProducts()
    {
      var items = this.context.ShoppingCartsProducts.AsQueryable<Models.Net5Wasmconn.ShoppingCartsProduct>();
      this.OnShoppingCartsProductsRead(ref items);

      return items;
    }

    partial void OnShoppingCartsProductsRead(ref IQueryable<Models.Net5Wasmconn.ShoppingCartsProduct> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{ShoppingCartId},{ProductId}")]
    public SingleResult<ShoppingCartsProduct> GetShoppingCartsProduct([FromODataUri] int keyShoppingCartId,[FromODataUri] int keyProductId)
    {
        var items = this.context.ShoppingCartsProducts.Where(i=>i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId);
        return SingleResult.Create(items);
    }
    partial void OnShoppingCartsProductDeleted(Models.Net5Wasmconn.ShoppingCartsProduct item);
    partial void OnAfterShoppingCartsProductDeleted(Models.Net5Wasmconn.ShoppingCartsProduct item);

    [HttpDelete("{ShoppingCartId},{ProductId}")]
    public IActionResult DeleteShoppingCartsProduct([FromODataUri] int keyShoppingCartId,[FromODataUri] int keyProductId)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.ShoppingCartsProducts
                .Where(i => i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.ShoppingCartsProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnShoppingCartsProductDeleted(item);
            this.context.ShoppingCartsProducts.Remove(item);
            this.context.SaveChanges();
            this.OnAfterShoppingCartsProductDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnShoppingCartsProductUpdated(Models.Net5Wasmconn.ShoppingCartsProduct item);
    partial void OnAfterShoppingCartsProductUpdated(Models.Net5Wasmconn.ShoppingCartsProduct item);

    [HttpPut("{ShoppingCartId},{ProductId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutShoppingCartsProduct([FromODataUri] int keyShoppingCartId,[FromODataUri] int keyProductId, [FromBody]Models.Net5Wasmconn.ShoppingCartsProduct newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.ShoppingCartsProducts
                .Where(i => i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.ShoppingCartsProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnShoppingCartsProductUpdated(newItem);
            this.context.ShoppingCartsProducts.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.ShoppingCartsProducts.Where(i => i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId);
            Request.QueryString = Request.QueryString.Add("$expand", "ShoppingCart,Product");
            this.OnAfterShoppingCartsProductUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{ShoppingCartId},{ProductId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchShoppingCartsProduct([FromODataUri] int keyShoppingCartId,[FromODataUri] int keyProductId, [FromBody]Delta<Models.Net5Wasmconn.ShoppingCartsProduct> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.ShoppingCartsProducts.Where(i => i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.ShoppingCartsProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnShoppingCartsProductUpdated(item);
            this.context.ShoppingCartsProducts.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.ShoppingCartsProducts.Where(i => i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId);
            Request.QueryString = Request.QueryString.Add("$expand", "ShoppingCart,Product");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnShoppingCartsProductCreated(Models.Net5Wasmconn.ShoppingCartsProduct item);
    partial void OnAfterShoppingCartsProductCreated(Models.Net5Wasmconn.ShoppingCartsProduct item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.ShoppingCartsProduct item)
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

            this.OnShoppingCartsProductCreated(item);
            this.context.ShoppingCartsProducts.Add(item);
            this.context.SaveChanges();

            var keyShoppingCartId = item.ShoppingCartId;
            var keyProductId = item.ProductId;

            var itemToReturn = this.context.ShoppingCartsProducts.Where(i => i.ShoppingCartId == keyShoppingCartId && i.ProductId == keyProductId);

            Request.QueryString = Request.QueryString.Add("$expand", "ShoppingCart,Product");

            this.OnAfterShoppingCartsProductCreated(item);

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
