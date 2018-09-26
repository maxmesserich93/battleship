using DieClientApp.DieServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieClientApp
{
    public class CallbackHandler: IDieServiceCallback
    {
        public void IKnowToFindYou(string message)
        {
            Console.WriteLine("Ok, you got me!!!!");
            Console.WriteLine("I'll display your message!!!!");
            Console.WriteLine("Here it is: " + message);
        }
    }
}
