using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("ShoppingCarts", Schema = "dbo")]
  public partial class ShoppingCart
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
    public string UserId
    {
      get;
      set;
    }
  }
}
