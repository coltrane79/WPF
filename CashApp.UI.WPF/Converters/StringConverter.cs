using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CashApp.UI.WPF.Converters
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return value.ToString();
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string retValue = (string)value;
            switch (targetType.FullName)
            {
                case "int":
                    return int.Parse(retValue);
                case "decimal":
                    return decimal.Parse(retValue);
                case "DateTime":
                    return DateTime.Parse(retValue);
                default:
                    return retValue;
            }                                                                                                    
        }
    }
}
