using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Game
    {
        public int ID { set; get; }
        public DateTime Date { set; get; }
        public PlayerScore Player1 { set; get; }
        public PlayerScore Player2 { set; get; }

        public int PlayerHits1 { set; get; }
        public int PlayerHits2 { set; get; }



    }
}