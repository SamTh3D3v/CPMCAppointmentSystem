using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DataLayer.Enums;

namespace ControlLibrary.Converters
{
    public class RdvStateValueToBoolConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return (RdvState) value == RdvState.NotYet;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return ((bool) value) ? RdvState.NotYet : RdvState.Cancelled;
        }
    }
}
