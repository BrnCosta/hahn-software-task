using HahnAssessmentTask.HangFireJobs;
using HahnAssessmentTask.HangFireJobs.Jobs;
using HahnAssessmentTask.Infrastructure;
using Hangfire;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.ConfigurePersistenceApp(builder.Configuration);

builder.Services.ConfigureHangFireService(builder.Configuration);

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
  var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

  try
  {
    var recurringJob = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    recurringJob.AddOrUpdate<AlphaVantageUpsertJob>(
      "AlphaVantageUpsertJob",
      job => job.ExecuteAsync(),
      Cron.Hourly  
    );
  }
  catch (Exception ex)
  {
    logger.LogError(ex, "An error occurred configuring HangFire service.");
  }
}

host.Run();
