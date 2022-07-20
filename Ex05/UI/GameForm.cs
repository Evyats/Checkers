using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Ex05.Enums;
using Ex05.Logic;

namespace Ex05.UI
{
    public class GameForm : Form
    {
        public event Action<Cell> CellButtonClick;

        public int BoardSize { get; private set; }
        
        public CellButton[,] Buttons { get; private set; }
        
        public Label LabelPlayer1 { get; private set; }

        public Label LabelPlayer2 { get; private set; }

        public GameForm(int i_BoardSize)
        {
            this.BoardSize = i_BoardSize;
            this.Buttons = new CellButton[this.BoardSize, this.BoardSize];
            this.LabelPlayer1 = new Label();
            this.LabelPlayer2 = new Label();
        }

        public void CellButton_Click(Object sender, EventArgs e)
        {
            for (int row = 0; row < this.BoardSize; row++)
            {
                for (int column = 0; column < this.BoardSize; column++)
                {
                    if (this.Buttons[row, column] == sender)
                    {
                        OnCellButtonClick(new Cell(row, column));
                        break;
                    }
                }
            }
        }

        protected virtual void OnCellButtonClick(Cell io_Cell)
        {
            if (CellButtonClick != null)
            {
                CellButtonClick.Invoke(io_Cell);
            }
        }

        public void ChangeLabels(string i_Player1Name, string i_Player2Name, int i_Player1Score, int i_Player2Score)
        {
            this.LabelPlayer1.Text = string.Format("{0}: {1}", i_Player1Name, i_Player1Score);
            this.LabelPlayer2.Text = string.Format("{0}: {1}", i_Player2Name, i_Player2Score);
        }

        public CellButton GetCellButton(Cell io_Cell)
        {
            return Buttons[io_Cell.Row, io_Cell.Column];
        }

        public void ChangeCellButtonGraphic(Cell io_Cell, string i_SoldierGraphic)
        {
            CellButton cellButtonToChange = GetCellButton(io_Cell);

            if (cellButtonToChange != null)
            {
                cellButtonToChange.SoldierGraphic = i_SoldierGraphic;
            }
        }
    }
}