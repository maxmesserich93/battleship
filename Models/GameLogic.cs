using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.IO;

namespace Models
{
    [DataContract]
    public class GameLogic
    {
        /// <summary>
        /// An event risen when the state of the game changes.
        /// </summary>
        public event PhaseChangeHandler PhaseChangeEvent;
        public delegate void PhaseChangeHandler(GameLogic sender, GamePhase newPhase);


        public enum GamePhase
        {
            Init,
            ShipPlacement,
            InProgress,
            PlayerOneTurn,
            PlayerTwoTurn,
            Error,
            Finished
        }
        [DataMember]
        public string Uid { set; get; }
        [DataMember]
        public GameRuleSet RuleSet { set; get; }
        [DataMember]
        public List<PlayerData> Players { set; get; }

        [DataMember]
        public Dictionary<PlayerData, Field> FieldPlayerDictionary { get; set; }
        protected readonly Random _random;
        [DataMember]
        public int CurrentPlayerIndex { set; get; }
        [DataMember]
        
        public GamePhase Phase { get; set; }

        public GameLogic(string uid, GameRuleSet ruleSet)
        {
            Players = new List<PlayerData>();
            Uid = uid;
            RuleSet = ruleSet;
            FieldPlayerDictionary = new Dictionary<PlayerData, Field>();
            Phase = GamePhase.Init;
        }
        /// <summary>
        /// Adds a new player to the game.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerType"></param>
        public void AddPlayer(String name, IPlayerContract playerType)
        {
            PlayerData playerData = new PlayerData();
            playerData.Player = playerType;
            playerData.Name = name;

            Players.Add(playerData);
            Field playerField = new Field(RuleSet.FieldSize);
            //playerType.PlayerData.Field = playerField;
            FieldPlayerDictionary.Add(playerData, playerField);
            Console.WriteLine(" PLAYERS IN GAME :" + Players.Count());

            Players.ForEach(p => Debug.WriteLine(p.Name));
            UpdateGamePhase();

        }
        /// <summary>
        /// Checks wheter the ships are placed according to the gamerules.
        /// </summary>
        /// <param name="ships"></param>
        /// <returns></returns>
        private bool ValidatePlacemenent(List<ShipPlacement> ships)
        {
            IEnumerable<IGrouping<ShipType, ShipPlacement>> query = ships.GroupBy(x => x.ShipType);
            //Check for each shiptype whether the placements contain the correct amount of placements of that shiptype
            bool correct = query.Any(pair => RuleSet.GetShipTypeCount(pair.Key) == pair.Count());
            return correct;
        }


        public bool PlaceShips(string player, List<ShipPlacement> placements)
        {


            Console.WriteLine("Trying to find: " + player);
            Trace.Write("HAVE: ");
            Players.ForEach(p => Trace.Write(p.Name + ", "));
            Trace.WriteLine("");

            bool worked = false;
           var a =  Players.First(p => p.Name.Equals(player));

            var playerData = Players.Where(p => p.Name == player).First();
            if (playerData!= null)
            {
                if (ValidatePlacemenent(placements))
                {
                    worked =  PlaceShips(playerData, placements);
                }


                
            }
            if (worked && FieldPlayerDictionary.AsParallel().All(pair => pair.Value.Ships.Count() > 0))
            {
                Console.WriteLine("ASD");
                Phase = GamePhase.InProgress;
                UpdateGamePhase();

            }
            return worked;


        }




        public bool PlaceShips(PlayerData player, List<ShipPlacement> placements)
        {

            //Group ShipPlacements by their type to validate that the placements upholds the gamerules
            IEnumerable<IGrouping<ShipType, ShipPlacement>> query = placements.GroupBy(x => x.ShipType);
            //Check for each shiptype whether the placements contain the correct amount of placements of that shiptype
            bool correct = query.Any(pair => RuleSet.GetShipTypeCount(pair.Key) == pair.Count());

            if (!correct)
            {
                return false;
            }

                Field field = FieldPlayerDictionary[player];
                foreach (ShipPlacement placement in placements)
                {
                    Ship ship = PlaceShip(player, placement.ShipType, placement.IsVertical, placement.StartCoordinate);
                    Console.WriteLine(" PLACED: " + ship);
                    if (ship == null)
                    {
                        //throw new ArgumentException(placement.ToString());
                        return false;
                    }
                }
           
            return true;
        }
        /// <summary>
        /// Updates the game state.
        /// Invokes the PhaseChangeEvent when the state changes.
        /// </summary>
        public void UpdateGamePhase()
        {
            GamePhase newPhase = Phase;
            switch (Phase)
            {
                case GamePhase.Init:
                    if (FieldPlayerDictionary.Count == 2)
                    {
                        Console.WriteLine(" TWO PLAYERS!!!! PLACE SHIPS!");
                        newPhase = GamePhase.ShipPlacement;
                        Players.ForEach(p => p.Player.StartGame(RuleSet));
                    }
                    break;
                case GamePhase.ShipPlacement:
                    //newPhase = HandleShipPlacement();
                    Players.ForEach(a => a.Player.PlaceShips());
                    break;


                case GamePhase.InProgress:
                    //Check whether either players ships are killed
                    //Console.WriteLine("Game in progress!");
                    var deadPlayer = FieldPlayerDictionary
                    .Where(playerData =>
                    playerData.Value.Ships.Count(ship => !ship.IsKilled()) == 0
                    ).Select(x => x.Key).FirstOrDefault();
                    if (deadPlayer != null)
                    {
                        Console.WriteLine("Game OVER");
                        newPhase = GamePhase.Finished;
                    }
                    else
                    {
                        //Tell current player to shoot!
                        Players[CurrentPlayerIndex].Player.Shoot();
                    }
                    //newPhase = HandlePlayerTurn();

                    //Try to find a players who has no more ships
                    break;
                case GamePhase.Finished:
                    //Get the wining player
                    var winner = FieldPlayerDictionary
                    .Where(playerData =>
                    playerData.Value.Ships.Count(ship => !ship.IsKilled()) == 0
                ).Select(x => x.Key).FirstOrDefault();

                    Players.ForEach(p => p.Player.GameOver(winner.Name));

                    break;
            }
            if (Phase != newPhase)
            {

                Phase = newPhase;
                PhaseChangeEvent?.Invoke(this, Phase);
                UpdateGamePhase();
            }



        }

        //private GamePhase HandleShipPlacement()
        //{
        //    //Parallely get the shipplacements from the players
        //    var placements = Players.AsParallel().Select(player =>
        //    {
        //        Console.WriteLine(player);

        //        List<ShipPlacement> playerPlacements = player.Player.RequestShipPlacement().ToList();
        //        Console.WriteLine(player.ToString() + " - " + playerPlacements);


        //        return new KeyValuePair<PlayerData, List<ShipPlacement>>(player, playerPlacements);
        //    }).ToDictionary(x => x.Key, x => x.Value);

        //    //Validate the placements. If they are invalid the game is cancelled.
        //    if (placements.ToList().All(pair => ValidatePlacemenent(pair.Value)))
        //    {

        //        placements.ToList().ForEach(pair => PlaceShips(pair.Key, pair.Value));
        //        return GamePhase.InProgress;
        //    }
        //    else
        //    {

        //        Players.ForEach(p => p.Player.Error("Illegal ShipPlacements. Exiting!"));
        //    }
        //    return GamePhase.Error;
        //}


        public bool Shoot(string player, Coordinate coordinate)
        {

            //Check if the player is the current player
            if (Players[CurrentPlayerIndex].Name.Equals(player))
            {

                var result = FieldPlayerDictionary[Players[getOtherIndex()]].ShootCoordinate(coordinate);

                //Tell players about outcome of the shot
                Players[CurrentPlayerIndex].Player.ShotResult(result);
                Players[getOtherIndex()].Player.OpponentShot(result);
                CurrentPlayerIndex++;
                if (CurrentPlayerIndex > 1)
                {
                    CurrentPlayerIndex = 0;
                }

                var deadPlayer = FieldPlayerDictionary
                .Where(playerData =>
                playerData.Value.Ships.Count(ship => !ship.IsKilled()) == 0
                ).Select(x => x.Key).FirstOrDefault();
                if (deadPlayer != null)
                {
                    Console.WriteLine("Game OVER");

                    //newPhase = GamePhase.Finished;
                }




                UpdateGamePhase();
            }
            return false;


        }
        /// <summary>
        /// Handles the game loop
        /// </summary>
        /// <returns></returns>
        private GamePhase HandlePlayerTurn()
        {
            IPlayerContract player = Players[CurrentPlayerIndex].Player;
            Coordinate coordinate = player.RequestShotPlacement();



            var result = FieldPlayerDictionary[Players[getOtherIndex()]].ShootCoordinate(coordinate);



            if (result != null)
            {
                player.ShotResult(result);
                CurrentPlayerIndex++;
                if (CurrentPlayerIndex > 1)
                {
                    CurrentPlayerIndex = 0;
                }
                var deadPlayer = FieldPlayerDictionary
                .Where(playerData =>
                playerData.Value.Ships.Count(ship => !ship.IsKilled()) == 0
            ).Select(x => x.Key).FirstOrDefault();
                if (deadPlayer != null)
                {
                    Console.WriteLine("Game OVER");
                    return GamePhase.Finished;
                    //newPhase = GamePhase.Finished;
                }
            }
            return HandlePlayerTurn();

        }


        private int getOtherIndex()
        {
            int a = CurrentPlayerIndex + 1;
            if (a > 1)
            {
                return 0;
            }
            return a;
        }

        public Ship PlaceShip(PlayerData player, ShipType shipType, bool vertical, Coordinate coordinate)
        {


                Field field = FieldPlayerDictionary[player];


                int requiredShipTypeCount = RuleSet.GetShipTypeCount(shipType);
                if (requiredShipTypeCount == 0)
                {
                    throw new ArgumentException(shipType + " is not part of the game rules!");
                }
                //Count the number of ships of the type of newShip already placed on the field

                //Debug.WriteLine(shipType + " to place: " + requiredShipTypeCount);
                int placedShipsofType = field.Ships.Count(ship => ship.Type.Name.Equals(shipType.Name));
                //Debug.WriteLine(" PLACED SHIPS: ");
                //field.Ships.ForEach(s => Debug.WriteLine(s.Type));


                //Debug.WriteLine(shipType + " PLACED: " + placedShipsofType);

                //Only place the ship if required.
                if (placedShipsofType < requiredShipTypeCount)
                {
                    //Console.WriteLine(" PLACING " + shipType + " AT : " + coordinate);
                    Ship ship = FieldPlayerDictionary[player].PlaceShip(shipType, vertical, coordinate);
                    return ship;

                }
                return null;

        }
        /// <summary>
        /// Shoots the field of the opposite player of the provided one.
        /// </summary>
        /// <param name="shooter"></param>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public List<FieldPosition> ShootCoordinate(IPlayerContract shooter, Coordinate coordinate)
        {
            //Get the victim which is the other player 
            PlayerData victim = FieldPlayerDictionary.First(pair => pair.Key != shooter).Key;

            return FieldPlayerDictionary[victim].ShootCoordinate(coordinate);


        }
    }
}
