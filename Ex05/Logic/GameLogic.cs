using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.Logic
{
    public class GameLogic
    {
        public Board Board { get; private set; }

        public Player Player1 { get; private set; }
        
        public Player Player2 { get; private set; }
        
        public Player PlayerTurn { get; private set; }
        
        public Player PlayerWait { get; private set; }
        
        public Command LastCommand { get; private set; }
        
        public bool DoubleEatingMode { get; private set; }
        
        public Random RandomGenerator { get; private set; }

        public GameLogic(Player io_Player1, Player io_Player2, int i_BoardSize)
        {
            this.Player1 = io_Player1;
            this.Player2 = io_Player2;
            this.PlayerTurn = Player1;
            this.PlayerWait = Player2;
            this.Board = new Board(i_BoardSize);
            this.RandomGenerator = new Random();
        }

        public Soldier GetSoldier(Cell io_Cell)
        {
            return Board.GetSoldier(io_Cell);
        }

        private void changeTurn()
        {
            Player playerTemporary = PlayerTurn;

            this.PlayerTurn = this.PlayerWait;
            this.PlayerWait = playerTemporary;
        }

        private bool commandIsLegal(Command io_Command)
        {
            bool commandIsLegal = false;
            List<Command> legalCommands = getLegalCommands(this.PlayerTurn);

            foreach (Command legalCommand in legalCommands)
            {
                if (io_Command.Equals(legalCommand))
                {
                    commandIsLegal = true;
                    break;
                }
            }

            return commandIsLegal;
        }

        private List<Command> getLegalCommands(Player io_Player)
        {
            List<Command> legalCommands = new List<Command>();
            List<Cell> cells = this.Board.GetCellsWithSoldierTypeOf(io_Player.SoldierType);

            if (DoubleEatingMode)
            {
                legalCommands = this.Board.GetLegalEatCommands(LastCommand.Target);
            }
            else
            {
                foreach (Cell cell in cells)
                {
                    legalCommands.AddRange(this.Board.GetLegalEatCommands(cell));
                }

                if (legalCommands.Count == 0)
                {
                    foreach (Cell cell in cells)
                    {
                        legalCommands.AddRange(this.Board.GetLegalJumpCommands(cell));
                    }
                }
            }

            return legalCommands;
        }

        public Command GenerateRandomCommand()
        {
            List<Command> legalCommands = getLegalCommands(this.PlayerTurn);
            int randomIndex = RandomGenerator.Next(legalCommands.Count);

            return legalCommands[randomIndex];
        }

        public bool NoLegalMoves()
        {
            return getLegalCommands(this.PlayerTurn).Count == 0;
        }

        public Player GetWinner()
        {
            Player winner = null;
            int numberOfPlayerWaitSoldiers = this.Board.GetCellsWithSoldierTypeOf(PlayerWait.SoldierType).Count;
            int numberOfPlayerWaitMoves = this.getLegalCommands(this.PlayerWait).Count;

            if (numberOfPlayerWaitSoldiers == 0)
            {
                winner = PlayerTurn;
            }
            else if (numberOfPlayerWaitMoves != 0)
            {
                winner = PlayerWait;
            }

            return winner;
        }

        public int CalculateWinnerScore()
        {
            List<Cell> cellsOfPlayer1 = this.Board.GetCellsWithSoldierTypeOf(PlayerTurn.SoldierType);
            List<Cell> cellsOfPlayer2 = this.Board.GetCellsWithSoldierTypeOf(PlayerWait.SoldierType);
            int scoreOfPlayer1 = 0;
            int scoreOfPlayer2 = 0;

            foreach (Cell cell in cellsOfPlayer1)
            {
                scoreOfPlayer1 += this.Board.GetSoldier(cell).Score;
            }

            foreach (Cell cell in cellsOfPlayer2)
            {
                scoreOfPlayer2 += this.Board.GetSoldier(cell).Score;
            }

            return Math.Abs(scoreOfPlayer1 - scoreOfPlayer2);
        }

        public void ExecuteCommand(Command io_Command)
        {
            if (commandIsLegal(io_Command))
            {
                this.LastCommand = io_Command;
                this.DoubleEatingMode = false;
                this.Board.MoveSoldier(io_Command);
                if (io_Command.IsEating())
                {
                    this.Board.KillSoldier(io_Command.GetEatenCell());
                    if (this.Board.SoldierCanEat(io_Command.Target))
                    {
                        DoubleEatingMode = true;
                    }
                    else
                    {
                        changeTurn();
                    }
                }
                else
                {
                    changeTurn();
                }
            }
            else
            {
                throw new CommandNotLegalException();
            }
        }
    }
}