using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCThing.Models
{
    public class Game
    {
        public int ID { set; get; }
        public int PlayerOneId { set; get; }
        [Display(Name = "Player One")]
        public Player Player1 { set; get; }
        public int PlayerTwoId { set; get; }
        [Display(Name = "Player Two")]
        public Player Player2 { set; get; }


        public DateTime Date { set; get; }
        
        public int Player1Hits { set; get; }
        public int Player2Hits { set; get; }
        public int Winner { set; get; }

        public Game()
        {
            Player1 = new Player();
            Player2 = new Player();
            Date = DateTime.Now;
            Player1Hits = -1;
            Player2Hits = -1;
            Winner = -1;
        }
    }
}
