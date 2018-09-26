using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DieClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make sure callbacks can take place
            // Construct InstanceContext to handle messages on callback interface
            InstanceContext instanceContext = new InstanceContext(new CallbackHandler());

            Console.WriteLine("This is a Die Service Client");
            Console.WriteLine("Asks server to throw a die...");
            Console.WriteLine("And the value is...");

            DieServiceReference.DieServiceClient proxy = new DieServiceReference.DieServiceClient(instanceContext);
            int newDieValue = proxy.Roll();
            Console.WriteLine(newDieValue);

            Console.WriteLine( "Via the Die itself...");
            DieServiceReference.Die die = proxy.getTheDie();
            Console.WriteLine("Die value is " + die.DieValue);
            Console.WriteLine("Again " + die.ToString());

            proxy.Register("It is Richard here!");

            Console.ReadLine();
        }
    }
}
