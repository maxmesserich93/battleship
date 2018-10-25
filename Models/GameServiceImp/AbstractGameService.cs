using Models.GameLogic;
using Models.Player;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.GameServiceImp
{
    public abstract class AbstractGameService : IGameContract
    {
        /// <summary>
        /// Contains all currently active games.
        /// </summary>
        protected Dictionary<string, Game> _gameMap = new Dictionary<string, Game>();
        /// <summary>
        /// Links player identities to their callbacks
        /// </summary>
        protected Dictionary<PlayerData, IPlayerContract> _playerMap = new Dictionary<PlayerData, IPlayerContract>();

        protected Dictionary<string, PlayerData> _playerDataMap = new Dictionary<string, PlayerData>();

        /// <summary>
        /// Links player identities to their games.
        /// </summary>
        protected Dictionary<PlayerData, string> _playerToGame = new Dictionary<PlayerData, string>();
        /// <summary>
        /// Contains all games which need another player (joinable)
        /// </summary>
        protected Dictionary<string, Game> _waitingForOpponent = new Dictionary<string, Game>();



        protected GamePersistenceManager GamePersistenceManager;

        /// <summary>
        /// Func for defining the callback. 
        /// </summary>
        Func<IPlayerContract> CallbackCreationFunc { set; get; }

        private PlayerData _getPlayerData(string playerId)
        {
            return _playerDataMap[playerId];
        }

        private IPlayerContract _getCallback(string playerId) {
            return _playerMap[_playerDataMap[playerId]];
        }

        public AbstractGameService(Func<IPlayerContract> callbackCreator, string savedGames)
        {
            CallbackCreationFunc = callbackCreator;
            if(savedGames != null)
            {
                GamePersistenceManager = new GamePersistenceManager(savedGames);
            }
        }


        /// <summary>
        /// Delegate for listeing to phase changes in the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newPhase"></param>
        private void GameOverHandler(string gameId)
        {
            _gameMap.Remove(gameId);
            //GamePersistenceManager.RemoveGame(gameId);


        }



        public void JoinGame(string id, string gameId)
        {
            Console.WriteLine(id + " is joining " + gameId);
            var joiningPlayer = _getPlayerData(id);
            if (joiningPlayer == null)
            {
                Console.WriteLine(id + " is not a registered player!");
                return;
            }
            _assignPlayerToGame(joiningPlayer, gameId, true);
        }
        /// <summary>
        /// Creates a new game and adds it to the game map.
        /// </summary>
        /// <param name="gameRules"></param>
        protected Game _createNewGameInstance(GameRuleSet gameRules, GameData gameData)
        {

            var game = new Game(gameData) ;

            _gameMap.Add(game.Data.Id, game);
            _waitingForOpponent.Add(game.Data.Id, game);
            game.GameOver += GameOverHandler;
            if(GamePersistenceManager != null)
            {
                GamePersistenceManager.AddGameIfNotPresent(game.Data);
            }
 
            Debug.WriteLine("Craeted a new game :" + game.Data.Id);

            return game;

        }


        /// <summary>
        /// Adds the provided player to the game with the 
        /// </summary>
        /// <param name="playerContract"></param>
        /// <param name="game"></param>
        protected void _assignPlayerToGame(PlayerData playerData, string gameId, bool newGame)
        {
            var contract = _getCallback(playerData.UUID);
            Debug.WriteLine(playerData.Name + " to game: " + gameId);


            var game =_waitingForOpponent[gameId];

            if(game == null)
            {
                Console.WriteLine(playerData.Name + " could not join " + gameId);
            }
            //add player to game and check whether it now has two players
            game.AddPlayer(playerData, contract);
            _playerToGame[playerData] = game.Data.Id;
            if (game.PlayerDataList.Count == 2)
            {
                _waitingForOpponent.Remove(game.Data.Id);
                game.Initialize();
            }
        }


        protected PlayerData _registerPlayer(string userName, IPlayerContract contract)
        {
            var playerData = new PlayerData(userName);
            _playerDataMap.Add(playerData.UUID, playerData);
            _playerMap.Add(playerData, contract);
            return playerData;
        }

        public string Login(string userName)
        {
            Console.WriteLine("Login: " + userName);
            IPlayerContract callback = CallbackCreationFunc();
            Console.WriteLine(callback.ToString());
            var a = _registerPlayer(userName, callback);
            return a.UUID;
        }


        public void JoinBotGame(string identity)
        {
            Console.WriteLine("JoinBotGame: " + identity);
            var joiningPlayer = _playerDataMap[identity];
            if (joiningPlayer == null)
            {
                Console.WriteLine(identity + " is not a registered player!");
                return;
            }
            var newGame = _createNewGameInstance(GameRuleSet.DEFAULT_RULES(), new GameData());
            var bot = new SmartAIPlayer(newGame);
            var botData = _registerPlayer("bot", bot);
            _assignPlayerToGame(botData, newGame.Data.Id, true);
            bot.ProvideIdentity(botData.UUID);
            _assignPlayerToGame(joiningPlayer, newGame.Data.Id, true);     
            Console.WriteLine("Created new bot game for " + identity);
            
        }

        public List<GameInformation> GetAvailableGames(string playerid)
        {
            Console.WriteLine("Giving games");
            //Gets all games where the excluding the one the players owns

            var a =_waitingForOpponent.Where(kp => !kp.Value.PlayerDataList.Select(player => player.UUID).Contains(playerid)).Select(game =>
            {
                GameInformation gameData = new GameInformation();

                gameData.Owner = game.Value.PlayerDataList.First().Name;
                gameData.ID = game.Value.Data.Id;
                return gameData;
            }).Where(game => !game.Owner.Equals(playerid)).ToList();

            a.ForEach(d => Console.WriteLine(d.ToString()));
            return a;
        }

        public void HostGame(string identity)
        {
            Console.WriteLine("HostGame: " + identity);
            if (identity == null || identity.Length == 0)
            {
                Console.WriteLine("Invalid identity");
                return;
            }
            var player = _getPlayerData(identity);
            if (player == null)
            {
                Console.WriteLine(identity + " is not a registered player!");
                return;
            }
            IPlayerContract playerChannel = _getCallback(identity);
            var game = _createNewGameInstance(GameRuleSet.DEFAULT_RULES(), new GameData());

            _assignPlayerToGame(player, game.Data.Id, true);
        }

        public void ProvideShipPlacements(string identity, List<Ship> shipPlacements)
        {
            if (identity == null || identity.Length == 0)
            {
                Console.WriteLine("Invalid identity");
                return;
            }
            var player = _playerDataMap[identity];
            if (player == null)
            {
                Console.WriteLine(identity + " is not a registered player!");
                return;
            }

            Console.WriteLine("ProvideShipPlacement: " + player.UUID);

            //string gameID = "";
            string gameID;
            _playerToGame.TryGetValue(player, out gameID);
            Console.WriteLine("GameID: " + gameID);


            var game = _gameMap[gameID];

            Console.WriteLine("Game: " + game);


            var check = game.PlaceShips(identity, shipPlacements);
            Console.WriteLine(player.UUID + " placement complete "+check);


        }


        public void ProvideShotPlacement(string identity, Coordinate coordinate)
        {
            //Console.WriteLine("ProvideShotPlacement: " + identity);
            if (identity == null || identity.Length == 0)
            {
                Console.WriteLine("Invalid identity");
                return;
            }
            //Console.WriteLine("AbstractGameService.ProvideShotPlacement " + identity + " :: " + coordinate);

            var player = _playerDataMap[identity];
            if (player == null)
            {
                //Console.WriteLine(identity + " is not a registered player!");
                return;
            }

            //Console.WriteLine("playername: " + player.Name);
            _gameMap[_playerToGame[player]].Shoot(identity, coordinate);
            if(GamePersistenceManager != null)
            {
                
                GamePersistenceManager.Save();


                
            }
        }
        public void Logout(string id)
        {
            if (GamePersistenceManager == null)
            {
                var player = _playerDataMap[id];
                if (player == null)
                {
                    return;
                }
                string gameId = null;
                _playerToGame.TryGetValue(player, out gameId);

                if(gameId != null)
                {
                    var game = _gameMap[gameId];
                    game.QuitGame(id);
                    _waitingForOpponent.Remove(gameId);
                }

            }
        }

        public void Ready()
        {
            throw new NotImplementedException();
        }
    }
}

