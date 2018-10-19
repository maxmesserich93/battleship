using Models.GameLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
    [DataContract]
    public class GameData
    {
        [DataMember]
        public string Id { set; get; }

        [DataMember]
        public GamePhase Phase { set; get; }

        [DataMember]
        public int CurrentPlayer { set; get; }

        [DataMember]
        public List<Field> PlayerFields { get; set; }
        [DataMember]
        /// <summary>
        /// Stores the coordinates each player shot in the right order.
        /// </summary>
        public Dictionary<int, List<Coordinate>> PlayerShots { set; get; }
        /// <summary>
        /// Stores the results of each shot of the players.
        /// </summary>
        [DataMember]
        public Dictionary<int, Dictionary<Coordinate, List<FieldPosition>>> PlayerShotResults { set; get; }

        [DataMember]
        public GameRuleSet RuleSet { set; get; }

        public GameData()
        {
            Id = Guid.NewGuid().ToString();

            RuleSet = GameRuleSet.DEFAULT_RULES();
            PlayerFields = new List<Field>();
            PlayerShots = new Dictionary<int, List<Coordinate>>();
            PlayerShotResults = new Dictionary<int, Dictionary<Coordinate, List<FieldPosition>>>();
        }

        public int CurrentPlayerData()
        {
            return CurrentPlayer;
        }

        public int WaitingPlayerData()
        {
            if (CurrentPlayer == 0)
            {
                return 1;
            }
            return 0;
        }


        public List<FieldPosition> Shoot(int playerNumber, Coordinate coordinate)
        {


            var currentPlayerData = CurrentPlayerData();

            Debug.WriteLine("Data.shoot " + Phase + " - " + playerNumber + " / " + coordinate);

            //Check if the player is the current player
            if (Phase == GamePhase.InProgress && playerNumber.Equals(CurrentPlayer))
            {
                var result = PlayerFields[WaitingPlayerData()].ShootCoordinate(coordinate);

                if (!PlayerShots.ContainsKey(currentPlayerData))
                {
                    PlayerShots.Add(currentPlayerData, new List<Coordinate>());
                    var shotResults = new Dictionary<Coordinate, List<FieldPosition>>();
                    PlayerShotResults.Add(currentPlayerData, shotResults);
                }

                if (result != null)
                {
                    PlayerShots[currentPlayerData].Add(coordinate);
                    PlayerShotResults[currentPlayerData].Add(coordinate, result);
                }
                else
                {
                    if (playerNumber == 0)
                    {
                        throw new Exception(playerNumber + " ::: " + coordinate + " => " + PlayerFields[WaitingPlayerData()]._getPosition(coordinate));
                    }
                }

                return result;
            }

            
            return null;
        }

        public void NextPlayer()
        {
            CurrentPlayer++;
            if (CurrentPlayer > 1)
            {
                CurrentPlayer = 0;
            }
        }
    }
}
