using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModel
{
    public class LocalCallback : AbstractCallback, IPlayerContract
    {
        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void GameOver(string winner)
        {
            //base.Ga
        }

        public void OpponentShot(List<FieldPosition> fieldPositions)
        {
            base.OpponentShotHandler(fieldPositions.ToArray());
        }

        public void PlaceShips()
        {
            base.PlaceShipHandler?.Invoke();
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
            base.PlayerTurnHandler?.Invoke();
        }

        public void ShotResult(List<FieldPosition> fieldPositions)
        {
            base.PlayerShotHandler?.Invoke(fieldPositions.ToArray());
        }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            Debug.WriteLine("LocalCallback.StartGame: "+GameHandler);
       

            base.GameHandler?.Invoke(gameRuleSet);
        }

        public List<string> Turd()
        {
            throw new NotImplementedException();
        }
    }
}
