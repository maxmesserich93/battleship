//using Models.Player;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Runtime.Serialization;
//using System.Diagnostics;
//using System.IO;
//using System.Collections.Specialized;

//namespace Models
//{
//    [DataContract]
//    public class GameLogic : IGame
//    {




//        /// <summary>
//        /// An event risen when the state of the game changes.
//        /// </summary>
//        public event PhaseChangeHandler PhaseChangeEvent;
//        public delegate void PhaseChangeHandler(GameLogic sender, GamePhase newPhase);


//        public enum GamePhase
//        {
//            Init,
//            WaitingForPlayers,
//            ShipPlacement,
//            WaitingForPlacement,
//            InProgress,
//            PlayerOneTurn,
//            PlayerTwoTurn,
//            Error,
//            Finished
//        }


//        public List<IPlayerContract> _invokePlacement = new List<IPlayerContract>();

//        public GameData Data { set; get; }

//        public List<PlayerData> Players { set; get; }
//        private List<IPlayerContract> _playerContracts = new List<IPlayerContract>();

//        static protected readonly Random _random;


//        public GameLogic(GameData gameData)
//        {
//            Data = gameData;
//            Players = new List<PlayerData>();
//            _invokePlacement = new List<IPlayerContract>();
//            Data.Phase = GamePhase.Init;
//        }


//        /// <summary>
//        /// Tells the players of the previous shots and their results.
//        /// </summary>
//        public void HandleLoadedGame()
//        {
//            Debug.WriteLine("SUIZE "+ Data.PlayerShots.Count);
//            if (Data.PlayerShots.Count < 2)
//            {
//                return;
//            }
//            Debug.WriteLine("HANDLE LOAD GAME GO!");
//            _playerContracts.ForEach(player => player.GameRules(Data.RuleSet));
//            _playerContracts.ForEach(player => player.PlacementComplete());
//            Data.PlayerShots[0].ForEach(shot =>
//            {
//                var shotResult = Data.PlayerShotResults[0][shot];
//                _playerContracts[0].ShotResult(shot, shotResult);
//                _playerContracts[1].OpponentShot(shot, shotResult);
//            });
//            Data.PlayerShots[1].ForEach(shot =>
//            {
//                var shotResult = Data.PlayerShotResults[1][shot];
//                _playerContracts[1].ShotResult(shot, shotResult);
//                _playerContracts[0].OpponentShot(shot, shotResult);
//            });


//        }


//        public GameLogic(string uid, GameRuleSet ruleSet)
//        {
//            Data = new GameData();
//            Data.Id = uid;
//            Data.RuleSet = ruleSet;
//            Data.Phase = GamePhase.Init;
//            Players = new List<PlayerData>();
//        }
//        /// <summary>
//        /// Adds a new player to the game.
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="playerType"></param>
//        public void AddPlayer(PlayerData playerData, IPlayerContract playerType, bool createNewField)
//        {
//            _playerContracts.Add(playerType);
//            Players.Add(playerData);
//            Field playerField = new Field(Data.RuleSet.FieldSize);
//            if (createNewField)
//            {
//                Data.PlayerFields.Add(playerField);
//            }
            
//            Console.WriteLine(" PLAYERS IN GAME :" + Players.Count());
//            if(Players.Count ==2) {
//                Debug.WriteLine("AddPlayer: GameFull. Updating GamePhase");
//                UpdateGamePhase();
//            }
            


//        }



//        public bool PlaceShips(string playerId, List<Ship> placements)
//        {
//            var player = Players.Where(p => p.UUID.Equals(playerId)).FirstOrDefault();

//            Debug.WriteLine("PLACE :" + player.Name);
            
//            //Group ShipPlacements by their type to validate that the placements upholds the gamerules
//            IEnumerable<IGrouping<string, Ship>> query = placements.GroupBy(x => x.Type);
//            //Check for each shiptype whether the placements contain the correct amount of placements of that shiptype
//            bool correct = Data.RuleSet.ValidateRules(placements);

//            if (!correct)
//            {
//                throw new Exception("Invalid ship placement");
//            }
//            int playerNumber = Players.IndexOf(player);
//            Debug.WriteLine("INDEX: " + playerNumber);
//                Field field = Data.PlayerFields[playerNumber];
//                foreach (Ship placement in placements)
//                {
//                    if(!PlaceShip(player, placement))
//                    {
//                        return false;
//                    }

//                }
//            Debug.WriteLine(" ++++++++++++++++++++++ PLACEMENT COMPLETE FOR " + player.Name);
//            UpdateGamePhase();
//            return true;
//        }
//        /// <summary>
//        /// Updates the game state.
//        /// Invokes the PhaseChangeEvent when the state changes.
//        /// </summary>
//        public void UpdateGamePhase()
//        {
//            GamePhase newPhase = Data.Phase;
//            Debug.WriteLine("UPDATE "+Data.Phase);
//            switch (Data.Phase)
//            {
//                case GamePhase.Init:


//                    var checkRules = Data.PlayerFields.ToList().All(kp => Data.RuleSet.ValidateRules(kp.Ships));


//                    if (checkRules)
//                    {
//                        HandleLoadedGame();
//                        newPhase = GamePhase.InProgress;
//                    }
//                    else if (Data.PlayerFields.Count == 2)
//                    {
//                        newPhase = GamePhase.ShipPlacement;
//                        _playerContracts.ForEach(p => Console.WriteLine(p.ToString()));
//                        _playerContracts.ForEach(p => p.GameRules(Data.RuleSet));
//                    }
//                    break;

//                case GamePhase.ShipPlacement:
//                    if (Data.PlayerFields.ToList().All(kp => Data.RuleSet.ValidateRules(kp.Ships)))
//                    {

//                        _playerContracts.ForEach(player => player.PlacementComplete());
                        
//                        //newPhase = GamePhase.InProgress;
//                        break;
//                    }
                   
//                    //Get players who have not been told to start the game.
//                    var a = _playerContracts.Where(p => _invokePlacement.Contains(p) == false).ToList();
//                    a.ForEach(b => _invokePlacement.Add(b));
//                    a.ForEach(p => p.PlaceShips());
//                    newPhase = GamePhase.WaitingForPlacement;
//                    break;
//                case GamePhase.WaitingForPlacement:



//                    if (Data.PlayerFields.ToList().All(kp => Data.RuleSet.ValidateRules(kp.Ships)))
//                    {

//                        _playerContracts.ForEach(player => player.PlacementComplete());
//                        newPhase = GamePhase.InProgress;
//                    }
//                    break;


//                case GamePhase.InProgress:
//                    //Check whether either players ships are killed



                    

//                    var deadPlayer = Data.PlayerFields
//                    .Where(playerData =>
//                    playerData.Ships.Count(ship => !ship.IsKilled()) == 0
//                    ).FirstOrDefault();




//                    if (deadPlayer != null)
//                    {
//                        var index = Data.PlayerFields.IndexOf(deadPlayer);
//                        deadPlayer.GetData().ToList().ForEach(p => Debug.WriteLine(index + " : " + p));
//                        Console.WriteLine("Game OVER");
//                        newPhase = GamePhase.Finished;
//                    }
//                    else
//                    {
//                        //Tell current player to shoot!
//                        var player = _playerContracts[Data.CurrentPlayer];
//                        player.Shoot();
//                    }
                    
//                    break;
//                case GamePhase.Finished:
//                    //Get the wining player

//                    var winnerIndex = Data.PlayerFields
//                    .Where(playerData =>
//                    playerData.Ships.Count(ship => !ship.IsKilled()) > 0).FirstOrDefault();

//                    _playerContracts.ForEach(p => p.GameOver(Players[Data.PlayerFields.IndexOf(winnerIndex)].Name));
//                    break;
//            }
//            if (Data.Phase != newPhase)
//            {

//                Data.Phase = newPhase;
//                PhaseChangeEvent?.Invoke(this, Data.Phase);
//                UpdateGamePhase();
//            }


//        }

//        public bool Shoot(string playerId, Coordinate coordinate)
//        {
//            var player = Players.Where(p => p.UUID.Equals(playerId)).FirstOrDefault();

//            var result = Data.Shoot(Players.IndexOf(player), coordinate);

//            if(result != null)
//            {

//                _playerContracts[Data.CurrentPlayerData()].ShotResult(coordinate, result);
//                _playerContracts[Data.WaitingPlayerData()].OpponentShot(coordinate, result);
//                Data.NextPlayer();
//                UpdateGamePhase();
//                return true;

//            }
//            return false;



//        }
//        public bool PlaceShip(PlayerData player, Ship ship)
//        {
//            int playerNumber = Players.IndexOf(player);
//            Field field = Data.PlayerFields[playerNumber];

//                int requiredShipTypeCount = Data.RuleSet.GetShipTypeCount(ship);
//                if (requiredShipTypeCount == 0)
//                {
//                    throw new ArgumentException(ship + " is not part of the game rules!");
//                }
//                int placedShipsofType = field.Ships.Count(s => s.Type.Equals(ship.Type));

//            //Only place the ship if required.
//            if (placedShipsofType < requiredShipTypeCount)
//                {
//                //console.writeline(" placing " + shiptype + " at : " + coordinate);
//               return Data.PlayerFields[playerNumber].PlaceShip(ship);
//                }
//                return false;
//        }
//    }
//}
