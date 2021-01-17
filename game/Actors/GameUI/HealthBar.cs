using SFBF;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class HealthBar : Actor
    {
        Fish referencedFish;

        public HealthBar(Fish fish)
        {
            referencedFish = fish;
        }

        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            float barSize = 250.0f;
            float barThickness = 15.0f;

            RectangleShape healthRect = new RectangleShape();
            healthRect.Origin = new SFML.System.Vector2f(0.0f, 0.0f);
            healthRect.Position = new SFML.System.Vector2f(referencedFish.Position.X - barSize / 2, referencedFish.Position.Y - referencedFish.fishBase.Size.Y / 2);
            healthRect.Size = new SFML.System.Vector2f(1, 1);
            healthRect.Scale = new SFML.System.Vector2f(barSize * referencedFish.CurrentHealth / referencedFish.fishBase.MaxHealth, -barThickness);
            healthRect.FillColor = new Color(0, 255, 0, 180);
            RectangleShape damageRect = new RectangleShape();
            damageRect.Origin = new SFML.System.Vector2f(1.0f, 0.0f);
            damageRect.Position = new SFML.System.Vector2f(referencedFish.Position.X + barSize / 2, referencedFish.Position.Y - referencedFish.fishBase.Size.Y / 2);
            damageRect.Size = new SFML.System.Vector2f(1, 1);
            damageRect.Scale = new SFML.System.Vector2f(barSize * (1.0f - (referencedFish.CurrentHealth / referencedFish.fishBase.MaxHealth)), -barThickness);
            damageRect.FillColor = new Color(255, 0, 0, 180);

            RectangleShape staminaRect = new RectangleShape();
            staminaRect.Origin = new SFML.System.Vector2f(0.0f, 0.0f);
            staminaRect.Position = new SFML.System.Vector2f(referencedFish.Position.X - barSize / 2, referencedFish.Position.Y - referencedFish.fishBase.Size.Y / 2 - barThickness - 2);
            staminaRect.Size = new SFML.System.Vector2f(1, 1);
            staminaRect.Scale = new SFML.System.Vector2f(barSize * referencedFish.CurrentStamina / referencedFish.fishBase.MaxStamina, -barThickness / 4);
            staminaRect.FillColor = new Color(255, 255, 80, 180);
            if (referencedFish.CurrentStamina < referencedFish.fishBase.MaxStamina * 0.4f)
            {
                staminaRect.FillColor = new Color(255, 150, 0, 255);
            }
                
            w.Draw(healthRect);
            w.Draw(damageRect);
            w.Draw(staminaRect);
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {
            
        }
    }
}
