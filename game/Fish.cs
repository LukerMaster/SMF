using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class Fish
    {
        public Fish()
        {
            boostFalloff = 0.0f;
            CurrentStamina = MaxStamina;
            StaminaCurrentCooldown = StaminaRegenCooldown;
            CurrentHealth = MaxHealth;
        }

        public Fish DeepCopy()
        {
            Fish other = (Fish)this.MemberwiseClone();
            other.baseData = baseData.ShallowCopy();
            other.sprite = new Sprite(sprite.Texture);
            return other;
        }

        public Sprite sprite = new Sprite();
        /// <summary>
        /// Data of the fish read from file. Does NOT include tuning values.
        /// </summary>
        public FishData baseData = new FishData();

        private float boostFalloff;
        private float currentStamina;
        private float staminaCurrentCooldown;
        private float currentHealth;

        private float BoostFalloff { get => boostFalloff; set => boostFalloff = Math.Clamp(value, 0.0f, 1.0f); }
        private float StaminaCurrentCooldown { get => staminaCurrentCooldown; set => staminaCurrentCooldown = Math.Clamp(value, 0, StaminaRegenCooldown); }
        private float CurrentStamina { get => currentStamina; set => currentStamina = Math.Clamp(value, 0, MaxStamina); }
        private float CurrentHealth { get => currentHealth; set => currentHealth = Math.Clamp(value, 0, MaxHealth); }

        private bool boostDrainedProtection = false;

        public Color tint = new Color(255, 255, 255, 255);
        private bool flip = false;

        public Vector2f position = new Vector2f(0, 0);
        public Vector2f speed = new Vector2f(0, 0);
        public Vector2f acceleration = new Vector2f(0, 0);

        private int engineLvl = 0;
        private int chassisLvl = 0;
        private int bodyLvl = 0;
        private int finsLvl = 0;
        private int nitroLvl = 0;

        public int EngineLvl { get => engineLvl; set => engineLvl = Math.Clamp(value, 0, 4); }
        public int ChassisLvl { get => chassisLvl; set => chassisLvl = Math.Clamp(value, 0, 4); }
        public int BodyLvl { get => bodyLvl; set => bodyLvl = Math.Clamp(value, 0, 4); }
        public int FinsLvl { get => finsLvl; set => finsLvl = Math.Clamp(value, 0, 4); }
        public int NitroLvl { get => nitroLvl; set => nitroLvl = Math.Clamp(value, 0, 4); }

        public int MaxHealth { get => (int)TuningValues.GetMaxHealth(baseData, ChassisLvl, BodyLvl, FinsLvl); }
        public int HealthRegen { get => (int)TuningValues.GetHealthRegen(baseData, BodyLvl); }
        public int HealthRegenCooldown { get => baseData.HealthRegenCooldown; }
        public int MaxStamina { get => (int)TuningValues.GetMaxStamina(baseData, EngineLvl, NitroLvl); }
        public int StaminaRegenCooldown { get => baseData.StaminaRegenCooldown; }
        public int MaxSpeed { get => (int)TuningValues.GetMaxSpeed(baseData, EngineLvl, FinsLvl); }
        public int MaxAcceleration { get => (int)TuningValues.GetMaxAcceleration(baseData, EngineLvl, FinsLvl, BodyLvl); }
        public float Friction { get => TuningValues.GetFriction(baseData, finsLvl); }


        public void Update(float dt, bool up, bool down, bool left, bool right, bool attack, bool boost)
        {
            if (up) acceleration.Y = -MaxAcceleration; // Accelerate
            if (down) acceleration.Y = MaxAcceleration;
            if (!up && !down) acceleration.Y = 0;
            if (left) { acceleration.X = -MaxAcceleration; flip = true; }
            if (right) { acceleration.X = MaxAcceleration; flip = false; }
            if (!left && !right) acceleration.X = 0;
            acceleration.X = Math.Clamp(acceleration.X, -MaxAcceleration, MaxAcceleration);
            acceleration.Y = Math.Clamp(acceleration.Y, -MaxAcceleration, MaxAcceleration);



            if (up && speed.Y > 0 || down && speed.Y < 0) speed.Y *= (float)Math.Pow(1 - Friction, dt); // Braking
            else if (!up && !down) speed.Y *= (float)Math.Pow(1 - Friction / 1.25f, dt); // Slight braking
            if (left && speed.X > 0 || right && speed.X < 0) speed.X *= (float)Math.Pow(1 - Friction, dt); // Braking
            else if (!left && !right) speed.X *= (float)Math.Pow(1 - Friction / 1.25f, dt); // Slight braking

            speed += acceleration * dt;

            if (boost && !boostDrainedProtection)
            {
                if (CurrentStamina > 0.0f)
                {
                    BoostFalloff = 1.0f;
                    CurrentStamina -= dt * 1000;
                    speed += acceleration * dt;
                    StaminaCurrentCooldown = StaminaRegenCooldown;
                    speed.X = Math.Clamp(speed.X, -MaxSpeed * 1.25f, MaxSpeed * 1.25f);
                    speed.Y = Math.Clamp(speed.Y, -MaxSpeed * 1.25f, MaxSpeed * 1.25f);
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
                speed.X = Math.Clamp(speed.X, -MaxSpeed - (MaxSpeed * 0.25f * BoostFalloff), MaxSpeed + (MaxSpeed * 0.25f * BoostFalloff));
                speed.Y = Math.Clamp(speed.Y, -MaxSpeed - (MaxSpeed * 0.25f * BoostFalloff), MaxSpeed + (MaxSpeed * 0.25f * BoostFalloff));
            }
            if (StaminaCurrentCooldown == 0.0f)
                CurrentStamina += 500 * dt;

            if (!boost)
                boostDrainedProtection = false;

            position += speed * dt;
        }

        public void Draw(RenderWindow w)
        {
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = position;
            sprite.Color = tint;
            if (flip)
                sprite.Scale = new Vector2f((float)-baseData.SizeX / sprite.Texture.Size.X, (float)baseData.SizeY / sprite.Texture.Size.Y);
            else
                sprite.Scale = new Vector2f((float)baseData.SizeX / sprite.Texture.Size.X, (float)baseData.SizeY / sprite.Texture.Size.Y);
            w.Draw(sprite);
        }
    }
}
