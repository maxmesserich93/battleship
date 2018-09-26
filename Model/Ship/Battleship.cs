using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model.Ship
{
    [DataContract, KnownType(typeof(Battleship))]
    public class Battleship : AbstractShip
    {

        public static readonly string TYPE = "Battleship";
        public static readonly int LENGTH = 4;

        public Battleship() : base(LENGTH, TYPE) { }




    }
}

