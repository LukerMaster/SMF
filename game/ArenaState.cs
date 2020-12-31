using SFML.Graphics;
using SMF.engine;
using SMF.game.fish;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class ArenaState : engine.IGameState
    {
        InstanceVars vars;
        Fish playerFish;
        List<Fish> fishes = new List<Fish>();
        Sprite background;
        AssetManager assetManager;
        public ArenaState(InstanceVars vars)
        {
            this.vars = vars;
            assetManager = new AssetManager();
            background = new Sprite(assetManager.GetBackground(0));
            playerFish = new Fish(vars.Settings.selectedFishBase);

            playerFish.position = new SFML.System.Vector2f(vars.Settings.playfieldSize.X * 0.25f, 300);

            fishes.Add(new Fish(vars.Settings.selectedFishBase));
            fishes[0].position = new SFML.System.Vector2f(vars.Settings.playfieldSize.X * 0.75f, 300);
            fishes[0].FacingLeft = true;
        }
        public void Draw()
        {
            background.Scale = new SFML.System.Vector2f((float)vars.Settings.playfieldSize.X / background.Texture.Size.X, (float)vars.Settings.playfieldSize.Y / background.Texture.Size.Y);
            vars.Window.Draw(background);
            foreach (Fish f in fishes)
                f.Draw(vars.Window);
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
                vars.Input.BoostPressed);

            foreach (Fish f in fishes)
                f.Update(dt,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false);

            if (vars.Input.EscapePressed)
            {
                vars.gameStates.Add(new MenuState(vars));
                IsDisposable = true;
            }

        }

    }
}
