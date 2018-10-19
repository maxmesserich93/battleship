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
    public class GameTest
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
        //[TestMethod]
        //public void TestAddPlayer()
        //{
        //    var game = new Game("asd", GameRuleSet.DEFAULT_RULES());
        //    PlayerData player = new PlayerData("aasd");
        //    player.Name = "tmp";
        //    Game.AddPlayer(player, new StubPlayer());

        //    var players = Game.Players;

        //    Assert.IsTrue(players.Contains(player));



            
            

        //}


        //[TestMethod]
        //public void TestRules()
        //{


        //    //Assert.AreEqual(new Cruiser(), new Cruiser());
        //    GameData data = new GameData();
        //    var PlayerOneData = new PlayerData("turd");
        //    var PlayerTwoData = new PlayerData("burglar");


        //    //data.SetPlayer(new PlayerData("turd"), new Field(data.RuleSet.FieldSize));
        //    //data.SetPlayer(new PlayerData("burglar"), new Field(data.RuleSet.FieldSize));

        //    var playerOne = new StubPlayer();
        //    var playerTwo = new StubPlayer();
        //    data.CurrentPlayer = 0;
        //    var Game = new Game(data);
        //    Game.AddPlayer(PlayerOneData, playerOne);
        //    Game.AddPlayer(PlayerTwoData, playerTwo);

        //    //Should work
        //    var placed  = Game.PlaceShip(Game.Players[0], new Cruiser(new Coordinate(0, 0), true));

        //    Assert.IsTrue(placed);
        //    //Must not work because only once cruiser is allowed in default rules
        //    var placed2 = Game.PlaceShip(Game.Players[0], new Cruiser(new Coordinate(1, 0),true));
        //    Assert.IsFalse(placed2);




        //    var placed3 = Game.PlaceShip(Game.Players[0], new Cruiser(new Coordinate(2, 0), true));
        //    Assert.IsFalse(placed3);




        //}

        ///// <summary>
        ///// Test whether Placements work.
        ///// </summary>
        //[TestMethod]
        //public void TestPlaceShips()
        //{


        //    Game Game = new Game("asd", GameRuleSet.DEFAULT_RULES());
        //    PlayerData player = new PlayerData("asdasd");

        //    Game.AddPlayer(player, new StubPlayer());
        //    //List<ShipPlacement> illegalPlacement = new List<ShipPlacement>();

        //    //illegalPlacement.Add(new ShipPlacement(new Carrier(), true, new Coordinate(0, 0)));
        //    //illegalPlacement.Add(new ShipPlacement(new Cruiser(), true, new Coordinate(1, 0)));
        //    //illegalPlacement.Add(new ShipPlacement(new BattleShip(), true, new Coordinate(2, 0)));
        //    //illegalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(3, 0)));
        //    //illegalPlacement.Add(new ShipPlacement(new Destroyer(), true, new Coordinate(4, 0)));
        //    //illegalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(5, 0)));
        //    //illegalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(6, 0)));
        //    ////3 Submarines will fail!
        //    //illegalPlacement.Add(new ShipPlacement(new Submarine(), true, new Coordinate(7, 0)));

        //    //bool boom = Game.PlaceShips(player, illegalPlacement);

        //    //Assert.IsFalse(boom);

        //    Game Game2 = new Game("asd", GameRuleSet.DEFAULT_RULES());
        //    var playerData = new PlayerData("gfg");
        //    Game2.AddPlayer(playerData, new StubPlayer());
        //    List<Ship> legalPlacement = new List<Ship>();

        //    legalPlacement.Add(new Carrier(new Coordinate(0, 0), true));

        //    legalPlacement.Add(new Cruiser(new Coordinate(1, 0), true));

        //    legalPlacement.Add(new BattleShip(new Coordinate(2, 0), true));

        //    legalPlacement.Add(new Destroyer(new Coordinate(3, 0), true));
        //    legalPlacement.Add(new Destroyer(new Coordinate(4, 0), true));

        //    legalPlacement.Add(new Submarine(new Coordinate(5, 0), true));
        //    legalPlacement.Add(new Submarine(new Coordinate(6, 0), true));
        //    bool correct = Game2.PlaceShips(playerData.UUID, legalPlacement);

        //    Assert.IsTrue(correct);




        //}

        ////Test whether the game changes to in progress once both players placed their ships.
        //[TestMethod]
        //public void TestPhaseChange()
        //{
        //    Game Game = new Game("asd", GameRuleSet.DEFAULT_RULES());

        //    List<Ship> legalPlacement = new List<Ship>();

        //    legalPlacement.Add(new Carrier(new Coordinate(0, 0), true));

        //    legalPlacement.Add(new Cruiser(new Coordinate(1, 0), true));

        //    legalPlacement.Add(new BattleShip(new Coordinate(2, 0), true));

        //    legalPlacement.Add(new Destroyer(new Coordinate(3, 0), true));
        //    legalPlacement.Add(new Destroyer(new Coordinate(4, 0), true));

        //    legalPlacement.Add(new Submarine(new Coordinate(5, 0), true));
        //    legalPlacement.Add(new Submarine(new Coordinate(6, 0), true));
        //    //Add players
        //    IPlayerContract player1 = new StubPlayer();

        //    var playerData1 = new PlayerData("gfg");
        //    var playerData2 = new PlayerData("gfg");
        //    Game.AddPlayer(playerData1, new StubPlayer());

        //    Game.AddPlayer(playerData2, new StubPlayer());



        //    bool p1Placed = Game.PlaceShips(playerData1.UUID, legalPlacement);
        //    Assert.IsTrue(p1Placed);
        //    //Updating the gamestate will not result in a new state because the player2 has not placed ships
        //    Assert.AreEqual(Game.GamePhase.WaitingForPlacement, Game.Data.Phase);
        //    Game.UpdateGamePhase();
        //    Assert.AreEqual(Game.GamePhase.WaitingForPlacement, Game.Data.Phase);

        //    //When player2 has placed the ships and the game state is updated, the game should have switched state.

        //    bool p2Placed = Game.PlaceShips(playerData2.UUID, legalPlacement);
        //    Assert.IsTrue(p2Placed);
        //    Game.UpdateGamePhase();
        //    Assert.AreEqual(Game.GamePhase.InProgress, Game.Data.Phase);

        //    //Kill all of player2 ships









        //}
    }
}
