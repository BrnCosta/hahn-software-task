using HahnAssessmentTask.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Core.Interfaces
{
  public interface IUnitOfWork
  {
    IStockRepository StockRepository { get; }
    IStockDailyInformationRepository StockDailyInformationRepository { get; }
    Task Commit();
  }
}
