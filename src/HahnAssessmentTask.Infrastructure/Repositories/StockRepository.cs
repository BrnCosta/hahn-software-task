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
  public class StockRepository(AppDbContext context) : BaseRepository<Stock>(context), IStockRepository
  {
  }
}
