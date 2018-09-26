using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public class LocalPlayer : IPlayerContract
    {
        public GameLogic GameInstance { set; get; }
        public event OpponentShotDelegate OpponentShotEvent;
        public delegate void OpponentShotDelegate(GameLogic sender, List<FieldPosition> fieldPosition);
        public event PlayerTurnDelegate PlayerTurnEvent;
        public delegate void PlayerTurnDelegate(GameLogic sender);
        public event ShotResultDeleage ShotResultEvent;
        public delegate void ShotResultDeleage(GameLogic sender, List<FieldPosition> fieldPositions);

        public event PlaceShipDelegate PlaceShipsEvent;
        public delegate void PlaceShipDelegate(GameLogic sender);
        public event StartGameDelegate StartGameEvent;
        public delegate void StartGameDelegate(GameLogic sender);
        public event GameOverDelegate GameOverEvent;
        public delegate void GameOverDelegate(GameLogic sender, string winner);



        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void GameOver(string winner)
        {
            GameOverEvent(GameInstance,winner);
        }

        public void OpponentShot(List<FieldPosition> fieldPositions)
        {
            OpponentShotEvent(GameInstance, fieldPositions);
        }

        public void PlaceShips()
        {
            PlaceShipsEvent(GameInstance);
        }

        public ShipPlacement[] RequestShipPlacement()
        {
            throw new NotImplementedException();
        }

        public Coordinate RequestShotPlacement()
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            PlayerTurnEvent(GameInstance);
        }

        public void ShotResult(List<FieldPosition> fieldPositions)
        {
            ShotResultEvent(GameInstance, fieldPositions);
        }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            StartGameEvent(GameInstance);
        }

        public List<string> Turd()
        {
            throw new NotImplementedException();
        }
    }
}
