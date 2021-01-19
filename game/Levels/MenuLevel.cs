using SFBF;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SMF
{
    partial class MenuLevel : Level
    {
        public bool ShowCustomizeMenu;
        public Fish fishForCustomizeMenu;
        public IWeapon WeaponForCustomizeMenu;

        public bool Exit = false;
        public bool StartArena = false;


        Menu currentMenu;
        CustomizeMenu customizeMenu;

        FishAssetManager assets;
        MenuMusicPlayer musicPlayer;

        GameSettings gameSettings = new GameSettings();

        public WeaponBuilder weaponBuilder = new WeaponBuilder();
        public MenuLevel(Instance instance, WindowSettings settings, GameSettings gameSettings)
        {
            if (File.Exists("assets/music/menu.ogg"))
                musicPlayer = (MenuMusicPlayer)InstantiateActor(new MenuMusicPlayer(gameSettings));

            assets = (FishAssetManager)instance.assets;

            this.gameSettings = gameSettings;
            if (this.gameSettings == null)
                this.gameSettings = new GameSettings();

            Settings = settings;
            
            currentMenu = (Menu)InstantiateActor(new Menu("roboto", gameSettings));
            currentMenu.BottomLeftPos = new SFML.System.Vector2f(20, 960);
            CreateMainMenu();
            InstantiateActor(new MainBackground(Settings.ViewSize));

            

            fishForCustomizeMenu = (Fish)InstantiateActor(new Fish(this.gameSettings.playerFishBase));
            fishForCustomizeMenu.Position = new SFML.System.Vector2f(1700, 750);
            fishForCustomizeMenu.Scale = new SFML.System.Vector2f(2.0f, 2.0f);
            fishForCustomizeMenu.FacingLeft = true;
            fishForCustomizeMenu.DrawOrder = 1;
            CreateWeapon();

            fishForCustomizeMenu.weapon = WeaponForCustomizeMenu;

        }
        protected override void FixedUpdateScript(float dt, Instance data)
        {
            
        }

        public void CreateWeapon()
        {
            if (WeaponForCustomizeMenu != null)
                DestroyActor(WeaponForCustomizeMenu as Actor);

            WeaponForCustomizeMenu = weaponBuilder.CreateWeapon(gameSettings.selectedWeaponID);
            InstantiateActor(WeaponForCustomizeMenu as Actor);
            WeaponForCustomizeMenu.Position = fishForCustomizeMenu.Position;
            WeaponForCustomizeMenu.Scale = new SFML.System.Vector2f(1.75f, 1.75f);
            fishForCustomizeMenu.weapon = WeaponForCustomizeMenu;
        }

        protected override void UpdateScript(float dt, Instance data)
        {
            if (Exit) data.IsOn = false;

            FishInput input = new FishInput();
            input.MousePos = MousePos;

            fishForCustomizeMenu.ReceiveInput(input);


            if(WeaponForCustomizeMenu.ID != gameSettings.selectedWeaponID)
            {
                CreateWeapon();
            }

            if (StartArena)
            {
                musicPlayer.Stop();
                data.InstantiateLevel(new ArenaLevel(data, Settings, gameSettings));
                data.DestroyLevel(this);
            }
            if (ShowCustomizeMenu && customizeMenu == null)
                customizeMenu = (CustomizeMenu)InstantiateActor(new CustomizeMenu(this.gameSettings.playerFishBase));
            if (!ShowCustomizeMenu && customizeMenu != null)
            {
                DestroyActor(customizeMenu);
                customizeMenu = null;
            }
                
                
        }
    }
}
