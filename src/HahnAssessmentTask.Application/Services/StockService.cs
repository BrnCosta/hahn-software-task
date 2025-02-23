using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Enums;
using HahnAssessmentTask.Core.Interfaces;
using HahnAssessmentTask.Core.Interfaces.Services;
using System.Data;

namespace HahnAssessmentTask.Application.Services
{
  public class StockService(IUnitOfWork unitOfWork) : IStockService
  {
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task AddStock(Stock stock)
    {
      if (!Enum.TryParse<StockExchangeEnum>(stock.StockExchange, ignoreCase: true, out _))
      {
        throw new ArgumentException($"Stock exchange '{stock.StockExchange}' is invalid.");
      }

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

    public IEnumerable<Stock> GetAllStocks()
    {
      return _unitOfWork.StockRepository.GetAll();
    }
  }
}
