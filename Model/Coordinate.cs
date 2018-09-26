using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model
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
    }

}
