
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {


        public static void Main(string[] args)
        {

            CallbackContractImplementation i = new CallbackContractImplementation("turd");

            i.Server.JoinBotGame("turd");
            while (true) { }
          
        }
    }
}
