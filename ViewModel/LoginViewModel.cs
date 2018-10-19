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
            b.Login(PlayerName);
            b.IdentityHandler += _onLogin;
            GameService = b;


        }

        public void LocalConnect()
        {
            var b = new LocalGameServiceVM(new LocalCallback(), PlayerName);
            b.Login(PlayerName);
            b.IdentityHandler += _onLogin;
            GameService = b;
        }

        private void _onLogin(string u)
        {
            Debug.WriteLine(" CREATING LOBBY VM!");
            LobbyViewModel= new LobbyViewModel(GameService);
            OnPropertyChanged(nameof(LobbyViewModel));
        }
        /// <summary>
        /// Creates a ShipPlacementViewModel for a local game.
        /// </summary>
        /// <param name="vm"></param>
        public ShipPlacementViewModel CreateLocalShipPlacementVM()
        {
            var ca = new LocalCallback();
            //var b = new LocalGameServiceVM(ca, PlayerName);

            var b = new RemoteGameService("max");
            //Callback = ca;

            //GameService = b;
            //b.Player = ca;


            b.Callback.GameHandler = _awaitGameRules;
            if(PlayerName == null || PlayerName.Length == 0)
            {
                throw new ArgumentNullException("BAD NAME");
            }
            b.JoinBotGame();
            return null;
            //var model = new 
        }

        /// <summary>
        /// Invoked when the GameRuleSet for the game is received.
        /// </summary>
        /// <param name="gameRuleSet"></param>
        void _awaitGameRules(GameRuleSet gameRuleSet)
        {
            Debug.WriteLine("LoginViewModel _awaitGameRules");
            ShipPlacementViewModel = new ShipPlacementViewModel(GameService, gameRuleSet);
            //Tell the window that the viewmodel for shipplacemnts is created.
            OnPropertyChanged(nameof(ShipPlacementViewModel));


            //ShipPlacementWindow next = new ShipPlacementWindow(ShipPlacementVm);
            //next.Show();
            //this.Close();


        }





    }
}
