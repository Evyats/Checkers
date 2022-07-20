using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UI
{
    public class CellButton : Button
    {
        private string m_SoldierGraphic;
        private readonly Color r_DefaultColor = Color.Gray;
        private readonly Color r_MarkedColor = Color.CornflowerBlue;

        public int ButtonSize { get; private set; }

        public string SoldierGraphic
        {
            get
            {
                return m_SoldierGraphic;
            }

            set
            {
                m_SoldierGraphic = value;
                this.Text = value;
            }
        }

        public CellButton(int i_ButtonSize) : base()
        {
            this.ButtonSize = i_ButtonSize;
            this.Width = ButtonSize;
            this.Height = ButtonSize;
            this.BackColor = r_DefaultColor;
        }

        public void Mark()
        {
            this.BackColor = r_MarkedColor;
        }

        public void UnMark()
        {
            this.BackColor = r_DefaultColor;
        }
    }
}