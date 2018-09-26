using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models

{
    /// <summary>
    /// The service requires a session representing a game.
    /// The IPlayerContract specifying the callbacks to client
    /// is the CallbackContract of the IGameContract.
    /// </summary>
    [ServiceContract (Namespace = "http://fontysvenlo.org/BattleService", SessionMode = SessionMode.Required,
        CallbackContract = typeof(IPlayerContract))]
    
    public interface IGameContract
    {

        [OperationContract]
        bool Login(string userName);
        /// <summary>
        /// Gets all games which can be joined.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<GameData> GetAvailableGames();
        /// <summary>
        /// Method for joining a joinable game.
        /// </summary>
        /// <param name="name"></param>
        [OperationContract]
        void JoinGame(String playerName, String gameId);

        /// <summary>
        /// Host a game and wait for an additional player.
        /// </summary>
        /// <param name="name"></param>
        [OperationContract]
        void HostGame(String name);

        [OperationContract]
        void ProvideShipPlacements(string player,List<ShipPlacement> shipPlacements);

        [OperationContract]
        void ProvideShotPlacement(string player, Coordinate coordinate);


        /// <summary>
        /// Starts a game with a bot opponent.
        /// </summary>
        /// <param name="name"></param>
        [OperationContract]
        void JoinBotGame(String name);

    }
}
