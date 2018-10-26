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

        public void GameOver(int yourScore, int opponentScore)
        {
            base.GameOverHandler?.Invoke(yourScore, opponentScore);
        }

        public void OpponentShot(Coordinate coordinate, List<FieldPosition> fieldPositions)
        {
            base.OpponentShotHandler?.Invoke(fieldPositions.ToArray());
        }

        public new void PlaceShips()
        {
            base.PlaceShipHandler?.Invoke();
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

        public void GameRules(GameRuleSet gameRuleSet, string opponent)
        {
            Debug.WriteLine("-===================== LocalCallback.StartGame: "+GameHandler+", OPPONENT: " + opponent);
       

            base.GameHandler?.Invoke(gameRuleSet, opponent);
        }


        public void ProvideIdentity(string uuid)
        {
            base.ProvidedIdentityHandler.Invoke(uuid);
        }



        public new void PlacementComplete(List<Ship> yourShips)
        {
            
            base.PlacementCompleteHandler?.Invoke(yourShips);
        }

        public override void CloseClient()
        {

        }
    }
}
