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
        private Days days;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.days = (Days) value;
            return (((Days) value).HasFlag((Days) parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            this.days ^= (Days)parameter;    //<-- ما جأتم به السحر
            return this.days;
           // return (bool)value ? (decimal)((Days)parameter) : -1 * (decimal)((Days)parameter);     
        }
    }
}
