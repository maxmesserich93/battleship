using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.ServiceReference2;
using Models;

namespace ViewModel
{
    public class RemoteCallback : AbstractCallback, ConsoleApp1.ServiceReference2.IGameContractCallback
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

        public void OpponentShot(FieldPosition[] fieldPositions)
        {
            base.OpponentShotHandler(fieldPositions);
        }

        public void PlaceShips()
        {
            base.PlaceShipHandler.Invoke();
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
            base.PlayerTurnHandler.Invoke();
        }

        public void ShotResult(List<FieldPosition> fieldPositions)
        {
            base.PlayerShotHandler.Invoke(fieldPositions.ToArray());
        }

        public void ShotResult(FieldPosition[] fieldPositions)
        {
            base.PlayerShotHandler.Invoke(fieldPositions);
        }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            base.GameHandler.Invoke(gameRuleSet);
        }

        public List<string> Turd()
        {
            throw new NotImplementedException();
        }

        string[] IGameContractCallback.Turd()
        {
            throw new NotImplementedException();
        }
    }
}
