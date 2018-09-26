using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
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




    }

}
