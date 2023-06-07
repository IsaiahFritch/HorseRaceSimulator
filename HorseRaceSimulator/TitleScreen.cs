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
            // check if it's this player's first time playing
            if (Form1.openingScene == true)
            {
                // set the page number
                Form1.pageNumber = 1;

                // Launch Cut Scene Screen
                Form1.ChangeScreen(this, new CutSceneScreen());
            }
            // otherwise, just start the game
            else
            {
                // Launch Menu Screen
                Form1.ChangeScreen(this, new MenuScreen());
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // End program
            Application.Exit();
        }


    }
}
