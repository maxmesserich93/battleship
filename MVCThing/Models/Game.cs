using Microsoft.AspNetCore.Mvc.Rendering;
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
        public int PlayerOneID { set; get; }
        public int PlayerTwoID { set; get; }

        public DateTime Date { set; get; }
        public int Player1Hits { set; get; }
        public int Player2Hits { set; get; }
        [Range(0, 1, ErrorMessage = "Winner must be 0 or 1")]
        public int Winner { set; get; }

    }
}
