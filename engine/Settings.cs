using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SMF.engine;

public enum EResolution
{
    E640X480,
    E800X600,
    E1280X720,
    E1440X900,
    E1600X900,
    E1920X1080,
    E2160X1440,
    E3840X2160
}

namespace SMF.engine
{
    class Settings
    {
        public bool isGameOn = true;

        public String windowName;

        public Vector2u playfieldSize = new Vector2u(1920, 1080);

        public bool stretched = false;

        public bool fullscreen = false;

        public EResolution resolution = EResolution.E1280X720;

        public Vector2u GetScreenSize()
        {
            switch (resolution)
            {
                case EResolution.E640X480:
                    return new Vector2u(640, 480);
                case EResolution.E800X600:
                    return new Vector2u(800, 600);
                case EResolution.E1280X720:
                    return new Vector2u(1280, 720);
                case EResolution.E1440X900:
                    return new Vector2u(1440, 900);
                case EResolution.E1600X900:
                    return new Vector2u(1600, 900);
                case EResolution.E1920X1080:
                    return new Vector2u(1920, 1080);
                case EResolution.E2160X1440:
                    return new Vector2u(2160, 1440);
                case EResolution.E3840X2160:
                    return new Vector2u(3840, 2160);
                default:
                    return new Vector2u(640, 480);
            }
        }
    }
}
