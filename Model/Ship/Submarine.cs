using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Model.Ship
{
    [DataContract, KnownType(typeof(AbstractShip))]
    public class Submarine : AbstractShip
    {

        public static readonly string TYPE = "Submarine";
        public static readonly int LENGTH = 3;

        public Submarine() : base(LENGTH, TYPE) { }




    }
}
