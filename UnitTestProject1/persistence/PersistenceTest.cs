using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.GameLogic;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestProject1.persistence
{
    [TestClass]
    public class PersistenceTest
    {
        [TestMethod]
        public void Test()
        {
            //GamePersistenceManager manager = new GamePersistenceManager("test.xml");
            ////Delete all games created by previous invocations of this test
            //manager.GameList.Clear();

            //GameData data = new GameData();
            //var PlayerOneData = new PlayerData("turd");
            //var PlayerTwoData = new PlayerData("burglar");


            ////data.SetPlayer(new PlayerData("turd"), new Field(data.RuleSet.FieldSize));
            ////data.SetPlayer(new PlayerData("burglar"), new Field(data.RuleSet.FieldSize));

            //var playerOne = new StubPlayer();
            //var playerTwo = new StubPlayer();
            //data.CurrentPlayer = 0;


            //var game = new Game(data);
            //game.AddPlayer(PlayerOneData, playerOne);
            //game.AddPlayer(PlayerTwoData, playerTwo);

            //var pl1Placement = game.PlaceShips(game.Players[0].UUID, AIPlayer.SimplePlacement().ToList());
            //Assert.IsTrue(pl1Placement);

            //var pl2Placement = game.PlaceShips(game.Players[1].UUID, AIPlayer.SimplePlacement().ToList());

            //Assert.IsTrue(pl2Placement);

            //var shot = game.Shoot(game.Players[0].UUID, new Coordinate(0, 0));
            //Assert.IsTrue(shot);



            //var shotTwo = game.Shoot(game.Players[0].UUID, new Coordinate(0, 0));
            //Assert.IsFalse(shotTwo);

            //Assert.IsTrue(game.Shoot(game.Players[1].UUID, new Coordinate(0, 0)));


            //Assert.IsTrue(game.Shoot(game.Players[0].UUID, new Coordinate(0, 1)));
            //Assert.IsTrue(game.Shoot(game.Players[1].UUID, new Coordinate(0, 1)));

            //Assert.IsTrue(game.Shoot(game.Players[0].UUID, new Coordinate(0, 2)));
            //Assert.IsTrue(game.Shoot(game.Players[1].UUID, new Coordinate(0, 2)));

            //Assert.IsTrue(game.Shoot(game.Players[0].UUID, new Coordinate(0, 3)));
            //Assert.IsTrue(game.Shoot(game.Players[1].UUID, new Coordinate(0, 3)));

            //Assert.IsTrue(game.Shoot(game.Players[0].UUID, new Coordinate(0, 4)));
            //Assert.IsTrue(game.Shoot(game.Players[1].UUID, new Coordinate(0, 4)));

            //manager.GameList.Add(data);
            //manager.Save();






        }


    }
}
