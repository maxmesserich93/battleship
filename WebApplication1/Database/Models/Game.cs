using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Game
    {
        public int ID { set; get; }
        public Player Winner { set; get; }
        public Player Looser { set; get; }
        public DateTime Date { set; get; }
        public int Player1Hits { set; get; }
        public int Player2Hits { set; get; }
    }
}
