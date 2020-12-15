using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SMF.engine;

namespace SMF.game
{
    class MenuState : IGameState
    {
        InstanceVars vars;

        static Texture bgTexture = new Texture("assets/sprites/maps/0.png");
        static Texture smfTexture = new Texture("assets/sprites/menu/title.png");
        static Font font = new Font("assets/roboto.mlg");

        Sprite bgSprite = new Sprite(bgTexture);
        Sprite smfSprite = new Sprite(smfTexture);


        int currentMenu = 0;

        Menu mainMenu = new Menu();
        public MenuState(InstanceVars vars)
        {
            this.vars = vars;
            bgSprite.Scale = new Vector2f((float)vars.Settings.playfieldSize.X / bgTexture.Size.X, (float)vars.Settings.playfieldSize.Y / bgTexture.Size.Y);
            smfSprite.Position = new Vector2f(50, 50);
            smfSprite.Scale = new Vector2f(0.6f, 0.6f);

            mainMenu.FlipUpDown = true;
            TextButton tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Exit";
            tempBtn.OnClick = () => vars.Settings.isGameOn = false;
            mainMenu.componentList.Add(tempBtn);
            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 800);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Options";
            tempBtn.OnClick = () => currentMenu = 1;
            mainMenu.componentList.Add(tempBtn);
            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 700);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Customize";
            mainMenu.componentList.Add(tempBtn);
            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 600);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Singleplayer";
            mainMenu.componentList.Add(tempBtn);
            AdjustButtonSet tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 500);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Setting One:";
            tempSet.SetStringFunction(() => currentMenu == 1 ? "Dupka" : "Pieeeeeska");
            tempSet.SetOnClickPrev(() => currentMenu = 0);
            tempSet.SetOnClickNext(() => currentMenu = 1);
            mainMenu.componentList.Add(tempSet);
        }

        public void Update(float dt)
        {
            mainMenu.Update(dt,
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

            mainMenu.Draw(vars.Window);
        }

        public bool IsDisposable()
        {
            return false;
        }

        public bool IsVisible()
        {
            return true;
        }

        
    }
}
