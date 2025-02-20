using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Core.Entities
{
  public class Stock
  {
    public required string Symbol { get; set; }
    public required double Price { get; set; }
    public required string StockExchange { get; set; }

    // Relationships
    [JsonIgnore]
    public ICollection<StockDailyInformation> StockDailyInformations { get; } = [];
  }
}
