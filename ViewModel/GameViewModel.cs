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
        public bool PlayerTurn { set; get; }
        private bool _over;
        public bool GameOver { set { _over = value; OnPropertyChanged(nameof(GameOver)); } get { return _over; } }
        private FieldPosition _hover;

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
            GameService.Callback.PlayerShotHandler = OpponentFieldVM.HandleShotResult;
            GameService.Callback.OpponentShotHandler = PlayerFieldVM.HandleShotResult;

            GameService.Callback.PlayerTurnHandler = OnPlayerTurn;
            //Set ICommands of the FieldViewModel.

            //Set ships set by the player and approved by the GameService.
            placedShips.ForEach(ship => PlayerFieldVM.PlaceShip(ship));
            

            if(OpponentName == null || opponentName.Length == 0)
            {
                throw new Exception("NAME NOT THERE WHAT THE FUCK");
            }


        }

        private void OnPlayerTurn()
        {
            Debug.WriteLine("GameViewModel OnPlayerTurn()");
            PlayerTurn = true;
        }
        private void OpponentFieldClick(FieldPosition position)
        {

            if (PlayerTurn)
            {
                GameService.ProvideShotPlacement(position.Coordinate);

            }


        }
        private void PlayerShot(FieldPosition[] positions)
        {

            PlayerTurn = true;
        }
        private void OpponentShot(FieldPosition[] positions)
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
