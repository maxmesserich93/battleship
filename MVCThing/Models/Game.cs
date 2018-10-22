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
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { set; get; }
        [Required]
        public int Player1Hits { set; get; }
        [Required]
        public int Player2Hits { set; get; }
        [Required]
        [Range(0, 1, ErrorMessage = "Winner must be 0 or 1")]
        public int Winner { set; get; }

    }
}
