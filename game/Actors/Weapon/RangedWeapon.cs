using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    public class RangedWeapon : Actor, IWeapon
    {
        /// <summary>
        /// Used to avoid shooting yourself
        /// </summary>
        public Fish owner;
        public RangedWeapon.WeaponFileData weaponData;

        private Sprite sprite = new Sprite();
        private uint currentAmmoCount;
        private float currentShootCooldown = 0.0f;
        private float shootAnimProgress = 1.0f;
        private float reloadTimeRemaining = 0.0f;
        public uint AttacksCount => 1;
        public uint CurrentAmmoCount { get => currentAmmoCount; set => currentAmmoCount = Math.Clamp(value, 0, MaxAmmoCount); }
        public uint MaxAmmoCount => weaponData.AmmoCount;
        public bool ToDestroy => false;
        
        public Vector2f Scale { get; set; } = new Vector2f(1, 1);
        
        public float ShootAnimProgress { get => shootAnimProgress; set => shootAnimProgress = Math.Clamp(value, 0, 1); }
        public float ReloadTimeRemaining { get => reloadTimeRemaining; set => reloadTimeRemaining = Math.Max(value, 0); }
        public byte DrawLayer { get => 1; }
        string IWeapon.Name { get => weaponData.Name; }
        uint IWeapon.DPS { get => (uint)(weaponData.Damage / weaponData.TimeBetweenFire); }
        float IWeapon.Rotation { get; set; }
        Vector2f IWeapon.Position { get; set; }
        Vector2f IWeapon.Scale { get; set; } = new Vector2f(1.0f, 1.0f);
        int IWeapon.ID { get => weaponData.ID; }

        public RangedWeapon(WeaponFileData data, Fish owner = null)
        {
            DrawOrder = 7;
            this.owner = owner;
            weaponData = data;
            currentAmmoCount = weaponData.AmmoCount;
        }

        void IWeapon.Reload()
        {
            if (ReloadTimeRemaining <= 0)
            {
                ReloadTimeRemaining = weaponData.ReloadTime;
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

        void IWeapon.Attack(Level scene, Vector2f target)
        {
            if (currentShootCooldown <= 0 && currentAmmoCount > 0 && ReloadTimeRemaining <= 0)
            {
                ShootAnimProgress = 0.0f;
                currentShootCooldown = weaponData.TimeBetweenFire;
                CurrentAmmoCount--;

                float rotX = (float)Math.Cos(((this as IWeapon).Rotation - 45) * 0.0174532925) - (float)Math.Sin(((this as IWeapon).Rotation - 45) * 0.0174532925);
                float rotY = (float)Math.Sin(((this as IWeapon).Rotation - 45) * 0.0174532925) + (float)Math.Cos(((this as IWeapon).Rotation - 45) * 0.0174532925);
                scene.InstantiateActor(new Bullet((this as IWeapon).Position + new Vector2f(rotX * weaponData.SizeX, rotY * weaponData.SizeX), new Vector2f(rotX * weaponData.BulletSpeed, rotY * weaponData.BulletSpeed), weaponData.Damage, owner));
            }
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {
            ShootAnimProgress += 8 * dt;
            currentShootCooldown -= dt;
            ReloadTimeRemaining -= dt;
            if (currentAmmoCount == 0 && ShootAnimProgress == 1)
                (this as IWeapon).Reload();
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            
        }

        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            sprite.Texture = (assets.Assets as FishAssetBox).GetByID(FishAssetBox.EType.Weapon, (this as IWeapon).ID);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Position = (this as IWeapon).Position;
            sprite.Scale = new Vector2f((this as IWeapon).Scale.X * (float)weaponData.SizeX / (float)sprite.Texture.Size.X * Scale.X, (this as IWeapon).Scale.Y * (float)weaponData.SizeY / (float)sprite.Texture.Size.Y * Scale.Y);
            sprite.Rotation = (this as IWeapon).Rotation;
            ModifySpriteByAnim();

            if ((this as IWeapon).Rotation > 90 || (this as IWeapon).Rotation < -90)
                sprite.Scale = new Vector2f(sprite.Scale.X, -sprite.Scale.Y);
            w.Draw(sprite);
        }


        public class WeaponFileData
        {
            public WeaponFileData Copy()
            {
                WeaponFileData other = (WeaponFileData)this.MemberwiseClone();
                return other;
            }
            public int ID = 0;
            public string Name = "";
            public int SizeX = 128;
            public int SizeY = 72;

            public float BulletSlowdown = 0.01f;
            public int Damage = 10;
            public int BulletSpeed = 0;
            public uint AmmoCount = 1;
            public float TimeBetweenFire = 1;
            public float ReloadTime = 0.5f;
        }
    }
}
