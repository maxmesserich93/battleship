using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.field;

namespace ViewModel
{
    public class GameViewModel : AbstractServiceViewModel, INotifyPropertyChanged
    {
        public FieldViewModel PlayerFieldVM { set; get; }
        public FieldViewModel OpponentFieldVM { set; get; }
        public string OpponentName { get; }

        private bool _playerTurn;
        public bool PlayerTurn {
            set {
                _playerTurn = value; OnPropertyChanged(nameof(PlayerTurn));
            }
            get { return _playerTurn; } }
        private bool _over;
        public bool GameOver { set { _over = value; OnPropertyChanged(nameof(GameOver)); } get { return _over; } }
        private FieldPosition _hover;


        public Command OnLoadCommand { get; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GameViewModel(GameRuleSet gameRuleSet, List<Ship> placedShips, string opponentName, AbstractServiceViewModel vm) : base(vm)
        {
            OpponentName = opponentName;
            PlayerFieldVM = new FieldViewModel(this, gameRuleSet);
            OpponentFieldVM = new FieldViewModel(OpponentFieldClick, Hover, Unhover, this, gameRuleSet);

            //Set Shotresult delegates of the callback to interact with the FieldViewModels
            GameService.Callback.PlayerShotHandler += OpponentFieldVM.HandleShotResult;
            GameService.Callback.OpponentShotHandler += PlayerFieldVM.HandleShotResult;

            GameService.Callback.PlayerTurnHandler = PlayerShot;

            GameService.Callback.PlayerShotHandler += ShotReceived;

            //Set ICommands of the FieldViewModel.

            //Set ships set by the player and approved by the GameService.
            placedShips.ForEach(ship => PlayerFieldVM.PlaceShip(ship));
            //Tell server that the player is ready to receive game commands
            OnLoadCommand = new Command(() => GameService.PlayerReady());

        }

        private void OnPlayerTurn()
        {

            PlayerTurn = true;
        }
        private void OpponentFieldClick(FieldPosition position)
        {

            if (PlayerTurn)
            {
                GameService.ProvideShotPlacement(position.Coordinate);

            }


        }
        private void PlayerShot()
        {

            PlayerTurn = true;
        }
        private void ShotReceived(FieldPosition[] positions)
        {
            PlayerTurn = false;
        }

        private void Hover(FieldPosition position)
        {

            OpponentFieldVM.HighlightCoordinate(position.Coordinate);
            _hover = position;

        }

        private void Unhover(FieldPosition position)
        {

            OpponentFieldVM.UnhighlightCoordinate(position.Coordinate);
            //throw new Exception("ASDASDASDd");
        }
    }
}
