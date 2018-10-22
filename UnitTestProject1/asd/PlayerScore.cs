using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PlayerScore
    {
        public PlayerScore Player { get; }
        public IEnumerable<Game> Games { get; }

        public PlayerScore(PlayerScore playerScore, IEnumerable<Game> games)
        {
            Player = playerScore;
            Games = games;
        }

    }
}