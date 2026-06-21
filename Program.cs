using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Program
    {
        public static void Main(string[] _args)
        {
            Console.WriteLine("Program Started");

            Camera cam = Camera.Instance;

            Input input = new Input();
            string inputText = "";

            //temp var
            float CAM_MOVE_SPEED = 200f;
            float CAM_ZOOM_SPEED = 1f;

            //timing
            Stopwatch timer = new Stopwatch();
            timer.Start();
            float prevTime = 0;

            float RELOAD_DISTANCE_THRESHHOLD = 50f;
            float RELOAD_SIZE_THRESHHOLD = 0.25f;

            //drawing
            Window window = new Window("GraphingCalculator", cam.ScreenResolutionX, cam.ScreenResolutionY);

            Render render = new Render();

            Color BACKGROUND_COLOUR = Color.Black;

            foreach (string arg in _args)
            {
                render.AddGraphObject(arg, Color.Yellow);
            }

            //loop
            while (!window.CloseRequested)
            {
                //splashkit stuff just done in program
                SplashKit.ClearScreen(BACKGROUND_COLOUR);
                SplashKit.ProcessEvents();

                float deltaTime = (float)timer.Elapsed.TotalSeconds - prevTime;
                //Console.WriteLine(deltaTime);
                prevTime = (float)timer.Elapsed.TotalSeconds;

                //input
                if (SplashKit.KeyDown(KeyCode.WKey))
                {
                    cam.CamPos.Y += CAM_MOVE_SPEED * cam.CamSize * deltaTime;
                }
                if (SplashKit.KeyDown(KeyCode.SKey))
                {
                    cam.CamPos.Y -= CAM_MOVE_SPEED * cam.CamSize * deltaTime;
                }
                //input
                if (SplashKit.KeyDown(KeyCode.DKey))
                {
                    cam.CamPos.X += CAM_MOVE_SPEED * cam.CamSize * deltaTime;
                }
                if (SplashKit.KeyDown(KeyCode.AKey))
                {
                    cam.CamPos.X -= CAM_MOVE_SPEED * cam.CamSize * deltaTime;
                }
                //input
                if (SplashKit.KeyDown(KeyCode.EKey))
                {
                    cam.CamSize *= 1 - CAM_ZOOM_SPEED * deltaTime;

                    if (cam.CamSize < 0.001f)
                        cam.CamSize = 0.001f;
                }
                if (SplashKit.KeyDown(KeyCode.QKey))
                {
                    cam.CamSize *= 1 + CAM_ZOOM_SPEED * deltaTime;
                }

                //reload?
                render.AssessReload(RELOAD_DISTANCE_THRESHHOLD, RELOAD_SIZE_THRESHHOLD);

                //draw
                inputText = input.GetInput();
                if (inputText != "")
                {
                    string[] inputChunks = inputText.Replace(" ", "").ToLower().Split(new char[] { ':' });

                    if (inputChunks.Length > 1)
                    {
                        if (inputChunks[0] == "delete")
                        {
                            render.DeleteGraphObject(inputChunks[1]);
                        }
                    }
                    else
                    {
                        render.AddGraphObject(inputChunks[0]); 
                    }     
                }

                render.DrawFrame();
                SplashKit.DrawInterface();

                //display to screen
                SplashKit.RefreshScreen();
            }
        }
    }
}
