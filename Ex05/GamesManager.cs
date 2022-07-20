using Ex05.Enums;
using Ex05.Logic;
using Ex05.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public class GamesManager
    {
        public Player Player1 { get; private set; }
        
        public Player Player2 { get; private set; }

        public int BoardSize { get; private set; }

        public GamesManager(string i_Player1Name, string i_Player2Name, bool i_PlayAgainstComputer, int i_BoardSize)
        {
            this.Player1 = new Player(false, i_Player1Name, eSoldierType.X);
            this.Player2 = new Player(i_PlayAgainstComputer, i_Player2Name, eSoldierType.O);
            this.BoardSize = i_BoardSize;
        }

        public void StartGames()
        {
            bool playAnotherRound = true;
            string endGameMessage;
            DialogResult userAnswer;
            GameRunner gameRunner;

            while (playAnotherRound)
            {
                playAnotherRound = false;
                gameRunner = new GameRunner(this.Player1, this.Player2, this.BoardSize);
                gameRunner.StartGame();
                if (gameRunner.Winner == null)
                {
                    endGameMessage = string.Format("Tie!{0}Another Round?", Environment.NewLine);
                }
                else
                {
                    endGameMessage = string.Format("{0} won!{1}Another Round?", gameRunner.Winner.Name, Environment.NewLine);
                }

                userAnswer = MessageBox.Show(endGameMessage, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                playAnotherRound = userAnswer == DialogResult.Yes;
            }
        }
    }
}