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
    public partial class MenuScreen : UserControl
    {
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
            #region buttons
            e.Graphics.DrawImage(Form1.moneyImage, moneyCountImageBox);
            e.Graphics.DrawImage(Form1.shopImageCURRENT, shopImageBox);
            e.Graphics.DrawImage(Form1.betsImageCURRENT, betsImageBox);
            e.Graphics.DrawImage(Form1.startRaceImageCURRENT, raceImageBox);
            e.Graphics.DrawImage(Form1.leaveGameImageCURRENT, leaveGameImageBox);
            e.Graphics.DrawImage(Form1.stealImageCURRENT, stealImageBox);
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
            Form1.horseOneBets = 37;
            Form1.horseTwoBets = 0;
            Form1.horseThreeBets = 0;

            // remove bet money from total amount if there is enough money
            if (Form1.moneyAmount >= (Form1.horseOneBets + Form1.horseTwoBets + Form1.horseThreeBets))
            {
                Form1.moneyAmount -= (Form1.horseOneBets + Form1.horseTwoBets + Form1.horseThreeBets);
            }
            else
            {
                // display "insufficient funds" error
                // reset all betting fields
            }

            // Launch Game Screen
            Form1.ChangeScreen(this, new GameScreen());
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

        }
        #endregion
    }
}
