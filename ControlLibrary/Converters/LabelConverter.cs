using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ControlLibrary.ChartModel;
using Syncfusion.UI.Xaml.Charts;

namespace ControlLibrary.Converters
{
internal class LabelConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        ChartAdornment pieAdornment = value as ChartAdornment;
        if (pieAdornment != null)
            return String.Format((pieAdornment.Item as EntityPerFieldCountModel).Field + " : " + pieAdornment.YData);
        return null;
    }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value;
    }

}
}
