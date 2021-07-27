using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("Products", Schema = "dbo")]
  public partial class Product
  {
    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("@odata.etag")]
    public string ETag
    {
        get;
        set;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id
    {
      get;
      set;
    }

    public IEnumerable<ShoppingCartsProduct> ShoppingCartsProducts { get; set; }
    public IEnumerable<WishlistsProduct> WishlistsProducts { get; set; }
    public IEnumerable<OrdersProduct> OrdersProducts { get; set; }
    [ConcurrencyCheck]
    public DateTime CreatedOn
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? ModifiedOn
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public bool IsDeleted
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime? DeletedOn
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Name
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string Description
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string ImageSource
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int Quantity
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public decimal Price
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int CategoryId
    {
      get;
      set;
    }
    public Category Category { get; set; }
  }
}
