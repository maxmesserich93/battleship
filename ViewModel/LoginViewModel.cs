using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LoginViewModel
    {
        public string PlayerName { get; set; }
        string _serverAddress = "localhost:8000";


        public AbstractCallback Callback { set; get; }
        public AbstractGameService GameService { set; get; }

        public string ServerAddressString { get {
                return _serverAddress;
            } set { _serverAddress = value; } }

       public LoginViewModel()
        {
            PlayerName = "Max";
        }


        public Boolean CanConnect()
        {
            return false;

        }
    }
}
