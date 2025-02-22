using HahnAssessmentTask.Core.DTOs;
using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces;
using HahnAssessmentTask.Core.Interfaces.Services;

namespace HahnAssessmentTask.Application.Services
{
  public class StockDailyInformationService(IUnitOfWork unitOfWork) : IStockDailyInformationService
  {
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task UpsertDailyInformation(Stock stock, StockDailyDataDto stockDailyInfo, DateOnly dateNow)
    {
      StockDailyInformation? existingStockDailyInfo = await _unitOfWork.StockDailyInformationRepository.GetDailyInfoAsync(dateNow, stock.Symbol);

      if (existingStockDailyInfo != null)
      {
        UpdateExistingStockDailyInfo(existingStockDailyInfo, stockDailyInfo);
      }
      else
      {
        CreateNewStockDailyInfo(stock, stockDailyInfo, dateNow);
      }

      await _unitOfWork.Commit();
    }

    private void CreateNewStockDailyInfo(Stock stock, StockDailyDataDto stockDailyInfo, DateOnly dateNow)
    {
      _unitOfWork.StockRepository.Attach(stock);

      _unitOfWork.StockDailyInformationRepository.Create(
        new StockDailyInformation()
        {
          Stock = stock,
          StockSymbol = stock.Symbol,
          Date = dateNow,
          Open = stockDailyInfo.Open,
          High = stockDailyInfo.High,
          Low = stockDailyInfo.Low,
          Close = stockDailyInfo.Close,
          Volume = stockDailyInfo.Volume
        });
    }

    private void UpdateExistingStockDailyInfo(StockDailyInformation existingStockDailyInfo, StockDailyDataDto stockDailyInfo)
    {
      existingStockDailyInfo.Open = stockDailyInfo.Open;
      existingStockDailyInfo.High = stockDailyInfo.High;
      existingStockDailyInfo.Low = stockDailyInfo.Low;
      existingStockDailyInfo.Close = stockDailyInfo.Close;
      existingStockDailyInfo.Volume = stockDailyInfo.Volume;

      _unitOfWork.StockDailyInformationRepository.Update(existingStockDailyInfo);
    }
  }
}
