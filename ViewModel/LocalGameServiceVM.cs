using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class LocalGameServiceVM : AbstractGameService
    {
        LocalGameService Service { set; get; }
        public LocalGameServiceVM()
        {
            Service = new LocalGameService();
        }

        public override List<GameData> GetAvailableGames()
        {
            return Service.GetAvailableGames();
        }

        public override void HostGame(string name)
        {
            Service.HostGame(name);
        }

        public override void JoinBotGame(string name)
        {
            Service.JoinBotGame(Player);
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
            Service.ProvideShipPlacements(player, shipPlacements);
        }

        public override void ProvideShotPlacement(string player, Coordinate coordinate)
        {
            Service.ProvideShotPlacement(player, coordinate);
        }
    }
}
