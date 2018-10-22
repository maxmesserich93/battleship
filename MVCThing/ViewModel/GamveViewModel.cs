using Microsoft.AspNetCore.Mvc.Rendering;
using MVCThing.Models;
using System;
using System.Collections.Generic;
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
        public DateTime Date { set; get; }
        public int Player1Hits { set; get; }
        public int Player2Hits { set; get; }


        public GameViewModel(int id,Player playerOne, Player playerTwoID, Player winner, DateTime date, int player1Hits, int player2Hits)
        {
            ID = id;
            PlayerOne = playerOne;
            PlayerTwo = playerTwoID;
            Date = date;
            Player1Hits = player1Hits;
            Player2Hits = player2Hits;
            Winner = winner;
        }
    }
}
