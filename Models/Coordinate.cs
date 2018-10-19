using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{

    /// <summary>
    /// Extension Methods for Coordinates
    /// </summary>
    public static class CoordinateExtensions
    {
        public static List<Coordinate> GetVerticalList(this Coordinate start, int length)
        {
            var coordinates = new List<Coordinate>();
            for (int i = 0; i < length; i++)
            {
                coordinates.Add(new Coordinate(start.X, start.Y + i));
            }
            return coordinates;

        }

        public static List<Coordinate> GetHorizontalList(this Coordinate start, int length)
        {
            var coordinates = new List<Coordinate>();
            for (int i = 0; i < length; i++)
            {
                coordinates.Add(new Coordinate(start.X+i, start.Y));
            }
            return coordinates;

        }

        public static List<Coordinate> GetNeighbours(this Coordinate position, int max)
        {
            int[,] directions = new int[,]{ { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };
            var list = new List<Coordinate>();
            for (int i = 0; i < 4; i++)
            {
                var neighbour = position + new Coordinate(directions[i, 0], directions[i, 1]);
                if (!(neighbour.X < 0 || neighbour.X >= max || neighbour.Y < 0 || neighbour.Y >= max))
                {
                    list.Add(neighbour);
                }
            }
            return list;



        }
    }

   



    [DataContract]
    public class Coordinate
    {
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }


        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";

        }
        /// <summary>
        /// Coordinates with the same x and y values are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var coordinate = obj as Coordinate;
            return coordinate != null &&
                   X == coordinate.X &&
                   Y == coordinate.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }



        /// <summary>
        /// Combines two Coordinates by addition.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
            {
                throw new ArgumentNullException("Coordinates must not be null");
            }
            return new Coordinate(a.X + b.X, a.Y + b.Y);
        }
        /// <summary>
        /// Combines two Coordinates by substraction.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Coordinate operator -(Coordinate a, Coordinate b)
        {
            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
            {
                throw new ArgumentNullException("Coordinates must not be null");
            }
            return new Coordinate(a.X - b.X, a.Y - b.Y);
        }

        //public static bool operator ==(Coordinate a, Coordinate b) => a.X == b.X && a.Y == b.Y;
        //public static bool operator !=(Coordinate a, Coordinate b) => a.X != b.X || a.Y != b.Y;
    }

}
