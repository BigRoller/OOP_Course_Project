using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FashionHub.Resources.Converters
{
  public class FavoriteIconConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool)value
          ? new BitmapImage(new Uri("pack://application:,,,/Resources/icons/filled_heart.png"))
          : new BitmapImage(new Uri("pack://application:,,,/Resources/icons/heart.png"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
