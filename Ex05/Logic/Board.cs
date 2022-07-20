using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex05.Enums;

namespace Ex05.Logic
{
    public class Board
    {
        private readonly Soldier[,] r_BoardMatrix;
        
        public int BoardSize { get; private set; }

        public Board(int i_BoardSize)
        {
            this.BoardSize = i_BoardSize;
            this.r_BoardMatrix = new Soldier[BoardSize, BoardSize];
            this.initializeBoard();
        }

        private void initializeBoard()
        {
            for (int row = 0; row < this.BoardSize; row++)
            {
                for (int column = 0; column < this.BoardSize; column++)
                {
                    if (row % 2 == column % 2)
                    {
                        if (row < BoardSize / 2 - 1)
                        {
                            r_BoardMatrix[row,column] = new Soldier(eSoldierType.O);
                        }
                        else if (row >= BoardSize / 2 + 1)
                        {
                            r_BoardMatrix[row, column] = new Soldier(eSoldierType.X);
                        }
                    }
                }
            }
        }

        public Soldier GetSoldier(Cell io_Cell)
        {
            return r_BoardMatrix[io_Cell.Row, io_Cell.Column];
        }

        public void SetSoldier(Cell io_Cell, Soldier io_Soldier)
        {
            this.r_BoardMatrix[io_Cell.Row, io_Cell.Column] = io_Soldier;
        }

        public void MoveSoldier(Command io_Command)
        {
            Soldier soldier = GetSoldier(io_Command.Source);
            int targetRow = io_Command.Target.Row;

            KillSoldier(io_Command.Source);
            SetSoldier(io_Command.Target, soldier);
            if ((soldier.Type == eSoldierType.X && targetRow == 0)
                || (soldier.Type == eSoldierType.O && targetRow == this.BoardSize - 1))
            {
                soldier.TurnToKing();
            }
        }

        public void KillSoldier(Cell io_Cell)
        {
            SetSoldier(io_Cell, null);
        }

        public bool SoldierCanEat(Cell io_Cell)
        {
            return GetLegalEatCommands(io_Cell).Count > 0;
        }

        public List<Cell> GetCellsWithSoldierTypeOf(eSoldierType i_Type)
        {
            List<Cell> cells = new List<Cell>();
            Cell currentCell;
            Soldier currentSoldier;

            for (int row = 0; row < this.BoardSize; row++)
            {
                for (int column = 0; column < this.BoardSize; column++)
                {
                    currentCell = new Cell(row, column);
                    currentSoldier = GetSoldier(currentCell);
                    if (currentSoldier != null && currentSoldier.Type == i_Type)
                    {
                        cells.Add(currentCell);
                    }
                }
            }

            return cells;
        }

        public List<Command> GetLegalJumpCommands(Cell io_Source)
        {
            List<Command> commands = new List<Command>();
            Soldier sourceSoldier = GetSoldier(io_Source);
            Cell target = null;

            foreach (eDirection direction in sourceSoldier.Directions)
            {
                target = io_Source.GetDiagonalCell(direction);
                if (target.InBounds(this.BoardSize) && GetSoldier(target) == null)
                {
                    commands.Add(new Command(io_Source, target));
                }
            }

            return commands;
        }

        public List<Command> GetLegalEatCommands(Cell io_Source)
        {
            List<Command> commands = new List<Command>();
            Soldier sourceSoldier = GetSoldier(io_Source);
            Cell eaten = null;
            Cell target = null;

            foreach (eDirection direction in sourceSoldier.Directions)
            {
                eaten = io_Source.GetDiagonalCell(direction);
                target = eaten.GetDiagonalCell(direction);
                if (target.InBounds(this.BoardSize) && GetSoldier(target) == null &&
                    GetSoldier(eaten) != null && GetSoldier(eaten).Type != GetSoldier(io_Source).Type)
                {
                    commands.Add(new Command(io_Source, target));
                }
            }

            return commands;
        }
    }
}