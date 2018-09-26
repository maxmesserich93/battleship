using Model.Ship;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
{
    [DataContract]
    [KnownType(typeof(Battleship)), KnownType(typeof(Carrier)), KnownType(typeof(Cruiser)),
        KnownType(typeof(Destroyer)), KnownType(typeof(Submarine))]
   public abstract class AbstractShip
    {

        [DataMember]
        public Coordinate Coordinate { get; set; }
        [DataMember]
        public Boolean IsVertical { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int Length { get; set; }


        /// <summary>
        /// Contstructor of the AbstractShip.
        /// </summary>
        /// <param name="sourceCoordinate"></param>
        /// <param name="length"></param>
        /// <param name="vertical"></param>
        /// <param name="type"></param>
        protected AbstractShip( int length, string type)
        {
            Length = length;
            IsVertical = true;
            Type = type;
        }


        public override bool Equals(object obj)
        {
            var ship = obj as AbstractShip;
            return ship != null &&
                   EqualityComparer<Coordinate>.Default.Equals(Coordinate, ship.Coordinate) &&
                   IsVertical == ship.IsVertical &&
                   Type == ship.Type &&
                   Length == ship.Length;
        }

        public override int GetHashCode()
        {
            var hashCode = 743003864;
            hashCode = hashCode * -1521134295 + EqualityComparer<Coordinate>.Default.GetHashCode(Coordinate);
            hashCode = hashCode * -1521134295 + IsVertical.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            return hashCode;
        }

    

    }
}
