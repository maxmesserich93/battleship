using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{

    [DataContract]
    [KnownType(typeof(BattleShip)), KnownType(typeof(Carrier)), KnownType(typeof(Cruiser)),
        KnownType(typeof(Destroyer)), KnownType(typeof(Submarine))]
    public abstract class Ship
    {
        [DataMember]
        public Coordinate StartCoordinate { get; set; }
        [DataMember]
        public Boolean IsVertical { get; set; }
        [DataMember]
        public int Length { get; set; }
        [DataMember]
        public int HitCount { get; set; }
        [DataMember]
        public string Type { get; set; }

        protected Ship() { }

        protected Ship(int length, string type)
        {

            Length = length;
            Type = type;
            //IsVertical = isVertical;
        }
        protected Ship(int length, string type, Coordinate coordinate, bool isVertical)
        {
            Length = length;
            Type = type;
            IsVertical = isVertical;
            StartCoordinate = coordinate;

        }

        /// <summary>
        /// Returns true if the provided coordinate is part of the Ship.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Boolean IsCoordinatePartOfShip(Coordinate coordinate)
        {
            return GetCoordinates().Contains(coordinate);
            //if (IsVertical)
            //{
            //    if (coordinate.X != StartCoordinate.X)
            //    {
            //        return false;
            //    }
            //    return coordinate.Y >= StartCoordinate.Y && coordinate.Y < StartCoordinate.Y + Length;


            //}
            //else
            //{
            //    if (coordinate.Y != StartCoordinate.Y)
            //    {
            //        return false;
            //    }
            //    return coordinate.X >= StartCoordinate.X && coordinate.X < StartCoordinate.X + Length;

            //}
        }

        public List<Coordinate> GetCoordinates()
        {
            List<Coordinate> coordinates;
            if (IsVertical)
            {
                coordinates = CoordinateExtensions.GetVerticalList(StartCoordinate, Length);

            }
            else
            {
                coordinates = CoordinateExtensions.GetHorizontalList(StartCoordinate, Length);
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
            return HitCount == Length;
        }

        public override string ToString()
        {
            return GetType().Name + ": "+this.StartCoordinate+" - "+this.HitCount+" / "+this.Length;
        }

        public override bool Equals(object obj)
        {
            var ship = obj as Ship;
            return ship != null &&
                   ship.GetType().Equals(GetType()) && 
                   EqualityComparer<Coordinate>.Default.Equals(StartCoordinate, ship.StartCoordinate) &&
                   IsVertical == ship.IsVertical &&
                   Length == ship.Length &&
                   HitCount == ship.HitCount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    [DataContract, KnownType(typeof(Ship))]
    public class Carrier : Ship
    {
        public Carrier() : base(5, "Carrier") { }
        public Carrier(Coordinate coordinate, bool isVertical) : base(5, "Carrier",coordinate, isVertical) { }

    }
    [DataContract, KnownType(typeof(Ship))]
    public class BattleShip : Ship
    {
        public BattleShip() : base(4, "BattleShip") { }
        public BattleShip(Coordinate coordinate, bool isVertical) : base(4, "BattleShip", coordinate, isVertical) { }

    }
    [DataContract, KnownType(typeof(Ship))]
    public class Cruiser : Ship
    {
        public Cruiser() : base(3, "Cruiser") { }
        public Cruiser(Coordinate coordinate, bool isVertical) : base(3, "Cruiser", coordinate, isVertical) { }

    }
    [DataContract, KnownType(typeof(Ship))]
    public class Submarine : Ship
    {
        public Submarine() : base(2, "Submarine") { }
        public Submarine(Coordinate coordinate, bool isVertical) : base(2, "Submarine", coordinate, isVertical) { }

    }
    [DataContract, KnownType(typeof(Ship))]
    public class Destroyer : Ship
    {
        public Destroyer() : base(2, "Destroyer") { }
        public Destroyer(Coordinate coordinate, bool isVertical) : base(2, "Destroyer", coordinate, isVertical) { }

    }
}
