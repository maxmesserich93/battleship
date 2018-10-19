using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModel.Service
{
    public class LocalCallback : AbstractCallback, IPlayerContract
    {

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void GameOver(string winner)
        {
            base.GameOverHandler?.Invoke(winner);
        }

        public void OpponentShot(Coordinate coordinate, List<FieldPosition> fieldPositions)
        {
            base.OpponentShotHandler?.Invoke(fieldPositions.ToArray());
        }

        public void PlaceShips()
        {
            base.PlaceShipHandler?.Invoke();
        }

        public Ship[] RequestShipPlacement()
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

        public void ShotResult(Coordinate coordinate, List<FieldPosition> fieldPositions)
        {
            if(fieldPositions != null)
            {
                base.PlayerShotHandler?.Invoke(fieldPositions.ToArray());

            }
        }

        public void GameRules(GameRuleSet gameRuleSet)
        {
            Debug.WriteLine("LocalCallback.StartGame: "+GameHandler);
       

            base.GameHandler?.Invoke(gameRuleSet);
        }

        public List<string> Turd()
        {
            throw new NotImplementedException();
        }

        public void ProvideIdentity(string uuid)
        {
            base.ProvidedIdentityHandler.Invoke(uuid);
        }



        public void PlacementComplete(List<Ship> yourShips)
        {
            base.PlacementCompleteHandler?.Invoke(yourShips);
        }

        public override void CloseClient()
        {
            throw new NotImplementedException();
        }
    }
}
