using SFBF;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    partial class MenuLevel : Level
    {
        public bool ShowCustomizeMenu;
        public float MasterSoundVolume = 0.1f;
        public float MusicSoundVolume = 1.0f;
        public float SfxSoundVolume = 1.0f;
        public Fish fishForCustomizeMenu;

        public bool Exit = false;
        public bool StartArena = false;


        Menu currentMenu;
        CustomizeMenu customizeMenu;

        FishAssetManager assets;

        public FishBase menuFishBase;
        public WeaponBuilder weaponBuilder = new WeaponBuilder();
        public MenuLevel(Instance instance, WindowSettings settings, FishBase fishBase = null)
        {
            assets = (FishAssetManager)instance.assets;

            if (fishBase == null)
                menuFishBase = new FishBase(0);
            else
                menuFishBase = fishBase;

            Settings = settings;
            
            currentMenu = (Menu)InstantiateActor(new Menu("roboto"));
            currentMenu.BottomLeftPos = new SFML.System.Vector2f(20, 960);
            CreateMainMenu();
            InstantiateActor(new MainBackground(Settings.ViewSize));

            

            fishForCustomizeMenu = (Fish)InstantiateActor(new Fish(menuFishBase));
            fishForCustomizeMenu.Position = new SFML.System.Vector2f(1700, 750);
            fishForCustomizeMenu.Scale = new SFML.System.Vector2f(2.0f, 2.0f);
            fishForCustomizeMenu.FacingLeft = true;
            fishForCustomizeMenu.DrawOrder = 1;

            fishForCustomizeMenu.weapon = weaponBuilder.CreateWeapon(0);

        }
        protected override void FixedUpdateScript(float dt, Instance data)
        {
            
        }

        protected override void UpdateScript(float dt, Instance data)
        {
            if (Exit) data.IsOn = false;

            if (StartArena)
            {
                data.InstantiateLevel(new ArenaLevel(menuFishBase, data, Settings, fishForCustomizeMenu.weapon.ID, MasterSoundVolume * MusicSoundVolume));
                data.DestroyLevel(this);
            }
            if (ShowCustomizeMenu && customizeMenu == null)
                customizeMenu = (CustomizeMenu)InstantiateActor(new CustomizeMenu(menuFishBase));
            if (!ShowCustomizeMenu && customizeMenu != null)
            {
                DestroyActor(customizeMenu);
                customizeMenu = null;
            }
                
                
        }
    }
}
