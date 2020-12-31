using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game.fish
{
    class Fish
    {
        public bool FacingLeft = false;

        private float boostFalloff;
        private float currentStamina;
        private float staminaCurrentCooldown;
        private float currentHealth;

        private float BoostFalloff { get => boostFalloff; set => boostFalloff = Math.Clamp(value, 0.0f, 1.0f); }
        private float StaminaCurrentCooldown { get => staminaCurrentCooldown; set => staminaCurrentCooldown = Math.Clamp(value, 0, fishBase.MaxStaminaRegenCooldown); }
        private float CurrentStamina { get => currentStamina; set => currentStamina = Math.Clamp(value, 0, fishBase.MaxStamina); }
        private float CurrentHealth { get => currentHealth; set => currentHealth = Math.Clamp(value, 0, fishBase.MaxHealth); }

        private bool boostDrainedProtection = false;



        public Vector2f position = new Vector2f(0, 0);
        public Vector2f speed = new Vector2f(0, 0);
        public Vector2f acceleration = new Vector2f(0, 0);
        public Vector2f scale = new Vector2f(1, 1);

        public Sprite sprite;
        public FishBase fishBase;

        public Fish(FishBase fishBase)
        {
            this.fishBase = fishBase;
            sprite = new Sprite(fishBase.Texture);
            SetupStartupValues();
        }

        private void SetupStartupValues()
        {
            boostFalloff = 0.0f;
            CurrentStamina = fishBase.MaxStamina;
            StaminaCurrentCooldown = fishBase.MaxStaminaRegenCooldown;
            CurrentHealth = fishBase.MaxHealth;
        }

        public void Update(float dt, bool up, bool down, bool left, bool right, bool attack, bool boost)
        {
            if (up) acceleration.Y = -fishBase.MaxAcceleration; // Accelerate
            if (down) acceleration.Y = fishBase.MaxAcceleration;
            if (!up && !down) acceleration.Y = 0;
            if (left) { acceleration.X = -fishBase.MaxAcceleration; FacingLeft = true; }
            if (right) { acceleration.X = fishBase.MaxAcceleration; FacingLeft = false; }
            if (!left && !right) acceleration.X = 0;
            acceleration.X = Math.Clamp(acceleration.X, -fishBase.MaxAcceleration, fishBase.MaxAcceleration);
            acceleration.Y = Math.Clamp(acceleration.Y, -fishBase.MaxAcceleration, fishBase.MaxAcceleration);



            if (up && speed.Y > 0 || down && speed.Y < 0) speed.Y *= (float)Math.Pow(1 - fishBase.Friction, dt); // Braking
            else if (!up && !down) speed.Y *= (float)Math.Pow(1 - fishBase.Friction / 1.25f, dt); // Slight braking
            if (left && speed.X > 0 || right && speed.X < 0) speed.X *= (float)Math.Pow(1 - fishBase.Friction, dt); // Braking
            else if (!left && !right) speed.X *= (float)Math.Pow(1 - fishBase.Friction / 1.25f, dt); // Slight braking

            speed += acceleration * dt;

            if (boost && !boostDrainedProtection)
            {
                if (CurrentStamina > 0.0f)
                {
                    BoostFalloff = 1.0f;
                    CurrentStamina -= dt * 1000;
                    speed += acceleration * dt;
                    StaminaCurrentCooldown = fishBase.MaxStaminaRegenCooldown;
                    speed.X = Math.Clamp(speed.X, -fishBase.MaxSpeed * 1.25f, fishBase.MaxSpeed * 1.25f);
                    speed.Y = Math.Clamp(speed.Y, -fishBase.MaxSpeed * 1.25f, fishBase.MaxSpeed * 1.25f);
                }
                else
                {
                    boostDrainedProtection = true;
                }
            }
            else
            {
                BoostFalloff -= dt;
                StaminaCurrentCooldown -= dt * 1000;
                speed.X = Math.Clamp(speed.X, -fishBase.MaxSpeed - (fishBase.MaxSpeed * 0.25f * BoostFalloff), fishBase.MaxSpeed + (fishBase.MaxSpeed * 0.25f * BoostFalloff));
                speed.Y = Math.Clamp(speed.Y, -fishBase.MaxSpeed - (fishBase.MaxSpeed * 0.25f * BoostFalloff), fishBase.MaxSpeed + (fishBase.MaxSpeed * 0.25f * BoostFalloff));
            }
            if (StaminaCurrentCooldown == 0.0f)
                CurrentStamina += 500 * dt;

            if (!boost)
                boostDrainedProtection = false;

            position += speed * dt;
        }

        public void Draw(RenderWindow w)
        {
            sprite.Texture = fishBase.Texture;
            sprite.TextureRect = new IntRect(0, 0, (int)fishBase.Texture.Size.X, (int)fishBase.Texture.Size.Y);
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = position;
            sprite.Scale = scale;
            sprite.Color = fishBase.tint;
            if (FacingLeft)
                sprite.Scale = new Vector2f((float)-fishBase.SpriteSize.X / sprite.Texture.Size.X, (float)fishBase.SpriteSize.Y / sprite.Texture.Size.Y);
            else
                sprite.Scale = new Vector2f((float)fishBase.SpriteSize.X / sprite.Texture.Size.X, (float)fishBase.SpriteSize.Y / sprite.Texture.Size.Y);
            w.Draw(sprite);
        }
    }
}
