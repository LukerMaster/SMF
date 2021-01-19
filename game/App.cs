﻿using System;
using SFBF;

namespace SMF
{
    class App
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pseudo-Gaem Engine started!");
            WindowSettings initialSettings = new WindowSettings();
            initialSettings.Resolution = new SFML.System.Vector2u(1280, 720);
            initialSettings.ViewCenterPos = new SFML.System.Vector2i(1920 / 2, 1080 / 2);
            initialSettings.ViewSize = new SFML.System.Vector2u(1920, 1080);

            SFBF.Engine engine = new SFBF.Engine();
            engine.Data.assets = new FishAssetManager();
            engine.Data.InstantiateLevel(new MenuLevel(engine.Data, initialSettings, new GameSettings()));
            engine.Run();
        }
    }
}
