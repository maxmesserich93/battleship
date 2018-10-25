using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// The contract for players which allows the server to send data to clients.
    /// </summary>
    public interface IPlayerContract
    {

        [OperationContract(IsOneWay =true)]
        void ProvideIdentity(String uuid);
        
        //String Identity { set; get; }
        /// <summary>
        /// Tells the player that the game begins.
        /// </summary>
        /// <param name="gameRuleSet"></param>
        /// 
        [OperationContract(IsOneWay = true)]
        void GameRules(GameRuleSet gameRuleSet, string opponentName);

        [OperationContract(IsOneWay = true)]
        void PlacementComplete(List<Ship> yourShips);
        /// <summary>
        /// 
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Shoot();
        /// <summary>
        /// 
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void PlaceShips();

        /// <summary>
        /// Tells the player the Coordinate of a opponents shot.
        /// </summary>
        /// <param name="coordinate"></param>
        [OperationContract(IsOneWay = true)]
        void OpponentShot(Coordinate position, List<FieldPosition> fieldPositions);

        /// <summary>
        /// Tells the player whether his shot missed, hit or killed.
        /// </summary>
        /// <param name="fieldPositionStatuses"></param>
        [OperationContract(IsOneWay = true)]
        void ShotResult(Coordinate position, List<FieldPosition> fieldPositions);

        ///// <summary>
        ///// Tells the player that the game is over and provided the name of the player who won.
        ///// </summary>
        ///// <param name="winner"></param>
        //[OneWay]
        [OperationContract(IsOneWay =true)]
        void GameOver(int yourScore, int opponentScore);
        [OperationContract(IsOneWay = true)]
        void Error(string message);
    }
}
