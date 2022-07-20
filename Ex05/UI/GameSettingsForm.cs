using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UI
{
    public partial class GameSettingsForm : Form
    {
        public int BoardSize { get; private set; }

        public string Player1Name { get; private set; }
        
        public string Player2Name { get; private set; }
        
        public bool PlayAgainstComputer { get; private set; }

        public GameSettingsForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxPlayer2.Checked)
            {
                this.textBoxPlayer2.Text = "";
                this.textBoxPlayer2.Enabled = true;
            }
            else
            {
                this.textBoxPlayer2.Text = "[Computer]";
                this.textBoxPlayer2.Enabled = false;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (this.radioButton6.Checked)
            {
                this.BoardSize = 6;
            }
            else if(this.radioButton8.Checked)
            {
                this.BoardSize = 8;
            }
            else
            {
                this.BoardSize = 10;
            }

            this.PlayAgainstComputer = !checkBoxPlayer2.Checked;
            this.Player1Name = textBoxPlayer1.Text;
            this.Player2Name = textBoxPlayer2.Text;
            this.Close();
        }
    }
}