using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model.Ship
{
    [DataContract, KnownType(typeof(Destroyer))]
    public class Destroyer : AbstractShip
    {

        public static readonly string TYPE = "Destroyer";
        public static readonly int LENGTH = 2;

        public Destroyer() : base(LENGTH, TYPE) { }




    }
}

