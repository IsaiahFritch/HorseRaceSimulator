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
                #region Opening Scene
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
                #endregion

                #region Horse Stealing Scene
                case 11:
                    // text
                    outputLabel.Text = "Horse stealing";

                    // draw scene
                    e.Graphics.FillRectangle(whiteBrush, 50, 50, 1500, 800);
                    break;

                case 12:
                    // text
                    outputLabel.Text = "Honsies on my mind";

                    // draw scene
                    e.Graphics.FillRectangle(grayBrush, 50, 50, 1500, 800);
                    break;
                #endregion

                #region Single Horse Scene
                case 21:
                    // text
                    outputLabel.Text = "Horse walking (epic)";

                    // draw scene
                    e.Graphics.FillRectangle(whiteBrush, 50, 50, 1500, 800);
                    break;

                case 22:
                    // text
                    outputLabel.Text = "Horse crossing finish line (epic)";

                    // draw scene
                    e.Graphics.FillRectangle(grayBrush, 50, 50, 1500, 800);
                    break;
                    #endregion
            }

            // TODO DRAW BUTTONS

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            switch (Form1.pageNumber)
            {
                #region Opening Scene
                case 1:
                    // next page
                    Form1.pageNumber = 2;
                    break;

                case 2:
                    // Launch Menu Screen
                    Form1.ChangeScreen(this, new MenuScreen());
                    break;
                #endregion

                #region Horse Stealing Scene
                case 11:
                    // next page
                    Form1.pageNumber = 12;
                    break;

                case 12:
                    // add money
                    Form1.moneyAmount += 500;

                    // Remove a horse from the game
                    // choose a horse to "sell"/remove
                    int r = Form1.ranGen.Next(1,4);

                    // remove horse from game -- run until a horse is removed
                    while (r !=0)
                    {
                        if (r == 1)
                        {
                            // check if the horse hasn't already been sold
                            if (Form1.horseOneActive == true)
                            {
                                // remove horse
                                Form1.horseOneActive = false;

                                // end while loop
                                r = 0;
                            }
                            else
                            {
                                // check next horse
                                r++;
                            }
                        }

                        if (r == 2)
                        {
                            // check if the horse hasn't already been sold
                            if (Form1.horseTwoActive == true)
                            {
                                // remove horse
                                Form1.horseTwoActive = false;

                                // end while loop
                                r = 0;
                            }
                            else
                            {
                                // check next horse
                                r++;
                            }
                        }

                        if (r == 3)
                        {
                            // check if the horse hasn't already been sold
                            if (Form1.horseThreeActive == true)
                            {
                                // remove horse
                                Form1.horseThreeActive = false;

                                // end while loop
                                r = 0;
                            }
                            else
                            {
                                // check next horse
                                r = 1;
                            }
                        }

                    }

                    // Launch Menu Screen
                    Form1.ChangeScreen(this, new MenuScreen());
                    break;
                #endregion

                #region Single Horse Scene
                case 21:
                    // next page
                    Form1.pageNumber = 22;
                    break;

                case 22:
                    // Launch Menu Screen
                    Form1.ChangeScreen(this, new MenuScreen());
                    break;
                    #endregion
            }

            Refresh();
        }
    }
}
