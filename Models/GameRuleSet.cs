
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class GameRuleUtils
    {
        public static bool ValidateRules(this GameRuleSet gameRuleSet, IList<Ship> ships)
        {
            //Create dictionary which maps ship type to a list of all ship of that type.
            var query = ships.GroupBy(ship => ship.Type).ToDictionary(o => o.Key, o => o.ToList());


            return gameRuleSet.ShipTypeRules.ToList().All(keyPair => query.ContainsKey(keyPair.Key) && query[keyPair.Key].Count == keyPair.Value);


            //Check for each shiptype whether the placements contain the correct amount of placements of that shiptype
            //return query.Any(pair => gameRuleSet.ShipTypeRules[pair.Key] != pair.Count());
        
        }
        public static int GetRemainingNumberOfShipType(this GameRuleSet gameRuleSet, Field field, string shipType)
        {

            int typeCount = field.Ships.Count(placed => placed.Type.Equals(shipType));
            int required = gameRuleSet.GetShipTypeCount(shipType);
            return required - typeCount;


        }

        public static bool CanPlaceShip(this GameRuleSet gameRuleSet, Field field, Ship ship)
        {

            int typeCount = field.Ships.Count(placed => placed.Type.Equals(ship.Type));
            int required = gameRuleSet.GetShipTypeCount(ship);
            return typeCount < required;
        }

    }



    [DataContract]
    public class GameRuleSet
    {



        public static GameRuleSet DEFAULT_RULES()
        {
            GameRuleSet defaultRules = new GameRuleSet();
            
            defaultRules.AddShipTypeRule(nameof(Carrier), 1);
            defaultRules.AddShipTypeRule(nameof(Cruiser), 1);
            defaultRules.AddShipTypeRule(nameof(BattleShip), 1);
            defaultRules.AddShipTypeRule(nameof(Submarine), 2);
            defaultRules.AddShipTypeRule(nameof(Destroyer), 2);
            defaultRules.FieldSize = 10;
            return defaultRules;

        }
        [DataMember]
        public int FieldSize { get; set; }
        [DataMember]
        public Dictionary<string, int> ShipTypeRules { get; set; }
        public GameRuleSet()
        {
            ShipTypeRules = new Dictionary<string, int>();
        }



        public void AddShipTypeRule(string shipTypeName, int count)
        {
            
            if (count > 0)
            {
                ShipTypeRules.Add(shipTypeName, count);
            }
            else
            {
                throw new ArgumentException("Number of ships must be larger than 0");
            }

        }
        /// <summary>
        /// Returns the number of ships of the provided type to be placed on the field.
        /// </summary>
        /// <param name="shipType"></param>
        /// <returns></returns>
        public int GetShipTypeCount(Ship ship)
        {
           
            if (ShipTypeRules.ContainsKey(ship.Type)){
                return ShipTypeRules[ship.Type];
            }
            return 0;
            
        }

        public int GetShipTypeCount(string shipType)
        {


            if (ShipTypeRules.ContainsKey(shipType))
            {
                return ShipTypeRules[shipType];
            }
            return 0;
        }
    }
}
