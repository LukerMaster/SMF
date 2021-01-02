using SFML.Graphics;
using SMF.engine;
using SMF.game.fish;
using SMF.game.weapon;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class ArenaState : engine.IGameState
    {
        InstanceVars vars;
        List<GameObject> objects = new List<GameObject>();
        List<Tuple<Fish, Input>> players = new List<Tuple<Fish, Input>>();
        Sprite background;
        AssetManager assetManager;
        public ArenaState(InstanceVars vars)
        {
            this.vars = vars;
            assetManager = new AssetManager();
            background = new Sprite(assetManager.GetByID(AssetManager.EType.Background, 0));
            players.Add(new Tuple<Fish, Input>(new Fish(vars.Settings.selectedFishBase,new RangedWeapon(vars.Settings.selectedWeaponBase)), vars.Input));
            for (int i = 0; i < players.Count; i++)
            {
                objects.Add(players[i].Item1);
                if (i % 2 == 0)
                    players[i].Item1.position = new SFML.System.Vector2f(vars.Settings.playfieldSize.X * 0.25f, 300 + i * 50);
                else
                {
                    players[i].Item1.position = new SFML.System.Vector2f(vars.Settings.playfieldSize.X * 0.75f, 300 + i * 50);
                    players[i].Item1.FacingLeft = true;
                }
                    
            }
        }
        public void Draw()
        {
            background.Scale = new SFML.System.Vector2f((float)vars.Settings.playfieldSize.X / background.Texture.Size.X, (float)vars.Settings.playfieldSize.Y / background.Texture.Size.Y);
            vars.Window.Draw(background);
            foreach (Tuple<Fish, Input> f in players)
                f.Item1.Draw(vars.Window);
        }

        public bool IsDisposable { get; set; } = false;

        public bool IsVisible { get; set; } = true;

        public void Update(float dt)
        {
            foreach (GameObject o in objects)
                o.Update(dt, vars.Input, objects);

            if (vars.Input.EscapePressed)
            {
                vars.gameStates.Add(new MenuState(vars));
                IsDisposable = true;
            }

        }

    }
}
