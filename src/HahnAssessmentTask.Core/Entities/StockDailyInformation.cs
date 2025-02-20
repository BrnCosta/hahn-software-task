namespace HahnAssessmentTask.Core.Entities
{
  public class StockDailyInformation
  {
    public required DateTime Date { get; set; }
    public required double Open { get; set; }
    public required double Close { get; set; }
    public required double High { get; set; }
    public required double Low { get; set; }
    public required double Volume { get; set; }

    // Relationships
    public required string StockSymbol { get; set; }
    public required Stock Stock { get; set; }
  }
}
