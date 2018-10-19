using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Player
{
    public class SimpleAIPlayer : AIPlayer
    {
        public SimpleAIPlayer(IGame gameContract) : base(gameContract)
        {
        }
        public override void ShotResultSub(Coordinate shot, List<FieldPosition> fieldPositionStatuses)
        {
            _shots.Add(shot);
            _shotResults.Add(shot, fieldPositionStatuses);
        }
        public override Coordinate RequestShotPlacement()
        {
            return RandomShot();

        }
    }
}
