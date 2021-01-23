using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    public class MeleeWeapon : Actor, IWeapon
    {
        /// <summary>
        /// Used only to avoid damaging yourself.
        /// </summary>
        public Fish owner;

        public WeaponFileData weaponData;
        string IWeapon.Name { get => weaponData.Name; }
        uint IWeapon.DPS { get => (uint)(weaponData.Damage / weaponData.TimeBetweenAttack); }
        float IWeapon.Rotation { get; set; }

        float previousRotation;
        Vector2f IWeapon.Position { get; set; }
        Vector2f IWeapon.Scale { get; set; } = new Vector2f(1.0f, 1.0f);
        int IWeapon.ID { get => weaponData.ID; }

        Sprite sprite = new Sprite();

        void IWeapon.Attack(Level l, Vector2f target)
        {
            
        }

        void IWeapon.Reload()
        {
            
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {
            
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            if (Math.Sign(previousRotation) != Math.Sign((this as IWeapon).Rotation))
                previousRotation = -previousRotation;
            float rotationalSpeed = (this as IWeapon).Rotation - previousRotation;
            previousRotation = (this as IWeapon).Rotation;
            List<Fish> targets = level.GetActorsOfClass<Fish>();
            foreach (Fish t in targets)
            {
                if (t != owner && (t as ICollidable).isBoxColliding(sprite.GetGlobalBounds()))
                {
                    t.TakeDamage(weaponData.Damage * dt * (Math.Abs(rotationalSpeed) / 180));
                }
            }
        }

        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            sprite.Texture = (assets.Assets as FishAssetBox).GetByID(FishAssetBox.EType.Weapon, (this as IWeapon).ID);
            sprite.Scale = new Vector2f((this as IWeapon).Scale.X * (float)weaponData.SizeX / sprite.Texture.Size.X, (this as IWeapon).Scale.Y * (float)weaponData.SizeY / sprite.Texture.Size.Y);
            sprite.Position = (this as IWeapon).Position;
            sprite.Rotation = (this as IWeapon).Rotation;

            if ((this as IWeapon).Rotation > 90 || (this as IWeapon).Rotation < -90)
                sprite.Scale = new Vector2f(sprite.Scale.X, -sprite.Scale.Y);
            w.Draw(sprite);
        }

        public MeleeWeapon(WeaponFileData data, Fish owner = null)
        {
            DrawOrder = 7;
            this.owner = owner;
            weaponData = data;
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

            public int Range = 1;
            public int Damage = 10;
            public int TimeBetweenAttack = 1;
        }
    }
}
