using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("ShoppingCartsProducts", Schema = "dbo")]
  public partial class ShoppingCartsProduct
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
    public int ShoppingCartId
    {
      get;
      set;
    }
    public ShoppingCart ShoppingCart { get; set; }
    [Key]
    public int ProductId
    {
      get;
      set;
    }
    public Product Product { get; set; }
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
    public int Quantity
    {
      get;
      set;
    }
  }
}
