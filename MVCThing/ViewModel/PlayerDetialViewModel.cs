using MVCThing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCThing.ViewModel
{
    public class PlayerDetailViewModel
    {
        public Player Player { set; get; }
        public IEnumerable<Game> PlayedGames { set; get; }

    }
}
