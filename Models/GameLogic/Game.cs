using Models.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models.GameLogic
{
    public class Game : IGame
    {


        public GameData Data;

        public List<PlayerData> PlayerDataList { set; get; }

        public List<IPlayerContract> PlayerContracts { set; get; }

        public event OnGameOver GameOver;
        public delegate void OnGameOver(string gameId);

        public Game(GameData data)
        {
            Data = data;
            PlayerDataList = new List<PlayerData>();
            PlayerContracts = new List<IPlayerContract>();
        }

        /// <summary>
        // Adds the provided player data and contract as player to the game.
        // Creates a new Field if the GameData does not contain field.
        /// </summary>
        /// <param name="playerData"></param>
        public void AddPlayer(PlayerData playerData, IPlayerContract playerContract)
        {
            //Create a new field if it does not exsist
            if(Data.PlayerFields.Count < 2)
            {
                Data.PlayerFields.Add(new Field(Data.RuleSet.FieldSize));
            }
            PlayerDataList.Add(playerData);
            PlayerContracts.Add(playerContract);
        }

        public void Initialize()
        {
            Debug.WriteLine("GAME INIT: "+Data.Phase);

            PlayerContracts[0].GameRules(Data.RuleSet, PlayerDataList[1].Name);

            PlayerContracts[1].GameRules(Data.RuleSet, PlayerDataList[0].Name);

            if (Data.Phase == GamePhase.Init)
            {
                //Tell players to place their ships
                
                PlayerContracts.ForEach(c => c.PlaceShips());
                Data.Phase = GamePhase.ShipPlacement;
            }
            if(Data.Phase == GamePhase.InProgress)
            {
                //Game has been loaded. Players do not have to place their ships. Tell players their ShipPlacements from loaded data.
                for (int i = 0; i < 2; i++)
                {
                    var playerShips = Data.PlayerFields[i].Ships;
                    PlayerContracts[i].PlacementComplete(playerShips);
                    Debug.WriteLine(" ---------------------------------------PLACEMENTCOMPLETE INVOKED!");


                }
                Data.PlayerShots[0].ForEach(shot =>
                {
                    var shotResult = Data.PlayerShotResults[0][shot];
                    PlayerContracts[0].ShotResult(shot, shotResult);
                    PlayerContracts[1].OpponentShot(shot, shotResult);
                });
                Data.PlayerShots[1].ForEach(shot =>
                {
                    var shotResult = Data.PlayerShotResults[1][shot];
                    PlayerContracts[1].ShotResult(shot, shotResult);
                    PlayerContracts[0].OpponentShot(shot, shotResult);
                });

                PlayerContracts[Data.CurrentPlayer].Shoot();
                
            }
        }

        private int _getPlayerNumber(string playerId)
        {
            var data = PlayerDataList.First(a => a.UUID.Equals(playerId));
            return PlayerDataList.IndexOf(data);

        }

        public void QuitGame(string playerId)
        {
            var playerIndex = _getPlayerNumber(playerId);

            Console.WriteLine("QUIT GAME " + playerIndex);

                int maxScore = (int)Math.Pow(Data.RuleSet.FieldSize, 2);
                if (playerIndex == 0)
                {
                    PlayerContracts[1].GameOver(maxScore, -1);
                    Debug.WriteLine("TOLD "+PlayerDataList[1].Name);
                }
                else
                {
                    Debug.WriteLine("TOLD " + PlayerDataList[0].Name);

                    PlayerContracts[0].GameOver(maxScore, -1);
                }

            
        }

        public bool PlaceShips(string playerId, List<Ship> placements)
        {

           
            var playerIndex = _getPlayerNumber(playerId);
            Debug.WriteLine("PlaceShips: " + playerIndex);
            //Check whether the placements are correct
            if (!Data.RuleSet.ValidateRules(placements))
            {
                
                return false;
            }
            var placeable = placements.All(ship => Data.PlayerFields[playerIndex].ShipPlaceable(ship));
            if (!placeable)
            {
                return false;
            }

            placements.ForEach(ship => Data.PlayerFields[playerIndex].PlaceShip(ship));
            //Check whether placements are valid
            if (Data.PlayerFields.ToList().All(kp => Data.RuleSet.ValidateRules(kp.Ships)))
            {
                Data.Phase = GamePhase.InProgress;
                for (int i = 0; i < 2; i++)
                {
                    var playerShips = Data.PlayerFields[i].Ships;
                    PlayerContracts[i].PlacementComplete(playerShips);
                    Debug.WriteLine(" ---------------------------------------PLACEMENTCOMPLETE INVOKED!");
                    

                }
                PlayerContracts[Data.CurrentPlayer].Shoot();
                //Debug.WriteLine("TELLING " + Data.CurrentPlayer + " TO SHOOT!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + PlayerContracts[Data.CurrentPlayer]);
            }
            return true;
        }



        public void Shoot(string playerId, Coordinate coordinate)
        {
            var playerIndex = _getPlayerNumber(playerId);

            var result = Data.Shoot(playerIndex, coordinate);
            Debug.WriteLine("Shooting "+PlayerDataList[playerIndex].Name+" :::: "+playerIndex+" : "+coordinate);


            if (result != null)
            {
                Debug.WriteLine("SHOOT RESULT");
                result.ForEach(a => Debug.Write(a + " -- "));
                Debug.WriteLine("");
                PlayerContracts[Data.CurrentPlayerData()].ShotResult(coordinate, result);
                PlayerContracts[Data.WaitingPlayerData()].OpponentShot(coordinate, result);
                //Update CurrentPlayer of the data
                Data.NextPlayer();

                var deadPlayer = Data.PlayerFields.Where(playerData =>playerData.Ships.Count(ship => !ship.IsKilled()) == 0).FirstOrDefault();
                if (deadPlayer != null)
                {
                    var index = Data.PlayerFields.IndexOf(deadPlayer);



                    PlayerContracts[0].GameOver(Data.GetPlayerScore(0), Data.GetPlayerScore(1));
                    PlayerContracts[1].GameOver(Data.GetPlayerScore(1), Data.GetPlayerScore(0));
                    Console.WriteLine("Game OVER");
                    Data.Phase = GamePhase.Finished;
                    GameOver?.Invoke(Data.Id);
                }
                else
                {
                    //Tell current player to shoot!
                    Debug.WriteLine("TELLING " + Data.CurrentPlayer + " TO SHOOT!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + PlayerContracts[Data.CurrentPlayer]);
                    PlayerContracts[Data.CurrentPlayer].Shoot();
                    var player = PlayerContracts[Data.CurrentPlayer];
                    player.Shoot();
                }
            }
        }

    }
}
