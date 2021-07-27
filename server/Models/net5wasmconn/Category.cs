using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net5Wasm.Models.Net5Wasmconn
{
  [Table("Categories", Schema = "dbo")]
  public partial class Category
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

    public IEnumerable<Product> Products { get; set; }
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
    public string ImageSource
    {
      get;
      set;
    }
  }
}
