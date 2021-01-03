using SFML.Graphics;
using SFML.System;
using SMF.engine;
using SMF.game.weapon;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game.fish
{
    class Fish : Actor
    {
        private float boostFalloff;
        private float currentStamina;
        private float staminaCurrentCooldown;
        private float currentHealth;

        private float BoostFalloff { get => boostFalloff; set => boostFalloff = Math.Clamp(value, 0.0f, 1.0f); }
        private float StaminaCurrentCooldown { get => staminaCurrentCooldown; set => staminaCurrentCooldown = Math.Clamp(value, 0, fishBase.MaxStaminaRegenCooldown); }
        private float CurrentStamina { get => currentStamina; set => currentStamina = Math.Clamp(value, 0, fishBase.MaxStamina); }
        private float CurrentHealth { get => currentHealth; set => currentHealth = Math.Clamp(value, 0, fishBase.MaxHealth); }

        private bool boostDrainedProtection = false;

        private Vector2f speed;
        private Vector2f acceleration;

        public Weapon weapon;
        public bool FacingLeft = false;
        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; } = new Vector2f(1, 1);
        public float Rotation { get; set; }

        public Sprite sprite;
        public FishBase fishBase;

        [Obsolete("Temporary solution!")]
        public bool ToDestroy { get => false; } // TEMPORARY SOLUTION

        public Fish(FishBase fishBase)
        {
            this.fishBase = fishBase;
            sprite = new Sprite(fishBase.Texture);
            SetupStartupValues();
        }

        public Fish(FishBase fishBase, Weapon weapon)
        {
            this.weapon = weapon;
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

        public void Update(float dt, Input input, List<Actor> others)
        {
            if (input.UpPressed) acceleration.Y = -fishBase.MaxAcceleration; // Accelerate
            if (input.DownPressed) acceleration.Y = fishBase.MaxAcceleration;
            if (!input.UpPressed && !input.DownPressed) acceleration.Y = 0;
            if (input.LeftPressed) { acceleration.X = -fishBase.MaxAcceleration; FacingLeft = true; }
            if (input.RightPressed) { acceleration.X = fishBase.MaxAcceleration; FacingLeft = false; }
            if (!input.LeftPressed && !input.RightPressed) acceleration.X = 0;
            acceleration.X = Math.Clamp(acceleration.X, -fishBase.MaxAcceleration, fishBase.MaxAcceleration);
            acceleration.Y = Math.Clamp(acceleration.Y, -fishBase.MaxAcceleration, fishBase.MaxAcceleration);


            if (input.UpPressed && speed.Y > 0 || input.DownPressed && speed.Y < 0) speed.Y *= (float)Math.Pow(1 - fishBase.Friction, dt); // Braking
            else if (!input.UpPressed && !input.DownPressed) speed.Y *= (float)Math.Pow(1 - fishBase.Friction / 1.25f, dt); // Slight braking
            if (input.LeftPressed && speed.X > 0 || input.RightPressed && speed.X < 0) speed.X *= (float)Math.Pow(1 - fishBase.Friction, dt); // Braking
            else if (!input.LeftPressed && !input.RightPressed) speed.X *= (float)Math.Pow(1 - fishBase.Friction / 1.25f, dt); // Slight braking

            speed += acceleration * dt;

            if (input.BoostPressed && !boostDrainedProtection)
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

            if (!input.BoostPressed)
                boostDrainedProtection = false;

            Position += speed * dt;

            float weaponRotation = 360 * (float)Math.Atan2((input.MousePos - new Vector2i((int)Position.X, (int)Position.Y)).Y, (input.MousePos - new Vector2i((int)Position.X, (int)Position.Y)).X) / (float)(2 * Math.PI);

            if (weapon != null)
            {
                if (input.AttackPressed)
                    weapon.Attack(0);
                if (input.ReloadPressed)
                    weapon.Reload();

                weapon.Rotation = weaponRotation;
                weapon.Update(dt, input, others);
            }

        }

        public void Draw(RenderWindow w)
        {
            sprite.Texture = fishBase.Texture;
            sprite.TextureRect = new IntRect(0, 0, (int)fishBase.Texture.Size.X, (int)fishBase.Texture.Size.Y);
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = Position;
            sprite.Color = fishBase.tint;
            sprite.Scale = new Vector2f((float)fishBase.Size.X / (float)sprite.Texture.Size.X * Scale.X, (float)fishBase.Size.Y / (float)sprite.Texture.Size.Y * Scale.Y);
            if (FacingLeft)
                sprite.Scale = new Vector2f(-sprite.Scale.X, sprite.Scale.Y);
                
                   
            w.Draw(sprite);
            if (weapon != null)
            {
                weapon.Position = Position;
                weapon.Draw(w);
            }
        }

    }
}
