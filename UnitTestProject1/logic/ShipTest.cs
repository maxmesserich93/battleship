using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class ShipTest
    {
        /// <summary>
        /// Test vertical IsCoordinatePartOfShip
        /// </summary>
        [TestMethod]
        public void TestIsCoordinatePartOfVerticalShip()
        {

            Ship ship = new BattleShip(new Coordinate(2, 2), true);

            //Start coordinate should be part of the ship.
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(2, 2)));
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(2, 3)));
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(2, 4)));
            //Cruiser has a lenght of 4. Therefor 2,5 should be part of the ship.
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(2, 5)));
            
            Assert.IsFalse(ship.IsCoordinatePartOfShip(new Coordinate(2, 6)));
            //2, 7 not
            Assert.IsFalse(ship.IsCoordinatePartOfShip(new Coordinate(2, 7)));
            //2, 1 not
            Assert.IsFalse(ship.IsCoordinatePartOfShip(new Coordinate(2, 1)));
        }
        


        /// <summary>
        /// Test horizontal IsCoordinatePartOfShip
        /// </summary>
        [TestMethod]
        public void TestIsCoordinatePartOfHorizontalShip()
        {

            Ship ship = new BattleShip(new Coordinate(2, 2), false);
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(2, 2)));
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(3, 2)));
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(4, 2)));
            //Cruiser has a lenght of 4. Therefor 5,2 should be part of the ship.
            Assert.IsTrue(ship.IsCoordinatePartOfShip(new Coordinate(5, 2)));

            //6, 2 not
            Assert.IsFalse(ship.IsCoordinatePartOfShip(new Coordinate(6, 2)));
           
            Assert.IsFalse(ship.IsCoordinatePartOfShip(new Coordinate(7, 2)));
            //1,2 not
            Assert.IsFalse(ship.IsCoordinatePartOfShip(new Coordinate(1, 2)));
        }
        [TestMethod]
        public void TestGetCoordinatesHorizontal()
        {
            var horizontalCruiser = new BattleShip(new Coordinate(2, 2), false);

            List<Coordinate> coordinates = horizontalCruiser.GetCoordinates();
            List<Coordinate> expectedCoordinates = new List<Coordinate>();
            expectedCoordinates.Add(new Coordinate(2, 2));
            expectedCoordinates.Add(new Coordinate(3, 2));
            expectedCoordinates.Add(new Coordinate(4, 2));
            expectedCoordinates.Add(new Coordinate(5, 2));

            expectedCoordinates.ForEach(c => Assert.IsTrue(coordinates.Contains(c)));



            //Assert.AreEqual(coordinates, expectedCoordinates);

        }
        /// <summary>
        /// Test ShootShip by shooting a cruiser (4 coordinates) 4 times.
        /// The first 3 shots return false since the ship is not killed yet.
        /// The 4th returns true since all parts of the ship are shot.
        /// </summary>
        [TestMethod]
        public void TestShootShip()
        {
            var verticalCruiser = new Cruiser(new Coordinate(0, 0), true);

            Assert.IsFalse(verticalCruiser.ShootShip());
            Assert.IsFalse(verticalCruiser.IsKilled());
            Assert.IsFalse(verticalCruiser.ShootShip());
            Assert.IsFalse(verticalCruiser.IsKilled());
            Assert.IsTrue(verticalCruiser.ShootShip());
            Assert.IsTrue(verticalCruiser.IsKilled());



        }

        [TestMethod]
        public void TestGetCoordinatesVertical()
        {
            Ship verticalCruiser = new BattleShip(new Coordinate(2, 2), true);

            List<Coordinate> coordinates = verticalCruiser.GetCoordinates();
            List<Coordinate> expectedCoordinates = new List<Coordinate>();
            expectedCoordinates.Add(new Coordinate(2, 2));
            expectedCoordinates.Add(new Coordinate(2, 3));
            expectedCoordinates.Add(new Coordinate(2, 4));
            expectedCoordinates.Add(new Coordinate(2, 5));

            expectedCoordinates.ForEach(c => Assert.IsTrue(coordinates.Contains(c)));
        }
        [TestMethod]
        public void TestCollusion()
        {

            Ship a = new BattleShip(new Coordinate(0,0), true);
            Ship b = new Destroyer(new Coordinate(0, 0), true);

            a.StartCoordinate = new Coordinate(0, 0);
            b.StartCoordinate = new Coordinate(0, 0);
            
            List<Coordinate> collisions = a.CollisionWithShip(b);

            List<Coordinate> expectedCollisionPoints = new List<Coordinate>();

            expectedCollisionPoints.Add(new Coordinate(0, 0));
            expectedCollisionPoints.Add(new Coordinate(0, 1));
            /**
             * All points of the smaller ship should be part of the collision list.
             */
            expectedCollisionPoints.ForEach(p => Assert.IsTrue(collisions.Contains(p)));

            /**
             * No collision because the destroyer is placed at 0,6 while the last coordinate of the cruiser is 0,5
             */
            b.StartCoordinate = new Coordinate(0, 6);

            List<Coordinate> collisions2 = a.CollisionWithShip(b);
         
            Assert.AreEqual(0, collisions2.Count());

            /**
             * There should be an overlap at 2,1!
             */
            a.StartCoordinate = new Coordinate(0, 1);
            b.StartCoordinate = new Coordinate(2, 0);
            a.IsVertical = false;
            expectedCollisionPoints = new List<Coordinate>();
            expectedCollisionPoints.Add(new Coordinate(2, 1));
            List<Coordinate> point = a.CollisionWithShip(b);
            expectedCollisionPoints.ForEach(p => Assert.IsTrue(point.Contains(p)));



            /**
             * No collision since both ships are verical and start at different x positions.
             */
            b.StartCoordinate = new Coordinate(2, 29);
       
            List<Coordinate> collisions3 = a.CollisionWithShip(b);
            Assert.AreEqual(0, collisions3.Count);


        
        }


    }
}
