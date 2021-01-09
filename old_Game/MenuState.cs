using System;
using SFBE;
using SFML.Graphics;
using SFML.System;
using SMF.fish;
using SMF.weapon;

namespace SMF
{
    class MenuState : Level
    {
        MainMenu currentMenu;
        FishBase menuFishBase;
        WeaponBase menuWeaponBase;

        Fish fishForCustomizeMenu;

        FishAssetManager assets;

        bool ShowCustomizeMenu = false;
        public MenuState(FishBase fishBase = null, WeaponBase weaponBase = null, Instance data)
        {
            assets = (FishAssetManager)data.assets;

            if (fishBase != null)
                menuFishBase = fishBase;
            else
                menuFishBase = new FishBase(0, assets.GetByID(FishAssetManager.EType.Fish, 0));

            if (fishBase != null)
                menuWeaponBase = weaponBase;
            else
                menuWeaponBase = new WeaponBase(0, assets.GetByID(FishAssetManager.EType.Weapon, 0));

            fishForCustomizeMenu = new Fish(menuFishBase, assets.GetByID(FishAssetManager.EType.Fish , menuFishBase.ID));

            currentMenu = InstantiateActor(new MainMenu());
        }
        private void DrawCustomizeMenu()
        {
            fishForCustomizeMenu.Position = new Vector2f(1720, 770);
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

        protected override void UpdateScript(float dt, Instance data)
        {
            throw new NotImplementedException();
        }

        protected override void FixedUpdateScript(float dt, Instance data)
        {
            throw new NotImplementedException();
        }
    }
}
