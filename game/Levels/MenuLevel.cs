using SFBE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    partial class MenuLevel : Level
    {
        public bool ShowCustomizeMenu;
        public float MasterSoundVolume;
        public float MusicSoundVolume;
        public float SfxSoundVolume;
        public Fish fishForCustomizeMenu;

        public bool Exit = false;
        public bool StartArena = false;


        Menu currentMenu;

        FishAssetManager assets;

        public FishBase menuFishBase;
        public WeaponBase menuWeaponBase;
        public MenuLevel(Instance instance, WindowSettings settings)
        {
            Settings = settings;
            assets = (FishAssetManager)instance.assets;
            currentMenu = (Menu)InstantiateActor(new Menu("roboto"));
            currentMenu.BottomLeftPos = new SFML.System.Vector2f(20, 1060);
            CreateMainMenu();
            InstantiateActor(new MainBackground(Settings.ViewSize));

            menuFishBase = new FishBase(0, assets.GetByID(FishAssetManager.EType.Fish, 0));
            menuWeaponBase = new WeaponBase(0, assets.GetByID(FishAssetManager.EType.Weapon, 0));

        }
        protected override void FixedUpdateScript(float dt, Instance data)
        {
            
        }

        protected override void UpdateScript(float dt, Instance data)
        {
            if (Exit) data.IsGameOn = false;

            if (StartArena)
            {
                data.InstantiateLevel(new ArenaLevel(menuFishBase, data, Settings));
                data.DestroyLevel(this);
            }
        }
    }
}
