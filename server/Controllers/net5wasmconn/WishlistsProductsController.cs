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

  [ODataRoutePrefix("odata/net5wasmconn/WishlistsProducts")]
  [Route("mvc/odata/net5wasmconn/WishlistsProducts")]
  public partial class WishlistsProductsController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public WishlistsProductsController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/WishlistsProducts
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.WishlistsProduct> GetWishlistsProducts()
    {
      var items = this.context.WishlistsProducts.AsQueryable<Models.Net5Wasmconn.WishlistsProduct>();
      this.OnWishlistsProductsRead(ref items);

      return items;
    }

    partial void OnWishlistsProductsRead(ref IQueryable<Models.Net5Wasmconn.WishlistsProduct> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{WishlistId},{ProductId}")]
    public SingleResult<WishlistsProduct> GetWishlistsProduct([FromODataUri] int keyWishlistId,[FromODataUri] int keyProductId)
    {
        var items = this.context.WishlistsProducts.Where(i=>i.WishlistId == keyWishlistId && i.ProductId == keyProductId);
        return SingleResult.Create(items);
    }
    partial void OnWishlistsProductDeleted(Models.Net5Wasmconn.WishlistsProduct item);
    partial void OnAfterWishlistsProductDeleted(Models.Net5Wasmconn.WishlistsProduct item);

    [HttpDelete("{WishlistId},{ProductId}")]
    public IActionResult DeleteWishlistsProduct([FromODataUri] int keyWishlistId,[FromODataUri] int keyProductId)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.WishlistsProducts
                .Where(i => i.WishlistId == keyWishlistId && i.ProductId == keyProductId)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.WishlistsProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnWishlistsProductDeleted(item);
            this.context.WishlistsProducts.Remove(item);
            this.context.SaveChanges();
            this.OnAfterWishlistsProductDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnWishlistsProductUpdated(Models.Net5Wasmconn.WishlistsProduct item);
    partial void OnAfterWishlistsProductUpdated(Models.Net5Wasmconn.WishlistsProduct item);

    [HttpPut("{WishlistId},{ProductId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutWishlistsProduct([FromODataUri] int keyWishlistId,[FromODataUri] int keyProductId, [FromBody]Models.Net5Wasmconn.WishlistsProduct newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.WishlistsProducts
                .Where(i => i.WishlistId == keyWishlistId && i.ProductId == keyProductId)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.WishlistsProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnWishlistsProductUpdated(newItem);
            this.context.WishlistsProducts.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.WishlistsProducts.Where(i => i.WishlistId == keyWishlistId && i.ProductId == keyProductId);
            Request.QueryString = Request.QueryString.Add("$expand", "Wishlist,Product");
            this.OnAfterWishlistsProductUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{WishlistId},{ProductId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchWishlistsProduct([FromODataUri] int keyWishlistId,[FromODataUri] int keyProductId, [FromBody]Delta<Models.Net5Wasmconn.WishlistsProduct> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.WishlistsProducts.Where(i => i.WishlistId == keyWishlistId && i.ProductId == keyProductId);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.WishlistsProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnWishlistsProductUpdated(item);
            this.context.WishlistsProducts.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.WishlistsProducts.Where(i => i.WishlistId == keyWishlistId && i.ProductId == keyProductId);
            Request.QueryString = Request.QueryString.Add("$expand", "Wishlist,Product");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnWishlistsProductCreated(Models.Net5Wasmconn.WishlistsProduct item);
    partial void OnAfterWishlistsProductCreated(Models.Net5Wasmconn.WishlistsProduct item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.WishlistsProduct item)
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

            this.OnWishlistsProductCreated(item);
            this.context.WishlistsProducts.Add(item);
            this.context.SaveChanges();

            var keyWishlistId = item.WishlistId;
            var keyProductId = item.ProductId;

            var itemToReturn = this.context.WishlistsProducts.Where(i => i.WishlistId == keyWishlistId && i.ProductId == keyProductId);

            Request.QueryString = Request.QueryString.Add("$expand", "Wishlist,Product");

            this.OnAfterWishlistsProductCreated(item);

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
