using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace SMF.engine
{
    class InstanceVars
    {
        public List<IGameState> gameStates = new List<IGameState>();
        public InstanceVars(String windowName, EResolution res)
        {
            Settings = new Settings();
            Settings.Resolution = res;

            RecreateWindow(windowName);
        }

        private Settings settings;
        private RenderWindow window;
        private Input input;

        public Settings Settings    { get => settings;   private set => settings = value; }
        public RenderWindow Window  { get => window;     private set => window = value; }
        public Input Input          { get => input;      private set => input = value; }

        public void RecreateWindow(String windowName)
        {
            Window = new RenderWindow(new VideoMode(Settings.GetScreenSize().X, Settings.GetScreenSize().Y), windowName, Settings.Fullscreen ? Styles.Fullscreen : Styles.Titlebar | Styles.Close);
            Input = new Input(Window, Settings);
            Settings.shouldWindowBeRecreated = false;
        }
        
        
    }
}
