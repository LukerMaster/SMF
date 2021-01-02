using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;

namespace SMF.game.weapon
{
    class RangedWeapon : Weapon
    {
        private WeaponBase weaponBase;
        private Sprite sprite;
        private uint currentAmmoCount;
        public uint AttacksCount => 1;

        public uint CurrentAmmoCount { get => currentAmmoCount; set => Math.Min(value, weaponBase.MaxAmmo); }

        public RangedWeapon(WeaponBase weaponBase)
        {
            this.weaponBase = weaponBase;
            sprite = new Sprite(weaponBase.Texture);
        }

        public uint MaxAmmoCount => weaponBase.MaxAmmo;

        public void Attack(int type = 0)
        {
            Console.WriteLine("Pew");
        }

        public void Draw(RenderWindow w)
        {
            w.Draw(sprite);
        }

        public void Reload()
        {
            Console.WriteLine("Reloading");
        }
        public void Update(float dt, Vector2f pos, Vector2f aimPos, List<GameObject> others)
        {
            
        }
    }
}
