using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace FashionHub.Resources.Converters
{
  public class InverseBooleanToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      bool boolValue = (bool)value;
      if (parameter?.ToString() == "False")
        boolValue = !boolValue;

      return boolValue ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var visibility = (Visibility)value;
      bool result = visibility == Visibility.Visible;

      if (parameter?.ToString() == "False")
        result = !result;

      return result;
    }
  }
}
