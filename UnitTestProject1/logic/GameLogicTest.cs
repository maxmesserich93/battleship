using Microsoft.VisualStudio.TestTools.UnitTesting;

using Models;
using Models.GameLogic;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestProject1
{
    [TestClass]
    public class GameTest
    {

        //Test whether the game changes to in progress once both players placed their ships.
        [TestMethod]
        public void TestPhaseChange()
        {
            Game Game = new Game(new GameData());

            List<Ship> legalPlacement = new List<Ship>();

            legalPlacement.Add(new Carrier(new Coordinate(0, 0), true));

            legalPlacement.Add(new Cruiser(new Coordinate(1, 0), true));

            legalPlacement.Add(new BattleShip(new Coordinate(2, 0), true));

            legalPlacement.Add(new Destroyer(new Coordinate(3, 0), true));
            legalPlacement.Add(new Destroyer(new Coordinate(4, 0), true));

            legalPlacement.Add(new Submarine(new Coordinate(5, 0), true));
            legalPlacement.Add(new Submarine(new Coordinate(6, 0), true));
            //Add players
            IPlayerContract player1 = new StubPlayer();

            var playerData1 = new PlayerData("gfg");
            var playerData2 = new PlayerData("gfg");
            Game.AddPlayer(playerData1, new StubPlayer());

            Game.AddPlayer(playerData2, new StubPlayer());



            bool p1Placed = Game.PlaceShips(playerData1.UUID, legalPlacement);
            Assert.IsTrue(p1Placed);
            //Updating the gamestate will not result in a new state because the player2 has not placed ships
            Assert.AreEqual(GamePhase.Init, Game.Data.Phase);
            bool p2Placed = Game.PlaceShips(playerData2.UUID, legalPlacement);
            Assert.IsTrue(p2Placed);
            Assert.AreEqual(GamePhase.InProgress, Game.Data.Phase);

            ////When player2 has placed the ships and the game state is updated, the game should have switched state.

            //bool p2Placed = Game.PlaceShips(playerData2.UUID, legalPlacement);
            //Assert.IsTrue(p2Placed);
            //Game.UpdateGamePhase();
            //Assert.AreEqual(Game.GamePhase.InProgress, Game.Data.Phase);

            //Kill all of player2 ships









        }
    }
}
