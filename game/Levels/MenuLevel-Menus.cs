using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

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
            currentMenu.AddButtonSet("Resolution", () => Settings.Resolution.X + "x" + Settings.Resolution.Y, () => Settings.Resolution = Resolutions.GetSmaller(Settings.Resolution), () => Settings.Resolution = Resolutions.GetBigger(Settings.Resolution), 400);
            currentMenu.AddButtonSet("SFX Volume", () => Math.Round(gameSettings.SfxSoundVolume * 100) + "%", () => gameSettings.SfxSoundVolume -= 0.05f, () => gameSettings.SfxSoundVolume += 0.05f, 200);
            currentMenu.AddButtonSet("Music Volume", () => Math.Round(gameSettings.MusicSoundVolume * 100) + "%", () => gameSettings.MusicSoundVolume -= 0.05f, () => gameSettings.MusicSoundVolume += 0.05f, 200);
            currentMenu.AddButtonSet("Master Volume", () => Math.Round(gameSettings.MasterSoundVolume * 100) + "%", () => gameSettings.MasterSoundVolume -= 0.05f, () => gameSettings.MasterSoundVolume += 0.05f, 200);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreatePerformanceTuningMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();
            customizeMenu.ShowDescription = true;
            currentMenu.AddButton("Back", () => CreateCustomizeMenu(0));

            if (gameSettings.playerFishBase.EngineUpgradable)
                currentMenu.AddButtonSet("Engine", () => FishBase.GetLabelForTuningLevel(gameSettings.playerFishBase.EngineLvl), () => gameSettings.playerFishBase.EngineLvl--, () => gameSettings.playerFishBase.EngineLvl++, 350, () => customizeMenu.UpgradeString = XDocument.Load("assets/champs/TuningStuff.xml").Root.Element("Engine").Element("L" + gameSettings.playerFishBase.EngineLvl.ToString()).Value);

            if (gameSettings.playerFishBase.ChassisUpgradable)
                currentMenu.AddButtonSet("Chassis", () => FishBase.GetLabelForTuningLevel(gameSettings.playerFishBase.ChassisLvl), () => gameSettings.playerFishBase.ChassisLvl--, () => gameSettings.playerFishBase.ChassisLvl++, 350, () => customizeMenu.UpgradeString = XDocument.Load("assets/champs/TuningStuff.xml").Root.Element("Chassis").Element("L" + gameSettings.playerFishBase.ChassisLvl.ToString()).Value);

            if (gameSettings.playerFishBase.BodyUpgradable)
                currentMenu.AddButtonSet("Body", () => FishBase.GetLabelForTuningLevel(gameSettings.playerFishBase.BodyLvl), () => gameSettings.playerFishBase.BodyLvl--, () => gameSettings.playerFishBase.BodyLvl++, 350, () => customizeMenu.UpgradeString = XDocument.Load("assets/champs/TuningStuff.xml").Root.Element("Body").Element("L" + gameSettings.playerFishBase.BodyLvl.ToString()).Value);

            if (gameSettings.playerFishBase.FinsUpgradable)
                currentMenu.AddButtonSet("Fins", () => FishBase.GetLabelForTuningLevel(gameSettings.playerFishBase.FinsLvl), () => gameSettings.playerFishBase.FinsLvl--, () => gameSettings.playerFishBase.FinsLvl++, 350, () => customizeMenu.UpgradeString = XDocument.Load("assets/champs/TuningStuff.xml").Root.Element("Fins").Element("L" + gameSettings.playerFishBase.FinsLvl.ToString()).Value);

            if (gameSettings.playerFishBase.NitrousUpgradable)
                currentMenu.AddButtonSet("Nitrous", () => FishBase.GetLabelForTuningLevel(gameSettings.playerFishBase.NitrousLvl), () => gameSettings.playerFishBase.NitrousLvl--, () => { gameSettings.playerFishBase.NitrousLvl++; if (gameSettings.playerFishBase.EngineLvl == 0) gameSettings.playerFishBase.EngineLvl++; }, 350, () => customizeMenu.UpgradeString = XDocument.Load("assets/champs/TuningStuff.xml").Root.Element("Nitrous").Element("L" + gameSettings.playerFishBase.NitrousLvl.ToString()).Value);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreateVisualTuningMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();

            currentMenu.AddButton("Back", () => CreateCustomizeMenu(0));
            currentMenu.AddButtonSet("Color Tint: Blue", () => Math.Round(((float)gameSettings.playerFishBase.tint.B / 255) * 100) + "%", () => gameSettings.playerFishBase.tint -= new Color(0, 0, 51, 0), () => gameSettings.playerFishBase.tint += new Color(0, 0, 51, 0), 200);
            currentMenu.AddButtonSet("Color Tint: Green", () => Math.Round(((float)gameSettings.playerFishBase.tint.G / 255) * 100) + "%", () => gameSettings.playerFishBase.tint -= new Color(0, 51, 0, 0), () => gameSettings.playerFishBase.tint += new Color(0, 51, 0, 0), 200);
            currentMenu.AddButtonSet("Color Tint: Red", () => Math.Round(((float)gameSettings.playerFishBase.tint.R / 255) * 100) + "%", () => gameSettings.playerFishBase.tint -= new Color(51, 0, 0, 0), () => gameSettings.playerFishBase.tint += new Color(51, 0, 0, 0), 200);

            currentMenu.CurrentlySelected = currentlySelected;
        }
        void CreateCustomizeMenu(int currentlySelected = 0)
        {
            currentMenu.ClearMenu();
            ShowCustomizeMenu = true;
            if (customizeMenu != null)
                customizeMenu.ShowDescription = false;

            currentMenu.AddButton("Back", () => { ShowCustomizeMenu = false; CreateMainMenu(2); });
            currentMenu.AddButtonSet("Fish", () => gameSettings.playerFishBase.Name, () =>
            {
                if (File.Exists("assets/champs/" + (gameSettings.playerFishBase.ID - 1) + ".xml"))
                    gameSettings.playerFishBase.ChangeFishData(gameSettings.playerFishBase.ID - 1);
            }, () =>
            {
                if (File.Exists("assets/champs/" + (gameSettings.playerFishBase.ID + 1) + ".xml"))
                    gameSettings.playerFishBase.ChangeFishData(gameSettings.playerFishBase.ID + 1);
            }, 400);
            currentMenu.AddButton("Performance Parts", () => CreatePerformanceTuningMenu(0));
            currentMenu.AddButton("Visual Tuning", () => CreateVisualTuningMenu(0));

            currentMenu.AddButtonSet("Weapon", () => fishForCustomizeMenu.weapon.Name, () =>
            {
                if (File.Exists("assets/weapons/" + (fishForCustomizeMenu.weapon.ID - 1) + ".xml"))
                    fishForCustomizeMenu.weapon = weaponBuilder.CreateWeapon(fishForCustomizeMenu.weapon.ID - 1);
                gameSettings.selectedWeaponID = fishForCustomizeMenu.weapon.ID;
            }, () =>
            {
                if (File.Exists("assets/weapons/" + (fishForCustomizeMenu.weapon.ID + 1) + ".xml"))
                    fishForCustomizeMenu.weapon = weaponBuilder.CreateWeapon(fishForCustomizeMenu.weapon.ID + 1);
                gameSettings.selectedWeaponID = fishForCustomizeMenu.weapon.ID;
            }, 400);
            currentMenu.CurrentlySelected = currentlySelected;
        }
    }
}
