using SplashKitSDK;

namespace GraphingCalculator
{
    public class Vector2
    {
        private float x;
        private float y;

        //constuctor
        public Vector2()
        {
            x = 0f;
            y = 0f;
        }

        public Vector2(Point2D _point)
        {
            x = (float)_point.X;
            y = (float)_point.Y;
        }

        public Vector2(float _x, float _y)
        {
            x = _x;
            y = _y;
        }

        //properties
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        //methods
        public static Vector2 operator +(Vector2 _a, Vector2 _b)
        {
            return new Vector2(_a.X + _b.X, _a.Y + _b.Y);
        }

        public static Vector2 operator *(float _a, Vector2 _b)
        {
            return new Vector2(_a * _b.X, _a * _b.Y);
        }

        public static Vector2 operator *(Vector2 _a, Vector2 _b)
        {
            return new Vector2(_a.X * _b.X, _a.Y * _b.Y);
        }
    }
}