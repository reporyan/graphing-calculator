using System;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Input
    {   
        private Camera cam = Camera.Instance;
        Rectangle rectangle = new Rectangle();

        private string inputText = "";

        public Input()
        {
            rectangle.X = cam.ScreenResolutionX - 220;
            rectangle.Y = cam.ScreenResolutionY - 70;
            rectangle.Width = 200;
            rectangle.Height = 50;
        }

        public string GetInput()
        {   
            //THIS NEEDS TO BE UPDATED TO USE THE LATEST VERSION OF SPLASHKIT

            SplashKit.LabelElement("Enter Graph:", rectangle);
            inputText = SplashKit.TextBox(inputText, rectangle);
            if (SplashKit.KeyTyped(KeyCode.ReturnKey) && inputText != "")
            {
                string result = inputText;
                inputText = "";//so that I can clear this
                return result;
            }

            return "";
        }
    }
}