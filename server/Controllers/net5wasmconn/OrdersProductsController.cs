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

  [ODataRoutePrefix("odata/net5wasmconn/OrdersProducts")]
  [Route("mvc/odata/net5wasmconn/OrdersProducts")]
  public partial class OrdersProductsController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public OrdersProductsController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/OrdersProducts
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.OrdersProduct> GetOrdersProducts()
    {
      var items = this.context.OrdersProducts.AsQueryable<Models.Net5Wasmconn.OrdersProduct>();
      this.OnOrdersProductsRead(ref items);

      return items;
    }

    partial void OnOrdersProductsRead(ref IQueryable<Models.Net5Wasmconn.OrdersProduct> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderId},{ProductId}")]
    public SingleResult<OrdersProduct> GetOrdersProduct([FromODataUri] string keyOrderId,[FromODataUri] int keyProductId)
    {
        var items = this.context.OrdersProducts.Where(i=>i.OrderId == keyOrderId && i.ProductId == keyProductId);
        return SingleResult.Create(items);
    }
    partial void OnOrdersProductDeleted(Models.Net5Wasmconn.OrdersProduct item);
    partial void OnAfterOrdersProductDeleted(Models.Net5Wasmconn.OrdersProduct item);

    [HttpDelete("{OrderId},{ProductId}")]
    public IActionResult DeleteOrdersProduct([FromODataUri] string keyOrderId,[FromODataUri] int keyProductId)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.OrdersProducts
                .Where(i => i.OrderId == keyOrderId && i.ProductId == keyProductId)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.OrdersProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOrdersProductDeleted(item);
            this.context.OrdersProducts.Remove(item);
            this.context.SaveChanges();
            this.OnAfterOrdersProductDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrdersProductUpdated(Models.Net5Wasmconn.OrdersProduct item);
    partial void OnAfterOrdersProductUpdated(Models.Net5Wasmconn.OrdersProduct item);

    [HttpPut("{OrderId},{ProductId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOrdersProduct([FromODataUri] string keyOrderId,[FromODataUri] int keyProductId, [FromBody]Models.Net5Wasmconn.OrdersProduct newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.OrdersProducts
                .Where(i => i.OrderId == keyOrderId && i.ProductId == keyProductId)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.OrdersProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOrdersProductUpdated(newItem);
            this.context.OrdersProducts.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.OrdersProducts.Where(i => i.OrderId == keyOrderId && i.ProductId == keyProductId);
            Request.QueryString = Request.QueryString.Add("$expand", "Order,Product");
            this.OnAfterOrdersProductUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{OrderId},{ProductId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOrdersProduct([FromODataUri] string keyOrderId,[FromODataUri] int keyProductId, [FromBody]Delta<Models.Net5Wasmconn.OrdersProduct> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.OrdersProducts.Where(i => i.OrderId == keyOrderId && i.ProductId == keyProductId);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.OrdersProduct>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnOrdersProductUpdated(item);
            this.context.OrdersProducts.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.OrdersProducts.Where(i => i.OrderId == keyOrderId && i.ProductId == keyProductId);
            Request.QueryString = Request.QueryString.Add("$expand", "Order,Product");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrdersProductCreated(Models.Net5Wasmconn.OrdersProduct item);
    partial void OnAfterOrdersProductCreated(Models.Net5Wasmconn.OrdersProduct item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.OrdersProduct item)
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

            this.OnOrdersProductCreated(item);
            this.context.OrdersProducts.Add(item);
            this.context.SaveChanges();

            var keyOrderId = item.OrderId;
            var keyProductId = item.ProductId;

            var itemToReturn = this.context.OrdersProducts.Where(i => i.OrderId == keyOrderId && i.ProductId == keyProductId);

            Request.QueryString = Request.QueryString.Add("$expand", "Order,Product");

            this.OnAfterOrdersProductCreated(item);

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
