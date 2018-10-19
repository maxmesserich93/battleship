using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.GameLogic;
using Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.logic
{
    [TestClass]
    public class AITest
    {

        [TestInitialize]
        public void Init()
        {

            //var game = new GameLogic(new GameData());

            //PlayerData p1 = new PlayerData("p1");
            //p1.UUID = "p1";
            //PlayerData p2 = new PlayerData("p1");
            //p2.UUID = "p2";
            //var playerOne = new SmartAIPlayer(game);
            //game.AddPlayer(p1, new SmartAIPlayer(game));
            //playerOne.ProvideIdentity("p1");


            
            
            //var playerTwo = new SmartAIPlayer(game);
            //playerTwo.ProvideIdentity("p2");
            //game.AddPlayer(p2, playerTwo);
            

        }



        [TestMethod]
        public void Test(){
            var game = new Game(new GameData());

            PlayerData p1 = new PlayerData("p1");
            PlayerData p2 = new PlayerData("p1");


            var playerOne = new SmartAIPlayer(game);
            var playerTwo = new SmartAIPlayer(game);


            game.AddPlayer(p1, playerOne);
            playerOne.ProvideIdentity(p1.UUID);




            
            playerTwo.ProvideIdentity(p2.UUID);
            game.AddPlayer(p2, playerTwo);

        }
    }
}
