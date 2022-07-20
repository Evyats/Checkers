using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UI
{
    public class GameFormBuilder
    {
        public static readonly int sr_LeftBorder = 20;
        public static readonly int sr_RightBorder = 20;
        public static readonly int sr_TopBorder = 50;
        public static readonly int sr_BottomBorder = 20;
        public static readonly int sr_ButtonSize = 50;

        public static GameForm Build(int i_BoardSize)
        {
            GameForm gameForm = new GameForm(i_BoardSize);

            initializeForm(gameForm);
            createButtons(gameForm);
            createLabels(gameForm);

            return gameForm;
        }

        private static void initializeForm(GameForm io_GameForm)
        {
            int formWidth = sr_LeftBorder + sr_RightBorder + sr_ButtonSize * io_GameForm.BoardSize;
            int formHeight = sr_TopBorder + sr_BottomBorder + sr_ButtonSize * io_GameForm.BoardSize;

            io_GameForm.ClientSize = new Size(formWidth, formHeight);
            io_GameForm.MaximizeBox = false;
            io_GameForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            io_GameForm.StartPosition = FormStartPosition.CenterScreen;
        }

        private static void createButtons(GameForm io_GameForm)
        {
            Button button;

            for (int row = 0; row < io_GameForm.BoardSize; row++)
            {
                for (int column = 0; column < io_GameForm.BoardSize; column++)
                {
                    if (row % 2 == column % 2)
                    {
                        button = new CellButton(sr_ButtonSize);
                        button.Click += io_GameForm.CellButton_Click;
                        io_GameForm.Buttons[row, column] = button as CellButton;
                    }
                    else
                    {
                        button = new InActiveCellButton(sr_ButtonSize);
                    }

                    button.Top = sr_TopBorder + (row * sr_ButtonSize);
                    button.Left = sr_LeftBorder + (column * sr_ButtonSize);
                    io_GameForm.Controls.Add(button);
                }
            }
        }

        private static void createLabels(GameForm io_GameForm)
        {
            io_GameForm.LabelPlayer1.Left = 0;
            io_GameForm.LabelPlayer1.Width = io_GameForm.ClientSize.Width / 2;
            io_GameForm.LabelPlayer1.TextAlign = ContentAlignment.MiddleCenter;
            io_GameForm.LabelPlayer1.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            io_GameForm.LabelPlayer1.Top = sr_TopBorder / 2 - io_GameForm.LabelPlayer1.Height / 2;
            io_GameForm.Controls.Add(io_GameForm.LabelPlayer1);
            io_GameForm.LabelPlayer2.Left = io_GameForm.ClientSize.Width / 2;
            io_GameForm.LabelPlayer2.Width = io_GameForm.ClientSize.Width / 2;
            io_GameForm.LabelPlayer2.TextAlign = ContentAlignment.MiddleCenter;
            io_GameForm.LabelPlayer2.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            io_GameForm.LabelPlayer2.Top = sr_TopBorder / 2 - io_GameForm.LabelPlayer2.Height / 2;
            io_GameForm.Controls.Add(io_GameForm.LabelPlayer2);
        }
    }
}