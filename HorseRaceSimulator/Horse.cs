using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceSimulator
{
    internal class Horse
    {
        public int x, y, horseNumber, yMin, yMax, acceleration, yWiggle, horsePlace;
        public int width = 451, height = 219;
        bool accCheckOne = false, accCheckTwo = false, accCheckThree = false; 
        bool horseInLast = false;
        public double rotation;

        public Horse(int horseNumber_)
        {
            // collect number
            horseNumber = horseNumber_;

            // assign necessary values
            switch (horseNumber)
            {
                case 1:
                    x = -91;
                    y = 600 - 219 - 225; 
                    yMin = 600 - 219 - 225 - 125;
                    yMax = 600 - 219 - 225 + 90;
                    break;

                case 2:
                    x = -91;
                    y = 600 - 219; 
                    yMin = 600 - 219 - 90;
                    yMax = 600 - 219 + 90;
                    break;

                case 3:
                    x = -91;
                    y = 600 - 219 + 225; 
                    yMin = 600 - 219 + 225 - 90;
                    yMax = 600 - 219 + 225 + 150;
                    break;
            }
        }

        public void Move()
        {
            // X movement
            #region X movement
            // set acceleration
            if (accCheckOne == false)
            {
                accCheckOne = true;
                acceleration = Form1.ranGen.Next(1, 3); //1, 2
            }
            else if (x >= 500 && accCheckTwo == false)
            {
                accCheckTwo = true;
                acceleration = Form1.ranGen.Next(1, 5); //2, 3, 4
            }
            else if (x >= 1000 && accCheckThree == false)  //  || x >= 800 && horseInLast == true  TO DO: GIVE LAST HORSE A BOOST
            {
                accCheckThree = true;
                acceleration = Form1.ranGen.Next(1, 6); //3, 4, 5
                //if (horseInLast == true) { acceleration++;}   TO DO: GIVE LAST HORSE A BOOST
            }

            // set speed
            switch (acceleration)
            {
                case 1:
                    x += Form1.ranGen.Next(5, 6);
                    break;
                case 2:
                    x += Form1.ranGen.Next(6, 8);
                    break;
                case 3:
                    x += Form1.ranGen.Next(7, 9);
                    break;
                case 4:
                    x += Form1.ranGen.Next(8, 10);
                    break;
                case 5:
                    x += Form1.ranGen.Next(9, 12);
                    break;
                case 6:
                    x += Form1.ranGen.Next(10, 14);
                    break;
            }

            // random leaps forward
            if (Form1.ranGen.Next(1,30) == 1)
            {
                x += 50;
            }
            #endregion

            // Wiggle the horses up and down (visual only, no effect on game)
            #region wiggle visuals
            // up down movement
            if (y > yMin && y < yMax)
            {
                yWiggle = Form1.ranGen.Next(-5, 6);
                y += yWiggle;
            }

            // fixing the horses when the get stuck
            else if (y <= yMin) {y += 15;}
            else if (y >= yMax) {y -= 15;}

            //assign rotation
            //TODO: then turn the horse accordingly using a case statement
            switch (yWiggle)
            {
                case -5:
                    rotation = -2.5;
                    break;
                case -4:
                    rotation = -2;
                    break;
                case -3:
                    rotation = -1.5;
                    break;
                case -2:
                    rotation = -1;
                    break;
                case -1:
                    rotation = -0.5;
                    break;
                case 0:
                    rotation = 0;
                    break;
                case 1:
                    rotation = 0.5;
                    break;
                case 2:
                    rotation = 1;
                    break;
                case 3:
                    rotation = 1.5;
                    break;
                case 4:
                    rotation = 2;
                    break;
                case 5:
                    rotation = 2.5;
                    break;
            }
            #endregion
        }

        public bool DidHorseWin()
        {
            if (x + width > 1620)
            {
                return true;
            }
            return false;
        }
    }
}
