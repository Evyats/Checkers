using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.Logic
{
    public class Command
    {
        public Cell Source { get; private set; }

        public Cell Target { get; private set; }

        public Command(Cell io_Source, Cell io_Target)
        {
            Source = io_Source;
            Target = io_Target;
        }

        public bool Equals(Command io_Other)
        {
            return this.Source.Equals(io_Other.Source) && this.Target.Equals(io_Other.Target);
        }

        public bool IsEating()
        {
            return Source.DiagonalDistance(Target) == 2;
        }

        public Cell GetEatenCell()
        {
            return Source.MiddleCell(Target);
        }
    }
}