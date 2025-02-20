using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces;
using HahnAssessmentTask.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Application.Services
{
  public class StockService(IUnitOfWork unitOfWork) : IStockService
  {
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddStock(Stock stock)
    {
      var existingStock = await _unitOfWork.StockRepository.GetBySymbolAsync(stock.Symbol);

      if(existingStock != null)
      {
        throw new DuplicateNameException("Stock already exists");
      }

      try
      {
        _unitOfWork.StockRepository.Create(stock);

        await _unitOfWork.Commit();
      }
      catch (Exception e)
      {
        throw new ApplicationException($"Failed to insert stock item '{stock.Symbol}'. Details: {e.Message}", e);
      }
    }
  }
}
