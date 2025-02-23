using HahnAssessmentTask.Core.DTOs;
using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Enums;
using HahnAssessmentTask.Core.Extensions;
using HahnAssessmentTask.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace HahnAssessmentTask.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StockController(IStockService stockService) : ControllerBase
  {
    private readonly IStockService _stockService = stockService;

    [HttpPost]
    public async Task<IActionResult> AddStock(Stock stock)
    {
      try
      {
        await _stockService.AddStock(stock);
        return Created();
      }
      catch (DuplicateNameException)
      {
        return Conflict("Stock already exists");
      }
      catch (ApplicationException e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public IActionResult AllStocks()
    {
      return Ok(_stockService.GetAllStocks());
    }

    [HttpGet("exchanges")]
    public IActionResult GetStockExchanges()
    {
      var exchanges = Enum.GetValues(typeof(StockExchange))
                          .Cast<StockExchange>()
                          .Select(exchange => new StockExchangeDto
                          {
                            Value = exchange.ToString(),
                            Description = exchange.GetDescription()
                          })
                          .ToList();

      return Ok(exchanges);
    }
  }
}
