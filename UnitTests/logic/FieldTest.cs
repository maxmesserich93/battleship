using Model;
using Model.Ship;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.logic
{
    [TestFixture]
    class FieldTest
    {

        [Test]
        public void PlaceShip()
        {
            Field field = new Field(10);

            AbstractShip carrier = new Carrier();
            carrier.IsVertical = true;
            bool placement = field.PlaceShip(carrier, new Coordinate(4, 4));
            Assert.True(placement);




        }

    }
}
