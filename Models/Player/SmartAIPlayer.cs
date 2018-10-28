using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public class SmartAIPlayer : AIPlayer
    {
        public SmartAIPlayer(IGame gameContract) : base(gameContract)
        {
        }

        /// <summary>
        /// Contains for each hit the directions which where explored.
        /// When all directions are explored, the key is removed from this list.
        /// </summary>


        private List<Coordinate> _shotOptions = new List<Coordinate>();

        public override void ShotResultSub(Coordinate shot, List<FieldPosition> fieldPositionStatuses)
        {



            _shotOptions.Remove(shot);
            //Shot hit but not killed.
            if (fieldPositionStatuses.First().FieldPositionStatus == FieldPositionStatus.ShotHit)
            {

                //Get the neightbours of the position which have not been shot and have not been added as options.
                var neighbours = shot.GetNeighbours(_gameRuleSet.FieldSize);
                var unshotNeighbours = shot.GetNeighbours(_gameRuleSet.FieldSize)
                    .Where(c => _CanShot(c))
                    .Where(c => !_shotOptions.Contains(c)).ToList();
                
                unshotNeighbours.ForEach(c => _shotOptions.Add(c));

            }
            else if (fieldPositionStatuses.Count > 1)
            {

                fieldPositionStatuses.ForEach(c => _shotOptions.Remove(c.Coordinate));

            }
           



        }

        public override Coordinate RequestShotPlacement()
        {
            Coordinate shot;
            
            if (_shotOptions==null || _shotOptions.Count == 0)
            {
                shot = RandomShot();
            }
            else
            {
                //Debug.WriteLine("Options: ");
                //_shotOptions.ForEach(option => Debug.Write(option + " | "));
                //Debug.WriteLine("");
                shot = _shotOptions[0];
                
            }
            return shot;

        }

        protected override List<Ship> ShipPlacements()
        {
            return _gameRuleSet.ShipTypeRules.ToList().Select(keyValue =>
            {
                var tmpList = new List<Ship>();
                Ship ship;
                for (int i = 0; i < keyValue.Value; i++)
                {
                    do
                    {
                        var position = RandomShot();
                        var vertical = (random.Next() % 2) == 0;
                        ship = ShipFactory.CREATE_SHIP(keyValue.Key, position, vertical);
                    } while (!_field.ShipPlaceable(ship));
                    Debug.WriteLine(" -------------------- "+ship);
                    _field.PlaceShip(ship);
                    tmpList.Add(ship);
                }
                return tmpList;

            }).SelectMany(lists => lists).ToList();
        }
    }
}
