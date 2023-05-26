using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceSimulator
{
    internal class Horse
    {
        public int x, y, rotation, horseNumber, yMin, yMax, acceleration;
        public int width = 451, height = 219;
        bool accCheckOne = false, accCheckTwo = false, accCheckThree = false; 
        bool horseInLast = false;

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
            //IDEAS:
            // If the number = some value, have the horse perform a larger jump in distance.
            // Acceleration?
            // Roatation code

            // X movement
            #region X movement
            // assign acceleration
            if (accCheckOne == false)
            {
                accCheckOne = true;
                acceleration = Form1.ranGen.Next(1, 3); //1, 2
            }
            else if (x >= 400 && accCheckTwo == false)
            {
                accCheckTwo = true;
                acceleration = Form1.ranGen.Next(2, 5); //2, 3, 4
            }
            else if (x >= 700 && accCheckThree == false)  //  || x >= 800 && horseInLast == true  TO DO: GIVE LAST HORSE A BOOST
            {
                accCheckThree = true;
                acceleration = Form1.ranGen.Next(3, 6); //3, 4, 5
                //if (horseInLast == true) { acceleration++;}   TO DO: GIVE LAST HORSE A BOOST
            }

            // set speeds
            switch (acceleration)
            {
                case 1:
                    x += Form1.ranGen.Next(3, 5);
                    break;
                case 2:
                    x += Form1.ranGen.Next(4, 6);
                    break;
                case 3:
                    x += Form1.ranGen.Next(5, 7);
                    break;
                case 4:
                    x += Form1.ranGen.Next(6, 8);
                    break;
                case 5:
                    x += Form1.ranGen.Next(7, 9);
                    break;
                case 6:
                    x += Form1.ranGen.Next(9, 12);
                    break;
            }
            #endregion

            // Wiggle the horses up and down (visual only, no effect on game)
            #region wiggle visuals
            // up down movement
            if (y > yMin && y < yMax)
            {
                y += Form1.ranGen.Next(-5, 6);
            }

            // fixing the horses when the get stuck
            else if (y <= yMin) {y += 15;}
            else if (y >= yMax) {y -= 15;}

            //assign rotation
            //TODO: check how high or low the horse wiggled, then turn the horse accordingly using a case statement
            #endregion
        }
    }
}
