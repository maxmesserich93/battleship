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

        /// <summary>
        /// Tells the player that the game begins.
        /// </summary>
        /// <param name="gameRuleSet"></param>
        /// 
        [OperationContract(IsOneWay = true)]
        void StartGame(GameRuleSet gameRuleSet);
        /// <summary>
        /// Tells the player to place ships.
        /// </summary>
        //[OneWay]
        [OperationContract]
        ShipPlacement[] RequestShipPlacement();

        [OperationContract]
        List<string> Turd();

        ///// <summary>
        ///// Tells the player to place a shot.
        ///// </summary>
        ///// <returns></returns>
        //[OneWay]
        [OperationContract]
        Coordinate RequestShotPlacement();
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
        void OpponentShot(List<FieldPosition> fieldPositions);

        /// <summary>
        /// Tells the player whether his shot missed, hit or killed.
        /// </summary>
        /// <param name="fieldPositionStatuses"></param>
        [OperationContract(IsOneWay = true)]
        void ShotResult(List<FieldPosition> fieldPositions);

        ///// <summary>
        ///// Tells the player that the game is over and provided the name of the player who won.
        ///// </summary>
        ///// <param name="winner"></param>
        //[OneWay]
        [OperationContract(IsOneWay =true)]
        void GameOver(string winner);
        [OperationContract(IsOneWay = true)]
        void Error(string message);
    }
}
