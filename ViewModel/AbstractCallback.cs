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
    public abstract class AbstractCallback
    {
        private IPlayerContract _playerContract;

        public HandleNewGame GameHandler { set; get; }
        public delegate void HandleNewGame(GameRuleSet set);
        public ShotDelegate OpponentShotHandler { set; get; }
        public ShotDelegate PlayerShotHandler { set; get; }
        public delegate void ShotDelegate(FieldPosition[] fieldPositions);
        public delegate void PlayerTurn();
        public PlayerTurn PlayerTurnHandler { set; get; }
        public delegate void PlaceShips();
        public PlaceShips PlaceShipHandler { set; get; }
    }
}
