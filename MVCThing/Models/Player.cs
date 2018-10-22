using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCThing.Models
{
    public class Player
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public DateTime JoinDate { set; get; }
        public int GamesPlayed { set; get; }
        public int GamesWon { set; get; }
    }
}
