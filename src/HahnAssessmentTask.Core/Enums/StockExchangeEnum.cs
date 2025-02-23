using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Core.Enums
{
  public enum StockExchangeEnum
  {
    [Description("United States")]
    US,

    [Description("London Stock Exchange")]
    LON,

    [Description("Toronto Stock Exchange")]
    TRT,

    [Description("Toronto Venture Exchange")]
    TRV,

    [Description("XETRA (Germany)")]
    DEX,

    [Description("Bombay Stock Exchange")]
    BSE,

    [Description("Shanghai Stock Exchange")]
    SHH,

    [Description("Shenzhen Stock Exchange")]
    SHZ,
  }
}
