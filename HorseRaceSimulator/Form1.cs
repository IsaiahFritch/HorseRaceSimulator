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
    public partial class Form1 : Form
    {
        // Global Variables
        // random
        public static Random ranGen = new Random();

        // variables for horse reduction
        public static bool horseOneInjured = false, horseTwoInjured = false, horseThreeInjured = false;
        public static bool horseOneActive = true, horseTwoActive = true, horseThreeActive = true;

        // create all images
        #region menu background images
        public static Image backgroundImageOne = Properties.Resources.HRSbackground1;
        public static Image backgroundImageTwo = Properties.Resources.HRSbackground2;
        public static Image backgroundImageThree = Properties.Resources.HRSbackground3;
        #endregion
        #region menu buttons
        public static Image moneyImage = Properties.Resources.HRSmoneyCount;
        public static Image shopImageCURRENT = Properties.Resources.HRSshopButton;
        public static Image shopImage = Properties.Resources.HRSshopButton;
        public static Image shopHoverImage = Properties.Resources.HRSshopButtonHover;
        public static Image shopSelectedImage = Properties.Resources.HRSshopButtonSelected;
        public static Image betsImageCURRENT = Properties.Resources.HRSbetsButton;
        public static Image betsImage = Properties.Resources.HRSbetsButton;
        public static Image betsHoverImage = Properties.Resources.HRSbetsButtonHover;
        public static Image betsSelectedImage = Properties.Resources.HRSbetsButtonSelected;
        public static Image startRaceImageCURRENT = Properties.Resources.HRSraceButton;
        public static Image startRaceImage = Properties.Resources.HRSraceButton;
        public static Image startRaceHoverImage = Properties.Resources.HRSraceButtonHover;
        public static Image leaveGameImageCURRENT = Properties.Resources.HRSleaveGameButton;
        public static Image leaveGameImage = Properties.Resources.HRSleaveGameButton;
        public static Image leaveGameHoverImage = Properties.Resources.HRSleaveGameButtonHover;
        public static Image stealImageCURRENT = Properties.Resources.HRSstealButton;
        public static Image stealImage = Properties.Resources.HRSstealButton;
        public static Image stealImageHover = Properties.Resources.HRSstealButtonHover;
        #endregion

        #region game background images
        public static Image gameScreenRaceTrackImage = Properties.Resources.HRSgameScreenRaceTrack;
        public static Image gameScreenBackgroundImage = Properties.Resources.HRSgameScreenBackground;
        public static Image gameScreenMidgroundImage = Properties.Resources.HRSgameScreenMidground;
        public static Image gameScreenForegroundImage = Properties.Resources.HRSgameScreenForeground;
        #endregion
        #region game images
        public static Image raceHorseImage = Properties.Resources.HRShorseOneImage;
        public static Image bottleImage = Properties.Resources.HRSbottle;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Launch Title Screen
            ChangeScreen(this, new TitleScreen());
        }

        public static void ChangeScreen(object sender, UserControl next)
        {
            Form f;

            if (sender is Form)
            {
                f = (Form)sender;
            }
            else
            {
                UserControl current = (UserControl)sender;
                f = current.FindForm();
                f.Controls.Remove(current);
            }

            // TO DO: FIX WHATEVER WAS WRONG WITH THE COMMENTED OUT CODE SO THAT THE GAME SCREENS LOAD CORECTLY
            //next.Location = new Point((f.ClientSize.Width - next.Width) / 2, (f.ClientSize.Height - next.Width) / 2);
            next.Location = new Point(0,0);

            f.Controls.Add(next);
            next.Focus();
        }
    }
}
