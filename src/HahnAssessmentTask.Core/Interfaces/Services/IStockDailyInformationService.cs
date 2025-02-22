using HahnAssessmentTask.Core.DTOs;
using HahnAssessmentTask.Core.Entities;

namespace HahnAssessmentTask.Core.Interfaces.Services
{
  public interface IStockDailyInformationService
  {
    Task UpsertDailyInformation(Stock stock, StockDailyDataDto stockDailyInfo, DateOnly dateNow);
  }
}
