using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Service;

namespace ViewModel.Lobby
{
    public class LobbyViewModel : AbstractServiceViewModel,INotifyPropertyChanged
    {

        /// <summary>
        /// Contains all joinable games on the server.
        /// </summary>
        List<GameInformation> _games;
        public ShipPlacementViewModel ShipPlacementViewModel { set; get; }
        public GameViewModel GameViewModel { set; get; }
        private GameRuleSet gameRules;

        private Command _loadedCommand;
        public Command LoadedCommand { get { return _loadedCommand; } }

        public List<GameInformation> Games
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
            GameInformation _data;

        public LobbyViewModel(AbstractGameServiceViewModel gameService) : base(gameService)
        {
            //GameService.Callback.GameHandler = _awaitGameRules;
            //GameService.Callback.PlaceShipHandler = _awaitPlaceShips;
            //GameService.Callback.PlacementCompleteHandler = _awaitPlacementComplete;
            GameService.GameListHandler = (data) => { Games = data; Debug.WriteLine("Received Games: " + data); };
            _loadedCommand = new Command(() => UpdateGameList());

        }

        public event PropertyChangedEventHandler PropertyChanged;
       
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameInformation SelectedGame { get { return _data; } set{ _data = value;  OnPropertyChanged(nameof(SelectedGame)); OnPropertyChanged(nameof(GameSelected)); } }
        
        public bool GameSelected { get { return SelectedGame != null; } }



        public void UpdateGameList()
        {
           base.GameService.GetAvailableGames();
        }

        public void Join()
        {
            base.GameService.JoinGame(SelectedGame.ID);
        
        }

        public void HostGame()
        {
            base.GameService.HostGame();
        }

        public void JoinBot()
        {
            base.GameService.JoinBotGame();


        }
    }
}
