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

  [ODataRoutePrefix("odata/net5wasmconn/ShoppingCarts")]
  [Route("mvc/odata/net5wasmconn/ShoppingCarts")]
  public partial class ShoppingCartsController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public ShoppingCartsController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/ShoppingCarts
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.ShoppingCart> GetShoppingCarts()
    {
      var items = this.context.ShoppingCarts.AsQueryable<Models.Net5Wasmconn.ShoppingCart>();
      this.OnShoppingCartsRead(ref items);

      return items;
    }

    partial void OnShoppingCartsRead(ref IQueryable<Models.Net5Wasmconn.ShoppingCart> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<ShoppingCart> GetShoppingCart(int key)
    {
        var items = this.context.ShoppingCarts.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnShoppingCartDeleted(Models.Net5Wasmconn.ShoppingCart item);
    partial void OnAfterShoppingCartDeleted(Models.Net5Wasmconn.ShoppingCart item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteShoppingCart(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.ShoppingCarts
                .Where(i => i.Id == key)
                .Include(i => i.ShoppingCartsProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.ShoppingCart>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnShoppingCartDeleted(item);
            this.context.ShoppingCarts.Remove(item);
            this.context.SaveChanges();
            this.OnAfterShoppingCartDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnShoppingCartUpdated(Models.Net5Wasmconn.ShoppingCart item);
    partial void OnAfterShoppingCartUpdated(Models.Net5Wasmconn.ShoppingCart item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutShoppingCart(int key, [FromBody]Models.Net5Wasmconn.ShoppingCart newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.ShoppingCarts
                .Where(i => i.Id == key)
                .Include(i => i.ShoppingCartsProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.ShoppingCart>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnShoppingCartUpdated(newItem);
            this.context.ShoppingCarts.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.ShoppingCarts.Where(i => i.Id == key);
            this.OnAfterShoppingCartUpdated(newItem);
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
    public IActionResult PatchShoppingCart(int key, [FromBody]Delta<Models.Net5Wasmconn.ShoppingCart> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.ShoppingCarts.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.ShoppingCart>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnShoppingCartUpdated(item);
            this.context.ShoppingCarts.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.ShoppingCarts.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnShoppingCartCreated(Models.Net5Wasmconn.ShoppingCart item);
    partial void OnAfterShoppingCartCreated(Models.Net5Wasmconn.ShoppingCart item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.ShoppingCart item)
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

            this.OnShoppingCartCreated(item);
            this.context.ShoppingCarts.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Net5Wasmconn/ShoppingCarts/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
