using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.Logic
{
    public class Cell
    {
        public int Row { get; private set; }

        public int Column { get; private set; }

        public Cell(int i_Row, int i_Column)
        {
            Row = i_Row;
            Column = i_Column;
        }

        public bool Equals(Cell io_Other)
        {
            return this.Row == io_Other.Row && this.Column == io_Other.Column;
        }

        public bool InBounds(int i_BoardSize)
        {
            return this.Row >= 0 && this.Row < i_BoardSize && this.Column >= 0 && this.Column < i_BoardSize;
        }

        public Cell GetDiagonalCell(eDirection i_Direction)
        {
            Cell diagonalCell = null;

            if (i_Direction == eDirection.TopLeft)
            {
                diagonalCell = new Cell(this.Row - 1, this.Column - 1);
            }
            else if (i_Direction == eDirection.TopRight)
            {
                diagonalCell = new Cell(this.Row - 1, this.Column + 1);
            }
            else if (i_Direction == eDirection.BottomLeft)
            {
                diagonalCell = new Cell(this.Row + 1, this.Column - 1);
            }
            else if (i_Direction == eDirection.BottomRight)
            {
                diagonalCell = new Cell(this.Row + 1, this.Column + 1);
            }

            return diagonalCell;
        }

        public int DiagonalDistance(Cell io_Other)
        {
            int distanceI = Math.Abs(this.Row - io_Other.Row);
            int distanceJ = Math.Abs(this.Column - io_Other.Column);

            return (distanceI + distanceJ) / 2;
        }

        public Cell MiddleCell(Cell io_Other)
        {
            int middleI = (this.Row + io_Other.Row) / 2;
            int middleJ = (this.Column + io_Other.Column) / 2;

            return new Cell(middleI, middleJ);
        }
    }
}