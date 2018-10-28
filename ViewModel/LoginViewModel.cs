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
        private string _playerName;
        public string PlayerName {
            get
            {
                return _playerName;
            }
            set
            {
                _playerName = value;
                OnPropertyChanged(nameof(PlayerName));
                ConnectCommand?.RaiseCanExecuteChanged();
            }
        }
        string _serverAddress = "localhost:8000";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ShipPlacementViewModel ShipPlacementViewModel { set; get; }
        public AbstractCallback Callback { set; get; }
        public AbstractGameServiceViewModel GameService { set; get; }
        public MasterViewModel MasterViewModel{set;get;}

        public Command ConnectCommand { get; }
        public Command LocalConnectCommand { get; }
        public string ServerAddressString { get {
                return _serverAddress;
            } set { _serverAddress = value; } }

       public LoginViewModel()
        {


            LocalConnectCommand = new Command(() => LocalConnect());

            ConnectCommand = new Command(() => PlayerName != null && PlayerName.Length > 0, () => Connect());


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


        private void _createMasterViewModel(string id)
        {
            MasterViewModel = new MasterViewModel(GameService);

            OnPropertyChanged(nameof(MasterViewModel));
        }









    }
}
