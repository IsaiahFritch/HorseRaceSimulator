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
    public partial class CutSceneScreen : UserControl
    {
        // Global Variables
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush grayBrush = new SolidBrush(Color.LightGray);

        public CutSceneScreen()
        {
            InitializeComponent();
        }

        private void CutSceneScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draw images
            // TODO DRAW BACKGROUND
            
            // draw scenes
            switch (Form1.pageNumber)
            {
                case 1:
                    // text
                    outputLabel.Text = "FIRST PAGE TEST TEXT";

                    // draw scene
                    e.Graphics.FillRectangle(whiteBrush, 50, 50, 1500, 800); 
                    break;

                case 2:
                    // text
                    outputLabel.Text = "SECOND PAGE TEST TEXT";

                    // draw scene
                    e.Graphics.FillRectangle(grayBrush, 50, 50, 1500, 800);
                    break;
            }

            // TODO DRAW BUTTONS

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            switch (Form1.pageNumber)
            {
                case 1:
                    // next page
                    Form1.pageNumber = 2;
                    break;

                case 2:
                    // Launch Menu Screen
                    Form1.ChangeScreen(this, new MenuScreen());
                    break;
            }

            Refresh();
        }
    }
}
