using System;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Node : GraphObject
    {
        private bool noValue;

        public Node(float _x, float _y)
        {
            noValue = false;

            colour = Color.Red;
            position = new Vector2(_x, _y);

            if (float.IsNaN(_y) || float.IsInfinity(_y))
            {
                noValue = true;
            }
        }
        
        public bool NoValue
        {
            get
            {
                return noValue;
            }
        }

        //don't override because we don't want to draw this
    }
}