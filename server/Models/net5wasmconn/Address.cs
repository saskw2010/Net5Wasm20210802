using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("Addresses", Schema = "dbo")]
  public partial class Address
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

    public IEnumerable<Order> Orders { get; set; }
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
    public string Country
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string State
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string City
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
    public string PostalCode
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string PhoneNumber
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
