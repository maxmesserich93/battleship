using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.ServiceReference2;
using Models;

namespace ViewModel
{
    class RemoteGameService : AbstractGameService
    {
        ConsoleApp1.ServiceReference2.IGameContract Service;

        public RemoteGameService(ConsoleApp1.ServiceReference2.IGameContract _service)
        {
            Service = _service;
        }

        public override List<GameData> GetAvailableGames()
        {
            return Service.GetAvailableGames().ToList();
        }

        public override void HostGame(string name)
        {
            Service.HostGame(name);
        }

        public override void JoinBotGame(string name)
        {
            Service.JoinBotGame(name);
        }

        public override void JoinGame(string playerName, string gameId)
        {
            Service.JoinGame(playerName, gameId);
        }

        public override bool Login(string userName)
        {
            return Service.Login(userName);
        }

        public override void ProvideShipPlacements(string player, List<ShipPlacement> shipPlacements)
        {
            Service.ProvideShipPlacementsAsync(player, shipPlacements.ToArray());
        }

        public override void ProvideShotPlacement(string player, Coordinate coordinate)
        {
            Service.ProvideShotPlacementAsync(player, coordinate);
        }
    }
}
