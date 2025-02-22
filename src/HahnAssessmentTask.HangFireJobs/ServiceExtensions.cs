using HahnAssessmentTask.HangFireJobs.Jobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HahnAssessmentTask.HangFireJobs
{
  public static class ServiceExtensions
  {
    public static void ConfigureHangFireService(this IServiceCollection services, IConfiguration configuration)
    {
      var options = new SqlServerStorageOptions
      {
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(60)
      };

      services.AddHangfire(config => config.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
      services.AddHangfireServer();

      services.AddScoped<AlphaVantageUpsertJob>();
    }
  }
}
