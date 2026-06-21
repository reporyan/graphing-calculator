using System;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class AxisLine : GraphObject
    {
        private int direction;

        private enum Direction
        {
            x,
            y
        }

        public AxisLine(int _direction)
        {
            direction = _direction;
        }

        public AxisLine(int _direction, Color _colour) : this(_direction)
        {
            Colour = _colour;
        }
        
        public override void DrawObject()
        {
            if (direction == (int)Direction.x)
                SplashKit.DrawLine(Colour, 0, WorldToScreenY(0), Cam.ScreenResolutionX, WorldToScreenY(0));
            else if (direction == (int)Direction.y)
                SplashKit.DrawLine(Colour, WorldToScreenX(0), 0, WorldToScreenX(0), Cam.ScreenResolutionY);
        }
    }
}