using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class GameInstanceHandler
    {

        GameLogic _game;

        public GameInstanceHandler(GameLogic game)
        {
            _game = game;
        }

        /// <summary>
        /// Delegate for listeing to phase changes in the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newPhase"></param>
        protected virtual void OnPhaseChange(GameLogic sender, GameLogic.GamePhase newPhase)
        {
            switch (newPhase)
            {
                case GameLogic.GamePhase.ShipPlacement:
                    //init

                    break;
                case GameLogic.GamePhase.PlayerOneTurn:
                    //PlayerOne must shoot
                    break;
                case GameLogic.GamePhase.PlayerTwoTurn:
                    //PlayerTwo must shoot
                    break;
                case GameLogic.GamePhase.Finished:
                    //Inform clients about game beeing finished
                    break;
            }


        }


    }
}
