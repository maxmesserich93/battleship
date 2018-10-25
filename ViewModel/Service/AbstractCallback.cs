using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Service
{
    public abstract class AbstractCallback
    {


        public HandleNewGame GameHandler { set; get; }
        public delegate void HandleNewGame(GameRuleSet set, string opponent);
        public ShotDelegate OpponentShotHandler { set; get; }
        public ShotDelegate PlayerShotHandler { set; get; }
        public delegate void ShotDelegate(FieldPosition[] fieldPositions);
        public delegate void PlayerTurn();
        public PlayerTurn PlayerTurnHandler { set; get; }
        public delegate void PlaceShips();
        public PlaceShips PlaceShipHandler { set; get; }
        public delegate void GameOverDelegate(int yourScore, int opponentScore);
        public GameOverDelegate GameOverHandler { set; get; }
        public delegate void ProvidedIdentity(string id);
        public ProvidedIdentity ProvidedIdentityHandler { set; get; }

        public delegate void PlacementComplete(List<Ship> ships);
        public PlacementComplete PlacementCompleteHandler { set; get; }


        public abstract void CloseClient();
    }
}
