using SFBE;
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
        public CustomizeMenu(FishBase fishBase)
        {
            this.fishBase = fishBase;
        }
        protected override void Draw(RenderWindow w, AssetManager assets)
        {
            menuSprite.Texture = ((FishAssetManager)assets).GetCustomTexture("assets/menu/custom_menu.png");
            menuSprite.Position = new Vector2f(1502, 270);
            menuSprite.Scale = new Vector2f(418.0f / menuSprite.Texture.Size.X, 700.0f / menuSprite.Texture.Size.Y);
            w.Draw(menuSprite);

            RectangleShape[] bars = new RectangleShape[6];
            for (int i = 0; i < 6; i++)
                bars[i] = new RectangleShape();

            float[] percentages = new float[6];
            percentages[0] = fishBase.MaxHealth / 13000.0f;
            percentages[1] = Math.Max(0, fishBase.MaxHealthRegen / 1000.0f);
            percentages[2] = fishBase.MaxSpeed / 1600.0f;
            percentages[3] = fishBase.MaxAcceleration / 1500.0f;
            percentages[4] = (float)Math.Pow(fishBase.Friction, 3) / 0.941192f;
            percentages[5] = fishBase.MaxStamina / 20000.0f;
            bars[0].FillColor = new Color(255, 0, 0, 255);
            bars[1].FillColor = new Color(255, 60, 60, 255);
            bars[2].FillColor = new Color(200, 0, 200, 255);
            bars[3].FillColor = new Color(30, 30, 255, 255);
            bars[4].FillColor = new Color(60, 120, 60, 255);
            bars[5].FillColor = new Color(255, 255, 0, 255);

            for (int i = 0; i < 6; i++)
            {
                bars[i].Position = new Vector2f(menuSprite.Position.X + (110 * 418.0f / menuSprite.Texture.Size.X), menuSprite.Position.Y + ((20 + i * 120) * 700.0f / menuSprite.Texture.Size.Y));
                bars[i].Size = new Vector2f(percentages[i] * 800 * 418.0f / menuSprite.Texture.Size.X, 100 * 700.0f / menuSprite.Texture.Size.Y);
                w.Draw(bars[i]);
            }
        }

        protected override void FixedUpdate(float dt, Level level)
        {
            
        }

        protected override void Update(float dt, Level level)
        {

        }
    }
}
