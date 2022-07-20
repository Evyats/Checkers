using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex05.UI;
using Ex05.Logic;
using Ex05.Enums;
using System.Windows.Forms;

namespace Ex05
{
    public class GameRunner
    {
        private GameLogic m_GameLogic;
        private GameForm m_GameForm;

        public Player Player1 { get; private set; }
        
        public Player Player2 { get; private set; }
        
        public Player Winner { get; private set; }

        public int BoardSize { get; private set; }
        
        public Cell ChosenSoldierCell { get; private set; }

        public GameRunner(Player io_Player1, Player io_Player2, int i_BoardSize)
        {
            this.Player1 = io_Player1;
            this.Player2 = io_Player2;
            this.Winner = null;
            this.BoardSize = i_BoardSize;
            this.ChosenSoldierCell = null;
            this.m_GameLogic = new GameLogic(this.Player1, this.Player2, this.BoardSize);
            this.m_GameForm = GameFormBuilder.Build(this.BoardSize);
        }

        public void StartGame()
        {
            updateCellButtons();
            this.m_GameForm.ChangeLabels(m_GameLogic.Player1.Name, m_GameLogic.Player2.Name,
                m_GameLogic.Player1.Score, m_GameLogic.Player2.Score);
            this.m_GameForm.CellButtonClick += m_GameForm_CellButtonClick;
            this.m_GameForm.Closed += m_GameForm_FormClosing;
            this.m_GameForm.ShowDialog();
        }

        private void m_GameForm_CellButtonClick(Cell io_ChosenCell)
        {
            if (this.ChosenSoldierCell == null)
            {
                if (this.m_GameLogic.GetSoldier(io_ChosenCell) != null)
                {
                    this.m_GameForm.GetCellButton(io_ChosenCell).Mark();
                    this.ChosenSoldierCell = io_ChosenCell;
                }
            }
            else
            {
                if (io_ChosenCell.Equals(this.ChosenSoldierCell))
                {
                    cancelChosenCell();
                }
                else
                {
                    sendCommandToExecution(new Command(ChosenSoldierCell, io_ChosenCell));
                }
            }
        }

        private void sendCommandToExecution(Command io_Command)
        {
            try
            {
                this.m_GameLogic.ExecuteCommand(io_Command);
                if (this.m_GameLogic.NoLegalMoves())
                {
                    this.Winner = m_GameLogic.GetWinner();
                    endGame();
                }
                else if (m_GameLogic.PlayerTurn.IsComputer)
                {
                    sendCommandToExecution(m_GameLogic.GenerateRandomCommand());
                }
            }
            catch (CommandNotLegalException ex)
            {
                MessageBox.Show("This move is not legal!", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cancelChosenCell();
            updateCellButtons();
        }

        private void updateCellButtons()
        {
            Soldier soldier;

            for (int row = 0; row < this.m_GameForm.BoardSize; row++)
            {
                for (int column = 0; column < this.m_GameForm.BoardSize; column++)
                {
                    soldier = this.m_GameLogic.GetSoldier(new Cell(row, column));
                    this.m_GameForm.ChangeCellButtonGraphic(new Cell(row, column), getSoldierGraphic(soldier));
                }
            }
        }

        private void cancelChosenCell()
        {
            if (this.ChosenSoldierCell != null)
            {
                this.m_GameForm.GetCellButton(this.ChosenSoldierCell).UnMark();
                this.ChosenSoldierCell = null;
            }
        }

        private string getSoldierGraphic(Soldier io_Soldier)
        {
            string graphic = "";

            if (io_Soldier != null)
            {
                if (io_Soldier.Type == eSoldierType.X)
                {
                    if (io_Soldier.IsKing)
                    {
                        graphic = eSoldierGraphic.Z.ToString();
                    }
                    else
                    {
                        graphic = eSoldierGraphic.X.ToString();
                    }
                }
                else if (io_Soldier.Type == eSoldierType.O)
                {
                    if (io_Soldier.IsKing)
                    {
                        graphic = eSoldierGraphic.Q.ToString();
                    }
                    else
                    {
                        graphic = eSoldierGraphic.O.ToString();
                    }
                }
            }

            return graphic;
        }

        private void m_GameForm_FormClosing(object sender, EventArgs e)
        {
            this.Winner = m_GameLogic.PlayerWait;
            endGame();
        }

        private void endGame()
        {
            if (this.Winner != null)
            {
                this.Winner.AddScore(m_GameLogic.CalculateWinnerScore());
            }

            m_GameForm.Close();
        }
    }
}