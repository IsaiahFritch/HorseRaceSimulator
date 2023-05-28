using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseRaceSimulator
{
    internal class Bottle
    {
        public double x, y;
        public double width = 15, height = 40;
        double xScaling, yScaling, scale = 5;
        int xThrowDistance, yThrowDistance;
        double xSpeed, ySpeed;
        public int lifecycle = 18;
        public double rotation = 0, rotateAmount;

        public Bottle(int xAttendee_, int yAttendee_, int xHorse_, int yHorse_)
        {
            // set the starting location
            x = xAttendee_;
            y = yAttendee_;

            // find the distance of the throw
            xThrowDistance = xHorse_ - xAttendee_ + 100; //aim in front of horse to account for it moving forward
            yThrowDistance = yHorse_ - yAttendee_;

            // find the speed to throw at to reach the horse within a few frames
            xSpeed = (double)(xThrowDistance / lifecycle);
            ySpeed = (double)(yThrowDistance / lifecycle);

            // set scaling factor
            xScaling = width / scale;
            yScaling = height / scale;

            // set roation 
            rotateAmount = Form1.ranGen.Next(-10, 11);
        }

        public void Move()
        {
            // move the bottle
            x += xSpeed;
            y += ySpeed;

            // scale the bottle up as it soars "higher"
            #region scaling visual
            switch (lifecycle)
            {
                case 18:
                    //default sizes
                    rotation -= rotateAmount;
                    break;

                case 17:
                    width += xScaling;
                    height += yScaling;
                    break;

                case 16:
                    width += xScaling;
                    height += yScaling;
                    break;

                case 15:
                    width += xScaling;
                    height += yScaling;
                    break;

                case 14:
                    width += xScaling;
                    height += yScaling;
                    break;

                case 13:
                    width += xScaling;
                    height += yScaling;
                    break;

                case 12:
                    width += xScaling;
                    height += yScaling;
                    break;
                        
                case 11:
                    width += xScaling;
                    height += yScaling;
                    break;

                case 10:
                    width += xScaling;
                    height += yScaling;
                    break;
                case 9:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 8:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 7:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 6:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 5:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 4:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 3:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 2:
                    width -= xScaling;
                    height -= yScaling;
                    break;
                case 1:
                    width -= xScaling;
                    height -= yScaling;
                    break;
            }


            #endregion

            // rotate the bottle
            rotation += rotateAmount;

            // remove the bottle as it passes or hits the horse
            lifecycle--;
        }
    }
}
