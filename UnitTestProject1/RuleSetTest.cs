using Microsoft.VisualStudio.TestTools.UnitTesting;

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class RuleSetTest
    {
        [TestMethod]
        public void Test()
        {

            GameRuleSet set = GameRuleSet.DEFAULT_RULES();

            int count = set.GetShipTypeCount(new Submarine());

            Assert.AreEqual(2, count);




        }
    }
}
