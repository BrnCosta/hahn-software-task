using HahnAssessmentTask.Core.Entities;

namespace HahnAssessmentTask.Core.Interfaces.Repositories
{
  public interface IStockRepository : IBaseRepository<Stock>
  {
    Task<Stock?> GetBySymbolAsync(string symbol);
  }
}
