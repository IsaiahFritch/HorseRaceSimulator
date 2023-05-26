using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceSimulator
{
    internal class Horse
    {
        public int x, y, xSpeed, ySpeed, rotation, horseNumber;
        public int width = 451, height = 219;

        public Horse(int horseNumber_)
        {
            // collect number
            horseNumber = horseNumber_;

            // assign necessary values
            switch (horseNumber)
            {
                case 1:
                    x = -91;
                    y = 649 - 219 - 225;
                    break;

                case 2:
                    x = -91;
                    y = 649 - 219;
                    break;

                case 3:
                    x = -91;
                    y = 649 - 219 + 225;
                    break;
            }
        }

        public void Move()
        {
            //IDEAS:
            // If the number = some value, have the horse perform a larger jump in distance.
            // Acceleration?
            // Keep the horses' wiggle on the y axis restrained to keep them on the track.
            // Roatation code
        
            x += Form1.ranGen.Next(1, 20);
            y += Form1.ranGen.Next(-20, 20);
        }
    }
}
