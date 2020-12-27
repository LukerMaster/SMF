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

namespace SMF.game
{
    class MenuState : IGameState
    {
        InstanceVars vars;

        static Texture bgTexture = new Texture("assets/sprites/maps/0.png");
        static Texture smfTexture = new Texture("assets/sprites/menu/title.png");
        static Texture customizeMenuTexture = new Texture("assets/sprites/menu/custom_menu.png");
        static Font font = new Font("assets/roboto.mlg");


        Sprite bgSprite = new Sprite(bgTexture);
        Sprite smfSprite = new Sprite(smfTexture);
        Sprite customSprite = new Sprite(customizeMenuTexture);

        Menu currentMenu;

        Fish selectedFish;
        static Texture selectedFishTexture;

        bool ShowCustomizeMenu = false;

        public MenuState(InstanceVars vars)
        {
            this.vars = vars;
            bgSprite.Scale = new Vector2f((float)vars.Settings.playfieldSize.X / bgTexture.Size.X, (float)vars.Settings.playfieldSize.Y / bgTexture.Size.Y);
            smfSprite.Position = new Vector2f(50, 50);
            smfSprite.Scale = new Vector2f(0.6f, 0.6f);
            
            selectedFish = new Fish();
            LoadNewFishData(0);

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

        public bool IsDisposable()
        {
            return false;
        }

        public bool IsVisible()
        {
            return true;
        }

        Menu CreateMainMenu(int currentlySelected = 0) // Builder/helper function for creating menu faster.
        {
            Menu menu = new Menu();
            menu.FlipUpDown = true;
            TextButton tempBtn = new TextButton(font);

            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Exit";
            tempBtn.OnClick = () => vars.Settings.isGameOn = false;
            menu.componentList.Add(tempBtn);

            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 800);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Options";
            tempBtn.OnClick = () => currentMenu = CreateOptionsMenu();
            menu.componentList.Add(tempBtn);

            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 700);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Customize";
            tempBtn.OnClick = () => currentMenu = CreateCustomizeMenu();
            menu.componentList.Add(tempBtn);

            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 600);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Singleplayer";
            menu.componentList.Add(tempBtn);

            menu.CurrentlySelected = currentlySelected;

            return menu;
        }

        Menu CreateOptionsMenu()
        {
            Menu menu = new Menu();
            menu.FlipUpDown = true;
            TextButton tempBtn = new TextButton(font);
            AdjustButtonSet tempSet = new AdjustButtonSet(font);

            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Back";
            tempBtn.OnClick = () => currentMenu = CreateMainMenu(1);
            menu.componentList.Add(tempBtn);

            tempSet.Position = new Vector2i(10, 800);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Fullscreen";
            tempSet.SetStringFunction(() => vars.Settings.Fullscreen ? "Yes" : "No");
            tempSet.SetOnClickNext(() => vars.Settings.Fullscreen = !vars.Settings.Fullscreen);
            tempSet.SetOnClickPrev(() => vars.Settings.Fullscreen = !vars.Settings.Fullscreen);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 700);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Stretched";
            tempSet.SetStringFunction(() => vars.Settings.stretched ? "Yes" : "No");
            tempSet.SetOnClickNext(() => vars.Settings.stretched = !vars.Settings.stretched);
            tempSet.SetOnClickPrev(() => vars.Settings.stretched = !vars.Settings.stretched);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 600);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Resolution";
            tempSet.SetStringFunction(() => vars.Settings.GetScreenSize().X + "x" + vars.Settings.GetScreenSize().Y);
            tempSet.SetOnClickNext(() => vars.Settings.Resolution++);
            tempSet.SetOnClickPrev(() => vars.Settings.Resolution--);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 500);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "SFX Volume: ";
            tempSet.SetStringFunction(() => Math.Round(vars.Settings.SfxSoundVolume * 100) + "%");
            tempSet.SetOnClickNext(() => vars.Settings.SfxSoundVolume += 0.05f);
            tempSet.SetOnClickPrev(() => vars.Settings.SfxSoundVolume -= 0.05f);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 400);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Music Volume: ";
            tempSet.SetStringFunction(() => Math.Round(vars.Settings.MusicSoundVolume * 100) + "%");
            tempSet.SetOnClickNext(() => vars.Settings.MusicSoundVolume += 0.05f);
            tempSet.SetOnClickPrev(() => vars.Settings.MusicSoundVolume -= 0.05f);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 300);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Master Volume: ";
            tempSet.SetStringFunction(() => Math.Round(vars.Settings.MasterSoundVolume * 100) + "%");
            tempSet.SetOnClickNext(() => vars.Settings.MasterSoundVolume += 0.05f);
            tempSet.SetOnClickPrev(() => vars.Settings.MasterSoundVolume -= 0.05f);
            menu.componentList.Add(tempSet);

            return menu;
        }
        Menu CreatePerformanceTuningMenu()
        {
            ShowCustomizeMenu = true;

            Menu menu = new Menu();
            menu.FlipUpDown = true;
            TextButton tempBtn = new TextButton(font);

            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Back";
            tempBtn.OnClick = () => { currentMenu = CreateCustomizeMenu(); };
            menu.componentList.Add(tempBtn);

            AdjustButtonSet tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 800);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Engine";
            tempSet.SetStringFunction(() => TuningValues.GetLabelForTuningLevel(selectedFish.EngineLvl));
            tempSet.SetOnClickPrev(() => selectedFish.EngineLvl--);
            tempSet.SetOnClickNext(() => selectedFish.EngineLvl++);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 700);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Chassis";
            tempSet.SetStringFunction(() => TuningValues.GetLabelForTuningLevel(selectedFish.ChassisLvl));
            tempSet.SetOnClickPrev(() => selectedFish.ChassisLvl--);
            tempSet.SetOnClickNext(() => selectedFish.ChassisLvl++);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 600);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Body";
            tempSet.SetStringFunction(() => TuningValues.GetLabelForTuningLevel(selectedFish.BodyLvl));
            tempSet.SetOnClickPrev(() => selectedFish.BodyLvl--);
            tempSet.SetOnClickNext(() => selectedFish.BodyLvl++);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 500);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Fins";
            tempSet.SetStringFunction(() => TuningValues.GetLabelForTuningLevel(selectedFish.FinsLvl));
            tempSet.SetOnClickPrev(() => selectedFish.FinsLvl--);
            tempSet.SetOnClickNext(() => selectedFish.FinsLvl++);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 400);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Nitro";
            tempSet.SetStringFunction(() => TuningValues.GetLabelForTuningLevel(selectedFish.NitroLvl));
            tempSet.SetOnClickPrev(() => selectedFish.NitroLvl--);
            tempSet.SetOnClickNext(() => selectedFish.NitroLvl++);
            menu.componentList.Add(tempSet);


            return menu;
        }
        Menu CreateVisualTuningMenu()
        {
            ShowCustomizeMenu = true;

            Menu menu = new Menu();
            menu.FlipUpDown = true;
            TextButton tempBtn = new TextButton(font);

            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Back";
            tempBtn.OnClick = () => { currentMenu = CreateCustomizeMenu(); };
            menu.componentList.Add(tempBtn);

            AdjustButtonSet tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 800);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Color Tint: Blue";
            tempSet.SetStringFunction(() => Math.Round(((float)selectedFish.tint.B / 255) * 100) + "%");
            tempSet.SetOnClickPrev(() => selectedFish.tint -= new Color(0, 0, 15, 0));
            tempSet.SetOnClickNext(() => selectedFish.tint += new Color(0, 0, 15, 0));
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 700);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Color Tint: Green";
            tempSet.SetStringFunction(() => Math.Round(((float)selectedFish.tint.G / 255) * 100)  + "%");
            tempSet.SetOnClickPrev(() => selectedFish.tint -= new Color(0, 15, 0, 0));
            tempSet.SetOnClickNext(() => selectedFish.tint += new Color(0, 15, 0, 0));
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 600);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Color Tint: Red";
            tempSet.SetStringFunction(() => Math.Round(((float)selectedFish.tint.R / 255) * 100) + "%");
            tempSet.SetOnClickPrev(() => selectedFish.tint -= new Color(15, 0, 0, 0));
            tempSet.SetOnClickNext(() => selectedFish.tint += new Color(15, 0, 0, 0));
            menu.componentList.Add(tempSet);


            return menu;
        }
        Menu CreateCustomizeMenu()
        {
            ShowCustomizeMenu = true;

            Menu menu = new Menu();
            menu.FlipUpDown = true;
            TextButton tempBtn = new TextButton(font);

            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Back";
            tempBtn.OnClick = () => { ShowCustomizeMenu = false; currentMenu = CreateMainMenu(2); };
            menu.componentList.Add(tempBtn);

            AdjustButtonSet tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 800);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Fish";
            tempSet.SetStringFunction(() => selectedFish.baseData.Name);
            tempSet.SetOnClickPrev(() => LoadNewFishData(selectedFish.baseData.ID - 1));
            tempSet.SetOnClickNext(() => LoadNewFishData(selectedFish.baseData.ID + 1));
            menu.componentList.Add(tempSet);

            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 700);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Performance Parts";
            tempBtn.OnClick = () => { currentMenu = CreatePerformanceTuningMenu(); };
            menu.componentList.Add(tempBtn);


            tempBtn = new TextButton(font);
            tempBtn.Position = new Vector2i(10, 600);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Visual Tuning";
            tempBtn.OnClick = () => { currentMenu = CreateVisualTuningMenu(); };
            menu.componentList.Add(tempBtn);


            return menu;
        }
        void LoadNewFishData(int id)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FishData));
            try
            {
                using (Stream reader = new FileStream("assets/champs/" + id + ".xml", FileMode.Open))
                {
                    selectedFish.baseData = (FishData)serializer.Deserialize(reader);
                }

                selectedFishTexture = new Texture("assets/champs/" + id + ".png");

                selectedFish.sprite = new Sprite(selectedFishTexture);
                selectedFish.sprite.Scale = new Vector2f((float)selectedFish.baseData.SizeX / selectedFishTexture.Size.X,
                    (float)selectedFish.baseData.SizeY / selectedFishTexture.Size.Y);
            }
            catch (System.IO.FileNotFoundException)
            {
                // If there is no fish with this ID, ignore reading process. Leave it as it is.
            }
            
        }

        private void DrawCustomizeMenu()
        {
            Sprite previewSprite = new Sprite(selectedFish.sprite);
            previewSprite.Color = selectedFish.tint;
            previewSprite.Position = new Vector2f(1860, 700);
            previewSprite.Scale = new Vector2f(-Math.Abs(previewSprite.Scale.X * 1.5f), previewSprite.Scale.Y * 1.5f);
            customSprite.Position = new Vector2f(1502, 270);
            customSprite.Scale = new Vector2f(418.0f / customSprite.Texture.Size.X, 700.0f / customSprite.Texture.Size.Y);
            vars.Window.Draw(customSprite);
            vars.Window.Draw(previewSprite);

            RectangleShape[] bars = new RectangleShape[6];
            for (int i = 0; i < 6; i++)
                bars[i] = new RectangleShape();

            float[] percentages = new float[6];
            percentages[0] = selectedFish.MaxHealth / 5000.0f;
            percentages[1] = selectedFish.baseData.HealthRegen / 5000.0f;
            percentages[2] = selectedFish.baseData.MaxSpeed / 3000.0f;
            percentages[3] = selectedFish.baseData.Acceleration / 3000.0f;
            percentages[4] = selectedFish.baseData.Friction / 1.0f;
            percentages[5] = selectedFish.baseData.Stamina / 5000.0f;
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
