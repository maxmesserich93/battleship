using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// Abstract class for wrapping a GameService which may be defined by implementing the service reference Contract or the IGameContract if local.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractGameService : IGameContract
    {

        public IPlayerContract Player { set; get; }
        public abstract List<GameData> GetAvailableGames();
        public abstract void HostGame(string name);
        public abstract void JoinBotGame(string name);
        public abstract void JoinGame(string playerName, string gameId);
        public abstract bool Login(string userName);
        public abstract void ProvideShipPlacements(string player, List<ShipPlacement> shipPlacements);
        public abstract void ProvideShotPlacement(string player, Coordinate coordinate);
    }
}
