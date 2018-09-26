using Models.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LocalGameService : IGameContract
    {

        GameLogic GameInstance { get; set; }
        IPlayerContract PlayerContract { get; set; }
        string PlayerName { get; set; }
        public List<GameData> GetAvailableGames()
        {
            throw new NotImplementedException();
        }

        

        public void HostGame(string name)
        {
            throw new NotImplementedException();
        }

        public void JoinBotGame(string name)
        {
            PlayerName = name;
            PlayerContract = new LocalPlayer();
            GameInstance = new GameLogic("local", GameRuleSet.DEFAULT_RULES());
            GameInstance.AddPlayer(name, PlayerContract);
            Debug.WriteLine("Added player: " + name + ". Adding bot!!!!");
            GameInstance.AddPlayer("Bot", new AIPlayer(this, "Bot"));
            Debug.WriteLine("Added BOT!");
        }

        public void JoinBotGame(IPlayerContract player)
        {

            GameInstance = new GameLogic("local", GameRuleSet.DEFAULT_RULES());
            GameInstance.AddPlayer("asd", player);

            GameInstance.AddPlayer("Bot", new AIPlayer(this, "Bot"));
            Debug.WriteLine("Added BOT!");
        }

        public void JoinGame(string playerName, string gameId)
        {
            PlayerName = playerName;
            PlayerContract = new LocalPlayer();
            GameInstance = new GameLogic("local", GameRuleSet.DEFAULT_RULES());
            GameInstance.AddPlayer(playerName, PlayerContract);
            //GameInstance.AddPlayer("Bot", new AIPlayer(this, "Bot"));
        }

        public bool Login(string userName)
        {
            throw new NotImplementedException();
        }

        public void ProvideShipPlacements(string player, List<ShipPlacement> shipPlacements)
        { 
            GameInstance.PlaceShips(player, shipPlacements);
        }

        public void ProvideShotPlacement(string player, Coordinate coordinate)
        {
            GameInstance.Shoot(player, coordinate);
        }
    }
}
