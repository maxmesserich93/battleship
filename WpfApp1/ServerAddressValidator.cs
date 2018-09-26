using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Validator for the server adress.
    /// Checks whether the adress is valid.
    /// Checks whether the service can be found at the provided address.
    /// </summary>
    public class ServerAddressValidator : ValidationRule
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

            //Uri address = null;



            //Uri.TryCreate(input, UriKind.Absolute, out address);

            //if(address == null)
            //{
            
            //}
            //InstanceContext instanceContext;

            //ServiceReference1.GameContractClient server = null;

            //try
            //{
            //    ///instanceContext needs a reference to the server
            //    instanceContext = new InstanceContext(new CallbackContractImplementation());

            //    server = new ServiceReference1.GameContractClient(instanceContext);
            //    //server.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(0.5);
            //    EndpointAddress address = new EndpointAddress("http://localhost:8000/Service/Service");
            //    server.Endpoint.Address = address;



            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}


            return ValidationResult.ValidResult;

        }
    }
}
