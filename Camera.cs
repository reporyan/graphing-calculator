using System;
using System.Runtime.Versioning;

namespace GraphingCalculator
{
    public class Camera//singleton
    {
        private static Camera instance = new Camera();

        private Vector2 camPos;
        private float camSize;

        private int SCREEN_RES_X;
        private int SCREEN_RES_Y;

        private float EDGE_BUFFER = 1.25f;

        public Camera()
        {
            camPos = new Vector2(0f, 0f);
            camSize = 0.2f;

            SCREEN_RES_X = 800;
            SCREEN_RES_Y = 600;
        }
        
        //proprties
        public Vector2 CamPos
        {
            get
            {
                return camPos;
            }
            set
            {
                camPos = value;
            }
        }

        public float CamSize
        {
            get
            {
                return camSize;
            }
            set
            {
                camSize = value;
            }
        }

        public int ScreenResolutionX
        {
            get
            {
                return SCREEN_RES_X;
            }
            set
            {
                SCREEN_RES_X = value;
            }
        }

        public int ScreenResolutionY
        {
            get
            {
                return SCREEN_RES_Y;
            }
            set
            {
                SCREEN_RES_Y = value;
            }
        }

        public float CamBoundPosX()
        {
            return (ScreenResolutionX / 2 * CamSize) + CamPos.X;
        }
        
        public float CamBoundPosX(bool _doEdgeBuffer)
        {
            if(!_doEdgeBuffer)
                return (ScreenResolutionX / 2 * CamSize) + CamPos.X;
                
            return (ScreenResolutionX / 2 * CamSize * EDGE_BUFFER) + CamPos.X;
        }

        public float CamBoundNegX()
        {
            return (-ScreenResolutionX / 2 * CamSize) + CamPos.X;
        }

        public float CamBoundNegX(bool _doEdgeBuffer)
        {
            if (!_doEdgeBuffer)
                return (-ScreenResolutionX / 2 * CamSize) + CamPos.X;
                
            return (-ScreenResolutionX / 2 * CamSize * EDGE_BUFFER) + CamPos.X;
        }

        public float CamBoundPosY()
        {
            return (ScreenResolutionY / 2 * CamSize) + CamPos.Y;
        }

        public float CamBoundPosY(bool _doEdgeBuffer)
        {
            if (!_doEdgeBuffer)
                return (ScreenResolutionY / 2 * CamSize) + CamPos.Y;

            return (ScreenResolutionY / 2 * CamSize * EDGE_BUFFER) + CamPos.Y;
        }

        public float CamBoundNegY()
        {
            return (-ScreenResolutionY / 2 * CamSize) + CamPos.Y;
        }

        public float CamBoundNegY(bool _doEdgeBuffer)
        {
            if (!_doEdgeBuffer)
                return (-ScreenResolutionY / 2 * CamSize) + CamPos.Y;

            return (-ScreenResolutionY / 2 * CamSize * EDGE_BUFFER) + CamPos.Y;
        }

        static public Camera Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Camera();
                }
                return instance;
            }
        }
    }
}