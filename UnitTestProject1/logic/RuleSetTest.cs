using Microsoft.VisualStudio.TestTools.UnitTesting;

using Models;
using Models.Player;
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

            var placements = AIPlayer.SimplePlacement();


            var result = set.ValidateRules(placements);
            Assert.IsTrue(result);



        }
    }
}
