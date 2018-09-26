using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    class AIPlayer : IPlayerContract
    {
        string Name { get; set; }
        IGameContract GameContract { set; get; }
        GameRuleSet _gameRuleSet;
        Field _opponentField;


        public AIPlayer(IGameContract gameContract, string name)
        {
            Name = name;
            GameContract = gameContract;
        }

        List<Coordinate> _shot = new List<Coordinate>();
        public void Error(string message)
        {

        }

        public void GameOver(string winner)
        {
            
        }

        public void OpponentShot(Coordinate coordinate)
        {
            
        }

        public ShipPlacement[] RequestShipPlacement()
        {
            List<ShipPlacement> legalPlacement = new List<ShipPlacement>();

            legalPlacement.Add(new ShipPlacement(new Carrier(), true, new Coordinate(0, 0)));
            legalPlacement.Add(new ShipPlacement(new Cruiser(), true, new Coordinate(1, 0)));
            legalPlacement.Add(new ShipPlacement(new BattleShip(), true, new Coordinate(2, 0)));
            legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(3, 0)));
            legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(4, 0)));
            legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(5, 0)));
            legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(6, 0)));
            return legalPlacement.ToArray();

        }
        Random random = new Random();
        public Coordinate RequestShotPlacement()
        {
            
            Coordinate c = new Coordinate(random.Next(0, _gameRuleSet.FieldSize), random.Next(0, _gameRuleSet.FieldSize));
            if (_shot.Contains(c))
            {
                return RequestShotPlacement();
            }
            _shot.Add(c);
            return c;

        }

        public void ShotResult(List<FieldPosition> fieldPositionStatuses)
        {
        }

        public void StartGame(GameRuleSet gameRuleSet)
        {
            _gameRuleSet = gameRuleSet;
            _opponentField = new Field(_gameRuleSet.FieldSize);
        }

        public List<string> Turd()
        {
            var a = new List<string>();
            a.Add("ASD");
            return a;
        }

        public void Shoot()
        {
            GameContract.ProvideShotPlacement(Name, RequestShotPlacement());
        }

        public void PlaceShips()
        {
            GameContract.ProvideShipPlacements(Name, RequestShipPlacement().ToList());
        }

        public void OpponentShot(List<FieldPosition> fieldPositions)
        {
    
        }
    }
}
