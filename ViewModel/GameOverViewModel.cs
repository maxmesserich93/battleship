using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class GameOverViewModel : AbstractServiceViewModel
    {
        public string Result { get; }
        public bool YouWin { get; }
        public string WinnerScore { get; }
        public string LooserScore { get; }

        public Command BackToLobbyCommand { get; set; }

        public GameOverViewModel(int yourScore, int opponentScore, string opponentName, AbstractServiceViewModel vm) : base(vm)
        {
            if(yourScore > opponentScore)
            {
                WinnerScore = "Your score: " + yourScore;
                LooserScore = opponentName + " score: " + opponentScore;
                Result = "YOU WIN!";
                YouWin = true;
            }
            else
            {
                LooserScore = "Your score: " + yourScore;
                WinnerScore = opponentName + " score: " + opponentScore;
                Result = "YOU LOSE!";
                YouWin = false;
            }
        }

    }
}
