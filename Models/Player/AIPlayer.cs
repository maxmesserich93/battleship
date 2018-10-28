using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public abstract class AIPlayer : IPlayerContract
    {
        int count = 0;
        public string Identidy { get; set; }
        IGame GameContract { set; get; }
        protected IList<Coordinate> _shots;
        protected Dictionary<Coordinate, List<FieldPosition>> _shotResults;
        List<List<FieldPosition>> shotResults { set; get; }
        protected GameRuleSet _gameRuleSet;
        //Field _opponentField;

        private Dictionary<int, HashSet<int>> _unshotPositions = new Dictionary<int, HashSet<int>>();
        public AIPlayer(IGame game)
        {
            GameContract = game;
            _shots = new List<Coordinate>();
            _shotResults = new Dictionary<Coordinate, List<FieldPosition>>();

            

        }

        List<Coordinate> _shot = new List<Coordinate>();
        public void Error(string message)
        {

        }

        public void GameOver(int yourScore, int opponentScore)
        {
            
        }

        public void OpponentShot(Coordinate coordinate, List<FieldPosition> fieldPositionStatuses)
        {
            
        }

        public static Ship[] SimplePlacement()
        {
            List<Ship> legalPlacement = new List<Ship>();

            legalPlacement.Add(new Carrier(new Coordinate(0, 0), true));

            legalPlacement.Add(new Cruiser(new Coordinate(1, 0), true));

            legalPlacement.Add(new BattleShip(new Coordinate(2, 0), true));

            legalPlacement.Add(new Destroyer(new Coordinate(3, 0), true));
            legalPlacement.Add(new Destroyer(new Coordinate(4, 0), true));

            legalPlacement.Add(new Submarine(new Coordinate(5, 0), true));
            legalPlacement.Add(new Submarine(new Coordinate(6, 0), true));
            return legalPlacement.ToArray();

        }

        

        public Coordinate RandomShot()
        {

            var options = _unshotPositions.SelectMany(keyPair => keyPair.Value.Select(y => new Coordinate(y, keyPair.Key))).ToList();

            var index = random.Next(options.Count);

            return options[index];


            //Coordinate c = new Coordinate(random.Next(0, _gameRuleSet.FieldSize), random.Next(0, _gameRuleSet.FieldSize));
            //if (_shot.Any(o => o.X==c.X && o.Y ==c.Y))
            //{
            //    return RandomShot();
            //}
            //Debug.WriteLine("RandomShot: " + c);
            //_shot.ForEach(shot => Debug.Write(shot + " "));
            //return c;
        }

        Random random = new Random();


        public abstract Coordinate RequestShotPlacement();




        public void GameRules(GameRuleSet gameRuleSet, string oppoenent)
        {
            _gameRuleSet = gameRuleSet;
            for (int x = 0; x < gameRuleSet.FieldSize; x++)
            {
                HashSet<int> collumn = new HashSet<int>();
                for(int y=0; y < gameRuleSet.FieldSize; y++)
                {
                    collumn.Add(y);
                }
                _unshotPositions.Add(x, collumn);
            }
        }

        public void Shoot()
        {

            GameContract.Shoot(Identidy, RequestShotPlacement());
        }

        public void PlaceShips()
        {
            List<Ship> legalPlacement = SimplePlacement().ToList();
            GameContract.PlaceShips(Identidy, legalPlacement);
            
        }

        public void OpponentShot(List<FieldPosition> fieldPositions)
        {
    
        }

        public void ProvideIdentity(string uuid)
        {

            Identidy = uuid;
            Debug.WriteLine(" -------------------------------------AI PLAYER RECEIVED ID: " + uuid);


        }

        public void PlacementComplete()
        {
            //throw new NotImplementedException();
        }

        public abstract void ShotResultSub(Coordinate position, List<FieldPosition> fieldPositions);

        protected bool _CanShot(Coordinate position)
        {
            return _unshotPositions.ContainsKey(position.Y) && _unshotPositions[position.Y].Contains(position.X);
        }

        public void ShotResult(Coordinate position, List<FieldPosition> fieldPositions)
        {
            //Debug.WriteLine("AI SHOTRESULT: " + position);
            //fieldPositions.ForEach(p => Debug.Write(p +" "));
            //Debug.WriteLine("");
            HashSet<int> collumn = null;
            _unshotPositions.TryGetValue(position.Y, out collumn);
            if(collumn != null)
            {
                collumn.Remove(position.X);
                if(collumn.Count == 0)
                {
                    _unshotPositions.Remove(position.Y);
                }
            }


            //_unshotPositions.ToList().ForEach(rowCollumn => {

            //    rowCollumn.Value.ToList().ForEach(x => Debug.Write(x + " "));
            //    Debug.WriteLine("");
            //    });

            _shots.Add(position);
            //_shotResults.Add(position, fieldPositions);
            ShotResultSub(position, fieldPositions);
        }

        public void PlacementComplete(List<Ship> yourShips)
        {
            GameContract.PlayerReady(Identidy);
        }
    }
}
