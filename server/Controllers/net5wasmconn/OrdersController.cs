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

  [ODataRoutePrefix("odata/net5wasmconn/Orders")]
  [Route("mvc/odata/net5wasmconn/Orders")]
  public partial class OrdersController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public OrdersController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/Orders
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.Order> GetOrders()
    {
      var items = this.context.Orders.AsQueryable<Models.Net5Wasmconn.Order>();
      this.OnOrdersRead(ref items);

      return items;
    }

    partial void OnOrdersRead(ref IQueryable<Models.Net5Wasmconn.Order> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Order> GetOrder(string key)
    {
        var items = this.context.Orders.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnOrderDeleted(Models.Net5Wasmconn.Order item);
    partial void OnAfterOrderDeleted(Models.Net5Wasmconn.Order item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteOrder(string key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Orders
                .Where(i => i.Id == key)
                .Include(i => i.OrdersProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Order>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOrderDeleted(item);
            this.context.Orders.Remove(item);
            this.context.SaveChanges();
            this.OnAfterOrderDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderUpdated(Models.Net5Wasmconn.Order item);
    partial void OnAfterOrderUpdated(Models.Net5Wasmconn.Order item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOrder(string key, [FromBody]Models.Net5Wasmconn.Order newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Orders
                .Where(i => i.Id == key)
                .Include(i => i.OrdersProducts)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Order>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnOrderUpdated(newItem);
            this.context.Orders.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Address");
            this.OnAfterOrderUpdated(newItem);
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
    public IActionResult PatchOrder(string key, [FromBody]Delta<Models.Net5Wasmconn.Order> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Orders.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Order>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnOrderUpdated(item);
            this.context.Orders.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Address");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderCreated(Models.Net5Wasmconn.Order item);
    partial void OnAfterOrderCreated(Models.Net5Wasmconn.Order item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.Order item)
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

            this.OnOrderCreated(item);
            this.context.Orders.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Address");

            this.OnAfterOrderCreated(item);

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
