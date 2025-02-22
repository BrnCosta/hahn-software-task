using HahnAssessmentTask.Core.Entities;

namespace HahnAssessmentTask.Core.Interfaces.Repositories
{
  public interface IStockDailyInformationRepository : IBaseRepository<StockDailyInformation>
  {
    Task<StockDailyInformation?> GetDailyInfoAsync(DateOnly date, string stockSymbol);
  }
}
