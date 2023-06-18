using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HorseRaceSimulator
{
    public partial class MenuScreen : UserControl
    {
        //REMOVE PENS AND BRUSHES
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);

        // Global variables
        bool shopClicked = false;
        bool betsClicked = false;

        // create image boxes POSSIBLE TO SIMPLIFY BY REMOVING?   just set the positions to (0, 0)
        #region background
        Rectangle backgroundImageOneBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle backgroundImageTwoBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle backgroundImageThreeBox = new Rectangle(0, 0, 1920, 1080);
        #endregion

        #region buttons
        Rectangle moneyCountImageBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle shopImageBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle betsImageBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle raceImageBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle leaveGameImageBox = new Rectangle(0, 0, 1920, 1080);
        Rectangle stealImageBox = new Rectangle(0, 0, 1920, 1080);
        #endregion

        #region betting menu

        // all rough guesses for size and location
        Rectangle betMenuBox = new Rectangle(295, 110, 1330, 590);
        Rectangle horseOneInactiveBox = new Rectangle(320, 135, 300, 300);
        Rectangle horseTwoInactiveBox = new Rectangle(660, 135, 300, 300);
        Rectangle horseThreeInactiveBox = new Rectangle(1005, 135, 300, 300);

        #endregion

        public MenuScreen()
        {
            InitializeComponent();
            SetScreen();
        }

        public void SetScreen()
        {
            // clear text from buttons
            shopButton.Text = "";
            betsButton.Text = "";
            startButton.Text = "";
            exitButton.Text = "";
            stealButton.Text = "";

            // set text in inputs
            horseOneBetInput.Text = "0";
            horseTwoBetInput.Text = "0";
            horseThreeBetInput.Text = "0";

            // set money label
            moneyAmountLabel.Parent = moneyAmountUnderLabel;
            moneyAmountLabel.Text = $"{Form1.moneyAmount}";
            moneyAmountUnderLabel.Text = $"{Form1.moneyAmount}";
        }

        private void MenuScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draw images
            #region background
            e.Graphics.DrawImage(Form1.backgroundImageOne, backgroundImageOneBox);
            e.Graphics.DrawImage(Form1.backgroundImageTwo, backgroundImageTwoBox); //this image will change throughout the game as stuff is bought
            e.Graphics.DrawImage(Form1.backgroundImageThree, backgroundImageThreeBox);
            #endregion

            #region betting menu
            // check if the bet menu is up
            if (betsClicked == true)
            {
                // Display bet menu
                // show menu image
                e.Graphics.FillRectangle(whiteBrush, betMenuBox);

                // show inactive horses -- do nothing if no horses exist yet  _TODO_ SIMPLIFY THIS CODE.  ON FIRST RUN, IF PLAYER HAS NO MONEY THAN A TWO STATEMENT IF(or) WILL CHECK BOTH STATEMENTS AND CRASH
                try
                {
                    if (Form1.horseOneActive == false || Form1.horseOneInjured == true)
                    {
                        e.Graphics.FillRectangle(redBrush, horseOneInactiveBox);
                        horseOneBetInput.Enabled = false;
                    }
                    if (Form1.horseTwoActive == false || Form1.horseTwoInjured == true)
                    {
                        e.Graphics.FillRectangle(redBrush, horseTwoInactiveBox);
                        horseTwoBetInput.Enabled = false;
                    }
                    if (Form1.horseThreeActive == false || Form1.horseThreeInjured == true)
                    {
                        e.Graphics.FillRectangle(redBrush, horseThreeInactiveBox);
                        horseThreeBetInput.Enabled = false;
                    }

                    if (GameScreen.horses[0].injured == true)
                    {
                        e.Graphics.FillRectangle(redBrush, horseOneInactiveBox);
                        horseOneBetInput.Enabled = false;
                    }
                    if (GameScreen.horses[1].injured == true)
                    {
                        e.Graphics.FillRectangle(redBrush, horseTwoInactiveBox);
                        horseTwoBetInput.Enabled = false;
                    }
                    if (GameScreen.horses[2].injured == true)
                    {
                        e.Graphics.FillRectangle(redBrush, horseThreeInactiveBox);
                        horseThreeBetInput.Enabled = false;
                    }
                }
                catch { }

                // show inputs
                horseOneBetInput.Visible = true;
                horseTwoBetInput.Visible = true;
                horseThreeBetInput.Visible = true;
            }
            else
            {
                // Clear bet menu
                // hide inputs
                horseOneBetInput.Visible = false;
                horseTwoBetInput.Visible = false;
                horseThreeBetInput.Visible = false;
            }
            #endregion

            #region buttons
            e.Graphics.DrawImage(Form1.moneyImage, moneyCountImageBox);
            e.Graphics.DrawImage(Form1.betsImageCURRENT, betsImageBox);
            e.Graphics.DrawImage(Form1.startRaceImageCURRENT, raceImageBox);
            e.Graphics.DrawImage(Form1.leaveGameImageCURRENT, leaveGameImageBox);

            // hide/show shop button if the menu covers it
            if (betsClicked == false)
            {
                e.Graphics.DrawImage(Form1.shopImageCURRENT, shopImageBox);
                shopButton.Visible = true;
            }
            else
            {
                shopButton.Visible = false;
            }

            // see if the player is elligable to steal a horse
            if (Form1.moneyAmount == 0)
            {
                e.Graphics.DrawImage(Form1.stealImageCURRENT, stealImageBox);
                stealButton.Visible = true;
            }
            else
            {
                stealButton.Visible = false;
            }
            #endregion
        }

        #region EXIT BUTTON
        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            // switch to hover image when hovered over
            Form1.leaveGameImageCURRENT = Form1.leaveGameHoverImage;
            Refresh();
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            // switch to normal image when not hovered over
            Form1.leaveGameImageCURRENT = Form1.leaveGameImage;
            Refresh();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Save game
            // Create XML file
            XmlWriter writer = XmlWriter.Create("Resources/savedGame.xml", null);

            // create root element
            writer.WriteStartElement("Save");

            // write Player conditions
            writer.WriteStartElement("player");

            #region player info
            // add info to Player
            writer.WriteElementString("moneyAmount", $"{Form1.moneyAmount}");
            #endregion

            // finish writing Player
            writer.WriteEndElement();

            // write Horse conditions
            writer.WriteStartElement("horse");

            #region horse info
            // add info to Horse
            if (Form1.horseOneActive == true)
            {
                writer.WriteElementString("horseOneActive", "true");
            }
            else
            {
                writer.WriteElementString("horseOneActive", "false");
            }

            if (Form1.horseTwoActive == true)
            {
                writer.WriteElementString("horseTwoActive", "true");
            }
            else
            {
                writer.WriteElementString("horseTwoActive", "false");
            }

            if (Form1.horseThreeActive == true)
            {
                writer.WriteElementString("horseThreeActive", "true");
            }
            else
            {
                writer.WriteElementString("horseThreeActive", "false");
            }

            if (Form1.horseOneInjured == true)
            {
                writer.WriteElementString("horseOneInjured", "true");
            }
            else
            {
                writer.WriteElementString("horseOneInjured", "false");
            }

            if (Form1.horseTwoInjured == true)
            {
                writer.WriteElementString("horseTwoInjured", "true");
            }
            else
            {
                writer.WriteElementString("horseTwoInjured", "false");
            }

            if (Form1.horseThreeInjured == true)
            {
                writer.WriteElementString("horseThreeInjured", "true");
            }
            else
            {
                writer.WriteElementString("horseThreeInjured", "false");
            }
            #endregion

            // finish writing Horse
            writer.WriteEndElement();

            // finish writing Save
            writer.WriteEndElement();

            // close file
            writer.Close();

            // End program
            Application.Exit();
        }
        #endregion

        #region SHOP BUTTON
        private void shopButton_MouseEnter(object sender, EventArgs e)
        {
            // switch to hover image when hovered over
            Form1.shopImageCURRENT = Form1.shopHoverImage;
            Refresh();
        }

        private void shopButton_MouseLeave(object sender, EventArgs e)
        {
            // switch to normal image when not hovered over
            // check if the previous image was a clicked or unclicked image
            if (shopClicked == false)
            {
                Form1.shopImageCURRENT = Form1.shopImage;
            }
            else
            {
                Form1.shopImageCURRENT = Form1.shopSelectedImage;
            }
            Refresh();
        }

        private void shopButton_Click(object sender, EventArgs e)
        {
            // toggle bewtween selected image and normal when clicked
            if (shopClicked == false)
            {
                shopClicked = true;
                Form1.shopImageCURRENT = Form1.shopSelectedImage;
            }
            else
            {
                shopClicked = false;
                Form1.shopImageCURRENT = Form1.shopImage;
            }
            Refresh();
        }
        #endregion

        #region BETS BUTTON
        private void betsButton_MouseEnter(object sender, EventArgs e)
        {
            // switch to hover image when hovered over
            Form1.betsImageCURRENT = Form1.betsHoverImage;
            Refresh();
        }

        private void betsButton_MouseLeave(object sender, EventArgs e)
        {
            // switch to normal image when not hovered over
            // check if the previous image was a clicked or unclicked image
            if (betsClicked == false)
            {
                Form1.betsImageCURRENT = Form1.betsImage;
            }
            else
            {
                Form1.betsImageCURRENT = Form1.betsSelectedImage;
            }
            Refresh();
        }

        private void betsButton_Click(object sender, EventArgs e)
        {
            // toggle bewtween selected image and normal when clicked
            if (betsClicked == false)
            {
                betsClicked = true;
                Form1.betsImageCURRENT = Form1.betsSelectedImage;
            }
            else
            {
                betsClicked = false;
                Form1.betsImageCURRENT = Form1.betsImage;
            }
            Refresh();
        }
        #endregion

        #region START BUTTON
        private void startButton_MouseEnter(object sender, EventArgs e)
        {
            // switch to hover image when hovered over
            Form1.startRaceImageCURRENT = Form1.startRaceHoverImage;
            Refresh();
        }

        private void startButton_MouseLeave(object sender, EventArgs e)
        {
            // switch to normal image when not hovered over
            Form1.startRaceImageCURRENT = Form1.startRaceImage;
            Refresh();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // Record the bets
            // get values from bet screen
            try
            {
                Form1.horseOneBets = Math.Abs(Convert.ToInt32(horseOneBetInput.Text));
            }
            catch
            {
                horseOneBetInput.Text = "0";
            }
            try
            {
                Form1.horseTwoBets = Math.Abs(Convert.ToInt32(horseTwoBetInput.Text));
            }
            catch
            {
                horseTwoBetInput.Text = "0";
            }
            try
            {
                Form1.horseThreeBets = Math.Abs(Convert.ToInt32(horseThreeBetInput.Text));
            }
            catch
            {
                horseThreeBetInput.Text = "0";
            }

            if (Form1.moneyAmount >= (Form1.horseOneBets + Form1.horseTwoBets + Form1.horseThreeBets))
            {
                // remove bet money from total amount if there is enough money
                Form1.moneyAmount -= (Form1.horseOneBets + Form1.horseTwoBets + Form1.horseThreeBets);

                // Launch Game Screen
                Form1.ChangeScreen(this, new GameScreen());
            }
            else
            {
                // display "insufficient funds" error
                // reset all betting fields
                horseOneBetInput.Text = "0";
                horseTwoBetInput.Text = "0";
                horseThreeBetInput.Text = "0";
            }
        }
        #endregion

        #region STEAL BUTTON
        private void stealButton_MouseEnter(object sender, EventArgs e)
        {
            // switch to hover image when hovered over
            Form1.stealImageCURRENT = Form1.stealImageHover;
            Refresh();
        }

        private void stealButton_MouseLeave(object sender, EventArgs e)
        {
            // switch to normal image when not hovered over
            Form1.stealImageCURRENT = Form1.stealImage;
            Refresh();
        }

        private void stealButton_Click(object sender, EventArgs e)
        {
            // set the page number
            Form1.pageNumber = 11;

            // Launch Cut Scene Screen
            Form1.ChangeScreen(this, new CutSceneScreen());
        }
        #endregion
    }
}
