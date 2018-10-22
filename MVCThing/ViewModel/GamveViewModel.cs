using Microsoft.AspNetCore.Mvc.Rendering;
using MVCThing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCThing.ViewModel
{
    public class GameViewModel
    {
        public int ID { set; get; }
        public Player PlayerOne { set; get; }
        public Player PlayerTwo { set; get; }
        public Player Winner { set; get; }
        public Player Looser { set; get; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name ="Date of the game")]
        public DateTime Date { set; get; }
        public int Player1Hits { set; get; }
        public int Player2Hits { set; get; }


        public GameViewModel(Game game,Player playerOne, Player playerTwo)
        {
            ID = game.ID;
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            Date = game.Date;
            Player1Hits = game.Player1Hits;
            Player2Hits = game.Player2Hits;
            if (game.Winner == 0)
            {
                Winner = playerOne;
                Looser = playerTwo;
            }
            if(game.Winner == 1)
            {
                Winner = playerTwo;
                Looser = playerOne;
            }
            if(game.Winner > 1)
            {
                throw new IndexOutOfRangeException("winnerNumber must be 0 or 1");
            }

        }
    }
}
