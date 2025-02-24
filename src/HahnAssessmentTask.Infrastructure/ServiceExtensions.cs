using HahnAssessmentTask.Application.Services;
using HahnAssessmentTask.Core.Interfaces;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Core.Interfaces.Services;
using HahnAssessmentTask.Infrastructure.Context;
using HahnAssessmentTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HahnAssessmentTask.Infrastructure
{
  public static class ServiceExtensions
  {
    private static readonly string ConnectionString = 
      $"Server={Environment.GetEnvironmentVariable("DATABASE_SERVER")};" +
      $"Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};" +
      $"User Id={Environment.GetEnvironmentVariable("DATABASE_USER")};" +
      $"Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")};" +
      $"TrustServerCertificate=True;";

    public static void ConfigurePersistenceApp(this IServiceCollection services)
    {
      services.AddDbContext<AppDbContext>(
        opt => opt.UseSqlServer(ConnectionString,
          db => db.MigrationsAssembly("HahnAssessmentTask.API")
        )
      );

      // Repositories
      services.AddScoped<IStockRepository, StockRepository>();
      services.AddScoped<IStockDailyInformationRepository, StockDailyInformationRepository>();

      // Services
      services.AddScoped<IStockService, StockService>();
      services.AddScoped<IStockDailyInformationService, StockDailyInformationService>();

      // Unit of Work
      services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
  }
}
