using SFML.Graphics;
using SMF.engine;
using SMF.game.fish;
using SMF.game.weapon;
using System;
using System.Collections.Generic;
using System.Text;
using static SMF.game.Scene;

namespace SMF.game
{
    class ArenaState : engine.IGameState
    {
        InstanceVars vars;
        SceneController sceneController = new SceneController();
        List<Fish> players = new List<Fish>();
        Sprite background;
        AssetManager assetManager;
        public ArenaState(InstanceVars vars)
        {
            this.vars = vars;
            assetManager = new AssetManager();
            background = new Sprite(assetManager.GetByID(AssetManager.EType.Background, 0));
            players.Add((Fish)sceneController.scene.Instantiate(new Fish(vars.Settings.selectedFishBase, assetManager.GetByID(AssetManager.EType.Fish, vars.Settings.selectedFishBase.ID))));
            players[players.Count - 1].weapon = (Weapon)sceneController.scene.Instantiate(new RangedWeapon(vars.Settings.selectedWeaponBase, assetManager.GetByID(AssetManager.EType.Weapon, vars.Settings.selectedWeaponBase.ID)));
            for (int i = 0; i < players.Count; i++)
            {
                if (i % 2 == 0)
                    players[i].Position = new SFML.System.Vector2f(vars.Settings.playfieldSize.X * 0.25f, 300 + i * 50);
                else
                {
                    players[i].Position = new SFML.System.Vector2f(vars.Settings.playfieldSize.X * 0.75f, 300 + i * 50);
                    players[i].FacingLeft = true;
                }
                    
            }

        }

        public bool IsDisposable { get; set; } = false;

        public bool IsVisible { get; set; } = true;

        public void Update(float dt)
        {
            players[0].ReceiveInput(vars.Input);
            sceneController.Update(dt, vars.Input, assetManager);

            if (vars.Input.EscapePressed)
            {
                vars.gameStates.Add(new MenuState(vars));
                IsDisposable = true;
            }

        }
        public void Draw()
        {
            background.Scale = new SFML.System.Vector2f((float)vars.Settings.playfieldSize.X / background.Texture.Size.X, (float)vars.Settings.playfieldSize.Y / background.Texture.Size.Y);
            vars.Window.Draw(background);
            sceneController.Draw(vars.Window);
            
        }

    }
}
