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
        public DateTime CreationTime { set; get; }

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
            CreationTime = DateTime.Now;
            Debug.WriteLine("---------------------------------------------------CREATION TIME: " + CreationTime);
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

        public int GetPlayerScore(int index)
        {
            int opponentField = 0;
            if(index == 0)
            {
                opponentField = 1;
            }

            return PlayerFields[opponentField].CountHitPositions();

        }


        public List<FieldPosition> Shoot(int playerNumber, Coordinate coordinate)
        {


            var currentPlayerData = CurrentPlayerData();

            

            Debug.WriteLine(Phase);
            

            //Check if the player is the current player
            if (Phase == GamePhase.InProgress && playerNumber == CurrentPlayer)
            {


                Debug.WriteLine("Data.shoot " + " - " + playerNumber + " ON FIELD: "+WaitingPlayerData());

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
