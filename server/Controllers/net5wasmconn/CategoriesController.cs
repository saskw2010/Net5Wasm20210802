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

  [ODataRoutePrefix("odata/net5wasmconn/Categories")]
  [Route("mvc/odata/net5wasmconn/Categories")]
  public partial class CategoriesController : ODataController
  {
    private Data.Net5WasmconnContext context;

    public CategoriesController(Data.Net5WasmconnContext context)
    {
      this.context = context;
    }
    // GET /odata/Net5Wasmconn/Categories
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Net5Wasmconn.Category> GetCategories()
    {
      var items = this.context.Categories.AsQueryable<Models.Net5Wasmconn.Category>();
      this.OnCategoriesRead(ref items);

      return items;
    }

    partial void OnCategoriesRead(ref IQueryable<Models.Net5Wasmconn.Category> items);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Category> GetCategory(int key)
    {
        var items = this.context.Categories.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnCategoryDeleted(Models.Net5Wasmconn.Category item);
    partial void OnAfterCategoryDeleted(Models.Net5Wasmconn.Category item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteCategory(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Categories
                .Where(i => i.Id == key)
                .Include(i => i.Products)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Category>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnCategoryDeleted(item);
            this.context.Categories.Remove(item);
            this.context.SaveChanges();
            this.OnAfterCategoryDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryUpdated(Models.Net5Wasmconn.Category item);
    partial void OnAfterCategoryUpdated(Models.Net5Wasmconn.Category item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutCategory(int key, [FromBody]Models.Net5Wasmconn.Category newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Categories
                .Where(i => i.Id == key)
                .Include(i => i.Products)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Category>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnCategoryUpdated(newItem);
            this.context.Categories.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Categories.Where(i => i.Id == key);
            this.OnAfterCategoryUpdated(newItem);
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
    public IActionResult PatchCategory(int key, [FromBody]Delta<Models.Net5Wasmconn.Category> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Categories.Where(i => i.Id == key);

            items = EntityPatch.ApplyTo<Models.Net5Wasmconn.Category>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnCategoryUpdated(item);
            this.context.Categories.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Categories.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryCreated(Models.Net5Wasmconn.Category item);
    partial void OnAfterCategoryCreated(Models.Net5Wasmconn.Category item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Net5Wasmconn.Category item)
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

            this.OnCategoryCreated(item);
            this.context.Categories.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Net5Wasmconn/Categories/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
