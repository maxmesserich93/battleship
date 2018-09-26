using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ViewModel
{
    public class AddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = (string)value;

            UriBuilder builder = new UriBuilder("http", input + "/Service/Service");

            string addressString = builder.ToString();


            bool validAddress = Uri.IsWellFormedUriString(addressString, UriKind.Absolute);
            if (!validAddress)
            {
               return builder.Uri;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Uri)
            {
                return value.ToString();
            }
            return "invalid";
        }
    }
}
