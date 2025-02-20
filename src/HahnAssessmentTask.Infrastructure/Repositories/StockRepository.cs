using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HahnAssessmentTask.Infrastructure.Repositories
{
  public class StockRepository(AppDbContext context) : BaseRepository<Stock>(context), IStockRepository
  {
    public async Task<Stock?> GetBySymbolAsync(string symbol)
    {
      return await _context.Stocks
          .Include(s => s.StockDailyInformations)
          .FirstOrDefaultAsync(s => s.Symbol == symbol);
    }
  }
}
