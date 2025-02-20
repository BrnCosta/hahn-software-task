using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
