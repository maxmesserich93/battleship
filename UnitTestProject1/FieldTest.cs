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
            Field field = new Field(GameRuleSet.DEFAULT_RULES());
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
            Field field = new Field(GameRuleSet.DEFAULT_RULES());

            Ship ship = new Ship(new Cruiser());
            //Outof bounds because negative
            Assert.IsNull(field.PlaceShip(new Cruiser(), false, new Coordinate(-1, 0)));



            // cruiser can be placed at 0,0 vertically
            Assert.IsNotNull(field.PlaceShip(new Cruiser(), true, new Coordinate(0, 1)));
            Assert.IsNotNull(field.PlaceShip(new Destroyer(), true, new Coordinate(1, 1)));
            Assert.IsNotNull(field.PlaceShip(new Destroyer(), false, new Coordinate(0, 0)));
            //Adding the same ship at a different position is okay.

            Assert.IsNotNull(field.PlaceShip(new Cruiser(), false, new Coordinate(3, 2)));


            //Destroyer can not be placed a 0,0 horizontally because occupied
            Ship ship1 = new Ship(new Destroyer());
            ship1.IsVertical = false;
            Assert.IsNull(field.PlaceShip(new Destroyer(), false, new Coordinate(0, 0)));
            //However it can be placed at 1,0
            Assert.IsNotNull(field.PlaceShip(new Destroyer(), false, new Coordinate(1, 0)));
            //Coordinate of the Destroyer is now 10,0
            //Assert.AreEqual(new Coordinate(1, 0), ship1.StartCoordinate);

            Ship ship2 = new Ship(new Destroyer());
            ship2.IsVertical = false;
            //It can not be placed at 9,0 because it wont fit
            Assert.IsNull(field.PlaceShip(new Destroyer(), false, new Coordinate(9, 0)));
            //It does fit at 8,0
            Assert.IsNotNull(field.PlaceShip(new Destroyer(), false, new Coordinate(8, 0)));


        }


        /// <summary>
        /// Tests the responses of the ShootCoordinate-method.
        /// Tests whether a ship is killed after a certain amounts of hits.
        /// </summary>
        [TestMethod]
        public void TestShootCoordinate()
        {
            Field field = new Field(GameRuleSet.DEFAULT_RULES());
             //= new Ship(new Destroyer());
            Ship b = field.PlaceShip(new Destroyer(), true, new Coordinate(0, 0));
            field.PlaceShip(new Destroyer(),true, new Coordinate(5, 0));

            //Out of bounds shot returns 0
            List<FieldPosition> shotResponse1 = field.ShootCoordinate(new Coordinate(-1,0));
            Assert.IsNull(shotResponse1);

            //Shooting an empty coordinate returns ShotMiss
            List<FieldPosition> shotResponse2 = field.ShootCoordinate(new Coordinate(2, 2));

            Assert.AreEqual(1, shotResponse2.Count);

          
            Assert.AreEqual(FieldPositionStatus.ShotMiss, shotResponse2[0].FieldPositionStatus);

            //Shooting the same coordinate again returns null since this is illegal IM CALLING CHILD PROTECTIVE SERVICES!

            List<FieldPosition> shotResponse3 = field.ShootCoordinate(new Coordinate(2, 2));
            Assert.IsNull(shotResponse3);


            /**
             * Shooting 0,0 returns ShotHit since it is part of the cruiser.
             * The cruiser should have one registered hit.
             */

            List<FieldPosition> shotResponse4 = field.ShootCoordinate(new Coordinate(0, 0));
            Assert.AreEqual(FieldPositionStatus.ShotHit, shotResponse4[0].FieldPositionStatus);
            Assert.AreEqual(1, b.HitCount);
            //B is not killed
            Assert.IsFalse(b.IsKilled());

            //Shooting 0,0 again is not allowed 
            List<FieldPosition> again = field.ShootCoordinate(new Coordinate(0, 0));
            Assert.IsNull(again);

            //Shooting 0,1 will hit ship b and kill it
            List<FieldPosition> killShot = field.ShootCoordinate(new Coordinate(0, 1));

            Assert.AreEqual(2, killShot.Count);

           

            //Assert.AreEqual(FieldPositionStatus.ShotKill, killShot.Value);
            //B is killed
            Assert.IsTrue(b.IsKilled());

        }



    }
}
