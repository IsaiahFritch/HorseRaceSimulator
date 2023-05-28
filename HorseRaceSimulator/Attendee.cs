using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceSimulator
{
    internal class Attendee
    {
        public int x, y, width = 75, height = 150;
        int maxJumpHeight, minimumHeight;
        bool jumping = false, maxHeight = false;
        public int appearance;

        public Attendee(string location_)
        {
            // assign x location
            x = Form1.ranGen.Next(-100, 1920);

            // assign y location
            switch (location_)
            {
                case "top":
                    y = Form1.ranGen.Next(-50, 180 - height);
                    break;
                case "bottom":
                    y = Form1.ranGen.Next(1020 - height, 1055);
                    break;
            }

            // assign max jump height
            maxJumpHeight = y - 30 + Form1.ranGen.Next(-10, 11);
            minimumHeight = y;

            // assign image
            appearance = Form1.ranGen.Next(0, 4);
        }

        public void Move()
        {
            // Jumping/Cheering
            // set that they are jumping
            if (jumping == false && Form1.ranGen.Next(1,10) == 1)
            {
                jumping = true;
            }

            // jump up
            if (jumping == true && maxHeight == false)
            {
                y -= 3;
                if (y <= maxJumpHeight)
                {
                    maxHeight = true;
                }
            }

            // fall down
            else if (jumping == true && maxHeight == true)
            {
                y += 3; 
                if (y >= minimumHeight)
                {
                    jumping = false;
                    maxHeight = false;
                }
            }
        }
    }
}
