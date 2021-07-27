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

  [ODataRoutePrefix("odata/net5wasmconn/Wishlists")]
  [Route("mvc/odata/net5wasmconn/Wishlists")]
  public partial class WishlistsController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public WishlistsController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/Wishlists
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.Wishlist> GetWishlists()
    {
      var items = this.context.Wishlists.AsQueryable<Models.Net5Wasmconn.Wishlist>();
      this.OnWishlistsRead(ref items);

      return items;
    }

    partial void OnWishlistsRead(ref IQueryable<Models.Net5Wasmconn.Wishlist> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Wishlist> GetWishlist(int key)
    {
        var items = this.context.Wishlists.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnWishlistDeleted(Models.Net5Wasmconn.Wishlist item);
    partial void OnAfterWishlistDeleted(Models.Net5Wasmconn.Wishlist item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteWishlist(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Wishlists
                .Where(i => i.Id == key)
                .Include(i => i.WishlistsProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Wishlist>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnWishlistDeleted(item);
            this.context.Wishlists.Remove(item);
            this.context.SaveChanges();
            this.OnAfterWishlistDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnWishlistUpdated(Models.Net5Wasmconn.Wishlist item);
    partial void OnAfterWishlistUpdated(Models.Net5Wasmconn.Wishlist item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutWishlist(int key, [FromBody]Models.Net5Wasmconn.Wishlist newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Wishlists
                .Where(i => i.Id == key)
                .Include(i => i.WishlistsProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Wishlist>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnWishlistUpdated(newItem);
            this.context.Wishlists.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Wishlists.Where(i => i.Id == key);
            this.OnAfterWishlistUpdated(newItem);
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
    public IActionResult PatchWishlist(int key, [FromBody]Delta<Models.Net5Wasmconn.Wishlist> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Wishlists.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Wishlist>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnWishlistUpdated(item);
            this.context.Wishlists.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Wishlists.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnWishlistCreated(Models.Net5Wasmconn.Wishlist item);
    partial void OnAfterWishlistCreated(Models.Net5Wasmconn.Wishlist item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.Wishlist item)
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

            this.OnWishlistCreated(item);
            this.context.Wishlists.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Net5Wasmconn/Wishlists/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
