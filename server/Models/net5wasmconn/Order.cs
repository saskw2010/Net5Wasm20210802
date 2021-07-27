using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("Orders", Schema = "dbo")]
  public partial class Order
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
    public string Id
    {
      get;
      set;
    }

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
    public string UserId
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int DeliveryAddressId
    {
      get;
      set;
    }
    public Address Address { get; set; }
  }
}
