using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceSimulator
{
    internal class Attendee
    {
        public int x, y, width = 113, height = 225;
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
                    y = Form1.ranGen.Next(1028 - height, 1055);
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

        public bool ThrowBottle()
        {
            // Random chance to throw a bottle at a horse
            // should a bottle be thrown
            if (Form1.ranGen.Next(1, 30000) == 1)
            {
                #region note 
                // 1/1000 is about 2 bottles per game, so 1/2000 should be about 1 bottle per game. 
                // I would like about 1 bottle thrown every 4-5 games or so, so the chance will be 1/(2000*5).
                #endregion
                return true;
            }
            return false;
        }
    }
}
