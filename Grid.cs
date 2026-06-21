using System;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Grid : GraphObject
    {
        private List<Gridline> gridLines = new List<Gridline>();

        //if using grid, make these the same
        private float GRIDLINE_SCALING_FACTOR_HORIZONTAL = 48f;
        private float GRIDLINE_SCALING_FACTOR_VERTICAL = 92f;

        public Grid(Color _colour)
        {
            Colour = _colour;
            Reload();
        }

        public override void DrawObject()
        {
            foreach (Gridline line in gridLines)
            {
                line.DrawObject();
            }
        }
        
        public override void Reload()
        {
            gridLines = new List<Gridline>();

            //Y axis, lines going horizontally
            float gridLineOIncrement = MathF.Pow(10, MathF.Round(MathF.Log10(GRIDLINE_SCALING_FACTOR_HORIZONTAL * cam.CamSize)));//multiple of 10!

            for (float i = gridLineOIncrement; i < cam.CamBoundPosY(true); i += gridLineOIncrement)
            {
                gridLines.Add(new Gridline(new Vector2(0, i), 0, Colour));
            }
            for (float i = -gridLineOIncrement; i > cam.CamBoundNegY(true); i -= gridLineOIncrement)
            {
                gridLines.Add(new Gridline(new Vector2(0, i), 0, Colour));
            }

            //X axis, lines going vertically
            gridLineOIncrement = MathF.Pow(10, MathF.Round(MathF.Log10(GRIDLINE_SCALING_FACTOR_VERTICAL * cam.CamSize)));//multiple of 10!

            for (float i = gridLineOIncrement; i < cam.CamBoundPosX(true); i += gridLineOIncrement)
            {
                gridLines.Add(new Gridline(new Vector2(i, 0), 1, Colour));
            }
            for (float i = -gridLineOIncrement; i > cam.CamBoundNegX(true); i -= gridLineOIncrement)
            {
                gridLines.Add(new Gridline(new Vector2(i, 0), 1, Colour));
            }
        }
    }
}