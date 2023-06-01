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
        public static List<Horse> horses = new List<Horse>();
        List<Bottle> bottles = new List<Bottle>();

        // create audience list
        List<Attendee> audience = new List<Attendee>();

        //TODO: change the brush array into an image array to display the drawings
        SolidBrush[] audienceAppearance = new SolidBrush[] { new SolidBrush(Color.White), new SolidBrush(Color.LightGray), new SolidBrush(Color.Gray), new SolidBrush(Color.DarkGray) };

        int injuredCount = 0;
        int winner  = 0;
        int endTimer = 0;
        int activeHorseCount = 3;
        int topAudienceCount;
        int bottomAudienceCount;

        Brush whiteBrush = new SolidBrush(Color.White);

        public GameScreen()
        {
            InitializeComponent();

            // set up horses
            if (horses.Count() == 0)
            {
                Horse horseOne = new Horse(1);
                horses.Add(horseOne);
                Horse horseTwo = new Horse(2);
                horses.Add(horseTwo);
                Horse horseThree = new Horse(3);
                horses.Add(horseThree);
            }

            SetInitialConditions();
        }

        public void SetInitialConditions()
        {
            // Reset horses
            foreach (Horse h in horses)
            {  
                // assign necessary values
                h.accCheckOne = false;
                h.accCheckTwo = false;
                h.accCheckThree = false;
                h.acceleration = 1;

                switch (h.horseNumber)
                {
                    case 1:
                        h.x = -91;
                        h.y = 600 - 219 - 225;
                        h.yMin = 600 - 219 - 225 - 125;
                        h.yMax = 600 - 219 - 225 + 90;
                        break;

                    case 2:
                        h.x = -91;
                        h.y = 600 - 219;
                        h.yMin = 600 - 219 - 90;
                        h.yMax = 600 - 219 + 90;
                        break;

                    case 3:
                        h.x = -91;
                        h.y = 600 - 219 + 225;
                        h.yMin = 600 - 219 + 225 - 90;
                        h.yMax = 600 - 219 + 225 + 150;
                        break;
                }

                // "remove" injured horses
                if (h.injured == true)
                {
                    h.x = 10000;
                    activeHorseCount--;
                }

                // heal horses
                h.injured = false;
            }

            // Set up audience
            // record how many attendees are in the audience
            topAudienceCount = Form1.ranGen.Next(10, 31);
            bottomAudienceCount = Form1.ranGen.Next(10, 31);

            // add attendees to list 
            for (int i = 0; i < topAudienceCount; i++)
            {
                Attendee topAttendee = new Attendee("top");
                audience.Add(topAttendee);
            }
            for (int i = 0; i < bottomAudienceCount; i++)
            {
                Attendee bottomAttendee = new Attendee("bottom");
                audience.Add(bottomAttendee);
            }

            // order audience from furthest into background to closest to foreground
            audience = audience.OrderBy(o => o.y).ToList();
            #region Note for Mr. T
            // https://stackoverflow.com/questions/57371442/how-to-sort-listt-in-c-sharp
            // the two lines of sorting code were found from this website above.
            #endregion
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Horse actions
            // set injured to zero
            injuredCount = 0;

            foreach (Horse h in horses)
            {
                // check how many horses can move
                if (h.injured)
                {
                    injuredCount++;
                }

                // move horses
                if (h.injured == false) 
                { 
                    h.Move(); 
                }

                // end game if a horse won
                if (h.DidHorseWin() == true && winner == 0)
                {
                    winner = h.horseNumber;
                }
            }

            // Audience actions
            foreach (Attendee a in audience)
            {
                {
                    // move audience
                    a.Move();

                    // throw bottle
                    if (injuredCount + 1 < activeHorseCount && a.ThrowBottle() == true)
                    {
                        // select a horse
                        try // do nothing if the horse is already gone
                        {
                            // pick target
                            int horseTarget = Form1.ranGen.Next(0, horses.Count());

                            if (horses[horseTarget].x < 1620)
                            {
                                // create bottle
                                Bottle newBottle = new Bottle(a.x + a.width / 2, a.y + a.height / 2, horses[horseTarget].x + horses[horseTarget].width / 2, horses[horseTarget].y + horses[horseTarget].height / 2);
                                bottles.Add(newBottle);
                            }
                        }
                        catch { }
                    }
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
                        h.injured = true;
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
            if (winner != 0 || injuredCount == activeHorseCount)
            {
                endTimer++;
            }

            // end game
            if (endTimer == 100)
            {
                // end timer
                gameTimer.Enabled = false;

                //TODO SEND WINNER INFORMATION TO WINNING SCREEN
                //RIGHT NOW, JUST RETURN TO MENU

                // Launch Main Screen
                Form1.ChangeScreen(this, new MenuScreen());
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // draw screen background
            e.Graphics.DrawImage(Form1.gameScreenRaceTrackImage, 0, 0, 1920, 1080);

            // draw track background
            e.Graphics.DrawImage(Form1.gameScreenBackgroundImage, 0, 0, 1920, 1080);

            // draw top of audience (view face)
            for (int i = 0; i < topAudienceCount; i++)
            {
                e.Graphics.DrawImage(Form1.attendeeOneFrontImage, audience[i].x, audience[i].y, audience[i].width, audience[i].height);
            }

            // draw track midground
            e.Graphics.DrawImage(Form1.gameScreenMidgroundImage, 0, 0, 1920, 1080);

            // draw horses
            foreach (Horse h in horses)
            {
                e.Graphics.TranslateTransform(h.x, h.y + h.width);
                e.Graphics.RotateTransform((float)h.rotation);
                e.Graphics.DrawImage(Form1.raceHorseImage, 0, 0 - h.width, h.width, h.height);
                e.Graphics.ResetTransform();
            }

            // draw track foreground
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
            for (int i = topAudienceCount; i < bottomAudienceCount + topAudienceCount; i++)
            {
                e.Graphics.DrawImage(Form1.attendeeOneBackImage, audience[i].x, audience[i].y, audience[i].width, audience[i].height);
            }
        }
    }
}