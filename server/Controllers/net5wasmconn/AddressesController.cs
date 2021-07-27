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

  [ODataRoutePrefix("odata/net5wasmconn/Addresses")]
  [Route("mvc/odata/net5wasmconn/Addresses")]
  public partial class AddressesController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public AddressesController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/Addresses
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.Address> GetAddresses()
    {
      var items = this.context.Addresses.AsQueryable<Models.Net5Wasmconn.Address>();
      this.OnAddressesRead(ref items);

      return items;
    }

    partial void OnAddressesRead(ref IQueryable<Models.Net5Wasmconn.Address> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Address> GetAddress(int key)
    {
        var items = this.context.Addresses.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnAddressDeleted(Models.Net5Wasmconn.Address item);
    partial void OnAfterAddressDeleted(Models.Net5Wasmconn.Address item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteAddress(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Addresses
                .Where(i => i.Id == key)
                .Include(i => i.Orders)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Address>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnAddressDeleted(item);
            this.context.Addresses.Remove(item);
            this.context.SaveChanges();
            this.OnAfterAddressDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnAddressUpdated(Models.Net5Wasmconn.Address item);
    partial void OnAfterAddressUpdated(Models.Net5Wasmconn.Address item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutAddress(int key, [FromBody]Models.Net5Wasmconn.Address newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Addresses
                .Where(i => i.Id == key)
                .Include(i => i.Orders)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Address>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnAddressUpdated(newItem);
            this.context.Addresses.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Addresses.Where(i => i.Id == key);
            this.OnAfterAddressUpdated(newItem);
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
    public IActionResult PatchAddress(int key, [FromBody]Delta<Models.Net5Wasmconn.Address> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Addresses.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Address>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnAddressUpdated(item);
            this.context.Addresses.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Addresses.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnAddressCreated(Models.Net5Wasmconn.Address item);
    partial void OnAfterAddressCreated(Models.Net5Wasmconn.Address item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.Address item)
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

            this.OnAddressCreated(item);
            this.context.Addresses.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Net5Wasmconn/Addresses/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
