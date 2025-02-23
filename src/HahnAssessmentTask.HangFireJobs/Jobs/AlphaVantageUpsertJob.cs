using HahnAssessmentTask.Core.DTOs;
using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Services;
using Hangfire;
using Newtonsoft.Json.Linq;

namespace HahnAssessmentTask.HangFireJobs.Jobs
{
  public class AlphaVantageUpsertJob(IStockService stockService, IStockDailyInformationService stockDailyInformationService)
  {
    private readonly string _AlphaVantageUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY";

    private readonly IStockService _stockService = stockService;
    private readonly IStockDailyInformationService _stockDailyInformationService = stockDailyInformationService;

    private readonly HttpClient _httpClient = new();

    public async Task ExecuteAsync()
    {
      using (var connection = JobStorage.Current.GetConnection())
      {
        using (var lockHandle = connection.AcquireDistributedLock("AlphaVantageUpsertJobLock", TimeSpan.FromHours(1)))
        {
          IEnumerable<Stock> stocks = _stockService.GetAllStocks();
          DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

          foreach (var stock in stocks)
          {
            StockDailyDataDto? stockDailyInfo = await GetStockDailyInfo(stock);

            if (stockDailyInfo == null)
              continue;

            await _stockDailyInformationService.UpsertDailyInformation(stock, stockDailyInfo, dateNow);
          }
        }
      }
    }

    private async Task<StockDailyDataDto?> GetStockDailyInfo(Stock stock)
    {
#if DEBUG
      var apiKey = "demo";
#else
      var apiKey = Environment.GetEnvironmentVariable("ALPHA_VANTAGE_API_KEY");
#endif
      string symbolParameter;

      if (string.Equals(stock.StockExchange.ToString(), "US", StringComparison.OrdinalIgnoreCase))
      {
        symbolParameter = stock.Symbol;
      }
      else
      {
        symbolParameter = $"{stock.Symbol}.{stock.StockExchange}";
      }

      var requestUrl = $"{_AlphaVantageUrl}&symbol={symbolParameter}&apikey={apiKey}";

      var response = await _httpClient.GetAsync(requestUrl);

      if (!response.IsSuccessStatusCode)
        return null;

      var content = await response.Content.ReadAsStringAsync();

      return ParseStockDailyInformation(content);
    }

    private static StockDailyDataDto? ParseStockDailyInformation(string jsonContent)
    {
      var jsonObject = JObject.Parse(jsonContent);

      var metaData = jsonObject["Meta Data"];
      if (metaData == null)
        return null;

      string? lastRefreshed = metaData["3. Last Refreshed"]?.ToString();
      if (string.IsNullOrWhiteSpace(lastRefreshed))
        return null;

      if (!DateOnly.TryParse(lastRefreshed, out DateOnly date))
        return null;

      var timeSeries = jsonObject["Time Series (Daily)"];
      if (timeSeries == null)
        return null;

      var dailyData = timeSeries[date.ToString("yyyy-MM-dd")];

      if (dailyData == null)
        return null;

      return new StockDailyDataDto
      {
        Open = Convert.ToDouble(dailyData["1. open"]),
        High = Convert.ToDouble(dailyData["2. high"]),
        Low = Convert.ToDouble(dailyData["3. low"]),
        Close = Convert.ToDouble(dailyData["4. close"]),
        Volume = Convert.ToDouble(dailyData["5. volume"]),
      };
    }
  }
}
