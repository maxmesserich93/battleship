using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTestProject1
{
    [TestClass]
    public class FieldTest
    {
        /// <summary>
        /// Test the bounds checking.
        /// </summary>
        [TestMethod]
        public void BoundTest()
        {
            Field field = new Field(10);
            Assert.IsFalse(field.IsInBound(-1, 0));
            Assert.IsFalse(field.IsInBound(0, -1));
            Assert.IsFalse(field.IsInBound(10, 0));
            Assert.IsFalse(field.IsInBound(0, 10));
            Assert.IsTrue(field.IsInBound(0, 0));
            Assert.IsTrue(field.IsInBound(0, 9));
            Assert.IsTrue(field.IsInBound(9, 0));
            Assert.IsTrue(field.IsInBound(9, 0));

        }
        /// <summary>
        /// Test the placement of ships.
        /// Tests whether collision detection works
        /// Tests whether outoufbounds works.
        /// </summary>
        [TestMethod]
        public void TestPlaceShip()
        {
            Field field = new Field(10);

            //Ship ship = new Ship(new Cruiser());
            //Outof bounds because negative
            Assert.IsFalse(field.PlaceShip(new Cruiser(new Coordinate(-1, 0), false)));



            // cruiser can be placed at 0,0 vertically
            Assert.IsTrue(field.PlaceShip(new Cruiser(new Coordinate(0, 1), true)));
            Assert.IsTrue(field.PlaceShip(new Destroyer(new Coordinate(1, 1), true)));
            Assert.IsTrue(field.PlaceShip(new Destroyer(new Coordinate(0, 0), false)));
            //Adding the same ship at a different position is okay.

            Assert.IsNotNull(field.PlaceShip(new Cruiser(new Coordinate(3, 2), false)));


            //Destroyer can not be placed a 0,0 horizontally because occupied

            Assert.IsFalse(field.PlaceShip(new Destroyer(new Coordinate(0, 0), false)));


            //However it can be placed at 1,0
            Assert.IsTrue(field.PlaceShip(new Destroyer(new Coordinate(2, 0), false)));
            //Coordinate of the Destroyer is now 10,0
            //Assert.AreEqual(new Coordinate(1, 0), ship1.StartCoordinate);


            //It can not be placed at 9,0 because it wont fit
            Assert.IsFalse(field.PlaceShip(new Destroyer(new Coordinate(9, 0), false)));
            //It does fit at 8,0
            Assert.IsTrue(field.PlaceShip(new Destroyer(new Coordinate(8, 0), false)));


        }


        /// <summary>
        /// Tests the responses of the ShootCoordinate-method.
        /// Tests whether a ship is killed after a certain amounts of hits.
        /// </summary>
        [TestMethod]
        public void TestShootCoordinate()
        {
            Field field = new Field(10);
            //= new Ship(new Destroyer());
            var ship = new Destroyer(new Coordinate(0, 0), true);
            bool b = field.PlaceShip(ship);
            field.PlaceShip(new Destroyer(new Coordinate(5, 0), true));

            //Out of bounds shot returns 0
            List <FieldPosition> shotResponse1 = field.ShootCoordinate(new Coordinate(-1,0));
            Assert.IsNull(shotResponse1);

            //Shooting an empty coordinate returns ShotMiss
            List<FieldPosition> shotResponse2 = field.ShootCoordinate(new Coordinate(2, 2));

            Assert.AreEqual(1, shotResponse2.Count);

          
            Assert.AreEqual(FieldPositionStatus.ShotMiss, shotResponse2[0].FieldPositionStatus);

            //Shooting the same coordinate again returns null since this is illegal IM CALLING CHILD PROTECTIVE SERVICES!

            List<FieldPosition> shotResponse3 = field.ShootCoordinate(new Coordinate(2, 2));
            Assert.AreEqual(shotResponse3.Count, 0);


            /**
             * Shooting 0,0 returns ShotHit since it is part of the cruiser.
             * The cruiser should have one registered hit.
             */

            List<FieldPosition> shotResponse4 = field.ShootCoordinate(new Coordinate(0, 0));
            Assert.AreEqual(FieldPositionStatus.ShotHit, shotResponse4[0].FieldPositionStatus);
            Assert.AreEqual(1, ship.HitCount);
            //B is not killed
            Assert.IsFalse(ship.IsKilled());

            //Shooting 0,0 again is not allowed 
            List<FieldPosition> again = field.ShootCoordinate(new Coordinate(0, 0));
            Assert.AreEqual(again.Count, 0);

            //Shooting 0,1 will hit ship b and kill it
            List<FieldPosition> killShot = field.ShootCoordinate(new Coordinate(0, 1));

            Assert.AreEqual(2, killShot.Count);

           

            //Assert.AreEqual(FieldPositionStatus.ShotKill, killShot.Value);
            //B is killed
            Assert.IsTrue(ship.IsKilled());

        }



    }
}
