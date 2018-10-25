using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Lobby;
using ViewModel.Service;

namespace ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string PlayerName { get; set; }
        string _serverAddress = "localhost:8000";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ShipPlacementViewModel ShipPlacementViewModel { set; get; }
        public LobbyViewModel LobbyViewModel { set; get; }
        public AbstractCallback Callback { set; get; }
        public AbstractGameServiceViewModel GameService { set; get; }
        public MasterViewModel MasterViewModel{set;get;}

        public string ServerAddressString { get {
                return _serverAddress;
            } set { _serverAddress = value; } }

       public LoginViewModel()
        {
            PlayerName = "Player";





        }


        public Boolean CanConnect()
        {
            return false;

        }


        public void Connect()
        {
            //var b = new LocalGameServiceVM(new LocalCallback(), "max");
            var b = new RemoteGameService(PlayerName);
            GameService = b;
            GameService.Login(PlayerName);
            GameService.IdentityHandler += _createMasterViewModel;


        }

        public void LocalConnect()
        {
            var b = new LocalGameServiceVM(new LocalCallback(), PlayerName);
            GameService = b;
            GameService.Login(PlayerName);
            GameService.IdentityHandler += _createMasterViewModel;
            
        }

        private void _onLogin(string u)
        {
            Debug.WriteLine(" CREATING LOBBY VM!");
            LobbyViewModel= new LobbyViewModel(GameService);
            OnPropertyChanged(nameof(LobbyViewModel));
        }

        private void _createMasterViewModel(string id)
        {
            MasterViewModel = new MasterViewModel(GameService);

            OnPropertyChanged(nameof(MasterViewModel));
        }









    }
}
