using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Models.Player { 

    public class StubPlayer : IPlayerContract
    {

        public Field OpponentRepresentation { get; set; }

        public void PlaceShips() { }
        public Coordinate PlaceShot(Field field) { return null; }

        public void GameRules(GameRuleSet gameRuleSet, string opponent)
        {
            //throw new NotImplementedException();
        }

        public Ship[] RequestShipPlacement()
        {
            return null;
        }

        public Coordinate RequestShotPlacement()
        {
            return null;
        }

        public void OpponentShot(Coordinate coordinate)
        {
  
        }

        public void ShotResult(Coordinate shot, List<FieldPosition> fieldPositionStatuses)
        {
        }

        public void GameOver(int yourScore, int opponentScore)
        {
        }

        public void Error(string message)
        {
            
        }


        public void Shoot()
        {

        }

        public void OpponentShot(Coordinate shot, List<FieldPosition> fieldPositions)
        {
        }

        public void ProvideIdentity(string uuid)
        {
            //throw new NotImplementedException();
        }

        public void PlacementComplete()
        {
            //throw new NotImplementedException();
        }

        public void PlacementComplete(List<Ship> yourShips)
        {

        }
        //private Field 
        //public Field OpponentRepresentation { get ; set => throw new NotImplementedException(); }


    }
}
