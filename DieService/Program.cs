using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace DieService
{
    class Program
    {
        static void Main(string[] args)
        { 

        // Step 1 of the address configuration procedure: Create a URI to serve as the base address.
        Uri baseAddress = new Uri("http://localhost:8000/Service");

        // Step 2 of the hosting procedure: Create ServiceHost
        ServiceHost selfHost = new ServiceHost(typeof(GameService), baseAddress);

            try
            {
                // Step 3 of the hosting procedure: Add a service endpoint.
                selfHost.AddServiceEndpoint(
                    typeof(IGameContract),
                    new WSDualHttpBinding(),
                    "Service");


                // Step 4 of the hosting procedure: Enable metadata exchange.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                // Step 5 of the hosting procedure: Start (and then stop) the service.
                selfHost.Open();
         
                Console.WriteLine("The service is readysdsafsdg dfsg dfg sdfg sdf.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
                Console.ReadLine();
            }

        }
    }
}
