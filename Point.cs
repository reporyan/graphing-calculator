using SplashKitSDK;

namespace GraphingCalculator
{
    public class Point : GraphObject
    {
        public Point(string _equation, float _x, float _y)
        {
            equation = _equation;

            Position.X = _x;
            Position.Y = _y;
        }

        public override void DrawObject()
        {
            base.DrawObject();

            SplashKit.DrawText(Equation, Colour, WorldToScreenX(Position.X) + 10, WorldToScreenY(Position.Y));
        }
    }
}