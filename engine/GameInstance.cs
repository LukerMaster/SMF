using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SMF.game;

namespace SMF.engine
{
    class GameInstance
    {
        InstanceVars vars;

        public float desired_dt = 0.008333f;

        

        /// <summary>
        /// Resets the SFML View so it ALWAYS draws entire playfield regardless of window size or aspect ratio.
        /// </summary>
        private void WindowResized(object sender, SizeEventArgs e)
        {
            View view = new View();
            view.Size = new Vector2f(vars.Settings.playfieldSize.X, vars.Settings.playfieldSize.Y);
            view.Center = new Vector2f(vars.Settings.playfieldSize.X / 2, vars.Settings.playfieldSize.Y / 2);

            if (!vars.Settings.stretched)
            {
                float screenAspectRatio = (float)vars.Window.Size.X / vars.Window.Size.Y;
                float fieldAspectRatio = (float)vars.Settings.playfieldSize.X / vars.Settings.playfieldSize.Y;

                float fieldToScreenRatio = fieldAspectRatio / screenAspectRatio;

                FloatRect viewport = new FloatRect();
                viewport.Left = Math.Max(0, (1 - fieldToScreenRatio) / 2);
                viewport.Width = Math.Min(1, fieldToScreenRatio);

                viewport.Top = Math.Max(0, (1 - (1 / fieldToScreenRatio)) / 2);
                viewport.Height = Math.Min(1 / fieldToScreenRatio, 1);
                view.Viewport = viewport;
            }
            vars.Window.SetView(view);
            if (vars.Settings.shouldWindowBeRecreated)
            {
                uint resX = vars.Window.Size.X;
                uint resY = vars.Window.Size.Y;
                vars.Window.Close();
                vars.Window.Dispose();
                vars.RecreateWindow(vars.Settings.windowName);
                
            }
        }

        public GameInstance(String windowName, EResolution res)
        {
            vars = new InstanceVars(windowName, res);
            
            vars.Window.Resized += WindowResized;

            vars.gameStates.Add(new MenuState(vars));

            vars.Window.Closed += (object sender, EventArgs e) => vars.Settings.isGameOn = false;
        }

        /// <summary>
        /// Loops the private Update() function while trying to maintain fixed framerate and frametime-independency.
        /// Slows down however if frametime exceeds 10 times the desired value.
        /// </summary>
        public void Loop()
        {
            float elapsed_dt;
            float dt = desired_dt; // deltaTime used for updates (in seconds)
            Clock clock = new Clock();
            while (vars.Settings.isGameOn)
            {
                clock.Restart();

                WindowResized(null, null);
                vars.Window.DispatchEvents();
                vars.Window.Clear(Color.Black);

                Update(dt);

                vars.Window.Display();

                elapsed_dt = clock.ElapsedTime.AsSeconds();
                
                if (elapsed_dt < desired_dt)
                {
                    System.Threading.Thread.Sleep((int)((desired_dt - elapsed_dt) * 1000));
                    dt = desired_dt;
                }
                else
                {
                    dt = elapsed_dt - desired_dt;
                    if (dt > desired_dt * 10)
                        dt = desired_dt * 10; // If game runs SUPER BADLY, don't use super high DT for updates, rather slow the game.
                }
            }
            vars.Window.Close();
        }
        /// <summary>
        /// Does game update using specified deltatime (that should be properly calculated in Loop() function).
        /// Goes through every state in list of states.
        /// </summary>
        /// <param name="dt">Deltatime</param>
        void Update(float dt)
        {
            for (int i = 0; i < vars.gameStates.Count; i++)
            {
                vars.gameStates[i].Update(dt);
                if (vars.gameStates[i].IsVisible)
                    vars.gameStates[i].Draw();
                if (vars.gameStates[i].IsDisposable)
                    vars.gameStates.Remove(vars.gameStates[i]);
            }
        }
    }
}
