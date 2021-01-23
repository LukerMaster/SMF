using SFML.Graphics;
using SFML.System;
using System;
using SFBF;
namespace SMF
{
    public class Fish : Actor, ICollidable
    {
        private float boostFalloff;
        private float currentNitrous;
        private float nitrousCurrentCooldown;
        private float healthCurrentCooldown;
        private float currentHealth;

        public float NitrousFalloff { get => boostFalloff; set => boostFalloff = Math.Clamp(value, 0.0f, 1.0f); }
        public float NitrousCurrentCooldown { get => nitrousCurrentCooldown; set => nitrousCurrentCooldown = Math.Clamp(value, 0, fishBase.MaxNitrousRegenCooldown); }
        public float HealthCurrentCooldown { get => healthCurrentCooldown; set => healthCurrentCooldown = Math.Clamp(value, 0, fishBase.MaxHealthRegenCooldown); }
        public float CurrentNitrous { get => currentNitrous; set => currentNitrous = Math.Clamp(value, 0, fishBase.MaxNitrous); }
        public float CurrentHealth { get => currentHealth; set => currentHealth = Math.Clamp(value, 0, fishBase.MaxHealth); }

        private bool boostDrainedProtection = false;

        public Vector2f speed;
        private Vector2f acceleration;

        public IWeapon weapon;
        public bool FacingLeft = false;
        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; } = new Vector2f(1, 1);
        public float Rotation { get; set; }

        private Sprite sprite = new Sprite();
        public FishBase fishBase;

        public Fish(FishBase fishBase)
        {
            this.fishBase = fishBase;
            SetupStartupValues();
        }

        public Fish(FishBase fishBase, IWeapon weapon)
        {
            this.weapon = weapon;
            this.fishBase = fishBase;
            SetupStartupValues();
        }

        private void SetupStartupValues()
        {
            DrawOrder = 5;
            boostFalloff = 0.0f;
            CurrentNitrous = fishBase.MaxNitrous;
            NitrousCurrentCooldown = fishBase.MaxNitrousRegenCooldown;
            CurrentHealth = fishBase.MaxHealth;
        }

        private bool lmbPressed;

        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;
        private bool downPressed;
        private bool boostPressed;
        private bool reloadPressed;
        private Vector2f mousePos;

        public float DamageMultiplier { get; set; } = 1.0f;
        public void ReceiveInput(FishInput input)
        {
            lmbPressed = input.AttackPressed;
            upPressed = input.UpPressed;
            leftPressed = input.LeftPressed;
            rightPressed = input.RightPressed;
            downPressed = input.DownPressed;
            boostPressed = input.BoostPressed;
            reloadPressed = input.ReloadPressed;
            mousePos = input.MousePos;
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {
            
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount * DamageMultiplier;
            HealthCurrentCooldown = fishBase.MaxHealthRegenCooldown;
        }
        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            float CurrentAcceleration = 1000 * fishBase.MaxForce / fishBase.Mass;
            if (boostPressed && !boostDrainedProtection)
            {
                if (CurrentNitrous > 0.0f)
                {
                    CurrentNitrous -= dt * 1000;
                    CurrentAcceleration = 1000 * (fishBase.MaxForce + fishBase.MaxNitrousForce) / fishBase.Mass;
                    NitrousCurrentCooldown = fishBase.MaxNitrousRegenCooldown;
                }
                else
                {
                    boostDrainedProtection = true;
                }
            }
            else
            {
                NitrousCurrentCooldown -= dt;
            }

            if (NitrousCurrentCooldown == 0.0f)
                CurrentNitrous += 500 * dt;

            if (!boostPressed)
                boostDrainedProtection = false;


            if (upPressed) acceleration.Y = -CurrentAcceleration; // Accelerate
            if (downPressed) acceleration.Y = CurrentAcceleration;
            if (!upPressed && !downPressed) acceleration.Y = 0;
            if (leftPressed) { acceleration.X = -CurrentAcceleration; FacingLeft = true; }
            if (rightPressed) { acceleration.X = CurrentAcceleration; FacingLeft = false; }
            if (!leftPressed && !rightPressed) acceleration.X = 0;
            //acceleration.X = Math.Clamp(acceleration.X, -Acceleration, Acceleration);
            //acceleration.Y = Math.Clamp(acceleration.Y, -Acceleration, Acceleration);

            speed += acceleration * dt;

            if (!upPressed && !downPressed) speed.Y *= (float)Math.Pow(1 - fishBase.Friction, dt); // braking
            if (upPressed && speed.Y > 0 || downPressed && speed.Y < 0) speed.Y *= (float)Math.Pow(1 - fishBase.Friction, dt); // braking
            if (!leftPressed && !rightPressed) speed.X *= (float)Math.Pow(1 - fishBase.Friction, dt); //  braking
            if (leftPressed && speed.X > 0 || rightPressed && speed.X < 0) speed.X *= (float)Math.Pow(1 - fishBase.Friction, dt); // braking

            if (Math.Abs(speed.X) > fishBase.MaxSpeed) speed.X *= (float)Math.Pow(0.8, dt);
            if (Math.Abs(speed.Y) > fishBase.MaxSpeed) speed.Y *= (float)Math.Pow(0.8, dt);

            Position += speed * dt;

            float weaponRotation = 360 * (float)Math.Atan2((mousePos - Position).Y, (mousePos - Position).X) / (float)(2 * Math.PI);

            if (weapon != null)
            {
                weapon.Rotation = weaponRotation;
                if (lmbPressed)
                    weapon.Attack(level, new Vector2f((float)mousePos.X, (float)mousePos.Y));
                if (reloadPressed)
                    weapon.Reload();
            }

            // Regeneration

            if (HealthCurrentCooldown <= 0 && CurrentHealth > 0)
            {
                CurrentHealth += fishBase.GetHealthRegen() * dt;
            }
            HealthCurrentCooldown -= dt;
        }

        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            sprite.Texture = ((FishAssetBox)assets.Assets).GetByID(FishAssetBox.EType.Fish, fishBase.ID);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = Position;
            sprite.Color = fishBase.tint;
            sprite.Scale = new Vector2f((float)fishBase.Size.X / (float)sprite.Texture.Size.X * (float)Scale.X, (float)fishBase.Size.Y / (float)sprite.Texture.Size.Y * Scale.Y);
            if (FacingLeft)
                sprite.Scale = new Vector2f(-sprite.Scale.X, sprite.Scale.Y);
            if (CurrentHealth <= 0)
                sprite.Scale = new Vector2f(sprite.Scale.X, -sprite.Scale.Y);

            w.Draw(sprite);
            if (weapon != null)
            {
                weapon.Position = Position;
            }
        }

        bool ICollidable.isPointColliding(Vector2f point)
        {
            if (sprite.GetGlobalBounds().Contains(point.X, point.Y))
                return true;
            else
                return false;
        }
        bool ICollidable.isBoxColliding(FloatRect rect)
        {
            if (sprite.GetGlobalBounds().Intersects(rect))
                return true;
            else
                return false;
        }
    }
}
