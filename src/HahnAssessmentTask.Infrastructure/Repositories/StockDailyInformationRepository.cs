using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;

namespace HahnAssessmentTask.Infrastructure.Repositories
{
  public class StockDailyInformationRepository(AppDbContext context) 
    : BaseRepository<StockDailyInformation>(context), IStockDailyInformationRepository
  {
  }
}
