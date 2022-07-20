using System;
using Ex05.UI;
using Ex05.Logic;
using System.Windows.Forms;

namespace Ex05
{
    public class Program
    {
        public static void Main()
        {
            Start();
        }

        public static void Start()
        {
            GameSettingsForm gameSettingsForm = new GameSettingsForm();
            GamesManager gameManager;

            gameSettingsForm.ShowDialog();
            if(gameSettingsForm.DialogResult == DialogResult.OK)
            {
                gameManager = new GamesManager(gameSettingsForm.Player1Name, gameSettingsForm.Player2Name, 
                    gameSettingsForm.PlayAgainstComputer, gameSettingsForm.BoardSize);
                gameManager.StartGames();
            }
        }
    }
}