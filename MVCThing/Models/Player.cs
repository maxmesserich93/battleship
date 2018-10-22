using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCThing.Models
{
    public class Player
    {
        public int ID { set; get; }
        public string Name { set; get; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime JoinDate { set; get; }
        [Required]
        public int GamesPlayed { set; get; }
        [Required]
        public int GamesWon { set; get; }
    }
}
