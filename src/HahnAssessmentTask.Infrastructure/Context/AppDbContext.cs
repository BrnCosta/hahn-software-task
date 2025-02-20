using HahnAssessmentTask.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Infrastructure.Context
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockDailyInformation> StockDailyInformation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Stock>().HasKey(e => e.Symbol);
      modelBuilder.Entity<StockDailyInformation>().HasKey(e => new { e.StockSymbol, e.Date });
    }
  }
}
