using HahnAssessmentTask.Core.Interfaces;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Infrastructure
{
  public class UnitOfWork(AppDbContext context, IStockRepository stockRepository, IStockDailyInformationRepository stockDailyInformationRepository) 
    : IUnitOfWork, IDisposable
  {
    private readonly AppDbContext _context = context;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IStockDailyInformationRepository _stockDailyInfoRepository = stockDailyInformationRepository;

    public IStockRepository StockRepository { get { return _stockRepository; } }
    public IStockDailyInformationRepository StockDailyInformationRepository { get { return _stockDailyInfoRepository; } }

    public async Task Commit()
    {
      await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }
  }
}
