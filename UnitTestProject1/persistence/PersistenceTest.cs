using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.GameLogic;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            GamePersistenceManager manager = new GamePersistenceManager("test.xml");
            //Delete all games created by previous invocations of this test
            manager.GameList.Clear();

            GameData data = new GameData();
            var PlayerOneData = new PlayerData("turd");
            var PlayerTwoData = new PlayerData("burglar");


            //data.SetPlayer(new PlayerData("turd"), new Field(data.RuleSet.FieldSize));
            //data.SetPlayer(new PlayerData("burglar"), new Field(data.RuleSet.FieldSize));

            var playerOne = new StubPlayer();
            var playerTwo = new StubPlayer();
            data.CurrentPlayer = 0;


            var game = new Game(data);

            string gameID = game.Data.Id;

            game.AddPlayer(PlayerOneData, playerOne);
            game.AddPlayer(PlayerTwoData, playerTwo);

            var pl1Placement = game.PlaceShips(PlayerOneData.UUID, AIPlayer.SimplePlacement().ToList());
            Assert.IsTrue(pl1Placement);

            var pl2Placement = game.PlaceShips(PlayerTwoData.UUID, AIPlayer.SimplePlacement().ToList());

            Assert.IsTrue(pl2Placement);

            for(int y=0; y < 4; y++)
            {
                for(int i=0; i<2; i++)
                {
                    
                    game.Data.Shoot(i, new Coordinate(0, y));
                    game.Data.NextPlayer();
                }
            }

            for (int i = 0; i < 2; i++)
            {

                var result = game.Data.Shoot(i, new Coordinate(0, 4));
                game.Data.NextPlayer();

                Debug.Write(" RESULT " + i);
                result.ForEach(a => Debug.Write(a + ", "));
                Debug.WriteLine("");

                Assert.AreEqual(result.Count, 5);

                Assert.IsTrue(result.Count(position => position.FieldPositionStatus != FieldPositionStatus.ShotKill) == 0);
            }




            manager.GameList.Add(data);
            manager.Save();


           var newManager = new GamePersistenceManager("test.xml");


            var loadedGame = newManager.LoadGame(gameID);


            loadedGame.PlayerFields.ForEach(field => field.PrintField());


            Assert.IsTrue(loadedGame.Id.Equals(gameID));
            //Check fieldpositions
            for(int i=0; i < 2; i++)
            {
                Assert.IsTrue(game.Data.PlayerFields[i].GetPosition(0, 0).FieldPositionStatus == FieldPositionStatus.ShotKill);
                Assert.IsTrue(game.Data.PlayerFields[i].GetPosition(0,1).FieldPositionStatus == FieldPositionStatus.ShotKill);
                Assert.IsTrue(game.Data.PlayerFields[i].GetPosition(0, 2).FieldPositionStatus == FieldPositionStatus.ShotKill);
                Assert.IsTrue(game.Data.PlayerFields[i].GetPosition(0, 3).FieldPositionStatus == FieldPositionStatus.ShotKill);

            }



        }


    }
}
