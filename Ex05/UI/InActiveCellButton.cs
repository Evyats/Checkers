using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UI
{
    public class InActiveCellButton : Button
    {
        private readonly Color DefaultColor = Color.White;

        public InActiveCellButton(int i_ButtonSize)
        {
            this.Width = i_ButtonSize;
            this.Height = i_ButtonSize;
            this.BackColor = DefaultColor;
            this.Enabled = false;
        }
    }
}