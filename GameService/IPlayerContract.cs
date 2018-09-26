using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GameService
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
        [OperationContract(IsOneWay = true)]
        void StartGame(GameRuleSet gameRuleSet);
        /// <summary>
        /// Tells the player to place ships.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void PlaceShips();

        /// <summary>
        /// Tells the player to place a shot.
        /// </summary>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void PlaceShot();

        /// <summary>
        /// Tells the player that the game is over and provided the name of the player who won.
        /// </summary>
        /// <param name="winner"></param>
        [OperationContract(IsOneWay = true)]
        void GameOver(string winner);
    }
}
