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
    public partial class GameScreen : UserControl
    {
        // Global variables
        List<Horse> horses = new List<Horse>();

        public GameScreen()
        {
            InitializeComponent();
            SetInitialConditions();
        }

        public void SetInitialConditions()
        {
            // set up horses
            Horse horseOne = new Horse(1);
            horses.Add(horseOne);
            Horse horseTwo = new Horse(2);
            horses.Add(horseTwo);
            Horse horseThree = new Horse(3);
            horses.Add(horseThree);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Move horses
            foreach (Horse h in horses)
            {
                h.Move();
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Form1.gameScreenRaceTrackImage, 0, 0, 1920, 1080);
            //HAVE TOP HALF OF CHEERING AUDIENCE HERE (view face)
            e.Graphics.DrawImage(Form1.gameScreenBackgroundImage, 0, 0, 1920, 1080);
            e.Graphics.DrawImage(Form1.gameScreenMidgroundImage, 0, 0, 1920, 1080);

            //HAVE HORSES PAINTED HERE
            foreach (Horse h in horses)
            {
                e.Graphics.DrawImage(Form1.raceHorseImage, h.x, h.y, h.width, h.height);
            }

            e.Graphics.DrawImage(Form1.gameScreenForegroundImage, 0, 0, 1920, 1080);
            //HAVE BOTTOM HALF OF CHEERING CROWD HERE (view back of head)
        }
    }
}
