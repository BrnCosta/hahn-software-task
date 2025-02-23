using HahnAssessmentTask.Application.Services;
using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HahnAssessmentTask.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StockDailyInformationController(IStockDailyInformationService stockDailyInformationService) : ControllerBase
  {
    private readonly IStockDailyInformationService _stockDailyInformationService = stockDailyInformationService;

    [HttpGet]
    public IActionResult GetAllStocks([FromQuery] DateTime date)
    {
      return Ok(_stockDailyInformationService.GetAllDailyInfos(DateOnly.FromDateTime(date)));
    }
  }
}
