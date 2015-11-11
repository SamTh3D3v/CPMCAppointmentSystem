using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DataLayer.Model;


namespace ControlLibrary.Converters
{
    public class FlagToIsCheckedConverter:IValueConverter
    {
        private Days _days;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this._days = (Days) value;
            return (((Days) value).HasFlag((Days) parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this._days ^= (Days)parameter;    
            return this._days;        
        }
    }
}
