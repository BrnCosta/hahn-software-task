using HahnAssessmentTask.Core.Entities;
using HahnAssessmentTask.Core.Interfaces.Repositories;
using HahnAssessmentTask.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Infrastructure.Repositories
{
  public class StockDailyInformationRepository(AppDbContext context) 
    : BaseRepository<StockDailyInformation>(context), IStockDailyInformationRepository
  {
  }
}
