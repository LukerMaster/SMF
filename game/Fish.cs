using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class Fish
    {
        public Sprite sprite = new Sprite();
        /// <summary>
        /// Data of the fish read from file. Does NOT include tuning values.
        /// </summary>
        public FishData baseData = new FishData();

        public Color tint = new Color(255, 255, 255, 255);

        public Vector2f position = new Vector2f(0, 0);
        public Vector2f speed = new Vector2f(0, 0);
        public Vector2f acceleration = new Vector2f(0, 0);

        public int EngineLvl = 0;
        public int ChassisLvl = 0;
        public int BodyLvl = 0;
        public int FinsLvl = 0;
        public int NitroLvl = 0;

        public int MaxHealth { get => (int)TuningValues.GetMaxHealth(baseData, ChassisLvl, BodyLvl, FinsLvl); }
        public int HealthRegen { get => (int)TuningValues.GetHealthRegen(baseData, BodyLvl); }
        public int HealthRegenCooldown { get => baseData.HealthRegenCooldown; }
        public int MaxStamina { get => (int)TuningValues.GetMaxStamina(baseData, EngineLvl, NitroLvl); }
        public int StaminaRegenCooldown { get => baseData.StaminaRegenCooldown; }
        public int MaxSpeed { get => (int)TuningValues.GetMaxSpeed(baseData, EngineLvl, FinsLvl); }
        public int MaxAcceleration { get => (int)TuningValues.GetMaxAcceleration(baseData, EngineLvl, FinsLvl, BodyLvl); }
        public float Friction { get; set; } = 0.95f;

        public void Update(float dt, bool up, bool down, bool left, bool right, bool attack, bool boost)
        {
            if (up) acceleration.Y = -MaxAcceleration; // Accelerate
            if (down) acceleration.Y = MaxAcceleration;
            if (!up && !down) acceleration.Y = 0;
            if (left) acceleration.X = -MaxAcceleration;
            if (right) acceleration.X = MaxAcceleration;
            if (!left && !right) acceleration.X = 0;
            acceleration.X = Math.Clamp(acceleration.X, -MaxAcceleration, MaxAcceleration);
            acceleration.Y = Math.Clamp(acceleration.Y, -MaxAcceleration, MaxAcceleration);

            if (up && speed.Y > 0 || down && speed.Y < 0) speed.Y *= (float)Math.Pow(1 - Friction, dt); // Braking
            else if (!up && !down) speed.Y *= (float)Math.Pow(1 - Friction / 2, dt); // Slight braking
            if (left && speed.X > 0 || right && speed.X < 0) speed.X *= (float)Math.Pow(1 - Friction, dt); // Braking
            else if (!left && !right) speed.X *= (float)Math.Pow(1 - Friction / 2, dt); // Slight braking

            speed += acceleration * dt;
            speed.X = Math.Clamp(speed.X, -MaxSpeed, MaxSpeed);
            speed.Y = Math.Clamp(speed.Y, -MaxSpeed, MaxSpeed);
            position += speed * dt;
            Console.WriteLine(acceleration + " acc");
            Console.WriteLine(speed + " speed");
        }

        public void Draw(RenderWindow w)
        {
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = position;
            sprite.Color = tint;
            w.Draw(sprite);
        }
    }
}
