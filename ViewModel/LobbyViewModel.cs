using ConsoleApp1;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LobbyViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Game Client for network.
        /// </summary>
        public GameClient<RemoteCallback> Client { get; set; }
        /// <summary>
        /// Contains all joinable games on the server.
        /// </summary>
        List<GameData> _games;
        public List<GameData> Games
        {
            get { return _games; }
            private set
            {
                _games = value;
                OnPropertyChanged(nameof(Games));
            }
        }
            /// <summary>
            /// Selected Game
            /// </summary>
            GameData _data;

        public event PropertyChangedEventHandler PropertyChanged;
       
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameData SelectedGame { get { return _data; } set{ _data = value;  OnPropertyChanged(nameof(SelectedGame)); OnPropertyChanged(nameof(GameSelected)); } }
        
        public bool GameSelected { get { return SelectedGame != null; } }

        public LobbyViewModel(GameClient<RemoteCallback> client)
        {

            Client = client;
            UpdateGameList();
            
        }

        public void UpdateGameList()
        {
           Games =  Client.Server.GetAvailableGames().ToList();
        }

        public ShipPlacementViewModel Join()
        {
            Client.SubContract = new RemoteCallback();
            Client.Server.JoinGame(Client.Name, SelectedGame.ID);
            var sd = new RemoteGameService(Client.Server);


            //var vmBase = new

            return null;




        }
        public ShipPlacementViewModel JoinBot()
        {
            //Client.SubContract = new ClientWrapper();
            //Client.Server.JoinBotGame(Client.Name);
            var c = new LocalGameServiceVM();
            var b = new LocalCallback();
            return null;
            //return new ShipPlacementViewModel(c,b);




        }





    }
}
