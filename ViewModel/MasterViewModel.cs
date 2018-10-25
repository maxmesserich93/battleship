using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Lobby;
using ViewModel.Service;

namespace ViewModel
{
    /// <summary>
    /// Responsible for handling the callback methods of the IPlayerContract in order to start, setup and play games.
    /// 
    /// </summary>
    public class MasterViewModel : AbstractServiceViewModel, INotifyPropertyChanged
    {
        private AbstractServiceViewModel _viewModel;
        private GameRuleSet _gameRuleSet;
        private string _opponentName;
        public event PropertyChangedEventHandler PropertyChanged;

        public AbstractServiceViewModel CurrentViewModel
        {
            set
            {
                _viewModel = value;
                NotifyPropertyChanged(nameof(CurrentViewModel));
            }
            get
            {
                return _viewModel;
            }
        }

        public MasterViewModel(AbstractGameServiceViewModel gameService) : base(gameService)
        {
            CurrentViewModel = new LobbyViewModel(gameService);

            gameService.Callback.GameHandler = _onGameRules;
            gameService.Callback.PlaceShipHandler = _onPlaceShips;
            gameService.Callback.PlacementCompleteHandler = _onPlacementComplete;
            gameService.Callback.GameOverHandler = _onGameOver;

        }
        /// <summary>
        /// Invoked when the GameRuleSet for the game is received.
        /// </summary>
        private void _onGameRules(GameRuleSet gameRuleSet, string opponentName)
        {
            _gameRuleSet = gameRuleSet;
            _opponentName = opponentName;
        }
        /// <summary>
        /// Open ShipPlacementPage.
        /// </summary>
        private void _onPlaceShips()
        {
            CurrentViewModel = new ShipPlacementViewModel(GameService, _gameRuleSet);

        }

        /// <summary>
        /// Open GamePage when GameService has approved the ShipPlacements.
        /// </summary>
        /// <param name="ships"></param>
        void _onPlacementComplete(List<Ship> ships)
        {
            CurrentViewModel = new GameViewModel(_gameRuleSet, ships, _opponentName, this);

        }

        private void _onGameOver(int yourScore, int opponentScore)
        {
            CurrentViewModel = new GameOverViewModel(yourScore, opponentScore, _opponentName, this);
        }


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
