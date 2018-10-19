using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GameViewModel : AbstractServiceViewModel, INotifyPropertyChanged
    {
        public FieldViewModel PlayerFieldVM { set; get; }
        public FieldViewModel OpponentFieldVM { set; get; }
        public bool PlayerTurn { set; get; }
        private bool _over;
        public bool GameOver { set { _over = value; OnPropertyChanged(nameof(GameOver)); } get { return _over; } }
        private FieldPosition _hover;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GameViewModel(FieldViewModel playerField, AbstractServiceViewModel vm) : base(vm)
        {
            PlayerFieldVM = playerField;
            OpponentFieldVM = new FieldViewModel(this, PlayerFieldVM.GameRuleSet);
            GameService.Callback.PlayerShotHandler += OpponentFieldVM.HandleShotResult;
            GameService.Callback.OpponentShotHandler += PlayerFieldVM.HandleShotResult;
            GameService.Callback.PlayerTurnHandler = OnPlayerTurn;
            OpponentFieldVM.TileClick = new TypedRelayCommand<FieldPosition>(OpponentFieldClick);
            OpponentFieldVM.TileHover = new TypedRelayCommand<FieldPosition>(Hover); 
        }

        private void OnPlayerTurn()
        {
            //Debug.WriteLine("GameViewModel OnPlayerTurn()");
            PlayerTurn = true;
        }
        private void OpponentFieldClick(FieldPosition position)
        {

            //if (PlayerTurn)
            //{
                GameService.ProvideShotPlacement(position.Coordinate);
                PlayerTurn = false;
            //}


        }

        private void OpponentShot(FieldPosition[] positions)
        {
            PlayerFieldVM.HandleShotResult(positions);

        }

        private void PlayerShot(FieldPosition[] positions)
        {

        }
        private void Hover(FieldPosition position)
        {
            if (_hover != null)
            {
                OpponentFieldVM.Field.Hover(_hover.Coordinate, false);
            }
            OpponentFieldVM.Field.Hover(position.Coordinate, true);
            _hover = position;

        }
    }
}
