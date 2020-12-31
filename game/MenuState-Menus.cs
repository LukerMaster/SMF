using SFML.Graphics;
using SFML.System;
using SMF.engine;
using SMF.engine.UI;
using SMF.game.fish;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    partial class MenuState : IGameState
    {
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
            tempBtn.OnClick = () =>
            {
                vars.Settings.selectedFishBase = menuFishBase;
                vars.gameStates.Add(new ArenaState(vars));
                IsDisposable = true;
            };
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
            AdjustButtonSet tempSet = new AdjustButtonSet(font);

            tempBtn.Position = new Vector2i(10, 900);
            tempBtn.Anchor = new Vector2f(0.0f, 0.5f);
            tempBtn.Height = 100;
            tempBtn.Label = "Back";
            tempBtn.OnClick = () => { currentMenu = CreateCustomizeMenu(); };
            menu.componentList.Add(tempBtn);
            int currentHeight = 800; // Used to position buttons next to each other no matter which are unlocked.

            if (menuFishBase.EngineUpgradable)
            {
                tempSet = new AdjustButtonSet(font);
                tempSet.Position = new Vector2i(10, currentHeight);
                tempSet.Anchor = new Vector2f(0.0f, 0.5f);
                tempSet.Height = 100;
                tempSet.Label = "Engine";
                tempSet.SetStringFunction(() => FishBase.GetLabelForTuningLevel(menuFishBase.EngineLvl));
                tempSet.SetOnClickPrev(() => menuFishBase.EngineLvl--);
                tempSet.SetOnClickNext(() => menuFishBase.EngineLvl++);
                menu.componentList.Add(tempSet);
                currentHeight -= 100;
            }

            if (menuFishBase.ChassisUpgradable)
            {
                tempSet = new AdjustButtonSet(font);
                tempSet.Position = new Vector2i(10, currentHeight);
                tempSet.Anchor = new Vector2f(0.0f, 0.5f);
                tempSet.Height = 100;
                tempSet.Label = "Chassis";
                tempSet.SetStringFunction(() => FishBase.GetLabelForTuningLevel(menuFishBase.ChassisLvl));
                tempSet.SetOnClickPrev(() => menuFishBase.ChassisLvl--);
                tempSet.SetOnClickNext(() => menuFishBase.ChassisLvl++);
                menu.componentList.Add(tempSet);
                currentHeight -= 100;
            }

            if (menuFishBase.BodyUpgradable)
            {
                tempSet = new AdjustButtonSet(font);
                tempSet.Position = new Vector2i(10, currentHeight);
                tempSet.Anchor = new Vector2f(0.0f, 0.5f);
                tempSet.Height = 100;
                tempSet.Label = "Body";
                tempSet.SetStringFunction(() => FishBase.GetLabelForTuningLevel(menuFishBase.BodyLvl));
                tempSet.SetOnClickPrev(() => menuFishBase.BodyLvl--);
                tempSet.SetOnClickNext(() => menuFishBase.BodyLvl++);
                menu.componentList.Add(tempSet);
                currentHeight -= 100;
            }

            if (menuFishBase.FinsUpgradable)
            {
                tempSet = new AdjustButtonSet(font);
                tempSet.Position = new Vector2i(10, currentHeight);
                tempSet.Anchor = new Vector2f(0.0f, 0.5f);
                tempSet.Height = 100;
                tempSet.Label = "Fins";
                tempSet.SetStringFunction(() => FishBase.GetLabelForTuningLevel(menuFishBase.FinsLvl));
                tempSet.SetOnClickPrev(() => menuFishBase.FinsLvl--);
                tempSet.SetOnClickNext(() => menuFishBase.FinsLvl++);
                menu.componentList.Add(tempSet);
                currentHeight -= 100;
            }

            if (menuFishBase.NitroUpgradable)
            {
                tempSet = new AdjustButtonSet(font);
                tempSet.Position = new Vector2i(10, currentHeight);
                tempSet.Anchor = new Vector2f(0.0f, 0.5f);
                tempSet.Height = 100;
                tempSet.Label = "Nitro";
                tempSet.SetStringFunction(() => FishBase.GetLabelForTuningLevel(menuFishBase.NitroLvl));
                tempSet.SetOnClickPrev(() => menuFishBase.NitroLvl--);
                tempSet.SetOnClickNext(() => menuFishBase.NitroLvl++);
                menu.componentList.Add(tempSet);
                currentHeight -= 100;
            }

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
            tempSet.SetStringFunction(() => Math.Round(((float)menuFishBase.tint.B / 255) * 100) + "%");
            tempSet.SetOnClickPrev(() => menuFishBase.tint -= new Color(0, 0, 15, 0));
            tempSet.SetOnClickNext(() => menuFishBase.tint += new Color(0, 0, 15, 0));
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 700);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Color Tint: Green";
            tempSet.SetStringFunction(() => Math.Round(((float)menuFishBase.tint.G / 255) * 100) + "%");
            tempSet.SetOnClickPrev(() => menuFishBase.tint -= new Color(0, 15, 0, 0));
            tempSet.SetOnClickNext(() => menuFishBase.tint += new Color(0, 15, 0, 0));
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 600);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Color Tint: Red";
            tempSet.SetStringFunction(() => Math.Round(((float)menuFishBase.tint.R / 255) * 100) + "%");
            tempSet.SetOnClickPrev(() => menuFishBase.tint -= new Color(15, 0, 0, 0));
            tempSet.SetOnClickNext(() => menuFishBase.tint += new Color(15, 0, 0, 0));
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
            tempSet.SetStringFunction(() => menuFishBase.Name);
            tempSet.SetOnClickPrev(() => menuFishBase.ChangeFishData(menuFishBase.ID - 1, assetManager.GetFishTexture(menuFishBase.ID - 1)));
            tempSet.SetOnClickNext(() => menuFishBase.ChangeFishData(menuFishBase.ID + 1, assetManager.GetFishTexture(menuFishBase.ID + 1)));
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
        Menu CreateSinglePlayerConfigMenu()
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
            tempSet.Label = "Map";
            //tempSet.SetStringFunction();
            //tempSet.SetOnClickNext(() => vars.Settings.Fullscreen = !vars.Settings.Fullscreen);
            //tempSet.SetOnClickPrev(() => vars.Settings.Fullscreen = !vars.Settings.Fullscreen);
            menu.componentList.Add(tempSet);

            tempSet = new AdjustButtonSet(font);
            tempSet.Position = new Vector2i(10, 700);
            tempSet.Anchor = new Vector2f(0.0f, 0.5f);
            tempSet.Height = 100;
            tempSet.Label = "Opponent";
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
    }
}
