using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;
using SMF.engine;
using SFML.System;

namespace SMF.engine
{
    class InstanceVars
    {
        public InstanceVars(String windowName, EResolution res)
        {
            Settings = new Settings();
            Settings.resolution = res;

            RecreateWindow(windowName);
        }

        private Settings settings;
        private RenderWindow window;
        private Input input;

        public Settings Settings { get => settings;   private set => settings = value; }
        public RenderWindow Window { get => window;     private set => window = value; }
        public Input Input       { get => input;      private set => input = value; }

        public void RecreateWindow(String windowName)
        {
            Window = new RenderWindow(new VideoMode(Settings.GetScreenSize().X, Settings.GetScreenSize().Y), windowName, Settings.fullscreen ? Styles.Fullscreen : Styles.Titlebar | Styles.Close);
            Input = new Input(Window, Settings);
        }
        
        
    }
}
