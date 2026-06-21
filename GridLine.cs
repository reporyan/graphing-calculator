using System;
using System.Runtime.CompilerServices;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Gridline : GraphObject
    {
        int direction;

        private enum Direction
        {
            x,
            y
        }

        private int LINE_LENGTH = 12;

        public Gridline(Vector2 _position, int _direction, Color _colour) : base(_position, _colour)
        {
            direction = _direction;
        }
        
        public override void DrawObject()
        {
            if (direction == 0)
            {
                SplashKit.DrawLine(Colour, WorldToScreenX(0) - LINE_LENGTH / 2, WorldToScreenY(Position.Y),
                WorldToScreenX(0) + LINE_LENGTH / 2, WorldToScreenY(Position.Y));

                //text
                SplashKit.DrawText(Position.Y.ToString("F1"), Colour, WorldToScreenX(0) + LINE_LENGTH, WorldToScreenY(Position.Y));
            }
            else
            {
                SplashKit.DrawLine(Colour, WorldToScreenX(Position.X), WorldToScreenY(0) - LINE_LENGTH / 2,
                WorldToScreenX(Position.X), WorldToScreenY(0) + LINE_LENGTH / 2);

                //text
                SplashKit.DrawText(Position.X.ToString("F1"), Colour, WorldToScreenX(Position.X), WorldToScreenY(0) + LINE_LENGTH);
            } 
        }
    }
}