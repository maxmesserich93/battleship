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
            GameService.Callback.GameHandler = _awaitGameRules;
            GameService.Callback.PlaceShipHandler = _awaitPlaceShips;
            GameService.Callback.PlacementCompleteHandler = _awaitPlacementComplete;
            GameService.GameListHandler = (data) => { Games = data; Debug.WriteLine("Received Games: " + data); };


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

        /// <summary>
        /// Invoked when the GameRuleSet for the game is received.
        /// </summary>
        /// <param name="gameRuleSet"></param>
        void _awaitGameRules(GameRuleSet gameRuleSet)
        {
            Debug.WriteLine("LobbyVM _awaitGameRules");
            gameRules = gameRuleSet;
        }

        void _awaitPlaceShips()
        {
            ShipPlacementViewModel = new ShipPlacementViewModel(GameService, gameRules);
            //Tell the window that the viewmodel for shipplacemnts is created.
            OnPropertyChanged(nameof(ShipPlacementViewModel));
        }

      

        void _awaitPlacementComplete(List<Ship> ships)
        {
            Debug.WriteLine("sadddddddddddddddddddddddddddddddddddddddddd AASDASD");
            var field = new Field(gameRules.FieldSize);
            var fieldVm = new FieldViewModel(this, gameRules);
            ships.ForEach(ship => fieldVm.PlaceShip(ship));

            

            GameViewModel = new GameViewModel(fieldVm, this);
            OnPropertyChanged(nameof(GameViewModel));

        } 



    }
}
