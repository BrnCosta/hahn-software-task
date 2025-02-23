using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Core.Extensions
{
  public static class EnumExtensions
  {
    public static string GetDescription(this Enum value)
    {
      FieldInfo? field = value.GetType().GetField(value.ToString());
      if (field is not null)
      {
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
      }
      return value.ToString();
    }
  }
}
