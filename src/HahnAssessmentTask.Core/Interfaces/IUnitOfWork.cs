using HahnAssessmentTask.Core.Interfaces.Repositories;

namespace HahnAssessmentTask.Core.Interfaces
{
  public interface IUnitOfWork
  {
    IStockRepository StockRepository { get; }
    IStockDailyInformationRepository StockDailyInformationRepository { get; }
    Task Commit();
  }
}
