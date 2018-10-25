using Models.GameServiceImp;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameServiceImp
{
    public class LocalGameService : AbstractGameService
    {


        public LocalGameService(Func<IPlayerContract> callbackCreator) : base(callbackCreator, "asd.xml")
        {

        }

        public new List<GameInformation> GetAvailableGames(string playerId)
        {
            return GamePersistenceManager.GameList.Where(data => data.Phase == GameLogic.GamePhase.InProgress).Select(data => new GameInformation() { ID = data.Id, Owner = "bot" }).ToList();
        }

        public new void JoinGame(string identity, string gameId)
        {
            var gameData = GamePersistenceManager.LoadGame(gameId);
            var joiningPlayer = _playerDataMap[identity];
            if (joiningPlayer == null)
            {
                Console.WriteLine(identity + " is not a registered player!");
                return;
            }
            var newGame = _createNewGameInstance(GameRuleSet.DEFAULT_RULES(), gameData);
            var bot = new SmartAIPlayer(newGame);
            var botData = _registerPlayer("bot", bot);
            _assignPlayerToGame(botData, newGame.Data.Id, false);
            bot.ProvideIdentity(botData.UUID);
            _assignPlayerToGame(joiningPlayer, newGame.Data.Id, false);
            Console.WriteLine("LOADED THE GAME "+gameId);

        }
    }
}
