using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model.Ship
{
    [DataContract, KnownType(typeof(Cruiser))]
    public class Cruiser : AbstractShip
    {

        public static readonly string TYPE = "Cruiser";
        public static readonly int LENGTH = 4;

        public Cruiser() : base(LENGTH, TYPE) { }




    }
}

