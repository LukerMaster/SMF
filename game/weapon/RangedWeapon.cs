using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SMF.engine;

namespace SMF.game.weapon
{
    class RangedWeapon : Weapon
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

        public RangedWeapon(WeaponBase weaponBase)
        {
            this.weaponBase = weaponBase;
            currentAmmoCount = weaponBase.MaxAmmo;
            sprite = new Sprite(weaponBase.Texture);
        }

        public void Attack(int type = 0)
        {
            if (currentShootCooldown <= 0 && currentAmmoCount > 0 && ReloadTimeRemaining <= 0)
            {
                ShootAnimProgress = 0.0f;
                currentShootCooldown = weaponBase.TimeBetweenFire;
                CurrentAmmoCount--;
                Console.WriteLine(CurrentAmmoCount + "/" + MaxAmmoCount);
            }
        }

        public void Draw(RenderWindow w)
        {
            sprite.Position = Position;
            sprite.Scale = new Vector2f((float)weaponBase.Size.X / (float)sprite.Texture.Size.X * Scale.X, (float)weaponBase.Size.Y / (float)sprite.Texture.Size.Y * Scale.Y);
            sprite.Rotation = Rotation;
            ModifySpriteByAnim();

            if (Rotation > 90 || Rotation < -90)
                sprite.Scale = new Vector2f(sprite.Scale.X, -sprite.Scale.Y);
            w.Draw(sprite);
        }

        public void Reload()
        {
            if (ReloadTimeRemaining <= 0)
            {
                ReloadTimeRemaining = weaponBase.ReloadTime;
                CurrentAmmoCount = MaxAmmoCount;
            }
        }
        public void Update(float dt, Input input, List<Actor> others)
        {
            ShootAnimProgress += 8 * dt;
            currentShootCooldown -= dt;
            ReloadTimeRemaining -= dt;
            if (currentAmmoCount == 0 && ShootAnimProgress == 1)
                Reload();
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
    }
}
