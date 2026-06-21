using System;
using System.Diagnostics;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Render
    {
        private List<GraphObject> graphObjects;

        private Camera cam = Camera.Instance;

        private Vector2 reloadPos = new Vector2(Camera.Instance.CamPos.X, Camera.Instance.CamPos.Y);//*
        private float reloadSize = Camera.Instance.CamSize;

        //contructor
        public Render()
        {
            graphObjects = new List<GraphObject>();

            graphObjects.Add(new AxisLine(0, Color.White));
            graphObjects.Add(new AxisLine(1, Color.White));
            graphObjects.Add(new Grid(Color.Gray));

            //graphObjects.Add(new GraphObject(new Vector2(0, 0), Color.Gray));
            
            //graphObjects.Add(new GraphObject(new Vector2(0, 10)));
            //graphObjects.Add(new GraphObject(new Vector2(50, 50)));

            //graphObjects.Add(new Graph("5*5", Color.Blue));
            //graphObjects.Add(new Graph("cosx"));
            //graphObjects.Add(new Graph("e^(sin(x^2))+(x^3-2x)/5+cos(x)*ln(x^2+1)"));
            //graphObjects.Add(new Graph("6x", Color.Blue));
            //graphObjects.Add(new Graph("0.01x^3-0.5x^2+10sin(0.2x)+5cos(0.1x)+54.02", Color.Green));
        }

        public void AddGraphObject(string _input)
        {
            AddGraphObject(_input, Color.LimeGreen);
        }

        public void AddGraphObject(string _input, Color _colour)
        {
            if (_input.Contains(','))
            {
                try
                {
                    //coordinate
                    string equation = _input;
                    _input = _input.Replace("(", "").Replace(")", "");
                    string[] coordinates = new string[2];
                    coordinates = _input.Split(new char[] { ',' });
                    Console.WriteLine(coordinates[0]);
                    Console.WriteLine(coordinates[1]);
                    graphObjects.Add(new Point(equation, float.Parse(coordinates[0]), float.Parse(coordinates[1])));
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("ERROR: Invalid Point Entry");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                try
                {
                    graphObjects.Add(new Graph(_input, _colour));
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("ERROR: Invalid Graph Entry");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        public void DeleteGraphObject(string _input)
        {
            for(int i = 0; i < graphObjects.Count; i++)
            {
                if(graphObjects[i].Equation == _input)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Removing GraphObject: " + graphObjects[i].Equation);
                    Console.ForegroundColor = ConsoleColor.White;

                    //remove
                    graphObjects.Remove(graphObjects[i]);     
                    i--;//because we deleted
                }
            }
        }

        public void DrawFrame()
        {
            //loop through and run draw object on every object
            foreach (GraphObject graphObject in graphObjects)
            {
                graphObject.DrawObject();
            }
        }

        public void AssessReload(float _reloadDistThreshhold, float _reloadSizeThreshhold)
        {
            //distance, based on size as well
            if (MathF.Sqrt(
            (float)MathF.Pow((reloadPos.X - cam.CamPos.X) / cam.CamSize, 2) +
            (float)MathF.Pow((reloadPos.Y - cam.CamPos.Y) / cam.CamSize, 2)
            ) >= _reloadDistThreshhold ||
            cam.CamSize >= reloadSize * (1 + _reloadSizeThreshhold) ||
            cam.CamSize <= reloadSize * (1 - _reloadSizeThreshhold))
            {
                Reload();
            }
        }
        
        public void Reload()
        {
            //loop through and run draw object on every object
            foreach (GraphObject graphObject in graphObjects)
            {
                graphObject.Reload();
            }

            reloadPos = new Vector2(cam.CamPos.X, cam.CamPos.Y);
            reloadSize = cam.CamSize;
        }
    }
}