using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.GameLogic
{

    public enum GamePhase
    {
        Init,
        WaitingForPlayers,
        ShipPlacement,
        WaitingForPlacement,
        InProgress,
        PlayerOneTurn,
        PlayerTwoTurn,
        Error,
        Finished
    }
}
