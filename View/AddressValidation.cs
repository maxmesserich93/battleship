using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace View
{
    public class AddressValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Debug.WriteLine("VALIDATING: " + value + " TYPE " + value.GetType());

            string input = (string)value.ToString();

            //UriBuilder builder = new UriBuilder(input);

            //string addressString = builder.ToString();


            bool validAddress = Uri.IsWellFormedUriString(input, UriKind.Absolute);

            if (!validAddress)
            {
                if (!input.StartsWith("http://"))
                {

                    string tmp = "http://" + input;
                    if (Uri.IsWellFormedUriString(tmp, UriKind.Absolute))
                    {
                        return ValidationResult.ValidResult;
                       
                    }
                }
                return new ValidationResult(false, "Provided a valid address (host:port)");
            }
            return ValidationResult.ValidResult;

            //if (!validAddress)
            //{

            //    Debug.WriteLine("ASD");


            //    //return new ValidationResult(false, "The provided address is not valid. Format: <address:port>!");
            //}


            //if (validIp)
            //{

            //}




        }
        }
    }


