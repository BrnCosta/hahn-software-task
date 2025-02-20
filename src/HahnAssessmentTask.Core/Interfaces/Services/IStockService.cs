using HahnAssessmentTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Core.Interfaces.Services
{
  public interface IStockService
  {
    Task AddStock(Stock stock);
  }
}
