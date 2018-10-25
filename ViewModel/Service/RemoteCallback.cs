using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;
using ConsoleApp1.ServiceReference1;
using Models;

namespace ViewModel.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class RemoteCallback : AbstractCallback, IGameContractCallback
    {
        public override void CloseClient()
        {
            
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void GameOver(int yourScore, int opponentScore)
        {
            base.GameOverHandler(yourScore, opponentScore);
        }

        public void GameRules(GameRuleSet gameRuleSet, string opponent)
        {
            base.GameHandler?.Invoke(gameRuleSet, opponent);
        }

        public void OpponentShot(Coordinate coordinate, List<FieldPosition> fieldPositions)
        {
            base.OpponentShotHandler(fieldPositions.ToArray());
        }

        public void OpponentShot(Coordinate coordinate, FieldPosition[] fieldPositions)
        {
            base.OpponentShotHandler(fieldPositions);
        }

        public void PlacementComplete(Ship[] yourShips)
        {
            base.PlacementCompleteHandler?.Invoke(yourShips.ToList());
        }

        public void PlaceShips()
        {
            base.PlaceShipHandler?.Invoke();
        }

        public void ProvideIdentity(string uuid)
        {
            base.ProvidedIdentityHandler(uuid);
        }

        public void Shoot()
        {
            base.PlayerTurnHandler?.Invoke();
        }

        public void ShotResult(Coordinate coordinate, List<FieldPosition> fieldPositions)
        {
            base.PlayerShotHandler?.Invoke(fieldPositions.ToArray());
        }

        public void ShotResult(Coordinate coordinate, FieldPosition[] fieldPositions)
        {
            base.PlayerShotHandler?.Invoke(fieldPositions);
        }



    }
}
