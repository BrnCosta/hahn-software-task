using HahnAssessmentTask.Core.Entities;

namespace HahnAssessmentTask.Core.Interfaces.Services
{
  public interface IStockService
  {
    Task AddStock(Stock stock);

    IEnumerable<Stock> GetAllStocks();
  }
}
