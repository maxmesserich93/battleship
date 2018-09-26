using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp1
{
    public class AddressValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;

            UriBuilder builder = new UriBuilder("http", input + "/Service/Service");

            string addressString = builder.ToString();


            bool validAddress = Uri.IsWellFormedUriString(addressString, UriKind.Absolute);
            if (!validAddress)
            {
                return new ValidationResult(false, "The provided address is not valid. Format: <address:port>!");
            }

            return ValidationResult.ValidResult;

        }
    }
}
