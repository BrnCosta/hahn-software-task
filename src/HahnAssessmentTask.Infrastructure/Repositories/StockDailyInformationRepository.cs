using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HahnAssessmentTask.Infrastructure.Repositories
{
  public class StockDailyInformationRepository(AppDbContext context) 
    : BaseRepository<StockDailyInformation>(context), IStockDailyInformationRepository
  {
    public async Task<StockDailyInformation?> GetDailyInfoAsync(DateOnly date, string stockSymbol)
    {
      return await _context.StockDailyInformations
        .FirstOrDefaultAsync(s => s.Date == date && s.Stock.Symbol == stockSymbol);
    }
  }
}
