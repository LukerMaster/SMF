using SFML.Graphics;
using SFML.System;
using System;
using SFBE;
namespace SMF
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

        public IWeapon weapon;
        public bool FacingLeft = false;
        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; } = new Vector2f(1, 1);
        public float Rotation { get; set; }

        private Sprite sprite;
        public FishBase fishBase;

        public Fish(FishBase fishBase, Texture tex)
        {
            this.fishBase = fishBase;
            sprite = new Sprite(tex);
            SetupStartupValues();
        }

        public Fish(FishBase fishBase, Texture tex, IWeapon weapon)
        {
            this.weapon = weapon;
            this.fishBase = fishBase;
            sprite = new Sprite(tex);
            SetupStartupValues();
        }

        private void SetupStartupValues()
        {
            boostFalloff = 0.0f;
            CurrentStamina = fishBase.MaxStamina;
            StaminaCurrentCooldown = fishBase.MaxStaminaRegenCooldown;
            CurrentHealth = fishBase.MaxHealth;
        }

        private bool lmbPressed;

        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;
        private bool downPressed;
        private bool boostPressed;
        private bool reloadPressed;
        private Vector2i mousePos;
        public void ReceiveInput(FishInput input)
        {
            lmbPressed = input.AttackPressed;
            upPressed = input.UpPressed;
            leftPressed = input.LeftPressed;
            rightPressed = input.RightPressed;
            downPressed = input.DownPressed;
            boostPressed = input.BoostPressed;
            reloadPressed = input.ReloadPressed;
        }

        protected override void Update(float dt, Level level)
        {
            
        }

        protected override void FixedUpdate(float dt, Level level)
        {
            if (upPressed) acceleration.Y = -fishBase.MaxAcceleration; // Accelerate
            if (downPressed) acceleration.Y = fishBase.MaxAcceleration;
            if (!upPressed && !downPressed) acceleration.Y = 0;
            if (leftPressed) { acceleration.X = -fishBase.MaxAcceleration; FacingLeft = true; }
            if (rightPressed) { acceleration.X = fishBase.MaxAcceleration; FacingLeft = false; }
            if (!leftPressed && !rightPressed) acceleration.X = 0;
            acceleration.X = Math.Clamp(acceleration.X, -fishBase.MaxAcceleration, fishBase.MaxAcceleration);
            acceleration.Y = Math.Clamp(acceleration.Y, -fishBase.MaxAcceleration, fishBase.MaxAcceleration);


            if (upPressed && speed.Y > 0 || downPressed && speed.Y < 0) speed.Y *= (float)Math.Pow(1 - fishBase.Friction, dt); // Braking
            else if (!upPressed && !downPressed) speed.Y *= (float)Math.Pow(1 - fishBase.Friction / 1.25f, dt); // Slight braking
            if (leftPressed && speed.X > 0 || rightPressed && speed.X < 0) speed.X *= (float)Math.Pow(1 - fishBase.Friction, dt); // Braking
            else if (!leftPressed && !rightPressed) speed.X *= (float)Math.Pow(1 - fishBase.Friction / 1.25f, dt); // Slight braking

            speed += acceleration * dt;

            if (boostPressed && !boostDrainedProtection)
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

            if (!boostPressed)
                boostDrainedProtection = false;

            Position += speed * dt;

            float weaponRotation = 360 * (float)Math.Atan2((mousePos - new Vector2i((int)Position.X, (int)Position.Y)).Y, (mousePos - new Vector2i((int)Position.X, (int)Position.Y)).X) / (float)(2 * Math.PI);

            if (weapon != null)
            {
                if (lmbPressed)
                    weapon.Attack(0, level);
                if (reloadPressed)
                    weapon.Reload();

                weapon.Rotation = weaponRotation;
            }
        }

        protected override void Draw(RenderWindow w, AssetManager assets)
        {
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = Position;
            sprite.Color = fishBase.tint;
            sprite.Scale = new Vector2f((float)fishBase.Size.X / (float)sprite.Texture.Size.X * (float)Scale.X, (float)fishBase.Size.Y / (float)sprite.Texture.Size.Y * Scale.Y);
            if (FacingLeft)
                sprite.Scale = new Vector2f(-sprite.Scale.X, sprite.Scale.Y);

            w.Draw(sprite);
            if (weapon != null)
            {
                weapon.Position = Position;
            }
        }
    }
}
