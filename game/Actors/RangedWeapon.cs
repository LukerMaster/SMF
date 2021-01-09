using System;
using System.Collections.Generic;
using System.Text;
using SFBE;
using SFML.Graphics;
using SFML.System;

namespace SMF
{
    class RangedWeapon : Actor, IWeapon
    {
        private WeaponBase weaponBase;
        private Sprite sprite;
        private uint currentAmmoCount;
        private float currentShootCooldown = 0.0f;
        private float shootAnimProgress = 1.0f;
        private float reloadTimeRemaining = 0.0f;
        public uint AttacksCount => 1;

        public uint CurrentAmmoCount { get => currentAmmoCount; set => currentAmmoCount = Math.Clamp(value, 0, MaxAmmoCount); }
        public uint MaxAmmoCount => weaponBase.MaxAmmo;
        public bool ToDestroy => false;
        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; } = new Vector2f(1, 1);
        public float Rotation { get; set; }
        public float ShootAnimProgress { get => shootAnimProgress; set => shootAnimProgress = Math.Clamp(value, 0, 1); }
        public float ReloadTimeRemaining { get => reloadTimeRemaining; set => reloadTimeRemaining = Math.Max(value, 0); }
        public byte DrawLayer { get => 1; }

        public RangedWeapon(WeaponBase weaponBase, Texture tex)
        {
            this.weaponBase = weaponBase;
            currentAmmoCount = weaponBase.MaxAmmo;
            sprite = new Sprite(tex);
        }

        public void Reload()
        {
            if (ReloadTimeRemaining <= 0)
            {
                ReloadTimeRemaining = weaponBase.ReloadTime;
                CurrentAmmoCount = MaxAmmoCount;
            }
        }
        private void ModifySpriteByAnim()
        {
            Vector2f offset = new Vector2f(20 * (float)(2 * Math.Pow(ShootAnimProgress - 0.5f, 2) - 0.5f), 0);
            if (sprite.Scale.X < 0)
                sprite.Position -= offset;
            else
                sprite.Position += offset;
            if (ReloadTimeRemaining > 0)
            {
                sprite.Scale = new Vector2f(sprite.Scale.X * 0.7f, sprite.Scale.Y * 0.7f);
            }
        }

        void IWeapon.Attack(int type, Level scene)
        {
            if (currentShootCooldown <= 0 && currentAmmoCount > 0 && ReloadTimeRemaining <= 0)
            {
                ShootAnimProgress = 0.0f;
                currentShootCooldown = weaponBase.TimeBetweenFire;
                CurrentAmmoCount--;

                float rotX = (float)Math.Cos((Rotation - 45) * 0.0174532925) - (float)Math.Sin((Rotation - 45) * 0.0174532925);
                float rotY = (float)Math.Sin((Rotation - 45) * 0.0174532925) + (float)Math.Cos((Rotation - 45) * 0.0174532925);

                scene.InstantiateActor(new Bullet(Position + new Vector2f(rotX * weaponBase.Size.X, rotY * weaponBase.Size.X), new Vector2f(rotX * weaponBase.BulletSpeed, rotY * weaponBase.BulletSpeed)));
            }
        }

        protected override void Update(float dt, Level level)
        {
            ShootAnimProgress += 8 * dt;
            currentShootCooldown -= dt;
            ReloadTimeRemaining -= dt;
            if (currentAmmoCount == 0 && ShootAnimProgress == 1)
                Reload();
        }

        protected override void FixedUpdate(float dt, Level level)
        {
            throw new NotImplementedException();
        }

        protected override void Draw(RenderWindow w, AssetManager assets)
        {
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Position = Position;
            sprite.Scale = new Vector2f((float)weaponBase.Size.X / (float)sprite.Texture.Size.X * Scale.X, (float)weaponBase.Size.Y / (float)sprite.Texture.Size.Y * Scale.Y);
            sprite.Rotation = Rotation;
            ModifySpriteByAnim();

            if (Rotation > 90 || Rotation < -90)
                sprite.Scale = new Vector2f(sprite.Scale.X, -sprite.Scale.Y);
            w.Draw(sprite);
        }
    }
}
