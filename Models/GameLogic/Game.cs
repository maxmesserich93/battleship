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
            PlayerContracts.ForEach(c => c.GameRules(Data.RuleSet));
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

                }
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
            if(playerIndex == null || playerIndex == -1)
            {
                PlayerContracts.ForEach(player => player.GameOver(" Opponent quit the game"));
            }


            //var index = 0;
            //if(playerIndex == 0)
            //{
            //    index = 1;
            //}

            //if(playerIndex < PlayerContracts.Count)
            //{
            //    PlayerContracts[index].GameOver(PlayerDataList[playerIndex].Name);

            //    PlayerContracts.ForEach(player => player.GameOver(PlayerDataList[index].Name));
            //}

        }

        public bool PlaceShips(string playerId, List<Ship> placements)
        {
            var playerIndex = _getPlayerNumber(playerId);
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
                PlayerContracts[Data.CurrentPlayer].Shoot();
            }
            return true;
        }

        public void Shoot(string playerId, Coordinate coordinate)
        {
            var playerIndex = _getPlayerNumber(playerId);

            var result = Data.Shoot(playerIndex, coordinate);

            if (result != null)
            {

                PlayerContracts[Data.CurrentPlayerData()].ShotResult(coordinate, result);
                PlayerContracts[Data.WaitingPlayerData()].OpponentShot(coordinate, result);
                Data.NextPlayer();

                var deadPlayer = Data.PlayerFields.Where(playerData =>playerData.Ships.Count(ship => !ship.IsKilled()) == 0).FirstOrDefault();
                if (deadPlayer != null)
                {
                    var index = Data.PlayerFields.IndexOf(deadPlayer);
                    deadPlayer.GetData().ToList().ForEach(p => Debug.WriteLine(index + " : " + p));
                    PlayerContracts.ForEach(player => player.GameOver(PlayerDataList[index].Name));
                    Console.WriteLine("Game OVER");
                    Data.Phase = GamePhase.Finished;
                    GameOver?.Invoke(Data.Id);
                }
                else
                {
                    //Tell current player to shoot!
                    var player = PlayerContracts[Data.CurrentPlayer];
                    player.Shoot();
                }
            }

            //throw new Exception(playerId + " - " + PlayerDataList[playerIndex].Name + " : ");

        }

    }
}
