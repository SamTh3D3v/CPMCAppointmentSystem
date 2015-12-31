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
    public class UserTypesCollectionToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var res = "";
            var collection = (value as ICollection<UserType>);
            if (collection != null)
                res = collection.Aggregate(res, (current, ut) => current + (ut.UserTypeName + ", "));
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
