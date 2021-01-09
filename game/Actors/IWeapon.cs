using SFBE;
using SFML.System;

namespace SMF
{
    interface IWeapon
    {
        /// <summary>
        /// Call specific attack function.
        /// </summary>
        /// <param name="type">Used to determine which attack is being used. Default attack type is 0, and you can
        /// see how many attacks are there with "AttacksCount" parameter.</param>
        public void Attack(int type, Level scene);
        public void Reload();
        public uint AttacksCount { get; }
        public uint CurrentAmmoCount { get; set; }
        public uint MaxAmmoCount { get; }
        float Rotation { get; set; }
        Vector2f Position { get; set; }
        
    }
}
