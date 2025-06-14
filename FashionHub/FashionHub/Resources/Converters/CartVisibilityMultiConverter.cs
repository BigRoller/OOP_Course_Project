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
  public class CartVisibilityMultiConverter : IMultiValueConverter
  {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {

      if (values[0] is int count && values[1] is bool isLoggedIn)
      {
        return (count > 0 && isLoggedIn) ? Visibility.Visible : Visibility.Collapsed;
      }

      return Visibility.Collapsed;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
