using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.Logic
{
    public class Player
    {
        public string Name { get; private set; }
        
        public int Score { get; private set; }

        public bool IsComputer { get; private set; }

        public eSoldierType SoldierType { get; private set; }

        public Player(bool i_IsComputer, string i_Name, eSoldierType i_SoldierType)
        {
            this.IsComputer = i_IsComputer;
            this.Name = i_Name;
            this.Score = 0;
            this.SoldierType = i_SoldierType;
        }

        public void AddScore(int i_ScoreToAdd)
        {
            this.Score += i_ScoreToAdd;
        }
    }
}