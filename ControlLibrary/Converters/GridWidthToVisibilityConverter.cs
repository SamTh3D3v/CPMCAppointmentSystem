using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ControlLibrary.Converters
{
    public class GridWidthToVisibilityConverter:IMultiValueConverter
    {    

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;
            var rootGridWidth=int.Parse(values[0].ToString());
            var menuGridWidth=int.Parse(values[1].ToString());
            var logoImageWidth=int.Parse(values[2].ToString());
            return (rootGridWidth < menuGridWidth+logoImageWidth) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
