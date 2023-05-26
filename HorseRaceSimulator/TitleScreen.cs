using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HorseRaceSimulator
{
    public partial class TitleScreen : UserControl
    {
        public TitleScreen()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Launch Menu Screen
            Form1.ChangeScreen(this, new MenuScreen());
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // End program
            Application.Exit();
        }


    }
}
