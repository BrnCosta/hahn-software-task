using HahnAssessmentTask.HangFireJobs.Jobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HahnAssessmentTask.HangFireJobs
{
  public static class ServiceExtensions
  {
    private static readonly string ConnectionString =
      $"Server={Environment.GetEnvironmentVariable("DATABASE_SERVER")};" +
      $"Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};" +
      $"User Id={Environment.GetEnvironmentVariable("DATABASE_USER")};" +
      $"Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")};" +
      $"TrustServerCertificate=True;";

    public static void ConfigureHangFireService(this IServiceCollection services)
    {
      var options = new SqlServerStorageOptions
      {
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(60)
      };

      services.AddHangfire(config => config.UseSqlServerStorage(ConnectionString));
      services.AddHangfireServer();

      services.AddScoped<AlphaVantageUpsertJob>();
    }
  }
}
