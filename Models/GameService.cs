using Models.Player;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //Per session because this allows multiple game instaces to run simulataniosly.
    //Each game has its own session.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class GameService : IGameContract
    {



        private Dictionary<string, GameLogic> _gameMap = new Dictionary<string, GameLogic>();
        private ConcurrentDictionary<string, string> _playerToGame = new ConcurrentDictionary<string, string>();
        private ConcurrentDictionary<string, GameLogic> _waitingForOpponent = new ConcurrentDictionary<string, GameLogic>();



        /// <summary>
        /// Delegate for listeing to phase changes in the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newPhase"></param>
        protected virtual void OnPhaseChange(GameLogic sender, GameLogic.GamePhase newPhase)
        {
            switch (newPhase)
            {

                case GameLogic.GamePhase.Finished:
                    //Remove the game.
                    _gameMap.Remove(sender.Uid);
                
                    break;
            }


        }


        public void JoinGame(String playerName, String gameId)
        {
            Console.WriteLine(playerName + " is joining " + gameId);

            //Create a callback channel to the user who just connected.
            IPlayerContract playerChannel = OperationContext.Current.GetCallbackChannel<IPlayerContract>();

            _waitingForOpponent.Keys.ToList().ForEach(key => Console.WriteLine("GameID: " + key));
            _playerToGame[playerName] = gameId;

            GameLogic join = null;
            _waitingForOpponent.TryRemove(gameId, out join);




            if (join != null)
            {
                join.AddPlayer(playerName, playerChannel);
                join.PhaseChangeEvent += OnPhaseChange;
                _gameMap.Add(join.Uid, join);
            }
            else
            {
                Console.WriteLine("Could not find " + gameId);
            }
            
        }


        public void JoinBotGame(string name)
        {



            Console.WriteLine(name + " is joining");

            //Create a callback channel to the user who just connected.
            IPlayerContract playerChannel = OperationContext.Current.GetCallbackChannel<IPlayerContract>();

            
            String id = Guid.NewGuid().ToString();

            GameLogic game = new GameLogic(id, GameRuleSet.DEFAULT_RULES());
            var botName = "bot" + Guid.NewGuid().ToString();

            game.AddPlayer(botName, new AIPlayer(this, botName));


            game.AddPlayer(name, playerChannel);
            _playerToGame[name] = id;




            _playerToGame[botName] = id;

            game.PhaseChangeEvent += OnPhaseChange;
            _gameMap.Add(id, game);
            game.UpdateGamePhase();
            //_playerMap.Add(newPlayer, playerChannel);

            //IPlayerContract botOpponent = new AIPlayer();


            //_playerMap[newPlayer].StartGame(_gameLogic.RuleSet);
        }

        public List<GameData> GetAvailableGames()
        {
            Console.WriteLine("Giving games");
            return _waitingForOpponent.Select(game =>
            {
                GameData gameData = new GameData();
                gameData.Owner = game.Value.Players[0].Name;
                gameData.ID = game.Value.Uid;
                return gameData;
            }).ToList();
        }

        public void HostGame(string name)
        {
            IPlayerContract playerChannel = OperationContext.Current.GetCallbackChannel<IPlayerContract>();
            String id = Guid.NewGuid().ToString();
            GameLogic game = new GameLogic(id, GameRuleSet.DEFAULT_RULES());
            game.AddPlayer(name, playerChannel);
            _waitingForOpponent.GetOrAdd(id, game);


        }

        public bool Login(string userName)
        {
            Console.WriteLine(" L O G I N : " + userName);
            return true;
        }

        public void ProvideShipPlacements(string player, List<ShipPlacement> shipPlacements)
        {

            Console.WriteLine("ProvideShipPlacement: " + player);

            _playerToGame.Keys.ToList().ForEach(w => Console.WriteLine(w));


            var a = _playerToGame[player];

            _gameMap[_playerToGame[player]].PlaceShips(player, shipPlacements);
        }

        public void ProvideShotPlacement(string player, Coordinate coordinate)
        {
            _gameMap[_playerToGame[player]].Shoot(player, coordinate);
        }
    }
}
