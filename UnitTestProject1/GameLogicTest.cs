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
    public class GameLogicTest
    {

        //class StubPlayer : AbstractPlayer
        //{
        //    public override Field OpponentRepresentation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        //    public override void ProvideShipPlacements()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public override Coordinate PlaceShot(Field field)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        [TestMethod]
        public void TestAddPlayer()
        {
            GameLogic gameLogic = new GameLogic("asd", GameRuleSet.DEFAULT_RULES());
            PlayerData player = new PlayerData();
            player.Name = "tmp";
            gameLogic.AddPlayer("tmp", player.Player);

            List<PlayerData> players = gameLogic.Players;

            Assert.IsTrue(players.Contains(player));



            
            

        }


        [TestMethod]
        public void TestRules()
        {


            //Assert.AreEqual(new Cruiser(), new Cruiser());

            GameLogic gameLogic = new GameLogic("asd", GameRuleSet.DEFAULT_RULES() );
            PlayerData player = new PlayerData();
            player.Player = new AbstractPlayer();
            player.Name = "tmp";
            gameLogic.AddPlayer("tmp", player.Player);
            Ship ship = new Ship(new Cruiser());
            //Should work
            Ship placed  = gameLogic.PlaceShip(player, new Cruiser(), true, new Coordinate(0, 0));

            Assert.IsNotNull(placed);
            //Must not work because only once cruiser is allowed in default rules
            Ship placed2 = gameLogic.PlaceShip(player, new Cruiser(), true, new Coordinate(1, 0));
            Assert.IsNull(placed2);




            Ship placed3 = gameLogic.PlaceShip(player, new Cruiser(), true, new Coordinate(2, 0));
            Assert.IsNull(placed3);




        }

        /// <summary>
        /// Test whether Placements work.
        /// </summary>
        [TestMethod]
        public void TestPlaceShips()
        {


            GameLogic gameLogic = new GameLogic("asd", GameRuleSet.DEFAULT_RULES());
            PlayerData player = new PlayerData();
            player.Player = new AbstractPlayer();
            player.Name = "tmp";

            gameLogic.AddPlayer("tmp", player.Player);
            List<ShipPlacement> illegalPlacement = new List<ShipPlacement>();

            illegalPlacement.Add(new ShipPlacement(new Carrier(), true, new Coordinate(0, 0)));
            illegalPlacement.Add(new ShipPlacement(new Cruiser(), true, new Coordinate(1, 0)));
            illegalPlacement.Add(new ShipPlacement(new BattleShip(), true, new Coordinate(2, 0)));
            illegalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(3, 0)));
            illegalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(4, 0)));
            illegalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(5, 0)));
            illegalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(6, 0)));
            //3 Submarines will fail!
            illegalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(7, 0)));

            bool boom = gameLogic.PlaceShips("tmp", illegalPlacement);

            Assert.IsFalse(boom);

            GameLogic gameLogic2 = new GameLogic("asd", GameRuleSet.DEFAULT_RULES());
            gameLogic2.AddPlayer("tmp", player.Player);
            List<ShipPlacement> legalPlacement = new List<ShipPlacement>();

            legalPlacement.Add(new ShipPlacement(new Carrier(), true, new Coordinate(0, 0)));
            legalPlacement.Add(new ShipPlacement(new Cruiser(), true, new Coordinate(1, 0)));
            legalPlacement.Add(new ShipPlacement(new BattleShip(), true, new Coordinate(2, 0)));
            legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(3, 0)));
            legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(4, 0)));
            legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(5, 0)));
            legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(6, 0)));
;
            bool correct = gameLogic2.PlaceShips("tmp", legalPlacement);

            Assert.IsTrue(correct);




        }

        //Test whether the game changes to in progress once both players placed their ships.
        [TestMethod]
        public void TestPhaseChange()
        {
            //GameLogic gameLogic = new GameLogic("asd", GameRuleSet.DEFAULT_RULES());
            //List<ShipPlacement> legalPlacement = new List<ShipPlacement>();
            //legalPlacement.Add(new ShipPlacement(new Carrier(), true, new Coordinate(0, 0)));
            //legalPlacement.Add(new ShipPlacement(new Cruiser(), true, new Coordinate(1, 0)));
            //legalPlacement.Add(new ShipPlacement(new BattleShip(), true, new Coordinate(2, 0)));
            //legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(3, 0)));
            //legalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(4, 0)));
            //legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(5, 0)));
            //legalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(6, 0)));
            //Add players
            //IPlayerContract player1 = new AbstractPlayer();
            //gameLogic.AddPlayer("p1", player1);
            //IPlayerContract player2 = new AbstractPlayer();
            //gameLogic.AddPlayer("p2", player2);



            //bool p1Placed = gameLogic.ProvideShipPlacements(player1, legalPlacement);
            //Assert.IsTrue(p1Placed);
            //Updating the gamestate will not result in a new state because the player2 has not placed ships
            //Assert.AreEqual(GameLogic.GamePhase.ShipPlacement, gameLogic.Phase);
            //gameLogic.UpdateGamePhase();
            //Assert.AreEqual(GameLogic.GamePhase.ShipPlacement, gameLogic.Phase);

            //When player2 has placed the ships and the game state is updated, the game should have switched state.

            //bool p2Placed = gameLogic.ProvideShipPlacements(player2, legalPlacement);
            //Assert.IsTrue(p2Placed);
            //gameLogic.UpdateGamePhase();
            //Assert.AreEqual(GameLogic.GamePhase.InProgress, gameLogic.Phase);

            //Kill all of player2 ships









        }
    }
}
