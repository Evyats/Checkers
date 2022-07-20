using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.Logic
{
    public class Soldier
    {
        public eSoldierType Type { get; private set; }
        
        public bool IsKing { get; private set; }

        public List<eDirection> Directions { get; private set; }
        
        public int Score { get; private set; }
        
        public Soldier(eSoldierType i_Type)
        {
            this.Type = i_Type;
            this.Score = 1;
            this.IsKing = false;
            this.Directions = new List<eDirection>();
            if (Type == eSoldierType.X)
            {
                Directions.Add(eDirection.TopLeft);
                Directions.Add(eDirection.TopRight);
            }
            else
            {
                Directions.Add(eDirection.BottomLeft);
                Directions.Add(eDirection.BottomRight);
            }
        }

        public void TurnToKing()
        {
            if (!IsKing)
            {
                IsKing = true;
                Score = 4;
                Directions = new List<eDirection>();
                Directions.Add(eDirection.TopLeft);
                Directions.Add(eDirection.TopRight);
                Directions.Add(eDirection.BottomLeft);
                Directions.Add(eDirection.BottomRight);
            }
        }
    }
}