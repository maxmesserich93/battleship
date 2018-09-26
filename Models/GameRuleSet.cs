
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
    public class GameRuleSet
    {



        public static GameRuleSet DEFAULT_RULES()
        {
            GameRuleSet defaultRules = new GameRuleSet();
            
            defaultRules.AddShipTypeRule(new Carrier(), 1);
            defaultRules.AddShipTypeRule(new Cruiser(), 1);
            defaultRules.AddShipTypeRule(new BattleShip(), 1);
            defaultRules.AddShipTypeRule(new Submarine(), 2);
            defaultRules.AddShipTypeRule(new Destroyer(), 2);
            //
            defaultRules.FieldSize = 10;
            return defaultRules;

        }
        
        public GameRuleSet()
        {
            ShipTypeRules = new Dictionary<ShipType, int>();
        }

        [DataMember]
        public int FieldSize { get; set; }
        [DataMember]
        public Dictionary<ShipType, int> ShipTypeRules { get; set; }

        public void AddShipTypeRule(ShipType shipType, int count)
        {
            
            if (count > 0)
            {
                ShipTypeRules.Add(shipType, count);
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
        public int GetShipTypeCount(ShipType shipType)
        {

            if (ShipTypeRules.ContainsKey(shipType)){
                return ShipTypeRules[shipType];
            }
            return 0;
            
        }

        public int BattleshipCount { get; set; }
        public int CarrierCount { get; set; }
        public int CruiserCount { get; set; }
        public int DestroyerCount { get; set; }
        public int SubmarineCount { get; set; }
    }
}
