using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1
{
    [ValueConversion(typeof(Uri), typeof(String))]
    public class AddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Uri)
            {
                return value.ToString();
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {



            Trace.WriteLine("CONVERT: " + value+ " - " +value.GetType());
            string input = (string)value;
            if (input == null)
            {
                return null;
            }
            UriBuilder builder = new UriBuilder("http", input + "/Service/Service");
            //UriBuilder builder = new UriBuilder(input);
            string addressString = builder.ToString();


            bool validAddress = Uri.IsWellFormedUriString(addressString, UriKind.Absolute);
            if (validAddress)
            {
                return builder.Uri;
            }
            return null;


        }
    }
}
