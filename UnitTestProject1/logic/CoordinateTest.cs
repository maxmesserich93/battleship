using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.logic
{
    [TestClass]
    public class CoordinateTest
    {
        [TestMethod]
        public void Test()
        {

            Coordinate c = new Coordinate(0, 0);
            var list = c.GetNeighbours(10);

            Assert.IsTrue(list.Contains(new Coordinate(1, 0)));
            Assert.IsTrue(list.Contains(new Coordinate(0, 1)));
            Assert.IsFalse(list.Contains(new Coordinate(-1, 0)));
            Assert.IsFalse(list.Contains(new Coordinate(0, -1)));




        }

        [TestMethod]
        public void Test2()
        {

            Coordinate c = new Coordinate(9, 9);
            var list = c.GetNeighbours(10);

            Assert.IsTrue(list.Contains(new Coordinate(8, 9)));
            Assert.IsTrue(list.Contains(new Coordinate(9, 8)));
            Assert.IsFalse(list.Contains(new Coordinate(10, 9)));
            Assert.IsFalse(list.Contains(new Coordinate(9, 10)));




        }

        [TestMethod]
        public void Test3()
        {

            Coordinate c = new Coordinate(0, 9);
            var list = c.GetNeighbours(10);

            Assert.IsTrue(list.Contains(new Coordinate(0, 8)));
            Assert.IsTrue(list.Contains(new Coordinate(1, 9)));
            Assert.IsFalse(list.Contains(new Coordinate(-1, 9)));
            Assert.IsFalse(list.Contains(new Coordinate(0, 10)));




        }

    }
}
