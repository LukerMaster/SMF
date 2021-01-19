using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    public class CustomizeMenu : Actor
    {
        FishBase fishBase;
        Sprite menuSprite = new Sprite();
        Text upgradeDesription = new Text();

        public bool ShowDescription { get; set; }
        public string UpgradeString { get; set; }
        public CustomizeMenu(FishBase fishBase)
        {
            this.fishBase = fishBase;
        }
        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            menuSprite.Texture = ((FishAssetManager)assets).GetCustomTexture("assets/menu/custom_menu.png");
            menuSprite.Position = new Vector2f(1502, 270);
            menuSprite.Scale = new Vector2f(418.0f / menuSprite.Texture.Size.X, 700.0f / menuSprite.Texture.Size.Y);
            w.Draw(menuSprite);

            upgradeDesription.Font = (assets as FishAssetManager).GetFont("roboto");
            upgradeDesription.Position = new Vector2f(20, 220);
            upgradeDesription.CharacterSize = 35;
            upgradeDesription.Color = new Color(255, 255, 255, 255);
            upgradeDesription.OutlineColor = new Color(255, 255, 255, 255);
            upgradeDesription.OutlineThickness = 0.5f;
            upgradeDesription.DisplayedString = UpgradeString;

            RectangleShape[] bars = new RectangleShape[6];
            for (int i = 0; i < 6; i++)
                bars[i] = new RectangleShape();

            float[] percentages = new float[6];
            percentages[0] = fishBase.MaxHealth / 6000.0f;
            percentages[1] = fishBase.MaxHealthRegen / 150.0f;
            percentages[2] = fishBase.MaxSpeed / 1600.0f;
            percentages[3] = ((float)fishBase.MaxForce / fishBase.Mass) / 1.5f;
            percentages[4] = (float)Math.Pow(fishBase.Friction, 3) / 0.970299f;
            percentages[5] = ((float)fishBase.MaxNitrousForce / fishBase.Mass) / 2.5f;
            bars[0].FillColor = new Color(255, 0, 0, 255);
            bars[1].FillColor = new Color(255, 60, 60, 255);
            bars[2].FillColor = new Color(200, 0, 200, 255);
            bars[3].FillColor = new Color(30, 30, 255, 255);
            bars[4].FillColor = new Color(60, 120, 60, 255);
            bars[5].FillColor = new Color(255, 255, 0, 255);

            for (int i = 0; i < 6; i++)
                percentages[i] = Math.Clamp(percentages[i], 0, 1);

            for (int i = 0; i < 6; i++)
            {
                bars[i].Position = new Vector2f(menuSprite.Position.X + (110 * 418.0f / menuSprite.Texture.Size.X), menuSprite.Position.Y + ((20 + i * 120) * 700.0f / menuSprite.Texture.Size.Y));
                bars[i].Size = new Vector2f(percentages[i] * 800 * 418.0f / menuSprite.Texture.Size.X, 100 * 700.0f / menuSprite.Texture.Size.Y);
                w.Draw(bars[i]);
            }
            if (ShowDescription)
                w.Draw(upgradeDesription);
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {

        }
    }
}
