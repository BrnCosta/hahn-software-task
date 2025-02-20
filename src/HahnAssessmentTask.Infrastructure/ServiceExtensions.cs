using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;
using HahnAssessmentTask.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HahnAssessmentTask.Infrastructure
{
  public static class ServiceExtensions
  {
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<AppDbContext>(
        opt => opt.UseSqlServer(
          configuration.GetConnectionString("DefaultConnection"),
          db => db.MigrationsAssembly("HahnAssessmentTask.API")
        )
      );

      // Repositories
      services.AddScoped<IStockRepository, StockRepository>();
      services.AddScoped<IStockDailyInformationRepository, StockDailyInformationRepository>();
    }
  }
}
