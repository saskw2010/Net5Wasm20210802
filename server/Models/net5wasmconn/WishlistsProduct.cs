using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("WishlistsProducts", Schema = "dbo")]
  public partial class WishlistsProduct
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
    public int WishlistId
    {
      get;
      set;
    }
    public Wishlist Wishlist { get; set; }
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
  }
}
