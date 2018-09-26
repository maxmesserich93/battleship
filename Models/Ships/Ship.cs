using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
    [DataContract]
    [KnownType(typeof(Carrier))]
    [KnownType(typeof(Submarine))]
    [KnownType(typeof(Cruiser))]
    [KnownType(typeof(Destroyer))]
    [KnownType(typeof(BattleShip))]
    public abstract class ShipType
    {
  
        [DataMember]
        public int Lenght { get; set; }
        [DataMember]
        public string Name { get; set; }

        protected ShipType(int lenght, string name)
        {
            Lenght = lenght;
            Name = name;
        }



        //public override bool Equals(object obj)
        //{
        //    var type = obj as ShipType;
        //    return type != null &&
        //           Lenght == type.Lenght &&
        //           Name == type.Name;
        //}

        //public override int GetHashCode()
        //{
        //    var hashCode = 1907045291;
        //    hashCode = hashCode * -1521134295 + Lenght.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        //    return hashCode;
        //}
    }
    [DataContract]
    public class Carrier : ShipType
    {
        public Carrier() : base(5, "Carrier") { }
        //[DataMember]
        //public override int Lenght => 5;
        //[DataMember]
        //public override string Name => "Carrier";


    }
    [DataContract]
    public class BattleShip : ShipType
    {
        public BattleShip() : base(4, "BattleShip") { }
        //[DataMember]
        //public override int Lenght => 4;
        //[DataMember]
        //public override string Name => "BattleShip";
    }
    [DataContract]
    public class Cruiser : ShipType
    {
        public Cruiser() : base(3, "Cruiser") { }
        //[DataMember]
        //public override int Lenght => 3;
        //[DataMember]
        //public override string Name => "Cruiser";
    }
    [DataContract]
    public class Submarine : ShipType
    {
        public Submarine() : base(2, "Submarine") { }
        //[DataMember]
        //public override int Lenght => 2;
        //[DataMember]
        //public override string Name => "Submarine";
    }
    [DataContract]
    public class Destroyer : ShipType
    {
        public Destroyer() : base(2, "Destroyer") { }

        //public override int Lenght => 2;

        //public override string Name => "Destroyer";
    }
    [DataContract]
    public class ShipPlacement
    {
        [DataMember]
        public ShipType ShipType { set; get; }
        [DataMember]
        public bool IsVertical { set; get; }
        [DataMember]
        public Coordinate StartCoordinate { set; get; }

        public ShipPlacement(ShipType shipType, bool isVertical, Coordinate startCoordinate)
        {
            ShipType = shipType;
            IsVertical = isVertical;
            StartCoordinate = startCoordinate;
        }

        public override bool Equals(object obj)
        {
            var placement = obj as ShipPlacement;
            return placement != null &&
                   EqualityComparer<ShipType>.Default.Equals(ShipType, placement.ShipType) &&
                   IsVertical == placement.IsVertical &&
                   EqualityComparer<Coordinate>.Default.Equals(StartCoordinate, placement.StartCoordinate);
        }

        public override string ToString()
        {
            return ShipType.ToString() + " : " + IsVertical.ToString() + " : " + StartCoordinate.ToString();
        }

        public override int GetHashCode()
        {
            var hashCode = -978531925;
            hashCode = hashCode * -1521134295 + EqualityComparer<ShipType>.Default.GetHashCode(ShipType);
            hashCode = hashCode * -1521134295 + IsVertical.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Coordinate>.Default.GetHashCode(StartCoordinate);
            return hashCode;
        }
    }

    //[DataContract]
    //[KnownType(typeof(Battleship)), KnownType(typeof(Carrier)), KnownType(typeof(Cruiser)),
    //    KnownType(typeof(Destroyer)), KnownType(typeof(Submarine))]
    public class Ship
    {

        [DataMember]
        ShipPlacement ShipPlacement { get; set; }
        [DataMember]
        public Coordinate StartCoordinate { get; set; }
        [DataMember]
        public Boolean IsVertical { get; set; }
        //[DataMember]

        //[DataMember]
        public int Length { get; set; }

        public int HitCount { get; set; }
        [DataMember]
        public ShipType Type { set; get; }



        public Ship(ShipType shipType)
        {
            Length = shipType.Lenght;
            IsVertical = true;
            Type = shipType;
        }

        public Ship(ShipPlacement placement)
        {
            ShipPlacement = placement;
            Length = placement.ShipType.Lenght;
            IsVertical = placement.IsVertical;
            StartCoordinate = placement.StartCoordinate;
            Type = placement.ShipType;
        }


        public override bool Equals(object obj)
        {
            var ship = obj as Ship;
            return ship != null &&
                   EqualityComparer<Coordinate>.Default.Equals(StartCoordinate, ship.StartCoordinate) &&
                   IsVertical == ship.IsVertical &&
                   Type == ship.Type &&
                   Length == ship.Length;
        }


        /// <summary>
        /// Returns true if the provided coordinate is part of the Ship.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Boolean IsCoordinatePartOfShip(Coordinate coordinate)
        {
            if (IsVertical)
            {
                if(coordinate.X != StartCoordinate.X)
                {
                    return false;
                }
                return coordinate.Y >= StartCoordinate.Y && coordinate.Y < StartCoordinate.Y + Length;


            }
            else
            {
                if(coordinate.Y != StartCoordinate.Y)
                {
                    return false;
                }
                return coordinate.X >= StartCoordinate.X && coordinate.X < StartCoordinate.X + Length;

            }
        }

        public List<Coordinate> GetCoordinates()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            if (IsVertical)
            {
                for(int i=0; i<Length; i++)
                {
                    coordinates.Add(new Coordinate(StartCoordinate.X, StartCoordinate.Y+i));
                }

            }
            else
            {
                for (int i = 0; i < Length; i++)
                {
                    coordinates.Add(new Coordinate(StartCoordinate.X+i, StartCoordinate.Y));
                }
            }
            return coordinates;
        }

        public List<Coordinate> CollisionWithShip(Ship other)
        {
            List<Coordinate> otherCoordinates = other.GetCoordinates();
            return GetCoordinates().Where(c => otherCoordinates.Contains(c)).ToList();
        }
        /// <summary>
        /// Shoots the ship and returns true if its killed.
        /// </summary>
        /// <returns></returns>
        public Boolean ShootShip()
        {
            HitCount++;
            return HitCount == Length;
        }

        public Boolean IsKilled()
        {
            return HitCount.Equals(Length);
        }

        public override int GetHashCode()
        {
            var hashCode = -1526870337;
            hashCode = hashCode * -1521134295 + EqualityComparer<Coordinate>.Default.GetHashCode(StartCoordinate);
            hashCode = hashCode * -1521134295 + IsVertical.GetHashCode();
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            hashCode = hashCode * -1521134295 + HitCount.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return this.Type.Name + ": "+this.StartCoordinate+" - "+this.HitCount;
        }
    }
}
