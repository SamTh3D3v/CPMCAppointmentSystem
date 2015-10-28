using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;

namespace ControlLibrary.Converters
{
    public class DateJourFerieToNumberConverter:IValueConverter
    {
        private DateTime date;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            date = ((DateTime) value);
            if (parameter.ToString() == "True")
            {               
                return ((DateTime) value).Month-1;                
            }
            else
            {                
                return ((DateTime) value).Day;                
            }
                       
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString()=="True")
            {
                date = new DateTime(date.Year, (int)value+1,date.Day);                
            }
            else
            {
                date = new DateTime(date.Year, date.Month, int.Parse(value.ToString()));                
            }
            return date;
        }
    }
}
