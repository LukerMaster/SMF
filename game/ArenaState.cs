using SFML.Graphics;
using SMF.engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class ArenaState : engine.IGameState
    {
        InstanceVars vars;
        Fish playerFish;

        public ArenaState(InstanceVars vars)
        {
            this.vars = vars;
            playerFish = vars.Settings.selectedFish;
        }
        public void Draw()
        {
            playerFish.Draw(vars.Window);
        }

        public bool IsDisposable { get; set; } = false;

        public bool IsVisible { get; set; } = true;

        public void Update(float dt)
        {
            playerFish.Update(dt,
                vars.Input.UpPressed,
                vars.Input.DownPressed,
                vars.Input.LeftPressed,
                vars.Input.RightPressed,
                vars.Input.LmbPressed,
                vars.Input.RmbPressed);
        }

    }
}
