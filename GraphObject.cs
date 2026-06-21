using System;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class GraphObject
    {
        //fields
        protected Vector2 position;
        protected Color colour;

        protected Camera cam;

        protected string equation = "";//point will use this for deletion

        //contrustor
        public GraphObject()
        {
            cam = Camera.Instance;
            colour = Color.Red;
            position = new Vector2(0f, 0f); //must assign field to avoid errors
        }

        //change these to take in vectors!!!
        public GraphObject(Vector2 _pos) : this()
        {
            position = _pos;
        }

        public GraphObject(Vector2 _pos, Color _colour) : this(_pos)
        {
            colour = _colour;
            position = _pos;
        }

        //properties
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Color Colour
        {
            get
            {
                return colour;
            }
            set
            {
                colour = value;
            }
        }

        public Camera Cam
        {
            get
            {
                return cam;
            }
        }

        public string Equation
        {
            get
            {
                return equation;
            }
        }

        //methods
        public virtual void DrawObject()
        {
            SplashKit.FillCircle(Colour, WorldToScreenX(Position.X), WorldToScreenY(Position.Y), 4f);
        }

        public virtual void Reload()
        {
            DrawObject();//good to have something here in case
        }

        //doing seperate since splashkit doesn't take v2 obviously
        public float WorldToScreenX(float _x)
        {
            return 1 / Cam.CamSize * (_x - Cam.CamPos.X) + Cam.ScreenResolutionX / 2;
        }

        public float WorldToScreenY(float _y)
        {
            return -1 / Cam.CamSize * (_y - Cam.CamPos.Y) + Cam.ScreenResolutionY / 2;
        }
    }
}