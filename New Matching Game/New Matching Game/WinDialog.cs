using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Matching_Game
{
    public partial class WinDialog : Form
    {
        public bool RestartRequested { get; private set; } = false;
        public WinDialog(int elapsedTime)
        {
            InitializeComponent();

            lblMessage.Text = $"You matched all the icons in {elapsedTime} seconds!";

            btnRestart.Click += btnRestart_Click;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartRequested = true;
            this.Close();  
        }
    }
}
