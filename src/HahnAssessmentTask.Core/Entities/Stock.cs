using System.Text.Json.Serialization;

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
