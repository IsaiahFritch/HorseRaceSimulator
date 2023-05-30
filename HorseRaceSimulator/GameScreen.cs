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
        List<Bottle> bottles = new List<Bottle>();

        // TODO: test if it's more efficent to paint using two lists that know what to image to draw, or one list that checks for the image in the class
        List<Attendee> audienceTop = new List<Attendee>();
        List<Attendee> audienceBottom = new List<Attendee>();
        //TODO: change the brush array into an image array to display the drawings
        SolidBrush[] audienceAppearance = new SolidBrush[] { new SolidBrush(Color.White), new SolidBrush(Color.LightGray), new SolidBrush(Color.Gray), new SolidBrush(Color.DarkGray) };

        int injuredCount = 0;
        int winner  = 0;
        int endTimer = 0;

        Brush whiteBrush = new SolidBrush(Color.White);

        public GameScreen()
        {
            InitializeComponent();
            SetInitialConditions();
        }

        public void SetInitialConditions()
        {
            // set up horses - check if they are active and add them
            if (Form1.horseOneActive == true && Form1.horseOneInjured == false) 
            {Horse horseOne = new Horse(1); horses.Add(horseOne);}
            if (Form1.horseTwoActive == true && Form1.horseTwoInjured == false)
            { Horse horseTwo = new Horse(2); horses.Add(horseTwo);}
            if (Form1.horseThreeActive == true && Form1.horseThreeInjured == false)
            { Horse horseThree = new Horse(3); horses.Add(horseThree);}

            // heal horses
            Form1.horseOneInjured = false;
            Form1.horseTwoInjured = false;
            Form1.horseThreeInjured = false;

            // set up audience
            for (int i = 0; i < Form1.ranGen.Next(10, 31); i++)
            {
                Attendee topAttendee = new Attendee("top");
                audienceTop.Add(topAttendee);
            }
            for (int i = 0; i < Form1.ranGen.Next(10, 31); i++)
            {
                Attendee bottomAttendee = new Attendee("bottom");
                audienceBottom.Add(bottomAttendee);
            }
            // order audience from furthest into background to closest to foreground
            audienceTop = audienceTop.OrderBy(o => o.y).ToList();
            audienceBottom = audienceBottom.OrderBy(o => o.y).ToList();
            #region Note for Mr. T
            // https://stackoverflow.com/questions/57371442/how-to-sort-listt-in-c-sharp
            // the two lines of sorting code were found from this website above.
            #endregion
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Horse actions
            foreach (Horse h in horses)
            {
                // move horses
                if (Form1.horseOneInjured == false && h.horseNumber == 1) {h.Move();}
                else if (Form1.horseTwoInjured == false && h.horseNumber == 2) {h.Move();}
                else if (Form1.horseThreeInjured == false && h.horseNumber == 3) {h.Move();}

                // end game if a horse won
                if (h.DidHorseWin() == true && winner == 0)
                {
                    winner = h.horseNumber;
                }
            }

            // check how many horses can move
            injuredCount = 0;
            if (Form1.horseOneInjured == true) {injuredCount++;}
            if (Form1.horseTwoInjured == true) {injuredCount++;}
            if (Form1.horseThreeInjured == true) 
            {injuredCount++;}

            // Audience actions
            foreach (Attendee a in audienceTop)
            {
                {
                    // move audience
                    a.Move();

                    // throw bottle
                    if (injuredCount +1 < horses.Count && a.ThrowBottle() == true)
                    {
                        // select a horse
                        try // do nothing if the horse is already gone
                        {
                            Horse h = horses[Form1.ranGen.Next(0, 3)];

                            // create bottle
                            Bottle newBottle = new Bottle(a.x + a.width / 2, a.y + a.height / 2, h.x + h.width / 2, h.y + h.height / 2);
                            bottles.Add(newBottle);
                        }
                        catch { }
                    }
                }
            }
            foreach (Attendee a in audienceBottom)
            {
                // move audience
                a.Move();

                // throw bottle
                if (injuredCount +1 < horses.Count && a.ThrowBottle() == true)
                {
                    // select a horse
                    try // do nothing if the horse is already gone
                    {
                        Horse h = horses[Form1.ranGen.Next(0, 3)];

                        // create bottle
                        Bottle newBottle = new Bottle(a.x + a.width / 2, a.y + a.height / 2, h.x + h.width / 2, h.y + h.height / 2);
                        bottles.Add(newBottle);
                    }
                    catch { }
                }
            }

            // Bottle actions
            foreach (Bottle b in bottles)
            {
                // move the bottle
                b.Move();

                // hit horses with bottle
                foreach (Horse h in horses)
                {
                    if (b.Collision(h) == true)
                    {
                        if (h.horseNumber == 1) {Form1.horseOneInjured = true;}
                        else if (h.horseNumber == 2) {Form1.horseTwoInjured = true;}
                        else {Form1.horseThreeInjured = true;}
                    }
                }

                // remove bottles
                if (b.lifecycle <= 0)
                {
                    bottles.Remove(b);
                    break;
                }
            }

            // End game
            // update counter - when a horse wins or if all horses become injured
            if (winner != 0 || injuredCount >= horses.Count)
            {
                endTimer++;
            }

            // end game
            if (endTimer == 100)
            {
                //TODO SEND WINNER INFORMATION TO WINNING SCREEN
                //RIGHT NOW, JUST RETURN TO MENU

                // Launch Main Screen
                Form1.ChangeScreen(this, new MenuScreen());
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw background
            e.Graphics.DrawImage(Form1.gameScreenRaceTrackImage, 0, 0, 1920, 1080);
            e.Graphics.DrawImage(Form1.gameScreenBackgroundImage, 0, 0, 1920, 1080);

            // draw top of audience (view face)
            foreach (Attendee a in audienceTop)
            {
                e.Graphics.DrawImage(Form1.attendeeOneFrontImage, a.x, a.y, a.width, a.height);
            }

            e.Graphics.DrawImage(Form1.gameScreenMidgroundImage, 0, 0, 1920, 1080);

            // draw horses
            foreach (Horse h in horses)
            {
                e.Graphics.TranslateTransform(h.x, h.y + h.width);
                e.Graphics.RotateTransform((float)h.rotation);
                e.Graphics.DrawImage(Form1.raceHorseImage, 0, 0 - h.width, h.width, h.height);
                e.Graphics.ResetTransform();
            }

            e.Graphics.DrawImage(Form1.gameScreenForegroundImage, 0, 0, 1920, 1080);

            // draw bottles
            foreach (Bottle b in bottles)
            {
                e.Graphics.TranslateTransform((float)b.x + (float)b.width / 2, (float)b.y + (float)b.height / 2);
                e.Graphics.RotateTransform((float)b.rotation);
                e.Graphics.DrawImage(Form1.bottleImage, -(float)b.width / 2, -(float)b.height / 2, (float)b.width, (float)b.height);
                e.Graphics.ResetTransform();
            }

            // draw bottom of audience (view back of head)
            foreach (Attendee a in audienceBottom)
            {
                e.Graphics.DrawImage(Form1.attendeeOneBackImage, a.x, a.y, a.width, a.height);
            }
        }
    }
}