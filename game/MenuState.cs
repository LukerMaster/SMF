using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml.Serialization;
using SFML.Graphics;
using SFML.System;
using SMF.engine;
using SMF.engine.UI;
using SMF.game.fish;

namespace SMF.game
{
    partial class MenuState : IGameState
    {
        InstanceVars vars;

        static Texture smfTexture = new Texture("assets/menu/title.png");
        static Texture customizeMenuTexture = new Texture("assets/menu/custom_menu.png");
        static Font font = new Font("assets/roboto.mlg");


        Sprite bgSprite;
        Sprite smfSprite = new Sprite(smfTexture);
        Sprite customSprite = new Sprite(customizeMenuTexture);

        Menu currentMenu;

        FishBase menuFishBase;
        Fish fishForCustomizeMenu;
        AssetManager assetManager = new AssetManager();

        bool ShowCustomizeMenu = false;

        public MenuState(InstanceVars vars)
        {
            this.vars = vars;

            bgSprite = new Sprite(assetManager.GetBackground(0));
            bgSprite.Scale = new Vector2f((float)vars.Settings.playfieldSize.X / bgSprite.Texture.Size.X, (float)vars.Settings.playfieldSize.Y / bgSprite.Texture.Size.Y);
            smfSprite.Position = new Vector2f(50, 50);
            smfSprite.Scale = new Vector2f(0.6f, 0.6f);


            if (vars.Settings.selectedFishBase != null)
                menuFishBase = vars.Settings.selectedFishBase.Copy();
            else
                menuFishBase = new FishBase(assetManager.GetFishTexture(0));

            fishForCustomizeMenu = new Fish(menuFishBase);

            currentMenu = CreateMainMenu();
            
        }

        public void Update(float dt)
        {
            currentMenu.Update(dt,
                vars.Input.MousePos,
                vars.Input.LmbPressed,
                vars.Input.EnterPressed,
                vars.Input.UpPressed,
                vars.Input.DownPressed,
                vars.Input.LeftPressed,
                vars.Input.RightPressed);

            

        }

        public void Draw()
        {
            vars.Window.Draw(bgSprite);
            vars.Window.Draw(smfSprite);
            currentMenu.Draw(vars.Window);
            if (ShowCustomizeMenu)
                DrawCustomizeMenu();
        }

        public bool IsDisposable { get; set; } = false;
        public bool IsVisible { get; set; } = true;

        

        private void DrawCustomizeMenu()
        {
            fishForCustomizeMenu.position = new Vector2f(1720, 770);
            customSprite.Position = new Vector2f(1502, 270);
            customSprite.Scale = new Vector2f(418.0f / customSprite.Texture.Size.X, 700.0f / customSprite.Texture.Size.Y);
            vars.Window.Draw(customSprite);
            fishForCustomizeMenu.Draw(vars.Window);

            RectangleShape[] bars = new RectangleShape[6];
            for (int i = 0; i < 6; i++)
                bars[i] = new RectangleShape();

            float[] percentages = new float[6];
            percentages[0] = menuFishBase.MaxHealth / 13000.0f;
            percentages[1] = Math.Max(0, menuFishBase.MaxHealthRegen / 1000.0f);
            percentages[2] = menuFishBase.MaxSpeed / 1600.0f;
            percentages[3] = menuFishBase.MaxAcceleration / 1500.0f;
            percentages[4] = (float)Math.Pow(menuFishBase.Friction, 3) / 0.941192f;
            percentages[5] = menuFishBase.MaxStamina / 20000.0f;
            bars[0].FillColor = new Color(255, 0, 0, 255);
            bars[1].FillColor = new Color(255, 60, 60, 255);
            bars[2].FillColor = new Color(200, 0, 200, 255);
            bars[3].FillColor = new Color(30, 30, 255, 255);
            bars[4].FillColor = new Color(60, 120, 60, 255);
            bars[5].FillColor = new Color(255, 255,0 , 255);

            for (int i = 0; i < 6; i++)
            {
                bars[i].Position = new Vector2f(customSprite.Position.X + (110 * 418.0f / customSprite.Texture.Size.X), customSprite.Position.Y + ((20 + i * 120) * 700.0f / customSprite.Texture.Size.Y));
                bars[i].Size = new Vector2f(percentages[i] * 800 * 418.0f / customSprite.Texture.Size.X, 100 * 700.0f / customSprite.Texture.Size.Y);
                vars.Window.Draw(bars[i]);
            }

        }

    }
}
