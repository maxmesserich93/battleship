using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model.Ship
{
    [DataContract, KnownType(typeof(Carrier))]
    public class Carrier : AbstractShip
    {

        public static readonly string TYPE = "Carrier";
        public static readonly int LENGTH = 4;

        public Carrier() : base(LENGTH, TYPE) { }




    }
}

