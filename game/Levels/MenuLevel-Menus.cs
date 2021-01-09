using SFBE;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace SMF
{
    partial class MenuLevel
    {
        

        void CreateMainMenu(int currentlySelected = 3) // Builder/helper function for creating menu faster.
        {
            currentMenu.ClearMenu();
            currentMenu.AddButton("Exit", () => Exit = true);
            currentMenu.AddButton("Options", () => CreateOptionsMenu());
            currentMenu.AddButton("Customize", () => CreateCustomizeMenu());
            currentMenu.AddButton("Singleplayer", () => StartArena = true);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreateOptionsMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();
            currentMenu.AddButton("Back", () => CreateMainMenu(1));
            currentMenu.AddButtonSet("Fullscreen", () => Settings.Fullscreen ? "Yes" : "No", () => Settings.Fullscreen = !Settings.Fullscreen, () => Settings.Fullscreen = !Settings.Fullscreen, 150);
            currentMenu.AddButtonSet("Stretched", () => Settings.Stretched ? "Yes" : "No", () => Settings.Stretched = !Settings.Stretched, () => Settings.Stretched = !Settings.Stretched, 150);
            currentMenu.AddButtonSet("Back", () => Settings.Stretched ? "Yes" : "No", () => Settings.Stretched = !Settings.Stretched, () => Settings.Stretched = !Settings.Stretched, 150);
            currentMenu.AddButtonSet("Resolution", () => Settings.Resolution.X + "x" + Settings.Resolution.Y, () => Settings.Resolution = new Vector2u(1920, 1080), () => Settings.Resolution = new Vector2u(1920, 1080), 400);
            currentMenu.AddButtonSet("SFX Volume", () => Math.Round(SfxSoundVolume * 100) + "%", () => SfxSoundVolume -= 0.05f, () => SfxSoundVolume += 0.05f, 200);
            currentMenu.AddButtonSet("Music Volume", () => Math.Round(MusicSoundVolume * 100) + "%", () => MusicSoundVolume -= 0.05f, () => MusicSoundVolume += 0.05f, 200);
            currentMenu.AddButtonSet("Master Volume", () => Math.Round(MasterSoundVolume * 100) + "%", () => MasterSoundVolume -= 0.05f, () => MasterSoundVolume += 0.05f, 200);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreatePerformanceTuningMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();
            currentMenu.AddButton("Back", () => CreateCustomizeMenu(0));

            if (menuFishBase.EngineUpgradable)
                currentMenu.AddButtonSet("Engine", () => FishBase.GetLabelForTuningLevel(menuFishBase.EngineLvl), () => menuFishBase.EngineLvl--, () => menuFishBase.EngineLvl++, 300);

            if (menuFishBase.ChassisUpgradable)
                currentMenu.AddButtonSet("Chassis", () => FishBase.GetLabelForTuningLevel(menuFishBase.ChassisLvl), () => menuFishBase.ChassisLvl--, () => menuFishBase.ChassisLvl++, 300);

            if (menuFishBase.BodyUpgradable)
                currentMenu.AddButtonSet("Body", () => FishBase.GetLabelForTuningLevel(menuFishBase.BodyLvl), () => menuFishBase.BodyLvl--, () => menuFishBase.BodyLvl++, 300);

            if (menuFishBase.FinsUpgradable)
                currentMenu.AddButtonSet("Fins", () => FishBase.GetLabelForTuningLevel(menuFishBase.FinsLvl), () => menuFishBase.FinsLvl--, () => menuFishBase.FinsLvl++, 300);

            if (menuFishBase.NitroUpgradable)
                currentMenu.AddButtonSet("Nitro", () => FishBase.GetLabelForTuningLevel(menuFishBase.NitroLvl), () => menuFishBase.NitroLvl--, () => menuFishBase.NitroLvl++, 300);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreateVisualTuningMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();
            ShowCustomizeMenu = true;

            currentMenu.AddButton("Back", () => CreateCustomizeMenu(0));
            currentMenu.AddButtonSet("Color Tint: Blue", () => Math.Round(((float)menuFishBase.tint.B / 255) * 100) + "%", () => menuFishBase.tint -= new Color(0, 0, 15, 0), () => menuFishBase.tint += new Color(0, 0, 15, 0), 200);
            currentMenu.AddButtonSet("Color Tint: Green", () => Math.Round(((float)menuFishBase.tint.G / 255) * 100) + "%", () => menuFishBase.tint -= new Color(0, 15, 0, 0), () => menuFishBase.tint += new Color(0, 15, 0, 0), 200);
            currentMenu.AddButtonSet("Color Tint: Red", () => Math.Round(((float)menuFishBase.tint.R / 255) * 100) + "%", () => menuFishBase.tint -= new Color(15, 0, 05, 0), () => menuFishBase.tint += new Color(15, 0, 0, 0), 200);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreateCustomizeMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();
            currentMenu.AddButton("Back", () => CreateMainMenu(2));
            currentMenu.AddButtonSet("Fish", () => menuFishBase.Name, () =>
            {
                if (File.Exists("assets/champs/" + (menuFishBase.ID - 1) + ".xml"))
                    menuFishBase.ChangeFishData(menuFishBase.ID - 1);
                if (fishForCustomizeMenu != null)
                    DestroyActor(fishForCustomizeMenu);
                fishForCustomizeMenu = (Fish)InstantiateActor(new Fish(menuFishBase, assets.GetByID(FishAssetManager.EType.Fish, menuFishBase.ID)));
            }, () =>
            {
                if (File.Exists("assets/champs/" + (menuFishBase.ID + 1) + ".xml"))
                    menuFishBase.ChangeFishData(menuFishBase.ID + 1);
                if (fishForCustomizeMenu != null)
                    DestroyActor(fishForCustomizeMenu);
                fishForCustomizeMenu = (Fish)InstantiateActor(new Fish(menuFishBase, assets.GetByID(FishAssetManager.EType.Fish, menuFishBase.ID)));
            }, 200);
            currentMenu.AddButton("Performance Parts", () => CreatePerformanceTuningMenu(0));
            currentMenu.AddButton("Visual Tuning", () => CreateVisualTuningMenu(0));

            currentMenu.AddButtonSet("Weapon", () => menuWeaponBase.Name, () =>
            {
                if (File.Exists("assets/weapons/" + (menuWeaponBase.ID - 1) + ".xml"))
                    menuWeaponBase.ChangeWeaponData(menuWeaponBase.ID - 1);
            }, () =>
            {
                if (File.Exists("assets/weapons/" + (menuWeaponBase.ID + 1) + ".xml"))
                    menuWeaponBase.ChangeWeaponData(menuWeaponBase.ID + 1);
            }, 200);
            currentMenu.CurrentlySelected = currentlySelected;
        }
    }
}
