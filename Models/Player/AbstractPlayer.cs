using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Models.Player
{
    [DataContract]
    public class AbstractPlayer : IPlayerContract
    {
        private PlayerData _playerData;
        [DataMember]
        public PlayerData PlayerData { get { return _playerData;  } set => _playerData = value; }
        public AbstractPlayer()
        {
            _playerData = new PlayerData();
        }
        public Field OpponentRepresentation { get; set; }

        public void PlaceShips() { }
        public Coordinate PlaceShot(Field field) { return null; }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            throw new NotImplementedException();
        }

        public ShipPlacement[] RequestShipPlacement()
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

        public void ShotResult(List<FieldPosition> fieldPositionStatuses)
        {
        }

        public void GameOver(string winner)
        {
        }

        public void Error(string message)
        {
            
        }

        public List<string> Turd()
        {
            var a = new List<string>();
            a.Add("ASD");
            return a;
        }

        public void Shoot()
        {

        }

        public void OpponentShot(List<FieldPosition> fieldPositions)
        {
        }
        //private Field 
        //public Field OpponentRepresentation { get ; set => throw new NotImplementedException(); }


    }
}
