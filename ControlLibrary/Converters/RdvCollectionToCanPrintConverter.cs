using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DataLayer.Enums;
using DataLayer.Model;

namespace ControlLibrary.Converters
{
    public class RdvCollectionToCanPrintConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rdvCollection = value as ObservableCollection<RendezVous>;
            return rdvCollection != null && rdvCollection.Any(r=>r.RdvStateValue==RdvState.NotYet);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
